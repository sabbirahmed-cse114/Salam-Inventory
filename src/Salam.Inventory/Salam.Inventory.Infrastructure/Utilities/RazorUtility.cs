using Microsoft.AspNetCore.Mvc.Rendering;

namespace Salam.Inventory.Infrastructure.Utilities
{
    public static class RazorUtility
    {
        public static IList<SelectListItem> ToSelectList<TItem, TValue>
            (
            this IEnumerable<TItem> products,
            Func<TItem, string> productName,
            Func<TItem, TValue> productId
            )
        {
            var selectList = (from product in products
                             select new SelectListItem
                             (productName(product), productId(product).ToString())).ToList();
            return selectList;
        }
    }
}