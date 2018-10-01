//using System.Xml;

namespace PipView.Extensions
{
    public static class Extensions
    {
        public static bool EqualsArray(this byte[] myArray, byte[] otherArray)
        {
            if (myArray.Length == otherArray.Length)
            {
                for (int pos = 0; pos < myArray.Length; pos++)
                {
                    if (myArray[pos] != otherArray[pos])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /*public static void AppendTextChild(this XmlElement parent, string Name, string Value)
        {
            XmlDocument doc = parent.OwnerDocument;

            XmlElement node = doc.CreateElement(Name);
            node.AppendChild(doc.CreateTextNode(Value));

            parent.AppendChild(node);
        }*/
    }
}
