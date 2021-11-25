using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OneCarProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var connection = new SqlConnection(
            //    "Data Source=DESKTOP-4RB7SO6\\TEW_SQLEXPRESS;Database=OneCarDb;Integrated Security=SSPI"

            //    ))
            //{
            //    connection.Open();
            //    Console.WriteLine("Connected successfully.");

            //    Console.WriteLine("Press any key to finish...");
            //    // Console.ReadKey(true);
            //}
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
