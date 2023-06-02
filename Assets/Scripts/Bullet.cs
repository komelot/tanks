using UnityEngine;

namespace Tanks
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        
        private Tank _shooter;
        private Vector2 _direction;
        private float _speed;
        
        private Rigidbody2D _rigidbody;
        
        public void Init(Tank shooter, Vector2 direction, float speed)
        {
            _shooter = shooter;
            _direction = direction;
            _speed = speed;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = _direction * _speed;

            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out Tank player))
            {
                if (player == _shooter)
                {
                    return;
                }
                
                player.TakeDamage();
            }
            
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            
            Destroy(gameObject);
        }
    }
}