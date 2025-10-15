using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entry_Pass_Generator_CIAL
{
    public class PassModel
    {
        public string LaborID{ get; set; }
        public string ContractorName { get; set; }
        public string GateAccess { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public string ExitDate { get; set; }
        public string CheckoutTime { get; set; }
        public string FullName { get; set; }
        public string DOB { get; set; }
        public string Area { get; set; }
        public string LabourImageBase64 { get; set; }
    }

}
