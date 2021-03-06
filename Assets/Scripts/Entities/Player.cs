using Abilities;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Entities
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PlayerLevel))]
    public class Player : MonoBehaviour
    {
        public Vector2 MoveDirection => _mover.MoveDirection;
        public const float Knockback = 0.25f;
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        [SerializeField] private float _moveSpeed;
        [SerializeField] private ParticleSystem _bloodParticleSystem;
        [SerializeField] private AbilitiesChooserUIView abilitiesChooserUIViewPrefab;
        [SerializeField] private Canvas _worldCanvas;
        [SerializeField] private GameObject _abilitiesContainer;
        [SerializeField] private Ability _startAbility;
        
        private PlayerAbilities _abilities;
        private Animator _animator;
        private Health _health;
        private Mover _mover;
        private PlayerLevel _playerLevel;

        public void DealDamage(float damage)
        {
            _health.DealDamage(damage);
            _animator.SetTrigger(TakeDamage);
            _bloodParticleSystem.Play();
        }

        private void MoveInput()
        {
            var moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _mover.SetMoveDirection(moveDirection);
            var isMoving = moveDirection.sqrMagnitude != 0;
            _animator.SetBool(Run, isMoving);

            if (moveDirection.x != 0)
            {
                var scaleModifier = moveDirection.x > 0 ? 1 : -1;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * scaleModifier,
                    transform.localScale.y, transform.localScale.z);
            }
        }

        private void OnDied()
        {
            Debug.Log("player died");
        }
        
        private void Start()
        {
            _startAbility.LevelUp();
        }

        private void Update()
        {
            MoveInput();
            if(Input.GetKeyDown(KeyCode.Q))
                _playerLevel.AddExperience(10);
        }

        private void OnDestroy()
        {
            _playerLevel.LevelChanged -= _abilities.ChooseNewAbility;
            _health.Died -= OnDied;
        }
        
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _mover.SetSpeed(_moveSpeed);
            _abilities = new PlayerAbilities(this, _abilitiesContainer, _worldCanvas, abilitiesChooserUIViewPrefab);

            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _health.Died += OnDied;
            _playerLevel = GetComponent<PlayerLevel>();
            _playerLevel.LevelChanged += _abilities.ChooseNewAbility;
        }
    }
}