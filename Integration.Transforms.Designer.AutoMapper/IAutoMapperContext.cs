using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Integration.Transforms.Designer.AutoMapper
{
    public interface IAutoMapperContext
    {        
        IMappingEngine Engine { get; }
        void Configure();
    }
}
