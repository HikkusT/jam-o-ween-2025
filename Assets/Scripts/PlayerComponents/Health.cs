using System;
using UnityEngine;

namespace PlayerComponents
{
    public class Health: MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        private int _currentHealth;
        
        public event Action OnDeath;

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
        
        
    }
}