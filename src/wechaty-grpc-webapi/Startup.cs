using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Wechaty.GateWay;
using Wechaty.Grpc.PuppetService.Contact;
using Wechaty.Grpc.PuppetService.FriendShip;
using Wechaty.Grpc.PuppetService.Message;
using Wechaty.Grpc.PuppetService.Room;
using Wechaty.Grpc.PuppetService.Tag;

namespace wechaty_grpc_webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGateWayService, GateWayService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFriendShipService, FriendShipService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ITagService, TagService>();

            services.AddSignalR();
            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {
                        Title = "wechaty grpc webapi",
                        Version = "v1"
                    });

                // 获取xml注释文件的目录
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "wechaty-grpc-webapi.xml");
                c.IncludeXmlComments(xmlPath, true);

                // 获取xml注释文件的目录
                var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "wechaty-grpc-webapi.xml");
                c.IncludeXmlComments(xmlPathModel, false);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "wechaty webapi v1"));
            }

          

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<EventStreamHub>("/event");
            });
        }
    }
}
