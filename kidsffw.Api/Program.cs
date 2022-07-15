using AspNetCoreRateLimit;
using kidsffw.Application;
using kidsffw.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMemoryCache();

//builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

//builder.Services.AddInMemoryRateLimiting();

//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


// Add services to the container.
builder.Services.AddCors(p=>p.AddPolicy("crossdomain", p=>p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var razorPayConfiguration = builder.Configuration.GetSection("RazorPayConfiguration").Get<RazorPayConfiguration>();
builder.Services.AddSingleton(razorPayConfiguration);
builder.Services.AddApplication();


//just to trigger the build 
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseIpRateLimiting();

app.UseCors("crossdomain");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
