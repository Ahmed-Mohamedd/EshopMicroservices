using BuildingBlocks.CQRS;
using Carter;
using Catalog.Api.Features.UpdateProduct;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Features.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool isSuccess);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id , ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(result);
            })
                 .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("DeleteProduct")
                .WithDescription("DeleteProduct");
        }
    }
}
