using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IdleState _idleState;
    public MoveState _moveState;
    public ShootState _shootState;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private GameObject _playerObj;

    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToMove = 10f;

    BaseState currentState;

    public Rigidbody2D Rigidbody2D { get => _rigidbody2D; set => _rigidbody2D = value; }
    public Collider2D Collider2D { get => _collider2D; set => _collider2D = value; }
    public GameObject PlayerObj { get => _playerObj; set => _playerObj = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
    public float DistanceToShoot { get => distanceToShoot; set => distanceToShoot = value; }
    public float DistanceToMove { get => distanceToMove; set => distanceToMove = value; }

    private void Awake()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _collider2D = this.GetComponent<Collider2D>();
        _playerObj = GameObject.FindWithTag("Player");

        _idleState = new IdleState(this);
        _moveState = new MoveState(this);
        _shootState = new ShootState(this);
    }

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    protected virtual BaseState GetInitialState()
    {
        return _idleState;
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }

}