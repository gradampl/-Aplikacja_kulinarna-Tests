using System;
using Xunit;
using TestSupport.EfHelpers;
using System.Linq;
using FluentAssertions;
using Aplikacja_kulinarna.Models;
using Aplikacja_kulinarna.Data;
using Xunit.Extensions.AssertExtensions;
using Aplikacja_kulinarna.Services.RecipeServices.Concrete;

namespace Test.ServicesLayer
{
    public class ListRecipeServiceTest
    {
       [Fact]
         public void ShouldListRecipes()
         {
             //SETUP
             var options = SqliteInMemory
                 .CreateOptions<CulinaryContext>();
             var numRecipes = 5;
            
             using (var context = new CulinaryContext(options))
             {
                 
                 context.Database.EnsureCreated();

                 for (var i = 0; i < numRecipes ; i++)
                 {
                      var r = new Recipe
                     {
                        Dish = $"Cup of tea #{i}",
                        DishRecipe = $"Boil water and add tea for {i} minute(s) ",
                        MinutesToPrepare = i,
                        QualityStars = i,
                     };

                   context.Recipes.Add(r);
                 }

                 context.SaveChanges();

                 //ATTEMPT
                 var service = new ListRecipeService(context);
                 var dtos = service.SelectAll().ToList();

                 //VERIFY
                 dtos.Count.ShouldEqual(numRecipes);
             }
     
         }
    }
}
