using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL.
    /// This class stores information about a Material.
    /// </summary>
    class Material
    {
        // Object properties
        public string Name { get; set; }

        public SpectralData ReflectanceDistribution { get; set; }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Material()
        {
            this.ReflectanceDistribution = new SpectralData();
        }

        /// <summary>
        /// Initializes the material with data from the XML Node.
        /// </summary>
        /// <param name="xmlNode">Node of type Material</param>
        public void Initialize(XmlElement xmlNode)
        {
            this.Name = xmlNode.GetAttribute(XMLDataConstants.Name);
            this.ReflectanceDistribution.Initialize(xmlNode.FirstChild as XmlElement);
        }

        /// <summary>
        /// Returns the string representation of this object - which is the material's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
