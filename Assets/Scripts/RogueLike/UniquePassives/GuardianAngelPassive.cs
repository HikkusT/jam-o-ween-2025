using PlayerComponents;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class GuardianAngelPassive : MonoBehaviour
    {
        private PlayerStats _health;
        
        private void Start()
        {
            _health = GetComponent<PlayerStats>();
            _health.OnDeath += AvoidDeath;
        }

        private void AvoidDeath()
        {
            _health.RestoreFullLife();
            Destroy(this);
        }
    }
}