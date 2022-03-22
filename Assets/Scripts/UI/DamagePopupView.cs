using TMPro;
using UnityEngine;

namespace UI
{
    public class DamagePopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;

        public void Init(float damage)
        {
            _damageText.text = damage.ToString();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}