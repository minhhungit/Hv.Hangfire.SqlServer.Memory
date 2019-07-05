using Hangfire.Annotations;
using Hangfire.Storage;
using System;

namespace Hv.Hangfire.SqlServer.Memory
{
    internal class MemoryFetchedJob : IFetchedJob
    {
        private readonly IMemoryTransaction _transaction;

        public MemoryFetchedJob([NotNull] IMemoryTransaction transaction,[NotNull] string jobId)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));
        }

        public string JobId { get; }

        public void RemoveFromQueue()
        {
            _transaction.Commit();
        }

        public void Requeue()
        {
            _transaction.Abort();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
