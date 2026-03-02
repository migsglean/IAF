using IAF.Server.Helpers;
using IAF.Server.Interfaces;
using IAF.Server.Repositories;
using IAF.Server.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.AddSingleton<DapperHelper>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPartsRepository, PartsRepository>();
builder.Services.AddScoped<IPartsService, PartsService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IAF API",
        Version = "v1",
        Description = "Inventory Assets Forecasting API"
    });
});

// Add CORS policy for Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IAF API v1");
        c.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

// Enable CORS
app.UseCors("AllowAngularDev");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Fallback to Angular index.html for client-side routing (if serving Angular build)
app.MapFallbackToFile("/index.html");

app.Run();