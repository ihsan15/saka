using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAKA.DTO;

namespace SAKA.Bussiness
{
    public class Kpi
    {
        //public static string ADDKpi()
        //{
        //    using (linqtosqlDataContext dc=new linqtosqlDataContext())
        //    {
        //        var Kpi = new KPI();
        //        Kpi.ID = Guid.NewGuid();
        //        Kpi.Name = "deneme";
        //        Kpi.Target = 30;
        //        Kpi.Thresholder = 3;
        //        Kpi.Thresholder_Type = true;
        //        Kpi.Period = 'Y';
        //        Kpi.Unit = "$";
        //        Kpi.Direction = true;
        //        Kpi.Creation_Date = DateTime.Now;

        //        dc.KPIs.InsertOnSubmit(Kpi);
        //        dc.SubmitChanges();

        //        return "Ekledik. Bak istersen";
        //    }            
        //}

        //public ScoreCard[] GetScoreCard()
        //{
        //    var ScList = new List<ScoreCard>();
        //    using (linqtosqlDataContext dc = new linqtosqlDataContext())
        //    {
        //                var list = dc.KPIs.Where(x => x.Name == "MusteriAdet").Select(c => new
        //                {
        //                    c.ID,
        //                    c.Target,
        //                    c.Thresholder,
        //                    c.Thresholder_Type
        //                }).ToList();
        //                foreach (var item in list)
        //                {
        //                    var sc = new ScoreCard();
        //                    sc.NAME = item.Name;
                            
                                                   
        //                   }
        //    return ScList;
        //    }
        //}
        //public static List<ScoreCard> GetKpi()
        //{
        //    using (linqtosqlDataContext dc = new linqtosqlDataContext())
        //    {
        //        var list = dc.KPIs.Where(x => x.Name == "Ciro").Select(c => new
        //        {
        //            c.Name,
        //            c.Target,
        //            c.Thresholder
        //        }).ToList();
        //        foreach (var item in list)
        //        {
        //            var sc = new ScoreCard();
        //            sc.NAME=item.ID
        //        }
        //        return list;
        //    }            
        //}

        public static int count()
        {
            return 3;
        }

        
        public static string sum()
        {
            return "maymun";
        }
    }
}
