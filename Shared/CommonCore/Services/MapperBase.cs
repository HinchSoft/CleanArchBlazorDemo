using CommonCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;


public abstract class MapperBase<TMod, TDto> : IMapper<TMod, TDto> where TMod : Entity
{
    public abstract TDto ToDto(TMod mod);

}
