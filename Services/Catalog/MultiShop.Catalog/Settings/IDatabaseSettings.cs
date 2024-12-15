namespace MultiShop.Catalog.Settings
{
    public interface IDatabaseSettings
    {
        // appsettings.json'daki db tabloları için interface oluşturduk, ve bu innterface için bir class şart. 

        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string ProductDetailCollectionName { get; set; }
        public string ProductImageCollectionName { get; set; }
        public string FeatureSliderCollectionName { get; set; }
        public string SpecialOfferCollectionName { get; set; }
        public string OfferDiscountCollectionName { get; set; }
        public string BrandCollectionName { get; set; }
        public string AboutCollectionName { get; set; }
        public string ContactCollectionName { get; set; }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
