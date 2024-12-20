using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public abstract class PublicationDate
{
    public abstract DateOnly Beginning { get; }
    public static PublicationDate? Parse(string dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString))
            return null;

        if(dateString.IndexOf('/')>=0)
            return new FullDate(DateOnly.Parse(dateString));
        
        var strs=dateString.Split(',');
        if (int.TryParse(strs[0],out int year))
        {
            if(strs.Length == 1)
                return new YearOnly(year);
            if(int.TryParse(strs[1],out int month))        
                return new YearMonth(year, month);
        }

        return null;
    }
}

public class FullDate(DateOnly date) : PublicationDate
{
    public DateOnly Date { get; } = date;
    public override DateOnly Beginning => date;
    public override string ToString()
    {
        return Date.ToString("yyyy/MM/dd");
    }
}

public class YearMonth(int year, int month) : PublicationDate
{
    private DateOnly Date { get; } = new(year, month, 1);
    public int Year => Date.Year;
    public int Month => Date.Month;
    public override DateOnly Beginning => Date;
    public override string ToString()
    {
        return $"{Year}, {Month}";
    }
}

public class YearOnly(int year) : PublicationDate
{
    private DateOnly Date { get; } = new(year, 1, 1);
    public int Year => Date.Year;
    public override DateOnly Beginning => Date;
    public override string ToString()
    {
        return Year.ToString();
    }
}
