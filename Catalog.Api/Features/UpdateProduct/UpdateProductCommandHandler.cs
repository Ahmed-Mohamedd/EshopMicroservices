using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using FluentValidation;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.Api.Features.UpdateProduct
{
    public record UpdateProductCommand(Guid Id , string Name , string Description , string ImageFile, List<string> Category , decimal Price )
        :IQuery<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is Requiured!");

            RuleFor(command => command.Name).NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(2).WithMessage("Product name must be at least 3 characters long.");

            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session)
        : IQueryHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if ( product == null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = request.Category;
            product.ImageFile = request.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
