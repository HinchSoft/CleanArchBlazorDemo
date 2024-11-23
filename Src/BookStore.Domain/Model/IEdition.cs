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

}

public class Seasonal:IEdition
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
