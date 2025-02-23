using identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<TokenGenerator>();

var app = builder.Build();

app.MapPost("/signin", (LoginInputModel model, TokenGenerator tokenGenerator) => {

    return new 
    {
        access_token = tokenGenerator.GenerateToken(Guid.NewGuid(), model.email)
    };

});

app.Run();


public sealed record LoginInputModel(string email, string password);