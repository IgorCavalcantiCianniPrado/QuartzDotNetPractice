﻿using Quartz;

namespace JobSchedulerWorker.Jobs;

public class HelloJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"Greetings from HelloJob! {DateTime.Now}");
    }
}
