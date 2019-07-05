using Hangfire;
using Hangfire.SqlServer;
using Hangfire.States;

namespace Hv.Hangfire.SqlServer.Memory
{
    public static class MemoryExtensions
    {
        public static IGlobalConfiguration<SqlServerStorage> UseMemoryQueues(
            this IGlobalConfiguration<SqlServerStorage> configuration,
            string pathPattern,
            params string[] queues)
        {
            if (queues.Length == 0)
            {
                queues = new[] { EnqueuedState.DefaultQueue };
            }

            var provider = new MemoryJobQueueProvider(pathPattern, queues);
            configuration.Entry.QueueProviders.Add(provider, queues);

            return configuration;
        }
    }
}
