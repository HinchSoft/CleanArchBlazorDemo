using Microsoft.AspNetCore.Components.QuickGrid;
using System;
using System.Text;

namespace Bookstore.UI;

public static class QuickGridExtensions
{
    public static string ToUrlQuery(this IEnumerable<SortedProperty>? sortedProperties)
    {
        var ret = new StringBuilder();
        if (sortedProperties != null && sortedProperties.Count() > 0)
        {
            ret.Append("orderby=");
            ret.AppendJoin(",", sortedProperties.Select(o => $"{o.PropertyName}-{o.Direction}"));
        }
        return ret.ToString();
    }

    public static Dictionary<string,string> ToDictionary(this IEnumerable<SortedProperty>? sortedProperties)
    {
        var ret = new Dictionary<string,string>();

        if (sortedProperties != null && sortedProperties.Count() > 0)
        {
            foreach (var sort in sortedProperties) 
                ret.Add(sort.PropertyName, sort.Direction switch 
                { 
                    SortDirection.Descending=>"Desc",
                    _=>"Asc" 
                });
        }
        return ret;
    }
}
