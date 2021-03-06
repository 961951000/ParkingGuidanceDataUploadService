﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using ParkingGuidanceDataUploadService.Interface;
using SimpleInjector;

namespace ParkingGuidanceDataUploadService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // configure services
                var container = new Container();

                container.Register<IAppService, AppService>();
                container.RegisterSingleton<IDbConnection>(new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

                container.Verify();

                // resolve the dependency graph
                var appService = container.GetInstance<IAppService>();

                // run the application
                appService.RunAsync(new CancellationTokenSource().Token).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
