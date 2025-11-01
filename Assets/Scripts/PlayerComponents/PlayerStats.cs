using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerStats: MonoBehaviour, ISpeedProvider

    {
        [SerializeField] private int maxHealth = 5;
        private int _currentHealth;
        [SerializeField] private float movSpeed = 3.5f;
        [SerializeField] private float frenzyDuration = 7f;
        
        private readonly List<float> _movModifiers = new();
        private readonly List<float> _frenzyModifiers = new();
        
        public event Action OnDeath;

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }

        }

        public float GetPlayerSpeed()
        {
            float tempSpeed = movSpeed;
            foreach (float modifier in _movModifiers)
            {
                tempSpeed *= modifier;
            }
        
            return tempSpeed;
        }

        public float GetFrenzyDuration()
        {
            float tempDuration = frenzyDuration;
            foreach (float modifier in _frenzyModifiers)
            {
                tempDuration *= modifier;
            }
            return tempDuration;
        }

        public int GetPlayerHealth()
        {
            return _currentHealth;
        }

        public void AddMovModifier(float amount)
        {
            _movModifiers.Add(amount);
        }

        public void AddFrenzyModifier(float amount)
        {
            _frenzyModifiers.Add(amount);
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

        float ISpeedProvider.Speed => GetPlayerSpeed();
    }
}