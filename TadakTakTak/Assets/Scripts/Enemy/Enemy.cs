using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public enum State
{
    Idle,
    Chase,
    Attack,
    Die,
}

public class Enemy : PoolableMono, IAgent, IHittable
{
    #region �������̽� ����
    private int _health; 
    public int Health
    {
        get => _health; 
        set
        {
            _health = Mathf.Clamp(_health, 0, _enemyDataSO.maxHP);
        }
    }


    [field:SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnHit { get; set; }

    private bool _isEnemy = true; 
    public bool IsEnemy => _isEnemy;

#endregion


    [SerializeField]
    private EnemyDataSO _enemyDataSO;
    private EnemyAnimation _enemyAnimation;
    public EnemyAnimation EnemyAnimation => _enemyAnimation; 
    private CapsuleCollider _colider;

    public NavMeshAgent _agent;

    [SerializeField]
    private AIState currentState; // ���� ���� 

    [SerializeField]
    private IdleState _idleState;
    [SerializeField]
    private ChaseState _chaseState;
    [SerializeField]
    private  AttackState _attackState;

    [SerializeField]
    private List<AIState> _aIStates = new List<AIState>(); 


    [SerializeField]
    private State _currentStateType; // ���� ���� 

    [SerializeField]
    private Transform _target; 

    private bool _isDead = false;
    public bool isRotate = false; // ��������, ���� ���¿��� �÷��̾� �ٶ󺸵��� ��

    [SerializeField]
    private AbstractGun _enemyGun; 

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead == true) return; 

        _health -= damage;
        OnHit?.Invoke(); 

        if (Health <= 0)
        {
            _isDead = true;
            _agent.ResetPath(); //��� ����
            OnDie?.Invoke(); //��� �̺�Ʈ �κ�ũ
            
        }
    }

    private void Awake()
    {
        _colider = GetComponent<CapsuleCollider>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyAnimation = GetComponent<EnemyAnimation>(); 
    }
    private void Start()
    {
        SetNavMeshAgent();
        SetAIState();
        _target = FindObjectOfType<PlayerController>().transform; 
        _agent.updateRotation = false;
        _agent.updateUpAxis = false ;

        _health = _enemyDataSO.maxHP;
        currentState = _idleState; 
    }
    private void Update()   
    {
        UpdateState();
        if(isRotate == true)
        {
            RotateBody();
        }
        if(Health <= 0)
        {
            PoolManager.Instance.Push(PoolType.Enemy, this); 
        }
        // ���������� ���� �Ÿ��� ȸ�� ���� �Ǵ� 
        //if (_agent.remainingDistance > -10f)
        //{
        //// ������Ʈ�� ȸ�� �� 
        //Vector3 direction = _agent.desiredVelocity;

        //    // ȸ�� ���� ����
        //    Quaternion rotation = Quaternion.LookRotation(direction);

        //    // ���� �������� �Լ��� �ε巯�� ȸ�� ó�� 
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        ////}
    }
    public override void Reset()
    {
        _agent.ResetPath(); // �����Ǿ� �־��� ��� ���� ��, ���� 
    }

    /// <summary>
    /// Chase
    /// </summary>
    public void ChaseTarget()
    {
        _enemyAnimation.AnimateRun(true);

        _agent.SetDestination(_target.position);
    }

    /// <summary>
    /// Idle
    /// </summary>
    public void SetIdle()
    {
        _agent.ResetPath();
        _enemyAnimation.AnimateRun(false);

    }
    [SerializeField]
    private bool _isActive;
    /// <summary>
    /// Attack
    /// </summary>
    public void PerformAttack()
    {
        if (_isDead == false && _isActive == true )
        {
            _enemyGun.Fire();
        }
        _enemyAnimation.AnimateRun(false);
    }
    public void RotateBody()
    {
        Vector3 direction = _target.position - transform.position ;

        // ȸ�� ���� ����
        Quaternion rotation = Quaternion.LookRotation(direction);

        // ���� �������� �Լ��� �ε巯�� ȸ�� ó�� 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
    private void SetAIState()
    {
        _aIStates.Add(_idleState);
        _aIStates.Add(_chaseState);
        _aIStates.Add(_attackState); 
    }
    /// <summary>
    /// NavMeshAgent�� �� ����
    /// </summary>
    public void SetNavMeshAgent()
    {
        _agent.speed = _enemyDataSO.maxSpeed;
        _agent.angularSpeed = _enemyDataSO.angularSpeed;
        _agent.acceleration = _enemyDataSO.acceleration;
        _agent.stoppingDistance = _enemyDataSO.stoppingDistance;
        _agent.autoBraking = _enemyDataSO.isAutoBreaking;
    }


    public void ChangeState(State state)
    {
        _currentStateType = state;
        currentState = _aIStates[(int)state];   
    }

    private float _attackDistance = 10;
    private float _chaseDistance = 30;  
    public bool CheckDistance(float targetDistance)
    {
        float distance = Vector3.Distance(_target.position, transform.position);
   //   Debug.Log("Distance : " + distance);
        return  distance <= targetDistance ? true : false; 
      
    }

    public void ChangeState(AIState state)
    {
        currentState = state; 
       
    }
    public void UpdateState()
    {
        currentState.UpdateState(); 
    }

    private void OnDrawGizmos()
    {
        // ���� �����Ÿ� 
        if (_currentStateType == State.Chase)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
        // ���� �����Ÿ� 
        if (_currentStateType == State.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}
