using Sicu.Modules.Swagger;
using Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwagger();

// Obtiene la clave de cifrado del archivo de configuración.
string? KGenerate = builder.Configuration.GetValue<string>("KGenerate");

//// Obtiene las URL permitidas y las desencripta.
List<string> originsList = new List<string>();
var encryptedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
foreach (var item in encryptedOrigins)
{
    originsList.Add(new EncryptDecrypt().Decrypt(item, KGenerate));
}
string[]? decryptedOrigins = originsList.ToArray();

// Configura las políticas de CORS.
builder.Services.AddCors(options =>
{
    options.AddPolicy("SitiosPermitidos",
        builder => builder
            .WithOrigins(decryptedOrigins ?? new string[] { "*" }) // Usamos "*" para permitir cualquier origen si AllowedOrigins es nulo
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI - ANAM v.1");
        c.RoutePrefix = string.Empty;
        
        var vPersonalised = Convert.ToBoolean(builder.Configuration["CustomSwaggerUi:Personalised"]);
        if (vPersonalised) 
        { 
            c.DocumentTitle = builder.Configuration["CustomSwaggerUi:DocTitle"]; 
            c.HeadContent = builder.Configuration["CustomSwaggerUi:HeaderImg"];                                             
            c.InjectStylesheet(builder.Configuration["CustomSwaggerUi:PathCss"]); 
        };
    });
}

// Configura el flujo de solicitud HTTP.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("SitiosPermitidos");

// Mapea los controladores.
app.MapControllers();

app.Run();