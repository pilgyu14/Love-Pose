using UnityEngine;

public class RugularBullet : Bullet
{
    protected Rigidbody _rigidbody;
    protected Animator _animator;
    protected float _timeToLive;

    protected int _enemyLayer;
    protected int _obstacleLayer;

    protected bool _isDead = false; //한개의 총알이 여러명의 적에 영향주는 것을 막기 위함.

    public override BulletSO BulletSO
    {
        get => _bulletSO;
        set
        {
            //_bulletData = value;
            base.BulletSO = value;

            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_isEnemy)
                _enemyLayer = LayerMask.NameToLayer("Player");
            else
                _enemyLayer = LayerMask.NameToLayer("Enemy");
        }
    }

    private void Awake()
    {
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    protected virtual void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;

        if (_timeToLive >= _bulletSO.lifeTime)
        {
            _isDead = true;
            if(_isEnemy == false)
            {
                PoolManager.Instance.Push(PoolType.RegularBullet, this);
            }
            else
            {
                PoolManager.Instance.Push(PoolType.EnemyRugularBullet, this); 
            }
        }

        if (_rigidbody != null && _bulletSO != null)
        {
            _rigidbody.MovePosition(
                transform.position +
                _bulletSO.bulletSpeed * transform.forward * Time.fixedDeltaTime);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("충돌");
        if (_isDead) return;  //만약 관통탄이면 여기서 뭔가 다른 작업을 해야 한다.

        //여기에는 피격해서 데미지를 주고 넉백시키는 코드가 여기에 들어가야된다.

        if (collision.gameObject.layer == _obstacleLayer)
        {
            HitObstacle(collision);
        }

        if (collision.gameObject.layer == _enemyLayer)
        {
            HitEnemy(collision);
        }
        _isDead = true;
        if(_isEnemy == false)
        {
            PoolManager.Instance.Push(PoolType.RegularBullet, this);
        }
        else
        {
            PoolManager.Instance.Push(PoolType.EnemyRugularBullet, this);
        }
    }

    private void HitEnemy(Collider collider)
    {
        Debug.Log("적과 충돌");
        // IKnockback kb = collider.GetComponent<IKnockback>();
        // kb?.Knockback(transform.right, _bulletSO.knockBackPower, _bulletSO.knockBackDelay);

        //피격시 총알이펙트 만들어야 해

        IHittable hittable = collider.gameObject.GetComponent<IHittable>();
        //if (hittable != null && hittable.IsEnemy == IsEnemy)
        //{
        //    return; //총알과 피격체의 피아식별이 같을 경우 아군피격
        //}
        hittable?.GetHit(damage: _bulletSO.damage * damageFactor, damageDealer: gameObject);

        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;

        ImpactScript impact = PoolManager.Instance.Pop(PoolType.EnemyEffect) as ImpactScript;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        impact.SetPositionAndRotation(collider.transform.position + (Vector3)randomOffset, rot);
    }

    private void HitObstacle(Collider collider)
    {
        Debug.Log("장애물과 충돌");

        //RaycastHit hit = Physics.Raycast(transform.position, transform.right, 10f, 1 << _obstacleLayer);
        //if (hit.collider != null)
        //{
        //    ImpactScript impact = PoolManager.Instance.Pop(PoolType.ObstacleEffect) as ImpactScript;
        //    Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        //    impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot);
        //}
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;

        ImpactScript impact = PoolManager.Instance.Pop(PoolType.EnemyEffect) as ImpactScript;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        impact.SetPositionAndRotation(collider.transform.position + (Vector3)randomOffset, rot);
        //벽에 맞았을 때 랜덤한 회전값으로 회전된 ImpactObject 생성되서 충돌위치에 정확하게 나타나고 사라진다.
    }

    public override void Reset()
    {
        damageFactor = 1;
        _timeToLive = 0;
        _isDead = false;
    }
}
