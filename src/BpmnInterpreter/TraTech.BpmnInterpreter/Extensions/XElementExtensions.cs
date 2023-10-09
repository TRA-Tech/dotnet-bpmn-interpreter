using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Extensions
{
    public static class XElementExtensions
    {
        public static IEnumerable<string> GetOutgoings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("outgoing"))
                .Select(outgoing => outgoing.Value);
        }

        public static IEnumerable<string> GetIncomings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("incoming"))
                .Select(outgoing => outgoing.Value);
        }
    }
}
