using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class PaginationInfo
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
}
