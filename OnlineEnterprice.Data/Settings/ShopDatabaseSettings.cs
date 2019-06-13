namespace OnlineEnterprice.Data.Settings
{
    public class ShopDatabaseSettings : IShopDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}