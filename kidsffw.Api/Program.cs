using kidsffw.Application;
using kidsffw.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(p=>p.AddPolicy("crossdomain", p=>p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var razorPayConfiguration = builder.Configuration.GetSection("RazorPayConfiguration").Get<RazorPayConfiguration>();
builder.Services.AddSingleton(razorPayConfiguration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseCors("crossdomain");
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();