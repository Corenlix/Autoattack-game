using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        public Vector2 MoveDirection => _moveDirection;
        private Vector2 _moveDirection;
        private Rigidbody2D _rigidbody;
        private float _speed;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _speed * _moveDirection;
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;
        }

        public void SetSpeed(float moveSpeed)
        {
            _speed = moveSpeed;
        }
        
        private void Awake()
         {
             _rigidbody = GetComponent<Rigidbody2D>();
         }
    }
}