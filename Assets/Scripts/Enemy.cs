using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damageInSecond;
    private Mover _mover;
    private Rigidbody2D _rigidbody;
    private Health _health;
    private Transform _target;
    private Player _overlapPlayer;
    private Animator _animator;
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void DealDamage(float damage, Vector2 knockbackDirection)
    {
        _health.DealDamage(damage);
        PopupSpawner.Instance.SpawnPopup(transform.position, damage);
        _rigidbody.MovePosition(_rigidbody.position + knockbackDirection);
        _animator.SetTrigger(TakeDamage);
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_moveSpeed);
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetTarget(FindObjectOfType<Player>().transform);
        _health.Died += OnDie;
    }

    private void Update()
    {
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
            
        _overlapPlayer.TryDealDamage(_damageInSecond * Time.deltaTime);
    }
    
    private void OnDie()
    {
        LootSpawner.Instance.Spawn(transform.position, LootType.Experience);
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
}
