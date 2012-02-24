using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL.
    /// This class represents a light source
    /// </summary>
    class LightSource
    {
        public string Name { get; set; }
        public SpectralData SpectralPowerDistribution { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LightSource()
        {
            SpectralPowerDistribution = new SpectralData();
        }

        /// <summary>
        /// Reads in data from the XML node.
        /// </summary>
        /// <param name="xmlNode">A light source XML node.</param>
        public void Initialize(XmlElement xmlNode)
        {
            this.Name = xmlNode.GetAttribute(XMLDataConstants.Name);
            this.SpectralPowerDistribution.Initialize(xmlNode.FirstChild as XmlElement);
        }

        /// <summary>
        /// Returns the name of the light source.
        /// </summary>
        /// <returns>Returns the name of the light source.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
