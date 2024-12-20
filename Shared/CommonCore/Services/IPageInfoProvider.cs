using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public interface IPageInfoProvider
{
    PaginationInfo PaginationInfo { get; }
}
