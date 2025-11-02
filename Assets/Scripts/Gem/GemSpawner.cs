using UnityEngine;

namespace Gem
{
    public class GemSpawner : ObjectSpawner
    {
        public override float GetSpawnTime()
        {
            float intensity = Mathf.Clamp(3 / Intensity, 0, 3);
            return 1 / (SpawnRate * (1 + intensity));
        }
    }
}
