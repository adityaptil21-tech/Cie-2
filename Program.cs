using HospitalAppointmentSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Caching: IMemoryCache (doctor list) + Response Caching middleware (doctor details page)
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

// Session (needs a distributed cache store; the in-memory one is fine for local use)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".CarePlus.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // no consent banner required for this app
    options.Cookie.SameSite = SameSiteMode.Lax;
});

// Application services (in-memory data, no database needed)
builder.Services.AddSingleton<IDoctorService, DoctorService>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();

// Typed HttpClient for JSONPlaceholder
builder.Services.AddHttpClient<IPostService, PostService>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseResponseCaching();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
