using System;

namespace Kamina.Logic.Core.Message
{
    internal abstract class BaseTimer : IDisposable
    {
        internal BaseTimer(Action<BaseTimer> onTimerHandeld)
        {
            OnTimerHandeld = onTimerHandeld;
        }

        public void Dispose()
        {
            OnTimerHandeld =null;
        }

        protected Action<BaseTimer> OnTimerHandeld;
    }
}
