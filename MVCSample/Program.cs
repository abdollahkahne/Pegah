using Microsoft.EntityFrameworkCore;
using MVCSample.Data;
using MVCSample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddDbContext<PeopleDbContext>(optionsBuilder => optionsBuilder.UseSqlite("DataSource=MyPeople.db"));
builder.Services.AddScoped<PeopleService>(); // since db context created by scope this should be too

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
