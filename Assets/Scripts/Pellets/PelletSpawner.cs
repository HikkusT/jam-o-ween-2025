using System;
using UnityEngine;

namespace Pellets
{
    public class PelletSpawner: ObjectSpawner
    {
        public override float GetSpawnTime()
        {
            return 1 / (SpawnRate * (0.7f + (Intensity * 0.4f)));
        }
    }
}