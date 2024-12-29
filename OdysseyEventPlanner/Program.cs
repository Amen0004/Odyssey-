using Microsoft.EntityFrameworkCore;
using OdysseyEventPlanner.Data;
using Microsoft.AspNetCore.HttpsPolicy;

var builder = WebApplication.CreateBuilder(args);

// Set the environment to Development programmatically
builder.Environment.EnvironmentName = "Development";

// Configure the database context with the connection string from appsettings.json
builder.Services.AddDbContext<OdysseyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Configure HTTPS Redirection Options
builder.Services.Configure<HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 4443; // Set the HTTPS port
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect; // Set the redirect status code
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enable HSTS in production
}

// Use CORS
app.UseCors("AllowAll"); // Apply the CORS policy

// Configure HTTPS Redirection Middleware
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();