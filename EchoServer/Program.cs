using EchoServer;
using Microsoft.AspNetCore.HttpOverrides;





var builder = WebApplication.CreateBuilder(args);

ProgramHelper.ReadEnviroments();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

const string redirectPath = "/redirect";
void CheckSameSite(HttpContext httpContext, CookieOptions options)
{
    if (options.SameSite == SameSiteMode.None)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        // TODO: Use your User Agent library of choice here. 
        //if (/* UserAgent doesn’t support new behavior */)
        {
            options.SameSite = SameSiteMode.Unspecified;
        }
    }
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var fordwardedHeaderOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
};
fordwardedHeaderOptions.KnownNetworks.Clear();
fordwardedHeaderOptions.KnownProxies.Clear();

app.UseForwardedHeaders(fordwardedHeaderOptions);

app.UseCookiePolicy();

//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyCatchAll",
        pattern: "{*key}",
        defaults: new { controller = "Default", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
