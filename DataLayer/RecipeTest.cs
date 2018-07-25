using System;
using Xunit;
using TestSupport.EfHelpers;
using System.Linq;
using FluentAssertions;
using Aplikacja_kulinarna.Models;
using Aplikacja_kulinarna.Data;
using Xunit.Extensions.AssertExtensions;

namespace Test
{
    public class RecipeTest
    {
       [Fact]
         public void ShouldSaveRecipe()
         {
             //given
             var options = SqliteInMemory
                 .CreateOptions<CulinaryContext>();
             using (var context = new CulinaryContext(options))
             {
                 //when
                 context.Database.EnsureCreated();
                 var r = new Recipe
                 {
                     Dish = "Cup of tea",
                     DishRecipe = "Boil water and add tea",
                     MinutesToPrepare = 5,
                     QualityStars = 1,
                 };
                 context.Recipes.Add(r);
                 context.SaveChanges();
               
             }
 
             using (var context = new CulinaryContext(options))
             {
                 //VERIFY  
                 var recipe = context.Recipes.First();
                 recipe.Dish.Should().BeEquivalentTo("Cup of tea");
                 recipe.DishRecipe.Should().BeEquivalentTo("Boil water and add tea");
                 recipe.MinutesToPrepare.ShouldEqual(5);
                 recipe.QualityStars.ShouldEqual(1);
             }
 
            
         }
    }
}
