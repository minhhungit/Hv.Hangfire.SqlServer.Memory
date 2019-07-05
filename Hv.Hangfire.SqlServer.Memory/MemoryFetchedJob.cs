using Hangfire.Annotations;
using Hangfire.Storage;
using System;

namespace Hv.Hangfire.SqlServer.Memory
{
    public class MemoryFetchedJob : IFetchedJob
    {
        public MemoryFetchedJob([NotNull] string jobId)
        {
            JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));
        }

        public string JobId { get; }

        public void RemoveFromQueue()
        {

        }

        public void Requeue()
        {

        }

        public void Dispose()
        {

        }
    }
}
