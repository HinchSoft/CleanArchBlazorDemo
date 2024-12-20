using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public interface IEdition
{
}

public class Ordinal:IEdition
{
    public int Number { get; private set; }

    public Ordinal(int number)=>Number = number;

    public override string ToString()
    {
        return Number.ToString();
    }
}

public class Seasonal:IEdition
{
    public Season Season { get; set; }
    public int Year { get; set; }

    public Seasonal(Season season, int year) =>
        (Season, Year)=(season, year);

    public override string ToString()
    {
        return $"{Enum.GetName(Season)} {Year}";
    }
}

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}
