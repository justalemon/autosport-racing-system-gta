using System.Xml;

namespace ARS
{
    public static class XmlUtils
    {
        public static bool NodeExists(XmlNode node, string name)
        {
            return node.SelectSingleNode(name) != null;
        }
        public static XmlElement GetChild(XmlNode node, string name)
        {
            XmlNode n = node.SelectSingleNode(name);
            if (n == null) return null;
            else return n as XmlElement;
        }
        public static string GetAttribute(XmlElement node, string name)
        {
            if (node == null) return "";
            if (node.HasAttribute(name)) return node.GetAttribute(name);
            return "";
        }
    }
}
