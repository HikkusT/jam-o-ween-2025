using PlayerComponents;
using RogueLike;
using UnityEngine;

namespace RogueLike.PassiveModifiers
{
    [CreateAssetMenu(fileName = "Passive_ModifyFrenzy", menuName = "Gamejam/Passive PowerUp/Modify Frenzy Duration")]
    public class ModifyFrenzyDuration : APowerUp
    {
        [Header("2. Effect Configuration")]
        [Tooltip("Duration multiplier. 1.2 = +20% duration.")]
        [SerializeField] private float durationMultiplier = 1.2f;

        public override void Apply(GameObject player)
        {
            if (player == null)
            {
                Debug.LogWarning($"frenzy not found. Could not apply {Name}.");
                return;
            }
            
            Frenzy frenzy = player.GetComponent<Frenzy>();
            frenzy.AddFrenzyModifier(durationMultiplier);
            Debug.Log($"PowerUp APPLIED: {Name}. New multiplier: {durationMultiplier}");
        }
    }
}