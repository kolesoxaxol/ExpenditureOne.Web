using Bogus;
using ExpenditureOne.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ExpenditureOne.DAL
{
    public class ExpenditureInitializer : IExpenditureInitializer
    {
        public void Initialize(ExpenditureContext context)
        {

            // TODO: think about it
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var faker = new Faker();

            var expenditureBogus = new Faker<Expenditure>().RuleFor(exp => exp.DateOfExpenditure, f => f.Date.Past(1))
                .RuleFor(exp => exp.Title, f => f.Lorem.Slug())
                .RuleFor(exp => exp.Description, f => f.Lorem.Paragraph());

            var categoryBogus = new Faker<Category>()
                .RuleFor(c => c.CategoryName, f => f.Lorem.Word())
                .RuleFor(c => c.Color, f => f.Commerce.Color());

            var expenditures = expenditureBogus.Generate(faker.Random.Number(120, 200));

            context.Expenditures.AddRange(expenditures);
            context.SaveChanges();



            var categories = categoryBogus.Generate(faker.Random.Number(25, 80));
            var expendituresCount = context.Expenditures.Count();
            foreach (var category in categories)
            {
                var expenditureCategoryLinkList = new List<ExpenditureCategory>();


                int expendituresInCategory = 1;
                if (faker.Random.Number(0, 3) == 0)
                {
                    expendituresInCategory++;
                }
                if (faker.Random.Number(0, 4) == 0)
                {
                    expendituresInCategory++;
                }
                if (faker.Random.Number(0, 5) == 0)
                {
                    expendituresInCategory++;
                }


                for (int i = 0; i < expendituresInCategory; i++)
                {
                    int expenditureId = faker.Random.Number(1, expendituresCount);
                    var expenditureCategoryLink = new ExpenditureCategory()
                    {
                        Id = i,
                        ExpenditureId = expenditureId
                    };

                    expenditureCategoryLinkList.Add(expenditureCategoryLink);
                }


                category.Expenditures = expenditureCategoryLinkList;


                context.Categories.Add(category);
            }

            context.SaveChanges();

        }



    }
}
