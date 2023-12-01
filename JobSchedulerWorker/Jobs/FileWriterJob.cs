using Quartz;

namespace JobSchedulerWorker.Jobs
{
    public class FileWriterJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            File.AppendAllText($"{currentDirectory}/FileWriterJob.txt", "Teste123 ");
        }
    }
}
