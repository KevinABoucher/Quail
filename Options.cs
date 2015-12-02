using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Quail
{
    /// <summary>
    /// Options class which will be serialized.
    /// </summary>
    [XmlRoot("Options")]
    public class MyOptions
    {
        [XmlElement("Option")]
        public Option option;

        [XmlElement("Window")]
        public Window window;

        private ArrayList mHighlightItems;

        public MyOptions()
        {
            option = new Option();
            window = new Window();
            mHighlightItems = new ArrayList();
        }

        [XmlElement("HighlightItem")]
        public HighlightItem[] HighlightItems
        {
            get
            {
                HighlightItem[] items = new HighlightItem[mHighlightItems.Count];
                mHighlightItems.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                HighlightItem[] items = (HighlightItem[])value;
                mHighlightItems.Clear();
                foreach (HighlightItem item in items)
                    mHighlightItems.Add(item);
            }
        }

        public int AddItem(HighlightItem item)
        {
            return mHighlightItems.Add(item);
        }

        public bool Save()
        {
            // Serialization
            XmlSerializer s = new XmlSerializer(typeof(MyOptions));
            string configFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            configFile = configFile.Substring(0, configFile.LastIndexOf(@"\")) + @"\config\user_options.xml";
            TextWriter w = new StreamWriter(configFile);
            s.Serialize(w, this);
            w.Close();
            return true;
        }

        public static MyOptions Load()
        {
            // Deserialization
            XmlSerializer s = new XmlSerializer(typeof(MyOptions));
            MyOptions opt;
            string assFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string configFile = assFile.Substring(0, assFile.LastIndexOf(@"\")) + @"\config\user_options.xml";
            if (!File.Exists(configFile))
            {
                configFile = assFile.Substring(0, assFile.LastIndexOf(@"\")) + @"\config\options.xml";
            }

            TextReader r = new StreamReader(configFile);
            opt = (MyOptions)s.Deserialize(r);

            r.Close();
            return opt;
        }
    }

    public class HighlightItem
    {
        [XmlElement("ContainsString")]
        public string ContainsString;

        [XmlElement("HighlightColor")]
        public string HiglightColor;

        public HighlightItem()
        {
        }

        public HighlightItem(string containsString, string color)
        {
            ContainsString = containsString;
            HiglightColor = color;
        }
    }

    // Window
    public class Window
    {
        [XmlElement("Left")]
        public int Left = 0;

        [XmlElement("Width")]
        public int Width = 640;

        [XmlElement("Top")]
        public int Top = 0;

        [XmlElement("Height")]
        public int Height = 480;
    }

    // Option
    public class Option
    {
        [XmlElement("DefaultLogLocation")]
        public string DefaultLogLocation = @"c:\temp\log.txt";

        [XmlElement("AutoLoadLog")]
        public bool AutoLoadLog = false;

        [XmlElement("AutoPause")]
        public bool AutoPause = true;

        [XmlElement("LogEditCommand")]
        public string LogEditCommand = @"notepad.exe {0}";

        [XmlElement("TailTimeout")]
        public int TailTimeout = 1000;

        [XmlElement("MaxBytesToLoad")]
        public long MaxBytesToLoad = 100000000;

        [XmlElement("BackgroundColor")]
        public string BackgroundColor = "WhiteSmoke";

        [XmlElement("FilterColor")]
        public string FilterColor = "Teal";

        [XmlElement("DefaultColor")]
        public string DefaultColor = "Black";

        [XmlElement("BorderColor")]
        public string BorderColor = "WhiteSmoke";

        [XmlElement("TimeFormat")]
        public string TimeFormat = "MM/dd/yyyy HH:mm:ss";

        public Option()
        {
        }
    }
}


