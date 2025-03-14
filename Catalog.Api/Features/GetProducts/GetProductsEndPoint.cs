﻿using Carter;
using Catalog.Api.Models;
using Mapster;
using MediatR;

namespace Catalog.Api.Features.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request,ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("GetProducts")
                .WithSummary("GetProducts");
            
        }
    }
}
