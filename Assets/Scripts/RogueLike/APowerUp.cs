using PlayerComponents;
using UnityEngine;

namespace RogueLike
{
    public abstract class APowerUp : ScriptableObject, IPowerUp
    {
        [SerializeField] private string powerUpName;
        [SerializeField] private Sprite powerUpImage;
        
        [TextArea(3, 5)]
        [SerializeField] private string powerUpDescription;

        public string Name => powerUpName;
        public Sprite Image => powerUpImage;
        public string Description => powerUpDescription;
        
        public abstract void Apply(GameObject player);
    }
}