namespace Domain;

public class Configuration
{
    public required string DatabaseName { get; set; }

    public required string CollectionName { get; set; }

    public required string ConnectionString { get; set; }
}
