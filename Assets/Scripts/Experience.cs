using UnityEngine;

public class Experience : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            Destroy(gameObject);
        }
    }
}
