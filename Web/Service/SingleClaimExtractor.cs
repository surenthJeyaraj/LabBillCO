using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Domain.Model;

namespace Web.Service
{
    public class SingleClaimExtractor
    {
        public string Extract(XmlDocument xmlDocument, string claimID)
        {
            XmlNode XNode = getSingleRawXML(xmlDocument, claimID);
            //Reset the HL values and ISA,GS,ST Control number value as per new XML
            resetHLAndControlNumberValues(XNode);
            //Get the Delimiter Values from the XML
            Delimiter delimiter = getDelimiters(XNode);
            //Trim the delimeters value from the ISA XML Node.
            trimDelimiterAttribute(XNode);
            ////Generate Stream using the Modified Single ClaimXML
            StreamGenerator generator = new StreamGenerator();
            return generator.GenerateStream(XNode, delimiter);
        }

        private XmlNode getSingleRawXML(XmlDocument xmlDocument, string claimID)
        {
            string[] hierarchyArray = new string[] { "PAT_CLAIM", "SUBSCRIB_HL", "BILLING_HL", "TRANSACTION" };
            string xPath = ".//CLM[@CLM01='" + claimID + "']";
            XmlNode node = xmlDocument.SelectSingleNode(xPath);
            while (node != null)
            {
                var currNodeName = node.Name;
                if (node.ParentNode == null)
                    break;
                node = node.ParentNode;
                if (hierarchyArray.Contains(currNodeName))
                {
                    for (int i = node.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        XmlNode PatNode = null;
                        //patient HL and Claim found in same XML hierarchy
                        if (node.ChildNodes[i].Name == currNodeName 
                            && currNodeName == "PAT_CLAIM")
                        {
                            for (int j = i; j >= 0; j--)
                            {
                                PatNode = node.ChildNodes[j].SelectSingleNode(".//PATIENT_HL");
                                if (PatNode != null)
                                {
                                    PatNode = PatNode.Clone();
                                    break;
                                }
                            }
                            if (node.ChildNodes[i].SelectSingleNode(".//PATIENT_HL") == null)
                            {
                                XmlNode clmNode = node.ChildNodes[i].SelectSingleNode(".//CLAIM_LOOP");
                                if (PatNode != null && clmNode != null)
                                {
                                    node.ChildNodes[i].InsertBefore(PatNode, clmNode);
                                }
                            }
                        }

                        if (node.ChildNodes[i].Name == currNodeName
                            && node.ChildNodes[i].SelectSingleNode(xPath) == null)
                        {
                            node.RemoveChild(node.ChildNodes[i]);
                        }
                    }
                }
            }
            return node;
        }
        private void resetHLAndControlNumberValues(XmlNode node)
        {
            var hlNodes = node.SelectNodes("//HL");
            var parentID = string.Empty;
            for (int i = 0; i < hlNodes.Count; i++)
            {
                var HierarchicalIDNumber = i + 1;
                hlNodes[i].Attributes[2].Value = HierarchicalIDNumber.ToString();
                hlNodes[i].Attributes[3].Value = parentID;
                hlNodes[i].Attributes[5].Value = (HierarchicalIDNumber == hlNodes.Count) ? "0" : "1";
                parentID = HierarchicalIDNumber.ToString();
            }
            var count = node.SelectSingleNode(".//TRANSACTION").SelectNodes(".//*[@*]").Count;
            node.SelectSingleNode(".//SE").Attributes[2].Value = count.ToString();
            node.SelectSingleNode(".//GE").Attributes[2].Value = "1";
            node.SelectSingleNode(".//IEA").Attributes[2].Value = "1";
        }
        private Delimiter getDelimiters(XmlNode node)
        {
            Delimiter delimiter = new Delimiter();
            XmlNode ISANode = node.SelectSingleNode(".//ISA");
            delimiter.SegementSeperator = Convert.ToChar(ISANode.Attributes["SEGSEP"].Value);
            delimiter.ElementSeperator = Convert.ToChar(ISANode.Attributes["ELESEP"].Value);
            delimiter.CompositeSeperator = Convert.ToChar(ISANode.Attributes["COMSEP"].Value);
            delimiter.RepetitionSeperator = Convert.ToChar(ISANode.Attributes["REPEATSEP"].Value);
            return delimiter;
        }
        private void trimDelimiterAttribute(XmlNode node)
        {
            string[] attNames = { "SEGSEP", "ELESEP", "COMSEP", "REPEATSEP" };
            XmlNode ISANode = node.SelectSingleNode(".//ISA");
            for (int i = ISANode.Attributes.Count - 1; i >= 0; i--)
            {
                if (attNames.Contains(ISANode.Attributes[i].Name))
                {
                    ISANode.Attributes.Remove(ISANode.Attributes[i]);
                }
            }
        }
    }
}