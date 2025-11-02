using System;
using DefaultNamespace;
using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _pelletSound;
        [SerializeField] private AudioSource _source;
        
        private Frenzy _frenzy;
        private FrenzyVisuals _frenzyVisuals;
        
        public event Action OnCollisionWithBalls;
        public event Action OnCollisionWithGems;
        public event Action OnGhostKilled;
        public event Action OnTakeDamage;

        private void Start()
        {
            _frenzy = GetComponent<Frenzy>();
            _frenzyVisuals = FindAnyObjectByType<FrenzyVisuals>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                OnCollisionWithBalls?.Invoke();
                _source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                // _source.PlayOneShot(_pelletSound, 0.2f);
                Destroy(other.gameObject);
                
            } else if (other.CompareTag("Gem"))
            {
                OnCollisionWithGems?.Invoke();
                _frenzyVisuals.PlayEffect();
                Destroy(other.gameObject);
            }
            
            else if (other.CompareTag("Ghost"))
            {
                if (_frenzy.IsActive)
                {
                    OnGhostKilled?.Invoke();
                    other.GetComponent<GhostVisuals>().PlayParticles();
                    Destroy(other.gameObject);
                    return;
                }
                
                OnTakeDamage?.Invoke();
                
            }
        }
    }
}