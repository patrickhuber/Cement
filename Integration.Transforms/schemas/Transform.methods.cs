using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Referee.Transforms
{
    public partial class Transform
    {
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Load(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Transform));
            using (var stream = File.OpenRead(file))
            {
                Assign(serializer.Deserialize(stream) as Transform);
            }        
        }

        /// <summary>
        /// Copies the specified transform.
        /// </summary>
        /// <param name="transform">The transform.</param>
        protected void Assign(Transform transform)
        {
            this.Functions = transform.Functions;
            this.Inputs = transform.Inputs;
            this.Outputs = transform.Outputs;
            this.Links = transform.Links;
            this.Operations = transform.Operations;
        }
    }
}
