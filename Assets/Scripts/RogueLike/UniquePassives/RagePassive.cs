using PlayerComponents;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class RagePassive : MonoBehaviour
    {
        private Frenzy _frenzy;
        private PlayerCollisionHandler _playerCollisionHandler;
        private float _extraTime = 2f;
        
        private void Start()
        {
            _frenzy = GetComponent<Frenzy>();
            _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
            _playerCollisionHandler.OnGhostKilled += OnGhostKilled;
        }

        public void OnGhostKilled()
        {
            _frenzy.IncreaseFrenzyTimer(_extraTime);
        }

    }
}