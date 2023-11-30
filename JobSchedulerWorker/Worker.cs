using JobSchedulerWorker.Jobs;
using Quartz;

namespace JobSchedulerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConfigurationRoot _configuration;
        private readonly IScheduler _scheduler;


        public Worker(ILogger<Worker> logger, IConfigurationRoot configuration, IScheduler scheduler)
        {
            _logger = logger;
            _configuration = configuration;
            _scheduler = scheduler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ScheduleJob();

            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task ScheduleJob()
        {
            await _scheduler.Start();

            var job = GetJob();
            var trigger = GetTrigger();

            await _scheduler.ScheduleJob(job, trigger);
        }

        private IJobDetail GetJob()
        {
            return JobBuilder.Create<HelloJob>()
                             .WithIdentity("job", "group")
                             .Build();
        }

        private ITrigger GetTrigger()
        {
            var nulableInterval = _configuration["JobScheduler:IntervailInMiliSeconds"];
            var intervalInMiliSeconds = nulableInterval ?? "1000";
            var nulableRepeatCount = _configuration["JobScheduler:RepeatCount"];
            var repeatCount = nulableRepeatCount ?? "10";

            var interval = TimeSpan.FromMilliseconds(double.Parse(intervalInMiliSeconds));

            return TriggerBuilder.Create()
                                 .WithIdentity("trigger", "group")
                                 .StartNow()
                                 .WithSimpleSchedule(x => x
                                     .WithInterval(interval)
                                     .WithRepeatCount(int.Parse(repeatCount)))
                                 .Build();
        }
    }
}
