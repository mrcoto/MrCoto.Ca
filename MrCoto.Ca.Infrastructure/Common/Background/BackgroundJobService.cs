using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cronos;
using Hangfire;
using MrCoto.Ca.Application.Common.Background;

namespace MrCoto.Ca.Infrastructure.Common.Background
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public Task<string> Enqueue(Expression<Action> action)
        {
            var jobId = BackgroundJob.Enqueue(action);
            return Task.FromResult(jobId);
        }

        public Task<string> Schedule(Expression<Action> action, TimeSpan timeSpan)
        {
            var jobId = BackgroundJob.Schedule(action, timeSpan);
            return Task.FromResult(jobId);
        }
        
        public Task<string> Schedule(Expression<Action> action, DateTime dateTime)
        {
            var jobId = BackgroundJob.Schedule(action, dateTime);
            return Task.FromResult(jobId);
        }

        public Task<bool> Delete(string jobId)
        {
            var result = BackgroundJob.Delete(jobId);
            return Task.FromResult(result);
        }

        public Task Recurring(Expression<Action> action, CronExpression cron)
        {
            RecurringJob.AddOrUpdate(action, cron.ToString(), TimeZoneInfo.Local);
            return Task.CompletedTask;
        }
    }
}