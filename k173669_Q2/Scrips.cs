using System;
using System.IO;
using System.Xml.Serialization;

namespace k173669_Q2
{
    [Serializable]
    public class Scrips
    {
        public string Category { get; set; }

        public string Scrip { get; set; }

        public double Price { get; set; }

        public void Serialize(string filename)
        {
            using FileStream fileStream = new FileStream(filename, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(Scrips));
            serializer.Serialize(fileStream, this);
        }

        public static Scrips Deserialize(string filename)
        {
            using FileStream fileStream = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Scrips));
            Scrips stock = (Scrips)serializer.Deserialize(fileStream);
            return stock;
        }

    }

}
