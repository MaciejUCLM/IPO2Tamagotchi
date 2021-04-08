using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tamagotchi
{
    class Persistence
    {
        private static Persistence instance;
        public static Persistence GetInstance()
        {
            if (instance == null)
                instance = new Persistence();
            return instance;
        }

        private string mPath;

        private Persistence()
        {
            mPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\tamagotchi.xml";
        }

        public T Load<T>()
        {
            T aux;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new System.IO.StreamReader(mPath))
                aux = (T)serializer.Deserialize(stream);
            return aux;
        }

        public void Save<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (var stream = new System.IO.StreamWriter(mPath))
                serializer.Serialize(stream, obj);
        }
    }
}
