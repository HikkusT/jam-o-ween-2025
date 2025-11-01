using PlayerComponents;
using UnityEngine;

namespace RogueLike.PassiveModifiers
{
    
    [CreateAssetMenu(fileName = "Extra Life", menuName = "Gamejam/Passive PowerUp/Add Life")]
    public class AddLife : APowerUp
    {
        [Tooltip("How much life does it grants?")]
        [SerializeField] private int livesToAdd = 1;

        public override void Apply(GameObject player)
        {
            if (player == null)
            {
                Debug.LogWarning($"Player not found. Not able to apply {Name}.");
                return;
            }

            Health health = player.GetComponent<Health>();
            health.ChangePermanentHealth(livesToAdd);

            Debug.Log($"PowerUp applied: {Name}. Lives added: {livesToAdd}");
        }
    }
}