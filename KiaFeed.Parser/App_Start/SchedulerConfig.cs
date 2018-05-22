
namespace KiaFeed.Parser
{
    using KiaFeed.Parser.Jobs;
    using Quartz;
    using Quartz.Impl;

    /// <summary>
    /// http://www.quartz-scheduler.net/documentation/index.html
    /// </summary>
    public static class SchedulerConfig
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            var indexJob = JobBuilder.Create<IndexJob>()
              .WithIdentity("job1")
              .Build();
            var indexTrigger = TriggerBuilder.Create()
                .WithIdentity("trigger1")
                .WithPriority(1)
                .StartNow()
                .ForJob(indexJob)
                .WithSimpleSchedule(s => s.WithIntervalInHours(1).RepeatForever().Build())
                .Build();
            scheduler.ScheduleJob(indexJob, indexTrigger);

            var docJob = JobBuilder.Create<DocJob>()
                .WithIdentity("job2")
                .Build();
            var docTrigger = TriggerBuilder.Create()
                .WithIdentity("trigger2")
                .WithPriority(2)
                .StartNow()
                .ForJob(docJob)
                .WithSimpleSchedule(s => s.WithIntervalInHours(1).RepeatForever().Build())
                .Build();
            scheduler.ScheduleJob(docJob, docTrigger);

            scheduler.Start();

        }
    }
}