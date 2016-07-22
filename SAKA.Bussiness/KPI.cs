using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAKA.DTO;
using Kardelen.Data;

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

        public static ScoreCard[] GetScoreCard()
        {
            var ScList = new List<ScoreCard>();


            using (var dc = new linqtosqlDataContext())
            {

                //var kpilist = (from kpiitem in dc.KPIs
                //where kpiitem.KPI_Values.Any()
                //select kpiitem).ToList();

                var kpilist = dc.KPIs.Where(kpi => kpi.KPI_Values.Any()).Select(kpi => new
                {
                    kpi.ID,
                    kpi.Name,
                    kpi.Target,
                    kpi.Unit,
                    kpi.Thresholder,
                    kpi.Thresholder_Type,
                    kpi.Period,
                    kpi.Direction
                }).ToList();

                foreach (var kpiitem in kpilist)
                {
                    var kpivalue = dc.KPI_Values
                        .Where(value => value.KPI_ID == kpiitem.ID)
                        .OrderByDescending(value => value.Date)
                        .Select(value => new
                    {
                        value.Value,
                        value.Date
                    }).First();

                    var scorecaritem = new ScoreCard();
                    scorecaritem.NAME = kpiitem.Name;
                    scorecaritem.UNIT = kpiitem.Unit;
                    scorecaritem.VALUE = kpivalue.Value;
                    scorecaritem.DATE=kpivalue.Date;
                    scorecaritem.PERIOD=(Period)kpiitem.Period;
                    scorecaritem.STATU = CalculateStatu(kpivalue.Value, kpiitem.Thresholder, kpiitem.Thresholder_Type, kpiitem.Target, kpiitem.Direction);

                    ScList.Add(scorecaritem);
                }
                //return ScList;
                return ScList.ToArray();

            }

            
            
        }

        public static Gauge[] GetGauge()
        {
            using (var dc = new linqtosqlDataContext())
            {
                var listKPI = dc.KPIs.Where(c => c.KPI_Values.Any()).Select(c => new
                {
                    c.Name,
                    c.ID,
                    c.Thresholder,
                    c.Thresholder_Type,
                    c.Direction,
                    c.Target,
                    c.Unit
                });

                var listGauge = new List<Gauge>();
                foreach (var dto in listKPI)
                {
                    var value = dc.KPI_Values.Where(c => c.KPI_ID == dto.ID).Select(c => new { c.Value, c.Date }).OrderByDescending(c => c.Date).First();
                    var item = new Gauge();
                    var deflection = dto.Thresholder_Type ? dto.Thresholder : dto.Target * dto.Thresholder / 100;

                    item.NAME = dto.Name;
                    item.UNIT = dto.Unit;
                    item.VALUE = value.Value;
                    item.DIRECTION = dto.Direction ? Direction.positive : Direction.negative;
                    item.TARGET_MAX = dto.Target + deflection;
                    item.TARGET_MIN = dto.Target - deflection;

                    listGauge.Add(item);

                }
                return listGauge.ToArray();
            }
        }

        public static void CalculateKpiValue()
        {
            using (var dc = new linqtosqlDataContext())
            {
                var kpis = dc.KPIs.Where(c => c.CODE != null).Select(c => new
                {
                    c.ID,
                    c.CONNSTRING,
                    c.CODE,
                    c.PERIOD,
                    c.TARGET,
                    c.THRESHOLD,
                    c.THRESHOLD_TYPE
                });

                foreach (var kpi in kpis)
                {
                    var sql = new SQL(kpi.CONNSTRING);
                    var SpNAme = "SP_" + kpi.CODE;
                    var period = (Period)kpi.PERIOD;
                    var date = default(DateTime);
                    var kpiValue = default(decimal);

                    if (period == Period.Year)
                    {
                        date = new DateTime(DateTime.Now.Year, 1, 1);
                        kpiValue = Convert.ToDecimal(sql.SelectObject(SpNAme, date.Year));
                    }
                    else if (period == Period.Month)
                    {
                        date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        kpiValue = Convert.ToDecimal(sql.SelectObject(SpNAme, date.Year, date.Month));
                    }
                    else
                    {
                        date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        kpiValue = Convert.ToDecimal(sql.SelectObject(SpNAme, date.Year, date.Month, date.Day));
                    }

                    var itemValue = dc.KPI_Values.FirstOrDefault(c => c.KPI_ID == kpi.ID && c.Date == date);

                    if (itemValue == null)
                    {
                        itemValue = new KPI_Value();

                        itemValue.KPI_ID = kpi.ID;
                        itemValue.Date = date;

                        dc.KPI_Values.InsertOnSubmit(itemValue);
                    }

                    itemValue.Target = kpi.TARGET;
                    itemValue.Thresholder = kpi.THRESHOLD;
                    itemValue.Thresholder_Type = kpi.THRESHOLD_TYPE;
                    itemValue.Value = kpiValue;

                    dc.SubmitChanges();
                }
            }
        }

        private static Statu CalculateStatu(decimal value,decimal threshold, bool threshold_type, decimal target, bool direction)
        {
            //decimal deflection = 0;

            //if (threshold_type)
            //    deflection = threshold;
            //else
            //    deflection = target * threshold / 100;

            decimal deflection = threshold_type ? threshold : target * threshold / 100;

            if (value > target + deflection)
                return direction ? Statu.Good : Statu.Bad;
            
            if (value < target - deflection)
                return direction ? Statu.Bad : Statu.Good;
            
            return Statu.notr;
        }
        
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

        //public static ScoreCard[] GetScoreCard()
        //{
        //    var list = new List<KPI>();
        //    using (var dc = new linqtosqlDataContext())
        //    {
        //        list = dc.KPIs.ToList();
        //    }

        //    return null;
        //}
        //public static int count()
        //{
        //    return 3;
        //}


        //public static string sum()
        //{
        //    return "maymun";
        //}
    }
}
