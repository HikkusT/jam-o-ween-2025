using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerStats: MonoBehaviour, ISpeedProvider

    {
        [SerializeField] private float movSpeed = 3.5f;
        
        private readonly List<float> _movModifiers = new();

        public float GetPlayerSpeed()
        {
            float tempSpeed = movSpeed;
            foreach (float modifier in _movModifiers)
            {
                tempSpeed *= modifier;
            }
        
            return tempSpeed;
        }

        public void AddMovModifier(float amount)
        {
            _movModifiers.Add(amount);
        }

        float ISpeedProvider.Speed => GetPlayerSpeed();
    }
}