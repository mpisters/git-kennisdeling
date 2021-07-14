#nullable enable
namespace RecipesAPI.Models
{
  public class CreatedIngredientDto
  {
    public CreatedIngredientDto(string name, int amount, string unit)
    {
      Name = name;
      Amount = amount;
      Unit = unit;
    }
    
    public string Name { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
  }
  
   public class UpdatedIngredientDto
    {
      public UpdatedIngredientDto(string? name, int? amount, string? unit)
      {
        Name = name;
        Amount = amount;
        Unit = unit;
      }

      public long? Id { get; set; }

      public string? Name { get; set; }
      public int? Amount { get; set; }
      public string? Unit { get; set; }
    }
}