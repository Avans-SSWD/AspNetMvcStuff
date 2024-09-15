using MvcStuff.Core.Domain.Interfaces;
using MvcStuff.Middleware;
using MvcStuff.Repositories;

// builder pattern -->
var builder = WebApplication.CreateBuilder(args);



// Services toevoegen -->
builder.Services.AddDistributedMemoryCache();
// Eventueel configuraren met lambda expressie -->
builder.Services.AddSession(s => {
    s.Cookie.IsEssential = true;
    
});

builder.Services.AddControllersWithViews();

// dependency injection en middleware configureren -->
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
//
// ... eventuele andere services
//

builder.Services.AddTransient<PrintHelloMiddleware>();

// configuratie klaar, webapp instantie maken -->
var app = builder.Build();

// Eigen code in de request pipeline
// (volgorde is belangrijk) -->
// filmpje: https://www.jetbrains.com/dotnet/guide/tutorials/aspnet-basics/request-pipeline/

app.UseMiddleware<PrintHelloMiddleware>();

app.Use(async (context, next) => {
    //// in de request pipeline gaan zitten -->
    //context.Response.ContentType = "text/html";
    //await context.Response.WriteAsync("<h1>Middleware 1</h1>");
    await next.Invoke();

});



// custom endpoint voor get requests naar de root folder -->
app.MapGet("/yohoo", () => "Hello World!");
// ook: app.Map[Post|Put|Delete]

// route matched alles in de vorm van /<iets>/<anders>/<nogwat>
//app.MapGet("{segment1}/{segment2}/{segment3}", async context => {
//    await context.Response.WriteAsync("Request met path segments:\n");
//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response
//        .WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();


//app.UseRouting();



app.UseAuthorization();


// endpoint routing configureren -->

//// matched alles -->
//app.MapControllerRoute(
//    name: "matchAlles",
//    pattern: "{*url}",
//    defaults: new { controller = "Home", action = "Alles" }
//    );

// defaults naar home met actionmethod index maar is variabel.
// filmpje: https://www.jetbrains.com/dotnet/guide/tutorials/aspnet-basics/routing/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// parameter is hier verplicht -->
// ook: slugs: https://stackoverflow.com/questions/65860483/how-can-i-implement-slug-based-routing-in-asp-net-core
app.MapControllerRoute(
    name: "editproduct",
    pattern: "edit/{slug}",
    defaults: new { controller = "Product", action="Edit"});


// alles klaar, start de webserver -->
app.Run();
