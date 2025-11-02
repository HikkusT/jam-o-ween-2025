using PlayerComponents;
using UnityEngine;

namespace RogueLike.InstantEffects
{
    [CreateAssetMenu(fileName = "RestoreLife", menuName = "Gamejam/InstantEffects/RestoreLife")]
    public class RestoreLife: APowerUp
    {
        public override void Apply(GameObject player)
        {
            Health health = player.GetComponent<Health>();
            health.ChangePermanentHealth(-1);
            health.RestoreFullLife();
        }
    }
}