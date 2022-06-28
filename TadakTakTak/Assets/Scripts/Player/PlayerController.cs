using Main.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IHittable, IAgent
{

    [SerializeField]
    private PlayerMoveComponent _playerMoveComponent;
    [SerializeField]
    private FireComponent _fireComponent;
    [SerializeField]
    private GunComponent _gunComponent;
    [SerializeField]
    private PoseComponent _poseComponent;

    public InputComponent inputComponent;


    [SerializeField]
    private bool _isPosing = false;

    [SerializeField]
    private int _maxHealth = 100; // 최대 체력 
    private int _health;
    private bool _isDead = false;  
    public int Health
    {
        get => _health; 
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth); 
        }
    }
    
    public bool IsPosing
    {
        get  => _isPosing;
        set
        {
            _isPosing = value; 
        }
    }
    public bool IsEnemy => false;

    [field : SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnHit { get; set; }

    void Start()
    {
        Health = _maxHealth; 
        _playerMoveComponent.Initialize(inputComponent);
        _poseComponent.Initialize(); 
        _fireComponent.Initialize(_gunComponent, inputComponent,_playerMoveComponent,_poseComponent); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == true) return; 

        _playerMoveComponent.ApplyGravity(); 
        _poseComponent.UpdateSomething();
        inputComponent.UpdateSomething(); 
        if (_isPosing == true)
        {
            _fireComponent.UpdateSomething();
            return;
        }
       //Debug.Log("업데이트");
        _playerMoveComponent.UpdateSomething();
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead == true) return; 
        Health -= damage;
     
        OnHit?.Invoke();
        if (Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true; 
        }
        EventManager.Instance.TriggerEvent(EventsType.SetHPUI, Health);
    }
}
