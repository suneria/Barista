using Common.Extensions;
using Stock;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    class TickQuery : XingQuery<Tick, Since>
    {
        public TickQuery() : base("t1310.res")
        {
        }

        protected override void storeRequest(Since request)
        {
            Since = request;
            setInBlock(new InBlock
            {
                Name = "t1310InBlock",
                InFields = new InField[]
                {
                    new InField { Name = "daygb", Value = "0" },
                    new InField { Name = "timegb", Value = "1" },
                    new InField { Name = "shcode", Value = request.Stock.Code },
                }
            });
            OutFields = new OutField[]
            {
                new OutField { Name = "chetime" },
                new OutField { Name = "price" },
                new OutField { Name = "cvolume" },
                new OutField { Name = "revolume" },
                new OutField { Name = "volume" },
                new OutField { Name = "diff" }
            };
        }

        protected override void receiveCallback(string szTrCode)
        {
            for (int currentBlockIndex = 0; currentBlockIndex < getBlockCount("t1310OutBlock1"); currentBlockIndex++)
            {
                RawData rawData = new RawData();
                int valueIndex = 0;
                foreach (OutField outField in OutFields)
                {
                    rawData.Values[valueIndex++] = getFieldData("t1310OutBlock1", outField.Name, currentBlockIndex);
                }
                _rawDatas.Add(rawData);
            }
            string receivedMinimumTimeInStr = getFieldData("t1310OutBlock", "cts_time");
            DateTime receivedMinimumTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(receivedMinimumTimeInStr))
            {
                receivedMinimumTime = receivedMinimumTimeInStr.ToDateTime("HHmmssffff");
            }
            if (canRequestNext() && needMoreData(receivedMinimumTime.ChangeDate(Since.Base)))
            {
                setFieldData("t1310InBlock", "cts_time", receivedMinimumTimeInStr);
                receivedOne();
            }
            else
            {
                makeFilledOrderAndSend();
                receivedAll();
            }
        }

        private bool needMoreData(DateTime receivedMinimumTime)
        {
            return Since.After < receivedMinimumTime; // 현재 시간이 큰 경우
        }

        private void makeFilledOrderAndSend()
        {
            // 데이터 Reverse
            _rawDatas.Reverse();
            // RawData => FilledOrder
            Tick[] unsignedFilledOrder = _rawDatas.Select(rawData => new Tick
            {
                Name = Since.Stock.Name,
                Quantity = int.Parse(rawData.Values[2]),
                Time = rawData.Values[0].ToDateTime("HHmmss").ChangeDate(Since.Base),
                Price = int.Parse(rawData.Values[1]),
                Amount = long.Parse(rawData.Values[2]) * long.Parse(rawData.Values[1]),
                Volume = long.Parse(rawData.Values[4]),
                Rate = float.Parse(rawData.Values[5])
            }).ToArray();
            // Net Volume, 이 값으로 +,-를 결정
            int[] netVolumes = _rawDatas.Select(rawData => int.Parse(rawData.Values[3])).ToArray();
            // Volume값을 매수면 +, 매도면 -로 바꿈
            IEnumerable<Tick> signedFilledOrder = convertSign(unsignedFilledOrder, netVolumes);
            // 값 전송
            foreach (Tick tick in signedFilledOrder)
            {
                Buffer.Post(tick);
            }
            Buffer.Complete();
        }

        private Tick[] convertSign(Tick[] datas, int[] netVolumes)
        {
            int previousNetVolume = 0;
            for (int i = 0; i < datas.Count(); i++)
            {
                int currentNetVolume = netVolumes[i];
                if (currentNetVolume < previousNetVolume)
                {
                    datas[i].Quantity = -datas[i].Quantity;
                    datas[i].Amount = -datas[i].Amount;
                }
                previousNetVolume = currentNetVolume;
            }
            return datas;
        }

        private Since Since { get; set; }

        private static OutField[] OutFields { get; set; }

        private List<RawData> _rawDatas = new List<RawData>();
    }
}
