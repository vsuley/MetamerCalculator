using System;
using System.Xml;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL:
    /// This class represents an observer.
    /// </summary>
    class Observer
    {
        public string Name { get; set; }
        public SpectralData[] ResponseSpectra { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Observer()
        {
            // Usualy we'd create instances for SpectralData here but right now we can't, since we don't know how many
            // channels we have.
        }

        public void Initialize(XmlElement xmlNode)
        {
            this.Name = xmlNode.GetAttribute(XMLDataConstants.Name);
            int channelCount = Convert.ToInt16(((XmlElement)xmlNode.FirstChild).GetAttribute(XMLDataConstants.Count));

            ResponseSpectra = new SpectralData[channelCount];

            for (int i = 0; i < channelCount; i++)
			{
                ResponseSpectra[i] = new SpectralData();

                XmlElement spectrumXML = xmlNode.GetElementsByTagName(XMLDataConstants.WaveData)[i] as XmlElement;
                ResponseSpectra[i].Initialize(spectrumXML);
			}
        }

        /// <summary>
        /// Returns the name of the observer
        /// </summary>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
