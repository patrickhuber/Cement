using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Referee.Transforms
{
    public class NodeBuilder
    {
        public Node Node { get; protected set; }
        
        public NodeBuilder(string name)
            : this(Guid.NewGuid(), name)
        {            
        }

        public NodeBuilder(Guid id, string name)
        {
            this.Node.Id = id.ToString();
        }

        public NodeBuilder() : this(string.Empty)
        {
        }

        public NodeBuilder Nodes(Action<IObjectFactory<NodeBuilder>> nodes)
        {
            var nodeFactory = new GenericObjectFactory<NodeBuilder>();
            nodes(nodeFactory);
            this.Node.Nodes = new List<Node>(nodeFactory.Items.Select(n => n.Node));
            return this;
        }

        public NodeBuilder Name(string name)
        {
            this.Node.Name = name;
            return this;
        }

        public NodeBuilder Id(Guid id)
        {
            return Id(id.ToString());
        }

        public NodeBuilder Id(string id)
        {
            this.Node.Id = id;
            return this;
        }
    }
}
