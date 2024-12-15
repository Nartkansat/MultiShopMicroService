using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Database
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);    // Tablo

            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x=>true).ToListAsync(); //tüm hepsini çağır
            return _mapper.Map<List<ResultCategoryDto>>(values);    //maple ve list ile dönder
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var values = await _categoryCollection.Find<Category>(x=>x.CategoryID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryID==updateCategoryDto.CategoryID, values); // categoryId'yi bul ve values parametresinden gelen değerle içeriği değiştir.
        }
    }
}
