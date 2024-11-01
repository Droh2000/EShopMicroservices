namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> 
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Requierd");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Requierd");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Requierd");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {// Esta clase es responsable de ejecutar la logica de negocio
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product Entity from command Object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            // Save to Database
            // Return CreateProductResult result
            session.Store(product); 
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
