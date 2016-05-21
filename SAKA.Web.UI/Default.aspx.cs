using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAKA.Service.Contract;
using System.ServiceModel;

namespace SAKA.Web.UI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var address = new EndpointAddress("http://localhost:59470/KPIService.svc");
            var binding = new BasicHttpBinding();
            var channel = ChannelFactory<IKPIService>.CreateChannel(binding, address);

            var count = channel.count();
            var sum = channel.sum();

           Response.Write(count + "  " + sum);

           var kpi = channel.AddKpi();

           Response.Write(" " + kpi);
            
        }
    }
}