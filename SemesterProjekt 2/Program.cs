using Microsoft.Extensions.Hosting;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
