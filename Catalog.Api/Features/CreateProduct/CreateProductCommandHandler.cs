using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using FluentValidation;
using Marten;
using MediatR;
using System.Windows.Input;

namespace Catalog.Api.Features.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description , string ImageFile , decimal Price)
       : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {

            // Name must not be empty and should have at least 3 characters
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(2).WithMessage("Product name must be at least 3 characters long.");

            // Price must be greater than zero
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");

            // Description should be between 10 and 500 characters
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required.");

            // At least one category must be selected
            RuleFor(p => p.Category).NotEmpty().WithMessage("At least one category is required.");

            // ImagePath must be a valid URL or file path
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Image path is required.");
                
        }
    }

    public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            var product = new Product()
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
