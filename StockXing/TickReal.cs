using Stock;
using Stock.Model;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System;

namespace StockXing
{
    class TickReal : Feed<Tick, OneDay>
    {
        public override void request(OneDay request)
        {
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        target(new Tick[]
            //        {
            //            new Tick(){Price = 11 },
            //        });
            //        //target.Post(new Tick[]
            //        //{
            //        //new Tick(){ Price = 11 },
            //        //});
            //    }
            //});
        }

        public override void cancel()
        {
        }
    }
}
