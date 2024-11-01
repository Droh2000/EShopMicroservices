namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResut>;

    public record GetProductsResut(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResut>
    {
        public async Task<GetProductsResut> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 1,cancellationToken);

            return new GetProductsResut(products);
        }
    }
}
