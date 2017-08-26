using Stock;
using XA_DATASETLib;

namespace StockXing
{
    /// <summary>
    /// Xing Query 기반 클래스
    /// </summary>
    abstract class XingQuery<TResult, TRequest> : Feed<TResult, TRequest>
    {
        #region 생성자
        public XingQuery(string resFilePath)
        {
            _xingQuery = new XAQueryClass();
            _xingQuery.ResFileName = resFilePath;
            _xingQuery.ReceiveData += receiveCallback;
            _next = false;
            _xingQueryRequest = new XingQueryRequest();
            _xingQueryRequest.RequestableEventHandler += requestXingQuery;
            _xingQueryRequest.InvalidEventHandler += invalidateXingQuery;
        }
        #endregion

        #region Query 요청/취소
        public override void request(TRequest request)
        {
            this.storeRequest(request);
            XingQueryScheduler.This.add(_xingQueryRequest);
        }

        public override void cancel()
        {
            XingQueryScheduler.This.remove(_xingQueryRequest);
        }

        public void receivedOne()
        {
            XingQueryScheduler.This.receivedOne(_xingQueryRequest);
        }

        public void receivedAll()
        {
            XingQueryScheduler.This.receivedAll(_xingQueryRequest);
        }

        public void requestXingQuery()
        {
            lock (_nextLock)
            {
                int result = _xingQuery.Request(_next); // 0 이상이면 성공, 0 미만이면 실패
                _next = true;
            }
        }

        public void invalidateXingQuery()
        {
            _xingQuery = null;
        }
        #endregion

        #region 구현해야 하는 콜백
        protected abstract void storeRequest(TRequest request);
        protected abstract void receiveCallback(string szTrCode);
        #endregion

        #region Xing Getter/Setter 래퍼
        protected void setInBlock(InBlock inBlock)
        {
            foreach (InField inField in inBlock.InFields)
            {
                _xingQuery.SetFieldData(inBlock.Name, inField.Name, 0, inField.Value);
            }
        }

        protected int getBlockCount(string blockName)
        {
            return _xingQuery.GetBlockCount(blockName);
        }

        protected string getFieldData(string blockName, string fieldName, int index = 0)
        {
            return _xingQuery.GetFieldData(blockName, fieldName, index);
        }

        protected void setFieldData(string blockName, string fieldName, string value, int index = 0)
        {
            _xingQuery.SetFieldData(blockName, fieldName, index, value);
        }

        protected bool canRequestNext()
        {
            return _xingQuery.IsNext;
        }
        #endregion

        #region Private 변수
        private XAQueryClass _xingQuery;
        private bool _next;
        private object _nextLock = new object();
        private XingQueryRequest _xingQueryRequest;
        #endregion
    }
}
