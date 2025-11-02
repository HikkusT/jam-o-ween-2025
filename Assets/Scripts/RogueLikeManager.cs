using System;
using PlayerComponents;
using UnityEngine;
using UnityEngine.Serialization;

public class RogueLikeManager: MonoBehaviour
{
        [SerializeField] private GameObject player;
        private PlayerCollisionHandler _playerCollisionHandler;
        private int _score;
        private int _intensity; // intensidade controla o quao caotico as coisas estão. ela aumenta quanto mais bolas

        private int TargetScore => (int)(25f * Mathf.Pow(_intensity, 1.2f));

        private event Action OnLevelUp;
        
        private void Start()
        {
                _score = 0;
                _intensity = 1;
                _playerCollisionHandler = player.GetComponent<PlayerCollisionHandler>();
                _playerCollisionHandler.OnCollisionWithBalls += OnCollisionWithBalls;
        }

        private void OnCollisionWithBalls()
        {
                _score++;
                Debug.Log("Score: " + _score);
                Debug.Log("Intensity: " + _intensity);
                
                if (_score >= TargetScore)
                {
                        OnLevelUp?.Invoke();
                        _intensity++;
                }
                        
        }
        
        
}