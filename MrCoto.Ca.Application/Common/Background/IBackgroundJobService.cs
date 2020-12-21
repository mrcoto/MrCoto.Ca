using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cronos;

namespace MrCoto.Ca.Application.Common.Background
{
    public interface IBackgroundJobService
    {
        public Task<string> Enqueue(Expression<Action> action);
        public Task<string> Schedule(Expression<Action> action, TimeSpan timeSpan);
        public Task<string> Schedule(Expression<Action> action, DateTime dateTime);
        public Task<bool> Delete(string jobId);
        public Task Recurring(Expression<Action> action, CronExpression cron);
    }
}