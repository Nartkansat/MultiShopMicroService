using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        // Genel Methodlar
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDto> GetByIdProductAsync(string id);

        // Özel methodlar
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductsWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId);
    
    }
}
