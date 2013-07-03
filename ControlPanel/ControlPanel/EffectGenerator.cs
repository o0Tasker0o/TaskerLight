using System.Threading;

namespace ControlPanel
{
    internal abstract class EffectGenerator
    {
        private Thread mTickThread;
        protected bool mRunning;
        protected ColourOutputManager mOutputManager;

        public EffectGenerator(ColourOutputManager outputManager)
        {
            mOutputManager = outputManager;
        }
        
        internal void Start()
        {
            if(!mRunning)
            {
                mRunning = true;
                mTickThread = new Thread(ThreadTick);
                mTickThread.Start();
            }
        }

        internal void Stop()
        {
            if (mRunning)
            {
                mRunning = false;
                mTickThread.Join();
            }
        }

        protected abstract void ThreadTick();
    }
}