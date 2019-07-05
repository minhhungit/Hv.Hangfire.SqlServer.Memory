using Hangfire.SqlServer;
using System.Collections.Generic;

namespace Hv.Hangfire.SqlServer.Memory
{
    internal class MemoryJobQueueProvider : IPersistentJobQueueProvider
    {
        private readonly MemoryJobQueue _jobQueue;
        private readonly MemoryJobQueueMonitoringApi _monitoringApi;

        public MemoryJobQueueProvider(string pathPattern, IEnumerable<string> queues)
        {
            _jobQueue = new MemoryJobQueue(pathPattern);
            _monitoringApi = new MemoryJobQueueMonitoringApi(pathPattern, queues);
        }

        public IPersistentJobQueue GetJobQueue()
        {
            return _jobQueue;
        }

        public IPersistentJobQueueMonitoringApi GetJobQueueMonitoringApi()
        {
            return _monitoringApi;
        }
    }
}
