using PlayerComponents;
using UnityEngine;

namespace RogueLike.InstantEffects
{
    [CreateAssetMenu(fileName = "StartTheFrenzy", menuName = "Gamejam/InstantEffects/StartTheFrenzy")]
    public class StartTheFrenzy: APowerUp
    {
        public override void Apply(GameObject player)
        {
            Frenzy frenzy = player.GetComponent<Frenzy>();
            frenzy.ActivateFrenzy();
        }
    }
}