using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hv.Hangfire.SqlServer.Memory
{
    internal class MemoryJobQueueMonitoringApi : IPersistentJobQueueMonitoringApi
    {
        private readonly string _pathPattern;
        private readonly IEnumerable<string> _queues;

        public MemoryJobQueueMonitoringApi(string pathPattern, IEnumerable<string> queues)
        {
            _pathPattern = pathPattern ?? throw new ArgumentNullException(nameof(pathPattern));
            _queues = queues ?? throw new ArgumentNullException(nameof(queues));
        }

        public IEnumerable<string> GetQueues()
        {
            return _queues;
        }

        public IEnumerable<long> GetEnqueuedJobIds(string queue, int @from, int perPage)
        {
            var messageQueue = MemoryQueueBank.Get(_pathPattern, queue);
            return messageQueue.Skip(from).Take(perPage).Select(x => long.Parse(x.Label));

            //var result = new List<long>();

            //var current = 0;
            //var end = from + perPage;

            //foreach (var job in messageQueue.GetConsumingEnumerable(CancellationToken.None))
            //{
            //    if (current >= from && current < end)
            //    {
            //        var message = job;
            //        if (message == null) continue;

            //        result.Add(long.Parse(message.Label));
            //    }

            //    if (current >= end) break;

            //    current++;
            //}

            //return result;
        }

        public IEnumerable<long> GetFetchedJobIds(string queue, int @from, int perPage)
        {
            return Enumerable.Empty<long>();
        }

        public EnqueuedAndFetchedCountDto GetEnqueuedAndFetchedCount(string queue)
        {
            var messageQueue = MemoryQueueBank.Get(_pathPattern, queue);
            return new EnqueuedAndFetchedCountDto
            {
                EnqueuedCount = messageQueue.Count()
            };
        }
    }
}
