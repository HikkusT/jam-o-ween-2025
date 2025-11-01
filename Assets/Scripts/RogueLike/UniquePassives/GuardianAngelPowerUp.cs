using PlayerComponents;
using Unity.VisualScripting;
using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class GuardianAngelPowerUp : APowerUp
    {
        public override void Apply(PlayerStats playerStats)
        {
            playerStats.AddComponent<GuardianAngelPassive>();
        }
    }
}