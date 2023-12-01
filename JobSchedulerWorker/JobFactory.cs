using JobSchedulerWorker.Jobs;
using Quartz;

namespace JobSchedulerWorker
{
    public static class JobFactory
    {
        public static IJobDetail Create(JobType jobType)
        {

            var jobBuilder = JobBuilder.Create()
                                       .WithIdentity("job", "group");

            switch (jobType)
            {
                case JobType.console:
                    jobBuilder.OfType<ConsoleOutJob>();
                    break;

                case JobType.file:
                    jobBuilder.OfType<FileWriterJob>();
                    break;

                default:
                    throw new Exception("Invalid JobType.");
            }

            return jobBuilder.Build();
        } 
    }
}
