using identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<TokenGenerator>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => {
        options.WithTitle("Trib Identity API")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.AsyncHttp);
    });
}

app.MapPost("/signin", (LoginInputModel model, TokenGenerator tokenGenerator) => {

    return new 
    {
        access_token = tokenGenerator.GenerateToken(Guid.NewGuid(), model.email)
    };

});

app.Run();


public sealed record LoginInputModel(string email, string password);