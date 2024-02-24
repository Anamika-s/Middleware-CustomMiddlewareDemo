namespace MiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.Run((c) => throw new Exception("ERRRRRROR")); ;
            // It also is used to put all the dependencies 
            // Also uses middleware
            //app.Run((context) => context.Response.WriteAsync("Hello1"));
            //app.Run((context) => context.Response.WriteAsync("Hello2"));

            // Middleware used to read contents from wwwroot folder

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseMiddleware<Middleware>();
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("home.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            //app.UseFileServer();
          


            //app.Use(async (context, next) =>
            //{
            //    context.Response.WriteAsync("use1");
            //    context.Response.WriteAsync(builder.Configuration["Message"]);
            //    next();
            //}
            //    );
            //app.Use(async (context, next) =>
            //{
            //   await context.Response.WriteAsync("use2");
            //    next();
            //});
            app.Map("/user", context => context.Run(c => c.Response.WriteAsync("user")));

            app.Map("/user1", a =>
            {
                a.Map("/test1", c => c.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("test1");
                    next();
                }));
            });

            app.Map("/newbranch" ,a =>
            {
                a.Map("/branch1", brancha =>
                brancha.Run(c => c.Response.WriteAsync("Running from newbrnach/branch1 ")));
                a.Map("/branch2", branchb =>
                branchb.Run(c => c.Response.WriteAsync("Running from newbrnach/branch2 ")));

                a.Run(c => c.Response.WriteAsync("Running frm newbranch only"));


            });
            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}