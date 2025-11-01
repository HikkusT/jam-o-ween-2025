using PlayerComponents;
using RogueLike;
using UnityEngine;

namespace RogueLike.PassiveModifiers
{
    [CreateAssetMenu(fileName = "Passive_ModifyFrenzy", menuName = "Gamejam/Passive PowerUp/Modify Frenzy Duration")]
    public class ModifyFrenzyDuration : ScriptableObject, IPowerUp
    {
        [Header("1. UI Data (Interface)")]
        [SerializeField] private string powerUpName = "Endless Frenzy";
        [SerializeField] private Sprite powerUpImage;
        
        [TextArea(3, 5)]
        [SerializeField] private string powerUpDescription = "Increases the duration of Frenzy mode.";

        [Header("2. Effect Configuration")]
        [Tooltip("Duration multiplier. 1.2 = +20% duration.")]
        [SerializeField] private float durationMultiplier = 1.2f;
        
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
            
            playerStats.AddFrenzyModifier(durationMultiplier);
            Debug.Log($"PowerUp APPLIED: {Name}. New multiplier: {durationMultiplier}");
        }
    }
}