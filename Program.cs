using Sicu.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwagger();

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

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseAuthorization();
app.MapControllers();
app.Run();