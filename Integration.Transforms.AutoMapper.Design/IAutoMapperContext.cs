using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Integration.Transforms.AutoMapper.Design
{
    public interface IAutoMapperContext
    {        
        IMappingEngine Engine { get; }
        void Configure();
    }
}
