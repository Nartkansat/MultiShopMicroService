using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // Bağlantı
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Database
            _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);    // Tablo

            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            var value = _mapper.Map<Contact>(createContactDto);
            await _contactCollection.InsertOneAsync(value);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x => x.ContactID == id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var values = await _contactCollection.Find(x => true).ToListAsync(); //tüm hepsini çağır
            return _mapper.Map<List<ResultContactDto>>(values);    //maple ve list ile dönder
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var values = await _contactCollection.Find<Contact>(x => x.ContactID == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdContactDto>(values);
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var values = _mapper.Map<Contact>(updateContactDto);
            await _contactCollection.FindOneAndReplaceAsync(x => x.ContactID == updateContactDto.ContactID, values); // ContactId'yi bul ve values parametresinden gelen değerle içeriği değiştir.
        }
    }
}
