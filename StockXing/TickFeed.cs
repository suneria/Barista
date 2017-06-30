using Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    public class TickFeed : Feed<Tick>
    {
        public void request(RequestForm requestForm, ITargetBlock<Tick> target)
        {
            Need need = whatNeeded();
            switch (need)
            {
                case Need.Past:
                    break;
                case Need.Present:
                    break;
                case Need.Future:
                    break;
            }
            //var realLayerAction = new ActionBlock<int[]>(data =>
            //{
            //    foreach (int num in data)
            //    {
            //        Console.WriteLine(num);
            //    }
            //});
            //BufferBlock<int[]> realLayerBuffer = new BufferBlock<int[]>();
            //Task.Run(() =>
            //{
            //    int start = 11;
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        realLayerBuffer.Post(new int[] { start++ });
            //    }
            //});
            ////
            //var queryLayerAction = new ActionBlock<Tuple<int[], int[]>>(data =>
            //{
            //    realLayerAction.Post(data.Item1);
            //    realLayerAction.Post(data.Item2);
            //    realLayerBuffer.LinkTo(realLayerAction);
            //});
            ////
            //JoinBlock<int[], int[]> queryLayerJoin = new JoinBlock<int[], int[]>();
            //queryLayerJoin.LinkTo(queryLayerAction);
            //Task.Run(() =>
            //{
            //    Thread.Sleep(6000);
            //    queryLayerJoin.Target2.Post(new int[] { 6, 7, 8, 9, 10 });
            //    Console.WriteLine("Queried");
            //});
            //Task.Run(() =>
            //{
            //    Thread.Sleep(3000);
            //    queryLayerJoin.Target1.Post(new int[] { 1, 2, 3, 4, 5 });
            //    Console.WriteLine("DB Loaded");
            //});
            //queryLayerAction.Completion.Wait();


            ISourceBlock<Tick> t = new BufferBlock<Tick>();
            t.LinkTo(target);
            throw new NotImplementedException();
        }

        private Need whatNeeded()
        {
            return Need.Future;
        }
    }
}
