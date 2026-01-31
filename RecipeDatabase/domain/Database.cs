namespace Domain;

using System.Text.RegularExpressions;
using MongoDB.Driver;
using static MongoDB.Driver.Builders<Recipe>;

public class Database
{
    private readonly IMongoCollection<Recipe> _collection;

    public Database(string databaseName, string collectionName)
    {
        var database = new MongoClient().GetDatabase(databaseName);
        _collection = database.GetCollection<Recipe>(collectionName);
    }

    public Task<List<Recipe>> GetRecipes(RecipeSearch? search)
    {
        var filter = Filter.Empty;
        if (search?.Name != null)
        {
            filter &= Filter.Regex(document => document.Name, new Regex(Regex.Escape(search.Name), RegexOptions.IgnoreCase));
        }
        if (search?.Tags != null)
        {
            filter &= Filter.All(document => document.Tags, search.Tags);
        }
        return _collection.Find(filter).ToListAsync();
    }

    public async Task<List<string>> GetAllTags()
    {
        var cursor = await _collection.DistinctManyAsync(document => document.Tags, Filter.Empty);
        return await cursor.ToListAsync();
    }

    public Task InsertRecipe(Recipe recipe)
    {
        return _collection.InsertOneAsync(recipe);
    }

    public Task InsertRecipes(List<Recipe> recipes)
    {
        return _collection.InsertManyAsync(recipes);
    }

    public Task UpdateRecipe(Recipe recipe)
    {
        return _collection.ReplaceOneAsync(Filter.Eq(document => document.Name, recipe.Name), recipe);
    }

    public Task DeleteRecipe(string recipeName)
    {
        return _collection.DeleteOneAsync(Filter.Eq(document => document.Name, recipeName));
    }

    public Task CreateIndexes()
    {
        return _collection.Indexes.CreateOneAsync(new CreateIndexModel<Recipe>(IndexKeys.Ascending(document => document.Tags)));
    }
}
