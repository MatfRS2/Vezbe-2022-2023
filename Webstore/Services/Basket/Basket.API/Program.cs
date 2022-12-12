using System.Reflection;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.GRPC.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(
    opts => {
        opts.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
    });
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// gRPC
builder.Services.AddGrpcClient<CouponProtoService.CouponProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<CouponGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// EventBus
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
