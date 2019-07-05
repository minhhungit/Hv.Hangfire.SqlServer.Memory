using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Hv.Hangfire.SqlServer.Memory
{
    internal interface IMemoryTransaction : IDisposable
    {
        MemoryQueueMessage Receive(TimeSpan timeout);

        void Commit();
        void Abort();
    }

    internal class MemoryTransaction : IMemoryTransaction
    {
        private string MessageKey = string.Empty;
        private ConcurrentDictionary<string, MemoryQueueMessage> _queue;

        public MemoryTransaction(ConcurrentDictionary<string, MemoryQueueMessage> queue)
        {
            _queue = queue;
        }

        public MemoryQueueMessage Receive(TimeSpan timeout)
        {
            var item = _queue.FirstOrDefault(x => x.Value.IsTaking == false);
            var msg = item.Value;

            if (msg != null)
            {
                msg.IsTaking = true;
                MessageKey = item.Key;
            }

            return msg;
        }

        public void Commit()
        {
            // remove item out of queue
            while (true)
            {
                if (_queue.Count > 0 && _queue.ContainsKey(MessageKey))
                {
                    if (_queue.TryRemove(MessageKey, out _))
                    {
                        break;
                    }
                }

                System.Threading.Thread.Sleep(1);
            }
        }

        public void Abort()
        {
            if (_queue.ContainsKey(MessageKey))
            {
                _queue[MessageKey].IsTaking = false;
            }
        }

        public void Dispose()
        {
        }        
    }
}
