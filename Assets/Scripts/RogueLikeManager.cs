using System;
using System.Collections.Generic;
using System.Linq;
using PlayerComponents;
using RogueLike;
using UnityEngine;
using Random = UnityEngine.Random;

public class RogueLikeManager: MonoBehaviour
{
        [SerializeField] private int scoreGetFromKillingGhosts = 3;
        [SerializeField] private GameObject player;

        [SerializeField] private List<APowerUp> _powerUps;
        [SerializeField] private CardSelectionScreen _cardSelectionScreen;
        
        private PlayerCollisionHandler _playerCollisionHandler;
        private int _score;
        private int _intensity; // intensity of the gameplay

        private int TargetScore => (int)(25f * Mathf.Pow(_intensity, 1.2f));

        private event Action OnLevelUp;
        
        private void Start()
        {
                _score = 0;
                _intensity = 1;
                _playerCollisionHandler = player.GetComponent<PlayerCollisionHandler>();
                _playerCollisionHandler.OnCollisionWithBalls += OnCollisionWithBalls;
                _playerCollisionHandler.OnGhostKilled += OnCollisionWithGhost;

                OnLevelUp += InternalLevelUp;
        }

        private void OnCollisionWithBalls()
        {
                _score++;
                Debug.Log("Score: " + _score);
                Debug.Log("Intensity: " + _intensity);
                
                if (_score >= TargetScore)
                {
                        OnLevelUp?.Invoke();
                        _cardSelectionScreen.Show(_powerUps.OrderBy(_ => Random.value).Take(3).Cast<IPowerUp>().ToList());
                }
                        
        }

        private void OnCollisionWithGhost()
        {
                _score += scoreGetFromKillingGhosts;
        }

        public int GetScore()
        {
                return _score;
        }
        
        public int GetIntensity()
        {
                if (_intensity <= 0)
                {
                        return 1;
                }
                
                return _intensity;
        }

        private void InternalLevelUp()
        {
                _intensity++;
                
        }
        
        
}