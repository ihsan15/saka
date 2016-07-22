﻿using SAKA.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAKA.Bussiness;
using SAKA.DTO;

namespace SAKA.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "KPIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select KPIService.svc or KPIService.svc.cs at the Solution Explorer and start debugging.
    public class KPIService : IKPIService
    {
        //public int count()
        //{
        //    return Kpi.count();
        //}

        //public string sum()
        //{
        //    return Kpi.sum();
        //}
        //public string AddKpi()
        //{
        //    return Kpi.ADDKpi();
        //}

        public ScoreCard[] GetScoreCard()
        {
            return Kpi.GetScoreCard();
        }

        public Gauge[] GetGauge()
        {
            return Kpi.GetGauge();
        }
    }
}
