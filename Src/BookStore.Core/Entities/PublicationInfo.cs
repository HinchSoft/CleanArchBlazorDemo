using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model;

public abstract class PublicationInfo
{
    public abstract PublicationDate? PublicationDate { get; }
    public static PublicationInfo Map(Type type,PublicationDate? publicationDate)
    {
        return type.Name switch
        {
            "Published" => new Published(publicationDate),
            "Planned" => new Planned(publicationDate),
            "NotPlannedYet" => new NotPlannedYet(),
        };
    }
}

public sealed class Published(PublicationDate PublishedOn) : PublicationInfo
{
    public override PublicationDate? PublicationDate => PublishedOn;
    public override string ToString()
    {
        return $"Published on {PublishedOn.ToString()}";
    }
}

public sealed class Planned(PublicationDate PlannedFor) : PublicationInfo
{
    public override PublicationDate? PublicationDate => PlannedFor;
    public override string ToString()
    {
        return $"Planned for {PlannedFor.ToString()}";
    }
}
public sealed class NotPlannedYet : PublicationInfo
{
    public override PublicationDate? PublicationDate => null;
    public override string ToString()
    {
        return "No planned date";
    }
}
