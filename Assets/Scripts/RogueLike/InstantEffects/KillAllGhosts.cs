using UnityEngine;

namespace RogueLike.InstantEffects
{
    [CreateAssetMenu(fileName = "KillAllGhosts", menuName = "Gamejam/InstantEffects/KillAllGhosts")]
    public class KillAllGhosts: APowerUp
    {
        private void DestroyGhostRec()
        {
            // Luizinho will not like this function
            GameObject ghost = GameObject.FindGameObjectWithTag("Ghost"); // performance hit
            if (!ghost) return;
            Destroy(ghost);
            DestroyGhostRec(); // kkk
        }
        public override void Apply(GameObject player)
        {
            DestroyGhostRec();
            Destroy(this);
        }
    }
}