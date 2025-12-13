using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="XElement"/> to facilitate BPMN parsing.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Retrieves the outgoing sequence flow IDs from the specified XML element.
        /// </summary>
        /// <param name="xElement">The XML element representing a BPMN node.</param>
        /// <param name="xNamespace">The XML namespace to use. If null, the namespace of the element is used.</param>
        /// <returns>A collection of outgoing sequence flow IDs.</returns>
        public static IEnumerable<string> GetOutgoings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("outgoing"))
                .Select(outgoing => outgoing.Value);
        }

        /// <summary>
        /// Retrieves the incoming sequence flow IDs from the specified XML element.
        /// </summary>
        /// <param name="xElement">The XML element representing a BPMN node.</param>
        /// <param name="xNamespace">The XML namespace to use. If null, the namespace of the element is used.</param>
        /// <returns>A collection of incoming sequence flow IDs.</returns>
        public static IEnumerable<string> GetIncomings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("incoming"))
                .Select(incoming => incoming.Value);
        }
    }
}
