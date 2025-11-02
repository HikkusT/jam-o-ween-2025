using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerComponents
{
    public enum FrenzyState
    {
        Inactive,
        Active,
        Exiting
    }
    
    public class Frenzy: MonoBehaviour
    {
        [SerializeField] private float duration = 7.5f;
        [SerializeField] private float exitDuration = 1.5f;
        private readonly List<float> _frenzyModifiers = new();
        
        private FrenzyState _currentState = FrenzyState.Inactive;
        private float _frenzyTimer;
        
        public FrenzyState CurrentState => _currentState;
        public bool IsActive => _currentState != FrenzyState.Inactive;
        
        public event Action OnFrenzyEnter;
        public event Action OnFrenzyExiting;
        public event Action OnFrenzyHardExit;
        
        private PlayerCollisionHandler _playerCollisionHandler;

        private void Start()
        {
            _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
            _playerCollisionHandler.OnCollisionWithGems += ActivateFrenzy;
            OnFrenzyEnter += FrenzyEnter;
            OnFrenzyExiting += FrenzyExiting;
            OnFrenzyHardExit += FrenzyHardExit;
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            _frenzyTimer -= Time.deltaTime;
            //Debug.Log($"time left: {_frenzyTimer}");

            if (_currentState == FrenzyState.Active && _frenzyTimer <= exitDuration)
            {
                _currentState = FrenzyState.Exiting;
                OnFrenzyExiting?.Invoke();
            }
            
            else if (_currentState == FrenzyState.Exiting && _frenzyTimer > exitDuration)
            {
                OnFrenzyEnter?.Invoke();
            }

            if (_frenzyTimer <= 0f)
            {
                OnFrenzyHardExit?.Invoke();
            }
        }
        
        public float GetFrenzyDuration()
        {
            float tempDuration = duration;
            foreach (float modifier in _frenzyModifiers)
            {
                tempDuration *= modifier;
            }
            return tempDuration;
        }
        
        public void AddFrenzyModifier(float amount)
        {
            _frenzyModifiers.Add(amount);
        }

        private void FrenzyEnter()
        {
            _currentState = FrenzyState.Active;
        }
        
        private void FrenzyExiting()
        {
            _currentState = FrenzyState.Exiting;
        }

        private void FrenzyHardExit()
        {
            _currentState = FrenzyState.Inactive;
            _frenzyTimer = 0f;
        }
        
        
        public void ActivateFrenzy()
        {
            float newDuration = GetFrenzyDuration();
            _frenzyTimer = newDuration;
            
            Debug.Log($"Frenzy activated for {_frenzyTimer}");
            
            if (!IsActive)
            {
                OnFrenzyEnter?.Invoke();
            }
            else
            {
                _currentState = FrenzyState.Active; 
            }
        }

        public void IncreaseFrenzyTimer(float amount)
        {
            _frenzyTimer += amount;
        }
    }
}