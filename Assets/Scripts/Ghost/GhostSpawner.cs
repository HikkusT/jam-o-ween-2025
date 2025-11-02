namespace Ghost
{
    public class GhostSpawner:  ObjectSpawner
    {
        public override float GetSpawnTime()
        {
            return 1 / (SpawnRate * (1 + Intensity));
        }
    }
}