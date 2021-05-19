
using MatBlazor;

namespace CHS.Web.Helpers
{
    public static class ListPageSizeStructure
    {
        public static BaseMatTable.PageSizeStructure[] GetPageSizeStructures()
        {
            BaseMatTable.PageSizeStructure[] ss = new BaseMatTable.PageSizeStructure[]
            {
                new BaseMatTable.PageSizeStructure { Text = "4", Value = 4 },
                new BaseMatTable.PageSizeStructure { Text = "10", Value = 10},
                new BaseMatTable.PageSizeStructure { Text = "15", Value = 15},
                new BaseMatTable.PageSizeStructure { Text = "20", Value = 20},
                new BaseMatTable.PageSizeStructure { Text = "30", Value = 30 },
                new BaseMatTable.PageSizeStructure { Text = "All", Value = int.MaxValue }
            };

            return ss;
        }
    }
}
