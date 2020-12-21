using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cronos;
using MrCoto.Ca.Application.Common.Background;

namespace MrCoto.Ca.WebApiTests.Fake
{
    public class FakeBackgroundService : IBackgroundJobService
    {
        public Task<string> Enqueue(Expression<Action> action)
        {
            action.Compile().Invoke();
            var jobId = Guid.NewGuid().ToString("n");
            return Task.FromResult(jobId);
        }

        public Task<string> Schedule(Expression<Action> action, TimeSpan timeSpan)
        {
            action.Compile().Invoke();
            var jobId = Guid.NewGuid().ToString("n");
            return Task.FromResult(jobId);
        }

        public Task<string> Schedule(Expression<Action> action, DateTime dateTime)
        {
            action.Compile().Invoke();
            var jobId = Guid.NewGuid().ToString("n");
            return Task.FromResult(jobId);
        }

        public Task<bool> Delete(string jobId)
        {
            return Task.FromResult(true);
        }

        public Task Recurring(Expression<Action> action, CronExpression cron)
        {
            action.Compile().Invoke();
            return Task.CompletedTask;
        }
    }
}