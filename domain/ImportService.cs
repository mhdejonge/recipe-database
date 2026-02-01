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
                Ingredients = CommaSplit(row["Ingredients"].ToString()),
                Instructions = row["Instructions"].ToString(),
                Tags = CommaSplit(row["Tags"].ToString()),
                Source = row["Source"].ToString()
            });
        }
        await database.InsertRecipes(recipes);
    }

    private static List<string>? CommaSplit(string? column)
    {
        return string.IsNullOrEmpty(column) ? null : [..column.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim())];
    }
}
