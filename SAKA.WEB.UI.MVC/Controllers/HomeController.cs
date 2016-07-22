using SAKA.DTO;
using SAKA.Service.Contract;
using SAKA.WEB.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace SAKA.WEB.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var address = new EndpointAddress("http://localhost:59470/KPIService.svc");
            var binding = new BasicHttpBinding();
            var channel = ChannelFactory<IKPIService>.CreateChannel(binding, address);

            //ViewBag.ScoreList= channel.GetScoreCard();

            var ScoreList = channel.GetScoreCard().Select(c => new ScoreModel
            {
                Name = c.NAME,
                Date = TarihFormat(c.DATE, c.PERIOD),
                Value = c.VALUE + " " + c.UNIT,
                Statu = GetImage(c.STATU)
            }).ToList();

            ViewBag.GaugeList = Gauge();

            return View(ScoreList);
        }

        public List<GaugeModel> Gauge()
        {
            var address = new EndpointAddress("http://localhost:59470/KPIService.svc");
            var binding = new BasicHttpBinding();
            var channel = ChannelFactory<IKPIService>.CreateChannel(binding, address);

            var GaugeList = new List<GaugeModel>();

            var genislik = 400;
            var yukseklik = 30;
            var genislikOrani = 0.2;

            foreach (var x in channel.GetGauge())
            {
                var g = new GaugeModel();
                g.Max = (int)Math.Round(Math.Max(x.TARGET_MAX, x.VALUE) * ((decimal)(1 + genislikOrani)));
                g.LeftSide = (int)Math.Round(genislik * x.TARGET_MIN / g.Max, 0);
                g.middle = (int)Math.Round(genislik * (x.TARGET_MAX - x.TARGET_MIN) / g.Max, 0);
                g.RightSide = genislik - g.LeftSide - g.middle;
                g.Value = (int)Math.Round(genislik * x.VALUE / g.Max, 0);

                g.LeftColor = x.DIRECTION == Direction.positive ? "red" : "green";
                g.RightColor = g.LeftColor == "red" ? "green" : "red";
                g.Name = x.NAME;
                GaugeList.Add(g);
            }

            return GaugeList;
        }
        private string TarihFormat(DateTime Date, Period period)
        {

            if (period == Period.Year)
                return Date.Year.ToString();
            if (period == Period.Month)
                return Date.Month + "." + Date.Year;
            return Date.ToShortDateString();
        }

        private string GetImage(Statu statu)
        {
            if (statu == Statu.Bad)
            {
                return "kpidefault-2.gif";
            }
            if (statu == Statu.notr)
            {
                return "kpidefault-1.gif";
            }
            return "kpidefault-0.gif";
        }
    }
}