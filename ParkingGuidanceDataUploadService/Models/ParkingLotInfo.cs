using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingGuidanceDataUploadService.Models
{
    public class ParkingLotInfo
    {
        public int ParkId { get; set; }
        public string CountCw { get; set; }
        public string StopCw { get; set; }
        public int PrepCw { get; set; }
    }
}
