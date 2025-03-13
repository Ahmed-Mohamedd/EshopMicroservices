using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Features.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);
    public class GetProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery Query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(Query.Id, cancellationToken);
            if (product == null) throw new ProductNotFoundException();

            return new GetProductByIdResult(product);
        }
    }
}
