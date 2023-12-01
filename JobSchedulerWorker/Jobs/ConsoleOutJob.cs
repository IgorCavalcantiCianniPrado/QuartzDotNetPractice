using Quartz;

namespace JobSchedulerWorker.Jobs;

public class ConsoleOutJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"|{DateTime.Now}| Greetings from HelloJob!");
    }
}
