using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace configuring_practice
{
    public class GreetingService : IGreetingService
    {

        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;

        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;

        }
        public void RunMethod()
        {
            for (int i = 0; i < _config.GetValue<int>("looptimes"); i++)
                //{
                _log.LogInformation("Run number {runNumber}", i.ToString());
            Console.ReadLine();
            //}
        }
    }
}