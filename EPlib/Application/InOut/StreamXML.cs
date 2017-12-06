using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

using EPlib.Drawable;
using EPlib.Util;
using System.Windows.Media;

namespace EPlib.Application.InOut
{
    public class StreamXML
    {
        public static void WriteXMLIElements(string path, List<UIElement> elements)
        {
            if (path != null)
            {
                List<SerialIE> outList = ToSerialIE(elements);

                // Writer object
                XmlSerializer writer = new XmlSerializer(typeof(List<SerialIE>));

                FileStream f = File.Create(path);

                writer.Serialize(f, outList);
                f.Close();
            }
        }

        public static List<SerialIE> ReadXMLIElements(string path)
        {
            if (path != null)
            {
                XmlSerializer reader = new XmlSerializer(typeof(List<SerialIE>));

                StreamReader sr = new StreamReader(path);
                List<SerialIE> temp = (List<SerialIE>)reader.Deserialize(sr);
                return temp;
            }
            return null;
        }

        public static SerialIE ToSerialIE(UIElement element)
        {
            var IE = (InteractiveElement)element;
            return new SerialIE
            {
                IsIcon = IE.IsIcon,
                ElementType = IE.GetThisType,
                Scale = IE.GetScale,
                Point = IE.GetPoint,
                Stroke = ColorHelper.ExtractColor(IE.GetStroke),
                Fill = ColorHelper.ExtractColor(IE.GetFill),
                Count = IE.GetCount,
                Name = IE.GetName
            };
        }

        public static List<SerialIE> ToSerialIE(List<UIElement> elementList)
        {
            List<SerialIE> ProcessList = new List<SerialIE>();

            foreach (var ui in elementList)
            {
                ProcessList.Add(ToSerialIE(ui));
            }

            return ProcessList;
        }
    }
}
