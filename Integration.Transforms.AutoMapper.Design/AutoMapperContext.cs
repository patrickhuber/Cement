using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Integration.Transforms.AutoMapper.Design
{
    public class AutoMapperContext : IAutoMapperContext
    {
        public IMappingEngine Engine { get; set; }

        public AutoMapperContext(IMappingEngine engine)
        {
            this.Engine = engine;
        }
        
        public AutoMapperContext()
            : this(Mapper.Engine)
        { }

        public void Configure()
        {
            Mapper.Initialize(cfg => 
            {
                cfg.AddProfile<DesignerProfile>();
            });
            this.Engine = Mapper.Engine;
        }
    }
}
