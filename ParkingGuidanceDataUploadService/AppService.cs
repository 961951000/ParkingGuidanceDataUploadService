using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                    var query = await _db.QueryAsync<ParkingLotInfo>("SELECT [CountCw],[StopCw],[PrepCw] FROM [dbo].[Tc_ParkingLotInfo] WHERE ParkingLotName = 'B2'");
                    var info = query.FirstOrDefault();
                    var url = $"http://park.hfcsbc.cn:8080/parkScreenPMS/ReceiveParkNum.action?parkId={@"停车场ID"}&total={info?.CountCw}&Surplus={info?.PrepCw}";
                    Console.WriteLine($"Upload Url: {url}");
                    const string testUrl = "http://park.hfcsbc.cn:8080/parkScreenPMS/ReceiveParkNum.action?parkId=3401030036&total=1192&Surplus=800";
                    Console.WriteLine($"Test Upload: {testUrl}");
                    var client = new HttpClient();
                    var result = await client.GetStringAsync(testUrl);
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);
                    obj.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Console.WriteLine($"result: {JsonConvert.SerializeObject(obj)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
