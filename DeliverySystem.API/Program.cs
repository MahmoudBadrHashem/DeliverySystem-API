using DeliverySystem.API.Middlewares;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Application.Services;
using DeliverySystem.Infrastructure;
using DeliverySystem.Infrastructure.Persistence;
using DeliverySystem.Infrastructure.Persistence.Identity;
using DeliverySystem.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//= Services configuration

builder.Services.AddControllers();

//== إعدادات  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependence in injections
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<AdminInfoOption>(builder.Configuration.GetSection("AdminInfo"));

//== FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<DeliverySystem.Application.Validators.CreateProductDtoValidator>();

//== Dependency Injection for Services 
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

//== Dependency Injection for Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

var app = builder.Build();
await DataService.DataSeed(app.Services);
//== Middleware Pipeline 

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeliverySystem API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();