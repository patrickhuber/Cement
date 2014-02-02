using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Referee.Transforms.AutoMapper.Designer
{
    public interface IAutoMapperContext
    {        
        IMappingEngine Engine { get; }
        void Configure();
    }
}
