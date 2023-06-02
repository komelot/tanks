using UnityEngine;

namespace Tanks
{
    public class Tank : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 3;
        
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _rotationSpeed = 0.5f;
        
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private float _fireRate = 0.25f;
        
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;

        [SerializeField] private KeyCode _up;
        [SerializeField] private KeyCode _down;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;

        [SerializeField] private KeyCode _shoot;

        public bool IsMoveEnabled { get; set; }
        
        public System.Action<int, int> onTakeDamage;

        private int _health;
        private float _nextFireTime;
        
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        private Animator _animator;
        private TrailRenderer _trailRenderer;

        public void TakeDamage()
        {
            _health--;
            onTakeDamage?.Invoke(_health, _maxHealth);
            
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        private void Awake()
        {
            _health = _maxHealth;

            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponentInChildren<Animator>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }
        
        private void Update()
        {
            HandleMovement();
            HandleShooting();
            HandleEffects();
        }

        private void HandleMovement()
        {
            if (IsMoveEnabled == false)
            {
                return;
            }
            
            if (Input.GetKey(_up))
            {
                _rigidbody.AddForce(transform.up * _speed);
            }
            else if (Input.GetKey(_down))
            {
                _rigidbody.AddForce(-transform.up * _speed);
            }

            if (Input.GetKey(_left))
            {
                _rigidbody.AddTorque(_rotationSpeed);
            }
            else if (Input.GetKey(_right))
            {
                _rigidbody.AddTorque(-_rotationSpeed);
            }
        }

        private void HandleShooting()
        {
            if (Input.GetKey(_shoot) && Time.time >= _nextFireTime)
            {
                _nextFireTime = Time.time + _fireRate;
                
                var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
                bullet.GetComponent<Bullet>().Init(this, transform.up, _bulletSpeed);
                
                _audioSource.Play();
                _animator.SetTrigger("shoot");
            }
        }

        private void HandleEffects()
        {
            _trailRenderer.emitting = _rigidbody.velocity.magnitude > 0.1f;
        }
    }
}
