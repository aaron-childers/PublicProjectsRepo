using System.IO;
using System.Xml.Linq;

namespace MouseShaker.Config
{
    public class MouseShakeConfig
    {
        public int ShakeDistance { get; set; }
        public int ShakeDurationMilliseconds { get; set; }
        public int IntervalMilliseconds { get; set; }

        // Load configuration from XML file in 'Config' Folder
        public static MouseShakeConfig Load(string filePath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(baseDirectory, "Config", filePath);

            if (!File.Exists(configPath))
                throw new FileNotFoundException("Configuration file not found.", configPath);

            var doc = XDocument.Load(configPath);
            return new MouseShakeConfig
            {
                ShakeDistance = int.Parse(doc.Root.Element("ShakeDistance").Value),
                ShakeDurationMilliseconds = int.Parse(doc.Root.Element("ShakeDurationMilliseconds").Value),
                IntervalMilliseconds = int.Parse(doc.Root.Element("IntervalMilliseconds").Value),
            };
        }

        // Save configuration to XML file
        public void Save(string filePath)
        {
            var doc = new XDocument(
                new XElement("MouseShakerConfig",
                    new XElement("ShakeDistance", ShakeDistance),
                    new XElement("ShakeDurationMilliseconds", ShakeDurationMilliseconds),
                    new XElement("IntervalMilliseconds", IntervalMilliseconds)
                )
            );
            doc.Save(filePath);
        }
    }
}