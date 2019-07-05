using Hangfire.Annotations;
using Hangfire.SqlServer;
using Hangfire.States;
using System;

namespace Hv.Hangfire.SqlServer.Memory
{
    public static class MemorySqlServerStorageExtensions
    {
        public static SqlServerStorage UseMemoryQueues(
            [NotNull] this SqlServerStorage storage,
            [NotNull] string pathPattern)
        {
            return UseMemoryQueues(storage, pathPattern, EnqueuedState.DefaultQueue);
        }

        public static SqlServerStorage UseMsmqQueues(
            [NotNull] this SqlServerStorage storage,
            [NotNull] string pathPattern,
            params string[] queues)
        {
            return UseMemoryQueues(storage, pathPattern, queues);
        }

        public static SqlServerStorage UseMemoryQueues(
            [NotNull] this SqlServerStorage storage,
            [NotNull] string pathPattern,
            params string[] queues)
        {
            if (storage == null) throw new ArgumentNullException(nameof(storage));

            if (queues.Length == 0)
            {
                queues = new[] { EnqueuedState.DefaultQueue };
            }

            var provider = new MemoryJobQueueProvider(pathPattern, queues);
            storage.QueueProviders.Add(provider, queues);

            return storage;
        }
    }
}
