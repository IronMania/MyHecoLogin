using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coIt.MyHeco.Login.Data.ComWork;
using coIt.MyHeco.Login.Data.MyHeco;
using coIT.MyHeco.Login.Domain;
using coIT.MyHeco.Login.Domain.Services;
using coT.MyHeco.Login.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace coT.MyHeco.Login.Web
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
            services.AddTransient<IComWorkRepository, ComWorkRepositoryDummy>();
            services.AddTransient<IMyHecoRepository, MyHecoRepositoryDummy>();
            services.AddTransient<IBenutzerService, BenutzerService>();
            services.AddTransient<SirenBenutzerMessageCreater>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();
        }
    }
}