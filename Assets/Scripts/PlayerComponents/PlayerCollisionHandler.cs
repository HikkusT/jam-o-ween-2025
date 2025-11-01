using System;
using PlayerComponents;
using RogueLike.UniquePassives;
using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private Frenzy _frenzy;
        
        public event Action OnCollisionWithBalls;
        public event Action OnGhostKilled;
        public event Action OnTakeDamage;

        private void Start()
        {
            _frenzy = GetComponent<Frenzy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                OnCollisionWithBalls?.Invoke();
                Destroy(other.gameObject);
                
            }
            
            else if (other.CompareTag("Ghost"))
            {
                if (!_frenzy.IsActive)
                {
                    OnTakeDamage?.Invoke();
                }
                
                OnGhostKilled?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}