using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Domain.Model;

namespace Web.Service
{
    public class StreamGenerator
    {
        public string GenerateStream(XmlNode xmlNode, Delimiter delimiter)
        {
            XmlNodeList nodeList = xmlNode.SelectNodes("*");
            return GenerateStream(nodeList, delimiter);
        }
        public string GenerateStream(XmlNodeList xmlNode, Delimiter delimiter)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XmlNode node in xmlNode)
            {
                if (node.NodeType == XmlNodeType.Element && node.Attributes.Count > 0)
                {
                    stringBuilder.Append(NodeToString(node, delimiter));
                }
                if (node.NodeType == XmlNodeType.Element && node.ChildNodes.Count > 0)
                {
                    stringBuilder.Append(GenerateStream(node.ChildNodes, delimiter));
                }
            }
            return stringBuilder.ToString();
        }
        private string NodeToString(XmlNode node, Delimiter delimiter)
        {
            if (node.Attributes.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder segement = new StringBuilder();
            string nextattribName = string.Empty;
            string nodeName = node.Name;
            int nodeNameLength = nodeName.Length;
            string nextNodeElementPos = string.Empty;
            string nodeElementPos = string.Empty;
            for (int i = 1; i < node.Attributes.Count - 1; i++)
            {
                nextattribName = node.Attributes[i + 1].Name;
                //Using This as there is no more then 99 elements in a segement
                nodeElementPos = node.Attributes[i].Name.Substring(nodeNameLength, 2);
                nextNodeElementPos = nextattribName.Substring(nodeNameLength, 2);
                if (node.Attributes[i].Name.Contains("-") && (nodeElementPos == nextNodeElementPos))
                {
                    segement.AppendFormat("{0}{1}", node.Attributes[i].Value, delimiter.CompositeSeperator);
                }
                else
                {
                    segement.AppendFormat("{0}{1}", node.Attributes[i].Value, delimiter.ElementSeperator);
                }
            }
            segement.AppendFormat("{0}{1}", node.Attributes[node.Attributes.Count - 1].Value, delimiter.SegementSeperator);
            return segement.ToString();
        }
    }
}