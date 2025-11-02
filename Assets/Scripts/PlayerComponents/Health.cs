using System;
using UnityEngine;

namespace PlayerComponents
{
    public class Health: MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        private int _currentHealth;
        private PlayerCollisionHandler _playerCollisionHandler;
        
        public event Action OnDeath;

        private void Start()
        {
            _playerCollisionHandler  = GetComponent<PlayerCollisionHandler>();
            _playerCollisionHandler.OnTakeDamage += TakeDamage;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }

        }
        
        public int GetPlayerHealth()
        {
            return _currentHealth;
        }
        
        public void ChangeHealth(int amount)
        {
            _currentHealth += amount;
        }

        public void ChangePermanentHealth(int amount)
        {
            maxHealth += amount;
        }

        public void RestoreFullLife()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage()
        {
            ChangeHealth(-1);
        }
        
    }
}