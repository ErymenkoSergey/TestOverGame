using System;

namespace TestOverMobile.Interface
{
    public interface IPaused
    {
        public event Action<bool> OnIsPause;
    }
}