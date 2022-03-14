using Abilities;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Level))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private ParticleSystem _bloodParticleSystem;
    [SerializeField] private AbilitiesChooser _abilitiesChooserPrefab;
    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private GameObject _abilitiesContainer;
    [SerializeField] private Ability _startAbility;
    private Mover _mover;
    private Animator _animator;
    private Health _health;
    private Level _level;
    private PlayerAbilities _abilities;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    public const float Knockback = 0.25f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_moveSpeed);
        _abilities = new PlayerAbilities(_abilitiesContainer, _worldCanvas, _abilitiesChooserPrefab);

        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _level = GetComponent<Level>();
        _level.LevelChanged += _abilities.ChooseNewAbility;
    }

    private void Start()
    {
        _startAbility.LevelUp();
    }

    private void Update()
    {
        MoveInput();
    }

    public bool TryDealDamage(float damage)
    {
        _health.DealDamage(damage);
        _animator.SetTrigger(TakeDamage);
        _bloodParticleSystem.Play();
        return true;
    }

    private void MoveInput()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mover.SetMoveDirection(moveDirection);
        bool isMoving = moveDirection.sqrMagnitude != 0;
        _animator.SetBool(Run, isMoving);

        if (moveDirection.x != 0)
        {
            int scaleModifier = moveDirection.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * scaleModifier,
                transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDestroy()
    {
        _level.LevelChanged -= _abilities.ChooseNewAbility;
    }
}