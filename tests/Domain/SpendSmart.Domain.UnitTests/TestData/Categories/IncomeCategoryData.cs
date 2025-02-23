using System.Linq;
using SpendSmart.Domain.Modules.Common;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Categories;

public sealed class IncomeCategoryData : TheoryData<Category>
{
    public IncomeCategoryData()
    {
        foreach (Category category in Category.List.Where(x => !x.IsExpense && !x.IsDefault))
        {
            Add(category);
        }
    }
}
