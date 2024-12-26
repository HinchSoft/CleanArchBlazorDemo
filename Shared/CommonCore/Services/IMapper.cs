using CommonCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services
{
    public interface IMapper 
    {
    }

    public interface IMapper<TMod,TDto>:IMapper where TMod:Entity
    {
        TDto ToDto(TMod mod);
    }
}
