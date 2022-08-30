using AdminBlog.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); 

builder.Services.AddDbContext<BlogContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("MyBlogCore")));
builder.Services.AddSession();
builder.Services.Configure<RazorViewEngineOptions>(o=>{
    o.ViewLocationFormats.Clear();
    
    o.ViewLocationFormats.Add("/Views/Admin/{1}/{0}"+RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Views/Admin/Shared/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Views/User/{1}/{0}"+RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Views/User/Shared/{0}" + RazorViewEngine.ViewExtension);

});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
            Path.Combine(@"C:\MyBlog\MyBlog\wwwroot", "img")),
            RequestPath = "/img"
        });

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute( 
    name: "default",
    pattern: "{controller=UserHome}/{action=Index}/{id?}");

app.Run();
