
using ApiUnitins.Servise;

namespace ApiUnitins
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //define o socketServer como um serviço singleton
            builder.Services.AddSingleton<SocketServer>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // // Defina os IPs permitidos aqui
            // var allowedIPs = new HashSet<string> { "192.168.1.1", "192.168.1.2" };
            // var allowedHosts = new HashSet<string> { "example1.com", "example2.com" };

            // app.Use(async (context, next) =>
            // {   
            //     var host = context.Request.Host.Host;
            //     var remoteIP = context.Connection.RemoteIpAddress?.ToString();
            //     if (allowedIPs.Contains(remoteIP)|| allowedHosts.Contains(host))
            //     {
            //         Console.WriteLine($"Acesso permitido para o IP: {remoteIP}");
            //         await next.Invoke();
            //     }
            //     else
            //     {
            //         Console.WriteLine($"Acesso negado para o IP: {remoteIP}");
            //         context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //         return;
            //     }
            // });

            // SocketServer e instanciado aqui 
            var socketServer = app.Services.GetRequiredService<SocketServer>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}