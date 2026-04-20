using ClassBlazor.Components;
using ClassBlazor.Services;
using ClassController;
using ClassController.Abstractions;
using ClassModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<LoginController>();
builder.Services.AddScoped<ProductController>();
builder.Services.AddScoped<IUserController, UserController>();
builder.Services.AddScoped<IProductController, ProductController>();

builder.Services.AddScoped<IRepository<User>>(sp =>
    new UserRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "data", "users.csv")));
builder.Services.AddScoped<IRepository<Product>>(sp =>
    new ProductRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "data", "products.csv")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
