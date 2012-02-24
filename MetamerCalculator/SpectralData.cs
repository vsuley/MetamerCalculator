using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL.
    /// THis class stores information about a spectra. Could be used within a material, source or observer.
    /// </summary>
    class SpectralData
    {
        public int LowestWavelength { get; set; }
        public int HighestWavelength { get; set; }
        public int StepSize { get; set; }
        public List<float> WaveData { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SpectralData()
        {
            WaveData = new List<float>();
        }

        public static SpectralData CreateRandomSpectrum(int low, int high, int stepsize, int? seed)
        {
            SpectralData spectraldata = new SpectralData();
            spectraldata.LowestWavelength = low;
            spectraldata.HighestWavelength = high;
            spectraldata.StepSize = stepsize;

            Random randomNumberGenerator = null == seed ? new Random() : new Random((int)seed);

            for (int i = low; i <= high; i += stepsize)
            {
                spectraldata.WaveData.Add((float)randomNumberGenerator.NextDouble());
            }

            return spectraldata;
        }

        /// <summary>
        /// Reads in spectral data from the XML node.
        /// </summary>
        /// <param name="xmlNode">An XML node from WaveData</param>
        public void Initialize(XmlElement xmlNode)
        {
            this.LowestWavelength = Convert.ToInt16(xmlNode.GetAttribute(XMLDataConstants.Start));
            this.HighestWavelength = Convert.ToInt16(xmlNode.GetAttribute(XMLDataConstants.End));
            this.StepSize = Convert.ToInt16(xmlNode.GetAttribute(XMLDataConstants.Step));

            string dataBlob = xmlNode.InnerText;
            foreach (string dataEntry in dataBlob.Split(" \r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                WaveData.Add((float)Convert.ToDouble(dataEntry));
            }

            if (WaveData.Count != (this.HighestWavelength - this.LowestWavelength) / StepSize + 1)
            {
                throw new InvalidOperationException("Number of data entries in spectrum data was not as expected");
            }
        }

        /// <summary>
        /// This method decreases the start wavelength to the new one specified. It simply duplicates the value at the 
        /// beginning of the spectra to fill in the new values.
        /// </summary>
        /// <param name="newStart">The new start of the spectra. Should be less than the current start.</param>
        public void StretchStartWavelength(int newStart)
        { 
            if (newStart >= this.LowestWavelength)
            {
                string error = string.Format("Specified new start ({0}) is lower than current start ({1})", newStart, this.LowestWavelength);
                throw new InvalidOperationException(error);
            }
    
            // Make sure that the newStart value makes sense with our current lowestWavelength and stepSize values.
            //
            int difference = this.LowestWavelength - newStart;
            if(difference % this.StepSize != 0)
            {
                throw new InvalidOperationException("The new requested start does not fall in line with sampling pattern");
            }
    
            // Sample the current lowest wavelength's value. We will use this to fil in new values.
            float valueToInsert = 0f; // this.WaveData[0];

            // Should be ok to stretch now.
            //
            int numberOfElements = difference / this.StepSize;
            int i;
            for(i = 0; i < numberOfElements; i++)
            {
                this.WaveData.Insert(0, valueToInsert);
                this.LowestWavelength -= this.StepSize;
            }
        }

        /// <summary>
        /// This method stretches the end to the new wavelength specified. The values at the current end are duplicated if a new value is not supplied.
        /// </summary>
        /// <param name="newEnd">The new end of the spectrum. Should be more than the current end.</param>
        public void StretchEndWavelength(int newEnd)
        {
            if (newEnd <= this.HighestWavelength)
            {
                string error = string.Format("Specified new end ({0}) is higher than current end ({1})", newEnd, this.HighestWavelength);
                throw new InvalidOperationException(error);
            }

            // Make sure that the newEnd value makes sense with our current HighestWavelength and stepSize values.
            //
            int difference = newEnd - this.HighestWavelength;
            if (difference % this.StepSize != 0)
            {
                throw new InvalidOperationException("The new requested start does not fall in line with sampling pattern");
            }

            // Sample the current highest wavelength's value. We will use this to fil in new values.
            float valueToInsert = 0f;//  this.WaveData[this.WaveData.Count - 1];

            // Should be ok to stretch now.
            //
            int numberOfElements = difference / this.StepSize;
            int i;
            for (i = 0; i < numberOfElements; i++)
            {
                this.WaveData.Insert(0, valueToInsert);
                this.HighestWavelength += this.StepSize;
            }
        }

        public override string ToString()
        {
            return string.Format("Low: {0}, High:{1}, Step:{2}, \nData:{3}",
                this.LowestWavelength,
                this.HighestWavelength,
                this.StepSize,
                this.WaveData);
        }
    }
}
