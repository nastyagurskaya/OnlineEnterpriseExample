namespace OnlineEnterprice.Data.Settings
{
    public class ShopDatabaseSettings : IShopDatabaseSettings
    {
        public string OrderCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}