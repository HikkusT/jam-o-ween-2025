using UnityEngine;

namespace RogueLike.InstantEffects
{
    [CreateAssetMenu(fileName = "KillAllGhosts", menuName = "Gamejam/InstantEffects/KillAllGhosts")]
    public class KillAllGhosts: APowerUp
    {
        private void DestroyGhost()
        {
            var ghosts = GameObject.FindGameObjectsWithTag("Ghost");
            foreach (var ghost in ghosts)
            {
                Destroy(ghost);
            }
        }
        public override void Apply(GameObject player)
        {
            DestroyGhost();
        }
    }
}