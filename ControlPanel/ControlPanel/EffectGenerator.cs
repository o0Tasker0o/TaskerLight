using System.Threading;

namespace ControlPanel
{
    public abstract class EffectGenerator
    {
        protected Thread mTickThread;
        protected bool mRunning;
        protected ColourOutputManager mOutputManager;

        protected EffectGenerator(ColourOutputManager outputManager)
        {
            mOutputManager = outputManager;
        }

        public void Start()
        {
            if(!mRunning)
            {
                mRunning = true;
                mTickThread = new Thread(ThreadTick);
                mTickThread.Start();
            }
        }

        public void Stop()
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