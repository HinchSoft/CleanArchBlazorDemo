using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public interface IEdition
{
}

public class Ordinal
{
    int number { get; set; }
}

public class Seasonal
{
    public Season Season { get; set; }
    public int Year { get; set; }
}

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}
