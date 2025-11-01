using PlayerComponents;
using UnityEngine;

namespace RogueLike.PassiveModifiers
{
    [CreateAssetMenu(fileName = "Passive_ModifySpeed", menuName = "Gamejam/Passive PowerUp/Modify Speed")]
    public class ModifySpeed : ScriptableObject, IPowerUp
    {
        [Header("1. UI Data (Interface)")]
        [SerializeField] private string powerUpName = "Speed of Light";
        [SerializeField] private Sprite powerUpImage;
        
        [TextArea(3, 5)]
        [SerializeField] private string powerUpDescription = "Increases movement speed.";

        [Header("2. Effect Configuration")]
        [Tooltip("Speed multiplier. 1.1 = +10% speed.")]
        [SerializeField] private float speedMultiplier = 1.1f;
        
        // --- IPowerUp Interface Implementation ---
        
        public string Name => powerUpName;
        public Sprite Image => powerUpImage;
        public string Description => powerUpDescription;

        public void Apply(PlayerStats playerStats)
        {
            if (playerStats == null)
            {
                Debug.LogWarning($"PlayerStats not found. Could not apply {Name}.");
                return;
            }
            
            playerStats.AddMovModifier(speedMultiplier);
            Debug.Log($"PowerUp APPLIED: {Name}. New multiplier: {speedMultiplier}");
        }
    }
}