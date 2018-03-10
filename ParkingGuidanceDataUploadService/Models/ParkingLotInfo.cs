using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingGuidanceDataUploadService.Models
{
    public class ParkingLotInfo
    {
        [JsonProperty(PropertyName = "parkId")]
        public int ParkId { get; set; }

        [JsonProperty(PropertyName = "total")]
        public string CountCw { get; set; }

        [JsonProperty(PropertyName = "Surplus")]
        public int PrepCw { get; set; }
    }
}
