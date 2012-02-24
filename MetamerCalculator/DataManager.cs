using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL:
    /// This class manages the data objects. 
    /// </summary>
    class DataManager
    {
        public List<Material> Materials { get; set; }
        public List<Observer> Observers { get; set; }
        public List<LightSource> LightSources { get; set; }

        /// <summary>
        /// Default constructor. Creates empty Lists for members.
        /// </summary>
        public DataManager()
        {
            Materials = new List<Material>();
            Observers = new List<Observer>();
            LightSources = new List<LightSource>();
        }

        /// <summary>
        /// Searches the internal list of Observers to find a match to the given name. The match is partial and the name
        /// does not have to be exact. The first found instance will be returned.
        /// </summary>
        /// <param name="partialName">String that should be matched to the name string of the observer.</param>
        /// <returns>The first matching observer. If no match is found then an exception will be thrown.</returns>
        public Observer GetObserverByPartialName(string partialName)
        {
            return Observers.Find(delegate(Observer obs)
            {
                return obs.Name.Contains(partialName);
            });
        }

        /// <summary>
        /// Searches the internal list of Light sources to find a match to the given name. The match is partial and the name
        /// does not have to be exact. The first found instance will be returned.
        /// </summary>
        /// <param name="partialName">String that should be matched to the name string of the light source.</param>
        /// <returns>The first matching observer. If no match is found then an exception will be thrown.</returns>
        public LightSource GetLightSourceByPartialName(string partialName)
        {
            return LightSources.Find( delegate(LightSource l) 
            {
                    return l.Name.Contains(partialName);
            });
        }

        /// <summary>
        /// Searches the internal list of materials to find a match to the given name. The match is partial and the name
        /// does not have to be exact. The first found instance will be returned.
        /// </summary>
        /// <param name="partialName">String that should be matched to the name string of the material.</param>
        /// <returns>The first matching observer. If no match is found then an exception will be thrown.</returns>
        public Material GetMaterialByPartialName(string partialName)
        {
            return Materials.Find(delegate(Material mat)
            {
                return mat.Name.Contains(partialName);
            });
        }

        /// <summary>
        /// This method reads in all the default spectral data files and populates light sources, observers
        /// and material libraries.
        /// </summary>
        public void InitializeData()
        {
            ReadInLights(@"Data\LightSources.xml");
            ReadInMaterials(@"Data\Materials.xml");
            ReadInObservers(@"Data\Observers.xml");
        }

        /// <summary>
        /// Read in all the light sources from the given file.
        /// </summary>
        /// <param name="fileName">Path to the data file.</param>
        protected void ReadInLights(string fileName)
        {
            XmlDocument lightSourcesXml = new XmlDocument();
            lightSourcesXml.Load(fileName);

            foreach (XmlElement lightSourceXML in lightSourcesXml.GetElementsByTagName(XMLDataConstants.LightSource))
            {
                LightSource lightSource = new LightSource();
                lightSource.Initialize(lightSourceXML);
                this.LightSources.Add(lightSource);
            }
        }

        /// <summary>
        /// Read in all the materials from the given file.
        /// </summary>
        /// <param name="fileName">Path to the data file.</param>
        protected void ReadInMaterials(string fileName)
        {
            XmlDocument materialsXML = new XmlDocument();
            materialsXML.Load(fileName);

            foreach (XmlElement materialXML in materialsXML.GetElementsByTagName(XMLDataConstants.Material))
            {
                Material material = new Material();
                material.Initialize(materialXML);
                this.Materials.Add(material);
            }
        }

        /// <summary>
        /// Read in all the observers from the given file.
        /// </summary>
        /// <param name="fileName">Path to the data file.</param>
        protected void ReadInObservers(string fileName)
        {
            XmlDocument observersXML = new XmlDocument();
            observersXML.Load(fileName);

            foreach (XmlElement observerXML in observersXML.GetElementsByTagName(XMLDataConstants.Observer))
            {
                Observer observer = new Observer();
                observer.Initialize(observerXML);
                this.Observers.Add(observer);
            }
        }
    }
}
