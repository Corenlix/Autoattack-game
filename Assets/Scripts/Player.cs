using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Level))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; 
    [SerializeField] private ParticleSystem _bloodParticleSystem; 
    [SerializeField] private AbilitiesChooser _abilitiesChooser;
    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private GameObject _abilitiesContainer;
    private List<Ability> _abilities;
    private Mover _mover;
    private Animator _animator;
    private Health _health;
    private Level _level;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    public const float Knockback = 0.25f;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_moveSpeed);

        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _level = GetComponent<Level>();
        _level.LevelChanged += ChooseNewAbility;
        _abilities = _abilitiesContainer.GetComponents<Ability>().ToList();
    }

    private void Start()
    {
        ActivateStartAbility();
    }

    private void ActivateStartAbility()
    {
        GetComponentInChildren<Whip>(true).LevelUp();
    }

    private void Update()
    {
        MoveInput();
    }

    private void ChooseNewAbility()
    {
        var chooser = Instantiate(_abilitiesChooser, _worldCanvas.transform);
        chooser.Generate(_abilities);
        chooser.Chose += OnChoseAbility;
        Time.timeScale = 0;
    }

    private void OnChoseAbility(AbilitiesChooser chooser)
    {
        Time.timeScale = 1;
        chooser.Chose -= OnChoseAbility;
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
        _level.LevelChanged -= ChooseNewAbility;
    }
}
