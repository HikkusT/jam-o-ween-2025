using PlayerComponents;
using UnityEngine;

namespace RogueLike.PassiveModifiers
{
    
    [CreateAssetMenu(fileName = "Extra Life", menuName = "Gamejam/Passive PowerUp/Add Life")]
    public class AddLife : APowerUp
    {
        [Tooltip("How much life does it grants?")]
        [SerializeField] private int livesToAdd = 1;

        public override void Apply(PlayerStats playerStats)
        {
            if (playerStats == null)
            {
                Debug.LogWarning($"PlayerStats not found. Not able to apply {Name}.");
                return;
            }

            playerStats.ChangePermanentHealth(livesToAdd);

            Debug.Log($"PowerUp applied: {Name}. Lives added: {livesToAdd}");
        }
    }
}