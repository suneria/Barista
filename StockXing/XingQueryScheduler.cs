using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockXing
{
    class XingQueryScheduler
    {
        private XingQueryScheduler()
        {
            _pendingQueries = new List<XingQueryRequest>();
            _removingQueries = new List<XingQueryRequest>();
            _receive = new ManualResetEvent(true);
            _request = new ManualResetEvent(true);
            Task.Run(() => run());
        }

        public static XingQueryScheduler This { get; } = new XingQueryScheduler();

        public void add(XingQueryRequest query)
        {
            lock (_lock)
            {
                _pendingQueries.Add(query);
                _request.Set();
            }
        }

        public void remove(XingQueryRequest query)
        {
            lock (_lock)
            {
                _removingQueries.Add(query);
            }
        }

        public void receivedOne(XingQueryRequest query)
        {
            lock (_lock)
            {
                _receive.Set();
            }
        }

        public void receivedAll(XingQueryRequest query)
        {
            lock (_lock)
            {
                _removingQueries.Add(query);
                _receive.Set();
            }
        }

        public void run()
        {
            while (true)
            {
                int sleepTime = 0;
                lock (_lock)
                {
                    if (_removingQueries.Count > 0)
                    {
                        foreach (XingQueryRequest removingQuery in _removingQueries)
                        {
                            int queryIndex = _pendingQueries.FindIndex(query => query == removingQuery);
                            if (queryIndex == 0)
                            {
                                sleepTime = 1000;
                            }
                            _pendingQueries.RemoveAt(queryIndex);
                            removingQuery.invalidateXingQuery();
                        }
                        _removingQueries.Clear();
                    }
                    else if (_pendingQueries.Count > 0)
                    {
                        _pendingQueries[0].requestXingQuery();
                        _receive.Reset();
                        sleepTime = 300;
                    }
                    else
                    {
                        _request.Reset();
                    }
                }
                Thread.Sleep(sleepTime);
                _receive.WaitOne();
                _request.WaitOne();
            }
        }

        private List<XingQueryRequest> _pendingQueries;
        private List<XingQueryRequest> _removingQueries;
        private object _lock = new object();
        private ManualResetEvent _receive;
        private ManualResetEvent _request;
    }
}
