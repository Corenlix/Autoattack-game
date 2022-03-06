using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Mover _mover;
    private Animator _animator;
    private Health _health;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_moveSpeed);

        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        MoveInput();
    }

    public bool TryDealDamage(float damage)
    {
        _health.DealDamage(damage);
        _animator.SetTrigger(TakeDamage);
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
}
