using System;
using System.Collections.Concurrent;

namespace Hv.Hangfire.SqlServer.Memory
{
    public class MemoryQueueBank
    {
        public static ConcurrentDictionary<string, ConcurrentDictionary<string, MemoryQueueMessage>> Queues = new ConcurrentDictionary<string, ConcurrentDictionary<string, MemoryQueueMessage>>();

        public static ConcurrentDictionary<string, MemoryQueueMessage> Get(string pattern, string queue)
        {
            return Queues.GetOrAdd(String.Format(pattern, queue), new ConcurrentDictionary<string, MemoryQueueMessage>());
        }
    }
}
