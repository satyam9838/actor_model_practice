using System;
using Akka.Actor;

namespace actor_system_practice
{
    public class ValidationActor : UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;

        public ValidationActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            var msg = message as string;
            //Todo -> Will do operation with recieve actor
            //Receive<int>(AddTwoNumbers);

            // message is empty
            if (string.IsNullOrEmpty(msg))
            {
                _consoleWriterActor.Tell(new Messages.NullInputError("No Input recieved"));
            }
            else
            {
                var valid = IsValid(msg);

                if (valid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Yes you have entered even no of inputs"));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError("Odd no of characters"));
                }
            }
            Sender.Tell(new Messages.ContinueProcessing());
        }

        private static bool IsValid(string message)
        {
            var valid = message.Length % 2 == 0;
            return valid;
        }
    }
}

