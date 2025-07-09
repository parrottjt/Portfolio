using System.Collections.Generic;
using System.Linq;

namespace TSCore.Utils
{
    public static class Sorting
    {
        public static List<T> QuickSort<T>(this List<T> list)
        {
            if (list.Count <= 1) return list;

            var lessThanPivot = new List<T>();
            var greaterThanPivot = new List<T>();
        
            var pivot = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                if (Comparer<T>.Default.Compare(list[i], pivot) <= 0)
                {
                    lessThanPivot.Add(list[i]);
                }
                else
                {
                    greaterThanPivot.Add(list[i]);
                }
            }

            return QuickSort(lessThanPivot)
                .Append(pivot)
                .Concat(
                    QuickSort(greaterThanPivot)
                )
                .ToList();
        }
    }
}
