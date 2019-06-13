namespace OnlineEnterprice.Data.Settings
{
    public interface IShopDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
