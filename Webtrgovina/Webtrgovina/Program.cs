using Webtrgovina.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// prilagodba za dokumentaciju, èitati https://medium.com/geekculture/customizing-swagger-in-asp-net-core-5-2c98d03cbe52
builder.Services.AddSwaggerGen(sgo =>
{ // sgo je instanca klase SwaggerGenOptions
  // èitati https://devintxcontent.blob.core.windows.net/showcontent/Speaker%20Presentations%20Fall%202017/Web%20API%20Best%20Practices.pdf
    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Webtrgovina API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "miroslav.janic@gmail.com",
            Name = "Miroslav Janic"
        },
        Description = "Ovo je dokumentacija za Webtrgovina API",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Edukacijska licenca"
        }
    };
    sgo.SwaggerDoc("v1", o);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

});
// Svi se od svuda na sve moguæe naèine mogu spojitina naš API
// Čitati https://code-maze.com/aspnetcore-webapi-best-practices/
builder.Services.AddCors(opcije =>
{
    opcije.AddPolicy("CorsPolicy",
        builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

});


// dodavanje baze podataka
builder.Services.AddDbContext<WebtrgovinaContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString(name: "WebtrgovinaContext"))
);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
// moguænost generiranja poziva rute u CMD i Powershell
app.UseSwaggerUI(opcije =>
{
    opcije.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    opcije.ConfigObject.
    AdditionalItems.Add("requestSnippetsEnabled", true);
});
//}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseDeveloperExceptionPage();
app.MapFallbackToFile("index.html");

app.Run();
