using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Debug);
    x.AddDebug();

    var path = Directory.GetCurrentDirectory();
    x.AddFile($"{path}\\Logs\\Log1.txt");
});

// Add services to the container.
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();

//Api 
builder.Services.AddHttpClient();

builder.Services.ContainerDependencies();

//mapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.CustomerValidator();

builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetDestinationByIDQueryHandler>();
builder.Services.AddScoped<RemoveDestinationCommandHandler>();
builder.Services.AddScoped<UpdateDestinationCommandHandler>();
builder.Services.AddScoped<CreateDestinationCommandHandler>();

builder.Services.AddControllersWithViews().AddFluentValidation();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();

//ILoggerFactory loggerFactory;
//var path = Directory.GetCurrentDirectory();
//loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");

//var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder.AddFile($"{Directory.GetCurrentDirectory()}\\Logs\\Log1.txt");
//});

//builder.Services.AddSingleton(loggerFactory);
//builder.Services.AddLogging();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();



//Authentication,bir kullan�c�n�n herhangi bir kayna�a eri�imde kimli�inin do�rulanmas� i�lemidir. 
//Kullan�c�ya Kimsin sorusu sorulur? Bu sorunun cevab� genellikle kullan�c�n�n kullan�c�  ad� ve  �ifre �eklinde cevap vermesiyle yan�tlan�r.
//Authentication, authorization'dan �nce gelmektedir.
//Authorization ise, kimli�i do�rulanan kullan�c�n�n eri�mek istedi�i kaynak �zerindeki yetkilerini tan�mlar.
//Peki authentication i�lemi yap�lmadan authorization i�lemi yap�lamaz m�?

//Kimsin sorusunun sorulmamas� demek herhangi birisi anlam�na gelir. 
//Dolay�s� ile kimli�i do�rulanmayan yani anonim kullan�c�lara izin verilece�i durumlarda bu i�lem ger�ekle�tirilir.

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();


