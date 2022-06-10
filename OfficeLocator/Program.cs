using OfficeLocator.Services;
using OfficeLocator.DAL;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.Services.AddScoped<IFacilityPreferenceService, FacilityPreferenceService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();

app.Run();