using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScreaming.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is SimulatedCorruptStateException)
                    {
                        return Directive.Restart;
                    }
                    if (exception is SimulatedTerribleMovieException)
                    {
                        return Directive.Resume;
                    }
                    return Directive.Restart;
                }
                );
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan("PlaybackStatisticsActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {

            ColorConsole.WriteLineCyan("PlaybackStatisticsActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
