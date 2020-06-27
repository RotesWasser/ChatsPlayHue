using ChatsPlayHue.Light;
using ChatsPlayHue.LightActions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace ChatsPlayHue.LightActionSource.DummyLightActionSource
{
    class DummyLightActionSource : ILightActionSource
    {
        private List<IObserver<LightAction>> observers = new List<IObserver<LightAction>>();

        private Thread t = null;
        private bool shouldStop = false;

        public void Start()
        {
            if (t != null)
                throw new ArgumentException("Already running!");

            shouldStop = false;

            t = new Thread(new ThreadStart(PeriodicRedBlueSender));

            t.Start();
        }

        public void Stop()
        {
            if (t == null)
                throw new ArgumentException("Not running!");

            shouldStop = true;

            t.Join();
        }

        private void PeriodicOnOffSender()
        {
            LightPowerState nextState = LightPowerState.On;

            while (!shouldStop)
            {
                foreach(var observer in observers)
                {
                    observer.OnNext(new ToggleLightAction(nextState));
                }

                nextState = nextState == LightPowerState.On ? LightPowerState.Off : LightPowerState.On;

                Thread.Sleep(500);
            }
        }

        private void PeriodicRedBlueSender()
        {
            Color nextColor = Color.Red;

            while (!shouldStop)
            {
                foreach(var observer in observers)
                {
                    observer.OnNext(new StaticColorLightAction(nextColor));
                }

                nextColor = nextColor == Color.Red ? Color.Blue : Color.Red;

                Thread.Sleep(500);
            }
        }

        public IDisposable Subscribe(IObserver<LightAction> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observer, observers);
        }



        private class Unsubscriber : IDisposable
        {
            private IObserver<LightAction> observer;
            private List<IObserver<LightAction>> observers;

            internal Unsubscriber(IObserver<LightAction> observer, List<IObserver<LightAction>> observers)
            {
                this.observer = observer;
                this.observers = observers;
            }

            public void Dispose()
            {
                if (observer != null)
                    observers.Remove(observer);
            }
        }
    }
}
