using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly IMongoCollection<OfferDiscount> _OfferDiscountCollection;
        private readonly IMapper _mapper;

        public OfferDiscountService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Database
            _OfferDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName);    // Tablo

            _mapper = mapper;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var value = _mapper.Map<OfferDiscount>(createOfferDiscountDto);
            await _OfferDiscountCollection.InsertOneAsync(value);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _OfferDiscountCollection.DeleteOneAsync(x => x.OfferDiscountID == id);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
        {
            var values = await _OfferDiscountCollection.Find(x => true).ToListAsync(); //tüm hepsini çağır
            return _mapper.Map<List<ResultOfferDiscountDto>>(values);    //maple ve list ile dönder
        }

        public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var values = await _OfferDiscountCollection.Find<OfferDiscount>(x => x.OfferDiscountID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOfferDiscountDto>(values);
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var values = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
            await _OfferDiscountCollection.FindOneAndReplaceAsync(x => x.OfferDiscountID == updateOfferDiscountDto.OfferDiscountID, values); // OfferDiscountId'yi bul ve values parametresinden gelen değerle içeriği değiştir.
        }
    }
}
