using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SailClubLibrary.Models;

namespace SailClubLibrary.Helpers.Filter
{
    public class FilterFunctions<T> 
    {
        public static List<T> Filter(List<T> values, List<Predicate<T>> predicates)
        {
            List<T> filteredValues = new List<T>();
            foreach (T item in values)
            {
                int count = 0;
                foreach (Predicate<T> predicate in predicates)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }
                if(count == predicates.Count)
                {
                    filteredValues.Add(item);
                }
            }
            return filteredValues;
        }
    }
}