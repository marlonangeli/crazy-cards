using CrazyCards.Api.Extensions;
using CrazyCards.Security.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOidcAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger(builder.Configuration, useOidc: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerUI(builder.Configuration, useOidc: true);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();