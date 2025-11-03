using PlayerComponents;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class RagePassive : MonoBehaviour
    {
        private Frenzy _frenzy;
        private PlayerCollisionHandler _playerCollisionHandler;
        [SerializeField] private float _baseExtraTime = 0.4f;
        
        private void Start()
        {
            _frenzy = GetComponent<Frenzy>();
            _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
            _playerCollisionHandler.OnGhostKilled += OnGhostKilled;
        }

        public void OnGhostKilled()
        {
            float extraTime = Mathf.Clamp(_baseExtraTime / 1 + _frenzy.KillCount, 0, _baseExtraTime);
            _frenzy.IncreaseFrenzyTimer(extraTime);
        }

    }
}