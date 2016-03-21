using Akka.Actor;
using System;

namespace MovieScreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        public UserActor()
        {

        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {

            ColorConsole.WriteLineGreen("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
