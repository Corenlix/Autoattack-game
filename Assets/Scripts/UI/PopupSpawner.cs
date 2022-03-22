using UnityEngine;

namespace UI
{
    public class PopupSpawner : MonoBehaviour
    {
        [SerializeField] private DamagePopupView _popupPrefab;

        public static PopupSpawner Instance { get; private set; }

        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void SpawnPopup(Vector3 position, float damage)
        {
            var spawnedPopup = Instantiate(_popupPrefab, position, Quaternion.identity);
            spawnedPopup.Init(damage);
        }
    }
}