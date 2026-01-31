namespace Domain;

using System.Data;
using ExcelDataReader;

public class ImportService(Database database)
{
    public async Task ImportRecipes(Stream fileStream)
    {
        var reader = ExcelReaderFactory.CreateReader(fileStream);
        var dataSet = reader.AsDataSet();
        var table = dataSet.Tables[0];
        var recipes = new List<Recipe>();
        foreach (DataRow row in table.Rows)
        {
            recipes.Add(new Recipe
            {
                Name = row["Name"].ToString()!,
                Ingredients = row["Ingredients"].ToString(),
                Instructions = row["Instructions"].ToString(),
                MealType = Enum.Parse<MealType>(row["MealType"].ToString()!),
                Tags = row["Tags"].ToString()?.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim()).ToList(),
                Source = row["Source"].ToString()
            });
        }
        await database.InsertRecipes(recipes);
    }
}
