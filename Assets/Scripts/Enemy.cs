using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damagePerSecond;
    private bool _isDead;
    private Mover _mover;
    private Rigidbody2D _rigidbody;
    private Health _health;
    private Transform _target;
    private Player _overlapPlayer;
    private Animator _animator;
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int Die = Animator.StringToHash("Die");
    public event Action<Enemy> Died;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public bool TryDealDamage(float damage, Vector2 knockbackDirection)
    {
        if (_isDead)
            return false;
        
        _health.DealDamage(damage);
        PopupSpawner.Instance.SpawnPopup(transform.position, damage);
        _rigidbody.MovePosition(_rigidbody.position + knockbackDirection);
        _animator.SetTrigger(TakeDamage);
        return true;
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_moveSpeed);
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetTarget(Game.Instance.CurrentPlayer.transform);
        _health.Died += OnDie;
    }

    private void Update()
    {
        if (_isDead) return;
        Move();
        Hit();
    }

    private void Move()
    {
        Vector2 moveDirection = _target.position - transform.position;
        _mover.SetMoveDirection(moveDirection);
        if (moveDirection.x == 0)
            return;
        int scaleModifier = moveDirection.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * scaleModifier,
            transform.localScale.y, transform.localScale.z);
    }

    private void Hit()
    {
        bool isReadyToHit = _overlapPlayer;
        if (!isReadyToHit) return;
            
        _overlapPlayer.TryDealDamage(_damagePerSecond * Time.deltaTime);
    }
    
    private void OnDie()
    {
        Died?.Invoke(this);
        LootSpawner.Instance.Spawn(transform.position, LootType.Experience);
        _animator.SetTrigger(Die);
        _mover.SetSpeed(0);
        _isDead = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            _overlapPlayer = player;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_overlapPlayer && other.gameObject == _overlapPlayer.gameObject)
        {
            _overlapPlayer = null;
        }
    }

    private void OnDestroy()
    {
        _health.Died -= OnDie;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
