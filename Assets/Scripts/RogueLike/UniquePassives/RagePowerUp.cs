using UnityEngine;

namespace RogueLike.UniquePassives
{
    public class RagePowerUp: APowerUp
    {
        public override void Apply(GameObject player)
        {
            player.AddComponent<RagePassive>();
        }
    }
}