using PlayerComponents;
using Unity.VisualScripting;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    [CreateAssetMenu(fileName = "GuardianAngelPowerUp", menuName = "Gamejam/UniquePassives/GuardianAngelPowerUp")]
    public class GuardianAngelPowerUp : APowerUp
    {
        public override void Apply(GameObject player)
        {
            player.AddComponent<GuardianAngelPassive>();
        }
    }
}