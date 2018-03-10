using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingGuidanceDataUploadService.Interface
{
    public interface IAppService
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
