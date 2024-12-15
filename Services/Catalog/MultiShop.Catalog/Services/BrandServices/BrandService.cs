using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _BrandCollection;
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Database
            _BrandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);    // Tablo

            _mapper = mapper;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var value = _mapper.Map<Brand>(createBrandDto);
            await _BrandCollection.InsertOneAsync(value);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _BrandCollection.DeleteOneAsync(x => x.BrandID == id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var values = await _BrandCollection.Find(x => true).ToListAsync(); //tüm hepsini çağır
            return _mapper.Map<List<ResultBrandDto>>(values);    //maple ve list ile dönder
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
        {
            var values = await _BrandCollection.Find<Brand>(x => x.BrandID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBrandDto>(values);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var values = _mapper.Map<Brand>(updateBrandDto);
            await _BrandCollection.FindOneAndReplaceAsync(x => x.BrandID == updateBrandDto.BrandID, values); // BrandId'yi bul ve values parametresinden gelen değerle içeriği değiştir.
        }
    }
}
