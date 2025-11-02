using System;
using UnityEngine;

namespace PlayerComponents
{
    public class Health: MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        private int _currentHealth;
        private bool _isDead = false;
        private bool _isInvincible;
        private float invincibilityTimer = 0f;
        
        private PlayerCollisionHandler _playerCollisionHandler;
        
        public event EventHandler<DeathEventArgs> OnDeath;
        public event Action OnHealthChanged;
        public event Action OnHardDeath;
        public event Action OnHitTaken;

        private void Start()
        {
            _isInvincible = false;
            _currentHealth = maxHealth;
            _playerCollisionHandler  = GetComponent<PlayerCollisionHandler>();
            _playerCollisionHandler.OnTakeDamage += TakeDamage;
            OnHealthChanged?.Invoke();
        }

        private void Update()
        {
            if (_isInvincible)
            {
                invincibilityTimer  -= Time.deltaTime;
                if (invincibilityTimer <= 0)
                {
                    _isInvincible = false;
                }
            }
        }
        public int GetPlayerCurrentHealth()
        {
            return _currentHealth;
        }

        public int GetPlayerMaxHealth()
        {
            return maxHealth;
        }
        
        public void ChangeHealth(int amount)
        {
            if (amount <= 0 && _isInvincible) return;
            
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            OnHealthChanged?.Invoke();
        }

        public void ChangePermanentHealth(int amount)
        {
            maxHealth = Mathf.Clamp(maxHealth + amount, 1, int.MaxValue);
            ChangeHealth(amount);
        }

        public void RestoreFullLife()
        {
            ChangeHealth(maxHealth);
        }

        public void TakeDamage()
        {
            if (_isDead || _isInvincible) return;
            
            ChangeHealth(-1);
            OnHitTaken?.Invoke();
            OnHealthChanged?.Invoke();
            
            int playerHealth = GetPlayerCurrentHealth();
            if (playerHealth > 0) {
                Debug.Log("TAKE DAMAGE");
                Debug.Log("Health: " + playerHealth);
            }
            else
            { 
                AttemptToDie();
            }
        }

        private void AttemptToDie()
        {
            DeathEventArgs args = new DeathEventArgs();

            OnDeath?.Invoke(this, args);

            if (args.IsSaved)
            {
                // A relic subscriber set 'IsSaved' to true.
                Debug.Log("Player was SAVED from death by a relic!");
            }
            else
            {
                // No one saved the player. Proceed with death.
                _isDead = true;
                OnHardDeath?.Invoke();
                Debug.Log("DEAD");
                
                // Put your REAL death logic here (e.g., play animation, show UI)
                gameObject.SetActive(false); // Example: just disable the player
            }
        }

        public void SetInvincibility(float time)
        {
            _isInvincible = true;
            invincibilityTimer = time;
        }
        
    }
}