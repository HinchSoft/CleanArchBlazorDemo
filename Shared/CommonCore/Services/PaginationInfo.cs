using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class PaginationInfo
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }

    public FieldList[] OrderBy { get; set; }
    public FieldList[] Filter { get; set; }

    public record FieldList(string Field, string Operator, string? Value);
}
