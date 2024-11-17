using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public class Release
{
    public Publisher Publisher { get; set; }
    public IEdition Edition { get; private set; }
    public PublicationInfo Publication { get; private set; }

}
