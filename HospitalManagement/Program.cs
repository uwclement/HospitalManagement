using HospitalManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddSession(option =>
{ 
option.IdleTimeout = TimeSpan.FromMinutes(50);
}
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();


//Session add
builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly = true;
	// Other session configurations if needed
});
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
}
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
