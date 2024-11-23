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

    //public Release(Publisher publisher, IEdition edition, PublicationInfo publication) =>
    //    (Publisher, Edition, Publication) = (publisher, edition, publication);

    private Type PublicationKind => Publication.GetType();

    private PublicationDate? publicationDate => Publication.Map<PublicationDate?>(
        published=>published.PublishedOn, planned=>planned.PlannedFor,notPlannedYet=>null);

    private Release(Type PublicationKind, PublicationInfo publicationInfo, IEdition edition)
    {
        Publisher = default!;
        Edition = edition;

    }
}
