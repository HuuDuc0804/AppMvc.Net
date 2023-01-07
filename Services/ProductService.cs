namespace App.Services;
public class ProductService : List<ProductModel>
{
    public ProductService()
    {
        this.AddRange( new ProductModel[]
        {
            new ProductModel() { Id = 1, Name = "Iphone X", Price = 10000 },
            new ProductModel() { Id = 2, Name = "Samsung J7", Price = 5000 },
            new ProductModel() { Id = 3, Name = "Note 8", Price = 4000 },
            new ProductModel() { Id = 4, Name = "Nokie 1202", Price = 800 },
            new ProductModel() { Id = 5, Name = "Sony XZ2 Premium", Price = 7000 },
        });
    }
}