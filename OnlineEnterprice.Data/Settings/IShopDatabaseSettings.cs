namespace OnlineEnterprice.Data.Settings
{
    public interface IShopDatabaseSettings
    {
        string OrderCollectionName { get; set; }
        string ProductCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
