using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetamerCalculator
{
    /// <summary>
    /// DATA MODEL:
    /// This class simply defines some string constants to help make reading our XML files a little bit
    /// easier and more consistent.
    /// </summary>
    abstract class XMLDataConstants
    {
        // Common
        public const string Name = @"Name";
        public const string WaveData = @"WaveData";
        public const string Start = @"Start";
        public const string End = @"End";
        public const string Step = @"Step";

        public const string LightSources = @"LightSources";
        public const string LightSource = @"LightSource";
        public const string Materials = @"Materials";
        public const string Material = @"Material";
        public const string Observers = @"Observers";
        public const string Observer = @"Observer";
        public const string Channels = @"Channels";
        public const string Channel = @"Channel";
        public const string Count = @"Count";
    }
}
