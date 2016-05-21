using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SAKA.Service.Contract
{
    [ServiceContract]
    public interface IKPIService
    {
        [OperationContract]
        int count();

        [OperationContract]
        string sum();

        [OperationContract]
        string AddKpi();
    }
}
