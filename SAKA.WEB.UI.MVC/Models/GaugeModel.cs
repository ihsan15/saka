using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAKA.WEB.UI.MVC.Models
{
    public class GaugeModel
    {
        public string Name { get; set; }
        public int LeftSide { get; set; }
        public int middle { get; set; }
        public int RightSide { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }

        public string LeftColor { get; set; }
        public string RightColor { get; set; }
    }
}