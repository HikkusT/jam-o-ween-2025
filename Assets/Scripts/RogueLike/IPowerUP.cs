using PlayerComponents;
using UnityEngine;

namespace RogueLike
{
    public interface IPowerUp
    {
        string Name { get; }
        Sprite Image { get; }
        string Description { get; }
    
        void Apply(PlayerStats playerStats);
    }
}