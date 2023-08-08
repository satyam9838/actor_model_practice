using System;
using Akka.Actor;

namespace actor_system_practice
{
    public class ConsoleReaderActor : UntypedActor
	{
        private readonly IActorRef _validationActor;
        public const string startCommand = "start";
        public const string exitCommand = "exit";

		public ConsoleReaderActor(IActorRef validationActor)
		{
            _validationActor = validationActor;
		}

        protected override void OnReceive(object message)
        {
            
            if (message.Equals(startCommand))
            {
                DoPrintInstructions();
            }
            GetAndValidateInput();
        }

        private void DoPrintInstructions()
        {
            Console.WriteLine("Starting Actor System");
            Console.WriteLine("Some entries will pass the validation some won't");
            Console.WriteLine("Type exit to quit the application");
        }

        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) && String.Equals(message, exitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }
            //sending message to validationActor to validate the message
            _validationActor.Tell(message);
        }
    }
}

