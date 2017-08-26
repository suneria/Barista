using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XA_SESSIONLib;

namespace Stock
{
    public class Connection
    {
        public bool login()
        {
            StreamReader idPwReader = new StreamReader("account.txt");
            string id = idPwReader.ReadLine();
            string pw = idPwReader.ReadLine();
            string certPW = idPwReader.ReadLine();
            idPwReader.Close();
            _session.DisconnectServer();
            _session._IXASessionEvents_Event_Login += (code, msg) =>
            {
                LoggedIn();
            };
            if (!_session.ConnectServer("hts.ebestsec.co.kr", 20001))
                return false;
            _session.GetAccountList(0);
            return _session.Login(id, pw, certPW, 0, true);
        }

        public delegate void LoginEventHandler();
        public event LoginEventHandler LoggedIn = delegate { };

        private XASessionClass _session = new XASessionClass();
    }
}
