using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public abstract class PartialDate
{
    public abstract DateOnly Beginning { get; }
}

public class FullDate(DateOnly date) : PartialDate
{
    public DateOnly Date { get; } = date;
    public override DateOnly Beginning => date;
}

public class YearMonth(int year, int month) : PartialDate
{
    private DateOnly Date { get; } = new(year, month, 1);
    public int Year => Date.Year;
    public int Month => Date.Month;
    public override DateOnly Beginning => Date;
}

public class YearOnly(int year) : PartialDate
{
    private DateOnly Date { get; } = new(year, 1, 1);
    public int Year => Date.Year;
    public int Month => Date.Month;
    public override DateOnly Beginning => Date;
}
