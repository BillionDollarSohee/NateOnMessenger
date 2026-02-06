
namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger 설정 (API 문서화 도구)
            builder.Services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "NateOn Messenger API",
                    Version = "v1",
                    Description = "API documentation for NateOn Messenger server.",
                });
            });

            // CORS 설정
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            // SingnalR 설정 (실시간 통신용)
            builder.Services.AddSignalR();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NateOn Messenger API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            // SingnalR 허브 매핑
            app.MapControllers();
            app.MapHub<server.Hubs.ChatHub>("/chatHub");

            app.Run();
        }
    }
}
