using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model
{
    public class PublicationInfo
    {
        public PartialDate? PublicationDate { get; private set; }
        public bool IsPublished { get; private set; }

        public static PublicationInfo CreateUnpublished() =>
            new(null, false);
        public static PublicationInfo CreatePlanned(PartialDate date) =>
            new(date, false);
        public static PublicationInfo CreatePublished(PartialDate date) =>
            new(date, true);

        public PublicationInfo(PartialDate? publicationDate, bool isPublished) =>
            (PublicationDate, IsPublished) = (publicationDate, isPublished);

    }
}
