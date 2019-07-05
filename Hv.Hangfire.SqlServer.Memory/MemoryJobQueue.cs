using Hangfire.SqlServer;
using Hangfire.Storage;
using System;
using System.Threading;

namespace Hv.Hangfire.SqlServer.Memory
{
    internal class MemoryJobQueue : IPersistentJobQueue
    {
        private static readonly TimeSpan SyncReceiveTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan ReceiveTimeout = new TimeSpan(1);
        private readonly string _pathPattern;

        public MemoryJobQueue(string pathPattern)
        {
            _pathPattern = pathPattern ?? throw new ArgumentNullException(nameof(pathPattern));
        }

        public IFetchedJob Dequeue(string[] queues, CancellationToken cancellationToken)
        {
            var queueIndex = 0;

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var messageQueue = MemoryQueueBank.Get(_pathPattern, queues[queueIndex]);

                    if (queueIndex == queues.Length - 1)
                    {
                        messageQueue.TryTake(out MemoryQueueMessage message, SyncReceiveTimeout);
                        return new MemoryFetchedJob(message.Label);
                    }
                    else
                    {
                        messageQueue.TryTake(out MemoryQueueMessage message, ReceiveTimeout);
                        return new MemoryFetchedJob(message.Label);
                    }
                }
                catch// (Exception ex)
                {
                    // Receive timeout occurred, we should just switch to the next queue
                }
                finally
                {

                }

                queueIndex = (queueIndex + 1) % queues.Length;

                Thread.Sleep(1);

            } while (true);
        }

#if FEATURE_TRANSACTIONSCOPE
        public void Enqueue(System.Data.IDbConnection connection, string queue, string jobId)
        {
            var messageQueue = MemoryQueueBank.Get(_pathPattern, queue);
            var message = new MemoryQueueMessage { Label = jobId };

            messageQueue.Add(message);
        }
#else
        public void Enqueue(System.Data.Common.DbConnection connection, System.Data.Common.DbTransaction transaction, string queue, string jobId)
        {
            var messageQueue = MemoryQueueBank.Get(_pathPattern, queue);
            var message = new MemoryQueueMessage { Label = jobId };

            messageQueue.Add(message);
        }
#endif
    }
}
