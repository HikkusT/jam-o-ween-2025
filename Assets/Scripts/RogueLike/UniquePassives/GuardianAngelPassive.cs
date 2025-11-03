using PlayerComponents;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class GuardianAngelPassive : MonoBehaviour
    {
        private Health _health;
        
        private void Start()
        {
            _health = GetComponent<Health>();
            _health.OnDeath += AvoidDeath;
        }

        private void AvoidDeath(object sender, DeathEventArgs args)
        {
            if (args.IsSaved) return;
            
            args.IsSaved = true;
            
            _health.ChangeHealth(1);
            Destroy(this);
        }
        
        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnDeath -= AvoidDeath;
            }
        }
    }
}