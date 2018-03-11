using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ParkingGuidanceDataUploadService.Helper;
using ParkingGuidanceDataUploadService.Interface;
using ParkingGuidanceDataUploadService.Models;

namespace ParkingGuidanceDataUploadService
{
    public class AppService : IAppService
    {
        private readonly IDbConnection _db;

        public AppService(IDbConnection db)
        {
            _db = db;
        }

        public async Task RunAsync(CancellationToken token)
        {
            var millisecondsFrequency = int.TryParse(ConfigurationManager.AppSettings["MillisecondsFrequency"], out var milliseconds) ? milliseconds : 10000;

            while (true)
            {
                try
                {
                    var parkingLotInfo = await _db.QuerySingleOrDefaultAsync<ParkingLotInfo>("SELECT CONVERT (VARCHAR(20), 3401030036) as [ParkId],[CountCw],[StopCw],[PrepCw] FROM [dbo].[Tc_ParkingLotInfo] WHERE ParkingLotName = 'B2'");
                    Console.WriteLine($"实参: {JsonConvert.SerializeObject(parkingLotInfo)}");

                    const string url = "http://park.hfcsbc.cn:8080/parkScreenPMS/ReceiveParkNum.action?parkId=3401030036&total=1192&Surplus=800";
                    var client = new HttpClient();
                    var result = await client.GetStringAsync(url);

                    var testInfo = await _db.QuerySingleAsync<ParkingLotInfo>("SELECT CONVERT (VARCHAR(20), 3401030036) as [ParkId],1192 as [CountCw],800 as[PrepCw] FROM [dbo].[Tc_ParkingLotInfo] WHERE ParkingLotName = 'B2'");
                    Console.WriteLine($"入参: {JsonConvert.SerializeObject(testInfo)}{Environment.NewLine}出参: {result}{Environment.NewLine}操作时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    Logger.Info($"请求：   {url}{Environment.NewLine}响应：   {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Logger.Error(ex);
                }
                finally
                {
                    if (token.WaitHandle.WaitOne(millisecondsFrequency))
                    {
                        throw new OperationCanceledException(token);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
