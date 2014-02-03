using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Transforms
{
    public class ModelBuilder
    {
        public Model Model { get; protected set; }
        public ModelBuilder()
        {
            this.Model = new Model();
        }
        public ModelBuilder Type(string type)
        {
            this.Model.Type = type;
            return this;
        }
        
        public ModelBuilder Nodes(Action<IObjectFactory<NodeBuilder>> nodes)
        {
            var nodeFactory = new GenericObjectFactory<NodeBuilder>();
            nodes(nodeFactory);
            this.Model.Nodes = new List<Node>(nodeFactory.Items.Select(n => n.Node));
            return this;
        }
    }
}
