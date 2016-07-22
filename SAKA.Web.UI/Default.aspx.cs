using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAKA.Service.Contract;
using System.ServiceModel;
using SAKA.DTO;

namespace SAKA.Web.UI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var address = new EndpointAddress("http://localhost:59470/KPIService.svc");
            var binding = new BasicHttpBinding();
            var channel = ChannelFactory<IKPIService>.CreateChannel(binding, address);

            // var count = channel.count();
            // var sum = channel.sum();

            //Response.Write(count + "  " + sum);

            //var kpi = channel.AddKpi();

            //Response.Write(" " + kpi);

            var list = channel.GetScoreCard();


            //Response.Write(list); 

            this.SCORECARD.DataSource = list.Select(c => new
            {
                Name = c.NAME,
                Value = c.VALUE + " " + c.UNIT,
                Period=TarihFormat(c.DATE,c.PERIOD),
                Statu=GetImage(c.STATU)
            });

            SCORECARD.DataBind();

            var genislik = 400;
            var yukseklik = 30;
            var genislikOrani = 0.2;

            foreach (var x in channel.GetGauge())
            {
                var max = Math.Max(x.TARGET_MAX, x.VALUE) * ((decimal)(1 + genislikOrani));
                var genislikleft = Math.Round(genislik * x.TARGET_MIN / max, 0);
                var genisliknotr = Math.Round(genislik * (x.TARGET_MAX - x.TARGET_MIN) / max, 0);
                var genislikright = genislik - genislikleft - genisliknotr;
                var genislikvalue = Math.Round(genislik * x.VALUE / max, 0);

                Table table = new Table();
                table.Style.Add(HtmlTextWriterStyle.MarginBottom, "20px");

                TableRow row1 = new TableRow();
                TableCell cell11 = new TableCell();
                row1.Width = Unit.Pixel(genislik);
                cell11.Style.Add(HtmlTextWriterStyle.PaddingLeft, genislikvalue + "px");
                cell11.Text = x.VALUE + " " + x.UNIT + "<br/><img src=../triangel.png";
                row1.Cells.Add(cell11);
                table.Rows.Add(row1);


                TableRow row2 = new TableRow();
                TableCell cell21 = new TableCell();
                row2.Width = Unit.Pixel(genislik);
                cell21.Width = Unit.Pixel((int)genislikleft);
                cell21.Height = Unit.Pixel(yukseklik);
                cell21.BackColor = x.DIRECTION == Direction.positive ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                
                TableCell cell22 = new TableCell();
                cell22.Width = Unit.Pixel((int)genisliknotr);
                //cell22.Height = Unit.Pixel(yukseklik);
                cell22.BackColor = System.Drawing.Color.Yellow;

                TableCell cell23= new TableCell();
                cell23.Width = Unit.Pixel((int)genislikright);
                //cell23.Height = Unit.Pixel(yukseklik);
                cell23.BackColor = x.DIRECTION == Direction.negative ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                row2.Cells.Add(cell21);
                row2.Cells.Add(cell22);
                row2.Cells.Add(cell23);
                table.Rows.Add(row2);

                TableRow row3 = new TableRow();
                TableCell cell31 = new TableCell();
                cell31.HorizontalAlign = HorizontalAlign.Center;
                cell31.Text = x.NAME;
                row1.Cells.Add(cell31);
                table.Rows.Add(row3);

                Tablo.Controls.Add(table);
            }

        }
        private string TarihFormat(DateTime Date, Period period)
        {

            if (period == Period.Year)
                return Date.Year.ToString();
            if (period == Period.Month)
                return Date.Month + " " + Date.Year;
            return Date.ToShortDateString();
        }

        private string GetImage(Statu statu)
        {
            if (statu==Statu.Bad)
            {
                return "~/sekil/kpidefault-2.gif";
            }
            if (statu==Statu.notr)
            {
                return "~/sekil/kpidefault-1.gif";
            }
            return "~/sekil/kpidefault-0.gif";
        }
    }
}