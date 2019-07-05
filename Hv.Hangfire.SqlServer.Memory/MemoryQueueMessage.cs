
namespace Hv.Hangfire.SqlServer.Memory
{
    public class MemoryQueueMessage
    {
        public string Label { get; set; }
        public bool IsTaking { get; set; }
    }
}
