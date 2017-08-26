using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Stock
{
    public abstract class Feed<TResult, TRequest>
    {
        public abstract void request(TRequest request);

        public abstract void cancel();

        public ITargetBlock<TResult> Target { set => Buffer.LinkTo(value); }

        public Task Completion { get => Buffer.Completion; }

        protected BufferBlock<TResult> Buffer { get; } = new BufferBlock<TResult>();
    }
}
