using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Transforms
{
    /// <summary>
    /// Defines a generic object factory that can be used to call the default constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
