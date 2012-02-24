using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MetamerCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager datamanager = new DataManager();
            datamanager.InitializeData();

            // Select a starting material/observer/light set.
            Material material = datamanager.GetMaterialByPartialName("Lime Green");
            Observer observer = datamanager.GetObserverByPartialName("1964");
            LightSource lightsource = datamanager.GetLightSourceByPartialName("SP35");

            Console.WriteLine("Material selected is: " + material);
            Console.WriteLine("Observer selected is: " + observer);
            Console.WriteLine("Light source selected is: " + lightsource);
            
            Utilities.CreateMetamericMaterialSmart(lightsource, material, observer);

            // Wait for me to type something before closing the console window.
            Console.ReadKey();
        }
    }
}
