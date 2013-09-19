using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Transforms
{
    public class GenericObjectFactory<T> : IObjectFactory<T>
    {
        public List<T> Items { get; set; }
        public GenericObjectFactory()
        {
            this.Items = new List<T>();
        }
        public T Add()
        {
            var instance = Activator.CreateInstance<T>();
            Items.Add(instance);
            return instance;
        }
    }
}
