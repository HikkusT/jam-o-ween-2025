using PlayerComponents;
using UnityEngine;

namespace RogueLike.AttributeModifiers
{
    [CreateAssetMenu(fileName = "Passive_ModifySpeed", menuName = "Gamejam/Passive PowerUp/Modify Speed")]
    public class ModifySpeed : APowerUp
    {
        [Header("2. Effect Configuration")]
        [Tooltip("Speed multiplier. 1.1 = +10% speed.")]
        [SerializeField] private float speedMultiplier = 1.1f;

        public override void Apply(GameObject player)
        {
            if (player == null)
            {
                Debug.LogWarning($"Player not found. Could not apply {Name}.");
                return;
            }
            
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            playerStats.AddMovModifier(speedMultiplier);
            Debug.Log($"PowerUp APPLIED: {Name}. New multiplier: {speedMultiplier}");
        }
    }
}