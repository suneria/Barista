using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Stock
{
    public interface Feed<TResult, TRequest>
    {
        void request(TRequest request, ITargetBlock<IEnumerable<TResult>> target);
        void cancel();
    }
}
