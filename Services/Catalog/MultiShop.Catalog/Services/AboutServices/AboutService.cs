using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices
{
    public class AboutService:IAboutService
    {
        private readonly IMongoCollection<About> _AboutCollection;
        private readonly IMapper _mapper;

        public AboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Database
            _AboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);    // Tablo

            _mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var value = _mapper.Map<About>(createAboutDto);
            await _AboutCollection.InsertOneAsync(value);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _AboutCollection.DeleteOneAsync(x => x.AboutID == id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var values = await _AboutCollection.Find(x => true).ToListAsync(); //tüm hepsini çağır
            return _mapper.Map<List<ResultAboutDto>>(values);    //maple ve list ile dönder
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            var values = await _AboutCollection.Find<About>(x => x.AboutID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdAboutDto>(values);
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var values = _mapper.Map<About>(updateAboutDto);
            await _AboutCollection.FindOneAndReplaceAsync(x => x.AboutID == updateAboutDto.AboutID, values); // AboutId'yi bul ve values parametresinden gelen değerle içeriği değiştir.
        }
    }
}
