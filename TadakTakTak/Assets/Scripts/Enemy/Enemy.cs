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
    #region 인터페이스 구현
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
    private AIState currentState; // 현재 상태 

    [SerializeField]
    private IdleState _idleState;
    [SerializeField]
    private ChaseState _chaseState;
    [SerializeField]
    private  AttackState _attackState;

    [SerializeField]
    private List<AIState> _aIStates = new List<AIState>(); 


    [SerializeField]
    private State _currentStateType; // 현재 상태 

    [SerializeField]
    private Transform _target; 

    private bool _isDead = false;
    public bool isRotate = false; // 추적상태, 공격 상태에서 플레이어 바라보도로 ㄱ

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
            _agent.ResetPath(); //즉시 정지
            OnDie?.Invoke(); //사망 이벤트 인보크
            
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
        // 목적지까지 남은 거리로 회전 여부 판단 
        //if (_agent.remainingDistance > -10f)
        //{
        //// 에이전트의 회전 값 
        //Vector3 direction = _agent.desiredVelocity;

        //    // 회전 각도 산출
        //    Quaternion rotation = Quaternion.LookRotation(direction);

        //    // 구면 선형보간 함수로 부드러운 회전 처리 
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        ////}
    }
    public override void Reset()
    {
        _agent.ResetPath(); // 설정되어 있었던 경로 지움 즉, 멈춤 
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

        // 회전 각도 산출
        Quaternion rotation = Quaternion.LookRotation(direction);

        // 구면 선형보간 함수로 부드러운 회전 처리 
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
    private void SetAIState()
    {
        _aIStates.Add(_idleState);
        _aIStates.Add(_chaseState);
        _aIStates.Add(_attackState); 
    }
    /// <summary>
    /// NavMeshAgent의 값 설정
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
        // 추적 사정거리 
        if (_currentStateType == State.Chase)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
        // 공격 사정거리 
        if (_currentStateType == State.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}
