using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EX.Service
{
    public class VisitorExecutor
    {
        ServiceHost host;
        public event Action<string> statusChanged;

//        private string uri;
        public string status;
        public bool needToStor;

        public VisitorExecutor(string ipAdress)
        {
 //           uri = @"net.tcp://" + ipAdress + @":3121/Visitor";
        }

        public void Start()
        {
 //           host = new ServiceHost(typeof(VisitorContract), new Uri(uri));
 //           NetTcpBinding n = new NetTcpBinding();
  //          n.Security.Mode = SecurityMode.None;
  //          host.AddServiceEndpoint(typeof(IVisitorContract), n, uri);
            host = new ServiceHost(typeof(VisitorContract));
            host.Open();
            
            status ="listerning....";
            statusChanged(status);
        }


        public void Stop()
        {
            host.Close();
            status = "close"+"\n";
            statusChanged(status);
        }
    }
}
