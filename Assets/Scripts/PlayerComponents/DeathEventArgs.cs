using System;

namespace PlayerComponents
{
    public class DeathEventArgs : EventArgs
    {
        public bool IsSaved { get; set; }

        public DeathEventArgs()
        {
            IsSaved = false;
        }
    }
}