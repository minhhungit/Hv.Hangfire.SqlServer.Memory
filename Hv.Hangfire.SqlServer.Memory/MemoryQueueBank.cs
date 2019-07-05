using System;
using System.Collections.Concurrent;

namespace Hv.Hangfire.SqlServer.Memory
{
    public class MemoryQueueBank
    {
        public static ConcurrentDictionary<string, BlockingCollection<MemoryQueueMessage>> Queues = new ConcurrentDictionary<string, BlockingCollection<MemoryQueueMessage>>();

        public static BlockingCollection<MemoryQueueMessage> Get(string pattern, string queue)
        {
            return Queues.GetOrAdd(String.Format(pattern, queue), new BlockingCollection<MemoryQueueMessage>());
        }
    }
}
