using FakeStoreNet.Application.Common;
using FakeStoreNet.Infrastructure.Caching;
using FluentValidation;
using FakeStoreNet.Application.Features.Product.Commands.CreateProduct;
using FakeStoreNet.Application.Features.Product.Commands.UpdateProduct;
using FakeStoreNet.Application.Features.Product.Commands.DeleteProduct;
using MediatR;
using FakeStoreNet.Application.Features.Product.Queries.GetAllProducts;
using FakeStoreNet.Application.Features.Product.Queries.GetProductById;
using FakeStoreNet.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FakeStoreNet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register domain event infrastructure
            builder.Services.AddSingleton<FakeStoreNet.Domain.Common.IEventBus, FakeStoreNet.Infrastructure.EventDispatching.InMemoryEventBus>();
            builder.Services.AddSingleton<FakeStoreNet.Domain.Common.IEventLogRepository, FakeStoreNet.Infrastructure.EventDispatching.InMemoryEventLogRepository>();

            // Register controllers and caching
            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

            // Register FluentValidation validators
            builder.Services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            builder.Services.AddTransient<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            builder.Services.AddTransient<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            builder.Services.AddTransient<IValidator<GetAllProductsQuery>, GetAllProductsQueryValidator>();
            builder.Services.AddTransient<IValidator<GetProductByIdQuery>, GetProductByIdQueryValidator>();

            // Register validation pipeline behavior
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            // Global exception handling for domain validation errors
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (DomainValidationException ex)
                {
                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var problem = new ValidationProblemDetails
                    {
                        Title = "Validation Failed",
                        Detail = ex.Message,
                        Status = StatusCodes.Status400BadRequest
                    };
                    await context.Response.WriteAsJsonAsync(problem);
                }
            });

            // Configure domain event subscriptions
            var eventBus = app.Services.GetRequiredService<FakeStoreNet.Domain.Common.IEventBus>();
            var eventLogRepo = app.Services.GetRequiredService<FakeStoreNet.Domain.Common.IEventLogRepository>();
            eventBus.Subscribe<FakeStoreNet.Domain.Common.Events.ProductCreatedEvent>(async evt =>
            {
                var payload = System.Text.Json.JsonSerializer.Serialize(evt);
                var log = new FakeStoreNet.Domain.Common.EventLog(evt.GetType().FullName!, payload, evt.OccurredOn);
                await eventLogRepo.SaveAsync(log);
            });

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
