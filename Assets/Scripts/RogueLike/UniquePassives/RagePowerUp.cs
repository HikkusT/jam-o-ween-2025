using UnityEngine;

namespace RogueLike.UniquePassives
{
    [CreateAssetMenu(fileName = "RagePowerUp", menuName = "Gamejam/UniquePassives/RagePowerUp")]
    public class RagePowerUp: APowerUp
    {
        public override void Apply(GameObject player)
        {
            player.AddComponent<RagePassive>();
        }
    }
}