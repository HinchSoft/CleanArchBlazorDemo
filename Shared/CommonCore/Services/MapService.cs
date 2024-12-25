using CommonCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class MapService
{
    protected static List<MapperIndex> Mappers = new List<MapperIndex>(); 

    public static void LoadMappers(Assembly[] assemblies)
    {
        foreach(var assy in assemblies)
        {
            foreach (var mType in assy.GetTypes().Where(t=> typeof(IMapper).IsAssignableFrom(t)))
            {
                var baseType = mType.BaseType;

                if (baseType is not null && baseType.GetGenericArguments().Count() == 2)
                {
                    var tmod = baseType.GetGenericArguments()[0];
                    var tdto = baseType.GetGenericArguments()[1];
                    var idx = new MapperIndex
                    {
                        ModelType = tmod,
                        DtoType = tdto,
                        MapperType = mType,
                    };
                    Mappers.Add(idx);
                }
            }
        }
    }

    protected class MapperIndex
    {
        public Type ModelType { get; set; }
        public Type DtoType { get; set; }
        public Type MapperType { get; set; }
        public IMapper? Mapper { get; set; }
    }
}

public class MapService<TMod>:MapService where TMod : Entity
{
    private readonly IServiceProvider _serviceProvider;

    public MapService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMapper<TMod,TDto> GetMapper<TDto>()
    {
        var mapper = Mappers.Find(m=>m.ModelType == typeof(TMod)&& m.DtoType==typeof(TDto));
        if (mapper is null) throw new MissingMethodException($"No Mapper found for types {typeof(TMod).FullName} and {typeof(TDto).FullName}");
        if(mapper.Mapper == null)
        {
            mapper.Mapper = (IMapper)ActivatorUtilities.CreateInstance(_serviceProvider, mapper.MapperType);
        }
        return mapper.Mapper as IMapper<TMod, TDto>;
    }
}
