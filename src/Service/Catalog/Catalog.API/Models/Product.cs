namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;//Para darle el valor predeterminado
        // Para la relacion 1-N con Categorias y hacemos que se cree un nueva lista de categorias cuando el producto este inicializado
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;

    }
}
