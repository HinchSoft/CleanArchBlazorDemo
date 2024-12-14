using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public class Release
{
    public PublicationInfo Publication { get; private set; }

    public Release(PublicationInfo publication) => Publication = publication;

    private Type PublicationKind => Publication.GetType();

    private PublicationDate? PublicationDate => Publication.PublicationDate;

    private Release(Type publicationKind,PublicationDate? publicationDate) // used by EF Core
    {
        Publication = PublicationInfo.Map(publicationKind,publicationDate);
    }
}
