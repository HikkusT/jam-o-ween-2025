using PlayerComponents;
using UnityEngine;

namespace RogueLike.InstantEffects
{
    [CreateAssetMenu(fileName = "Invincible", menuName = "Gamejam/InstantEffects/Invincible")]
    public class Invincible: APowerUp
    {
        [SerializeField] private float InvincibilityTime = 4f;
        public override void Apply(GameObject player)
        {
            Health health = player.GetComponent<Health>();
            health.SetInvincibility(InvincibilityTime);
        }
    }
}