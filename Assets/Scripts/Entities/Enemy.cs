using System;
using UI;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        private static readonly int Die = Animator.StringToHash("Die");
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damagePerSecond;
        private Animator _animator;
        private Health _health;
        private bool _isDead;
        private Mover _mover;
        private Player _overlapPlayer;
        private Rigidbody2D _rigidbody;
        private Transform _target;

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
            HitPlayer();
        }

        private void OnDestroy()
        {
            _health.Died -= OnDie;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player)) _overlapPlayer = player;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (_overlapPlayer && other.gameObject == _overlapPlayer.gameObject) _overlapPlayer = null;
        }

        public event Action<Enemy> Died;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public bool TryHit(float damage, Vector2 knockbackDirection)
        {
            if (_isDead)
                return false;

            _health.DealDamage(damage);
            PopupSpawner.Instance.SpawnPopup(transform.position, damage);
            _rigidbody.MovePosition(_rigidbody.position + knockbackDirection.normalized * Player.Knockback);
            _animator.SetTrigger(TakeDamage);
            return true;
        }

        private void Move()
        {
            Vector2 moveDirection = _target.position - transform.position;
            _mover.SetMoveDirection(moveDirection);
            if (moveDirection.x == 0)
                return;
            var scaleModifier = moveDirection.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * scaleModifier,
                transform.localScale.y, transform.localScale.z);
        }

        private void HitPlayer()
        {
            bool isReadyToHit = _overlapPlayer;
            if (!isReadyToHit) return;

            _overlapPlayer.DealDamage(_damagePerSecond * Time.deltaTime);
        }

        private void OnDie()
        {
            Died?.Invoke(this);
            LootSpawner.Instance.Spawn(transform.position, LootType.Experience);
            _animator.SetTrigger(Die);
            _mover.SetSpeed(0);
            _isDead = true;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}