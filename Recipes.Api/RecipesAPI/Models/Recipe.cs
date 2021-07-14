using System;
using System.Collections.Generic;

namespace RecipesAPI.Models
{
  public class Recipe
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Ingredient> Ingredients { get; set; }
  }
  
}