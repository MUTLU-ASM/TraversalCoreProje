using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Container;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();
builder.Services.AddControllersWithViews();

builder.Services.ContainerDependencies();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();

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


