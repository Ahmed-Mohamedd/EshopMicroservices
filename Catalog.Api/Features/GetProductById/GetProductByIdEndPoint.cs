using BuildingBlocks.CQRS;
using Carter;
using Catalog.Api.Features.GetProducts;
using Catalog.Api.Models;
using Mapster;
using MediatR;

namespace Catalog.Api.Features.GetProductById
{
    //public record GetProductByIdRequest;
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}" , async(Guid id , ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                 return Results.Ok(result);

            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("GetProductById")
                .WithSummary("GetProductById");
        }
    }
}
