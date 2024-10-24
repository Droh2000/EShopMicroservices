namespace Catalog.API.Products.CreateProduct
{
    public record CreatedProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreatedProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Creacion de Endpoint POST
            app.MapPost("/products", async (CreatedProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreatedProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreatedProduct")
            .Produces<CreatedProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");

            //throw new NotImplementedException();
        }
    }
}
