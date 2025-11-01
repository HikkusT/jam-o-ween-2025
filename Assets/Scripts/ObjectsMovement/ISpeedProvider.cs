namespace Interfaces
{
    public interface ISpeedProvider
    {
        float Speed { get; }
    }

    public class ConstantSpeed : ISpeedProvider
    {
        public float Speed { get; }
        
        public ConstantSpeed(float speed)
        {
            Speed = speed;
        }
    }
}