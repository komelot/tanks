using UnityEngine;
using UnityEngine.UI;

namespace Tanks
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Tank _tank;
        
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            
            _tank.onTakeDamage += OnTakeDamage;
        }
        
        private void OnTakeDamage(int health, int maxHealth)
        {
            _image.fillAmount = (float)health / maxHealth;
        }
    }
}