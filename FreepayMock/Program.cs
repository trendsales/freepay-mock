using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Nancy.Bootstrapper;

namespace FreepayMock
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any() && args[0] == "reinstall")
            {
                FreepayMockContext context = new FreepayMockContext();
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }

                context.Database.CreateIfNotExists();

                var sql = @"
SET IDENTITY_INSERT Merchants ON; 
INSERT INTO Merchants(MerchantId, Password) values(54321, 'passsword');
SET IDENTITY_INSERT Merchants OFF;";

                context.Database.ExecuteSqlCommand(sql);
                context.SaveChanges();
            }

            var port = 2020;
            using (WebApp.Start<Startup>($"http://+:{port}"))
            {
                Console.ReadLine();
            }
        }
    }
}
