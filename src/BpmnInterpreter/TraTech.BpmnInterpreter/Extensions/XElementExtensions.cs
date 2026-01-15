using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="XElement"/> that assist BPMN parsing.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Retrieves the outgoing sequence flow IDs from the specified BPMN XML element.
        /// </summary>
        /// <param name="xElement">The XML element representing a BPMN node.</param>
        /// <param name="xNamespace">The XML namespace to use. If <see langword="null"/>, the namespace of <paramref name="xElement"/> is used.</param>
        /// <returns>The values of all <c>outgoing</c> elements under <paramref name="xElement"/>.</returns>
        public static IEnumerable<string> GetOutgoings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("outgoing"))
                .Select(outgoing => outgoing.Value);
        }

        /// <summary>
        /// Retrieves the incoming sequence flow IDs from the specified BPMN XML element.
        /// </summary>
        /// <param name="xElement">The XML element representing a BPMN node.</param>
        /// <param name="xNamespace">The XML namespace to use. If <see langword="null"/>, the namespace of <paramref name="xElement"/> is used.</param>
        /// <returns>The values of all <c>incoming</c> elements under <paramref name="xElement"/>.</returns>
        public static IEnumerable<string> GetIncomings(this XElement xElement, XNamespace? xNamespace = null)
        {
            XNamespace _xNamespace = xNamespace ?? xElement.Name.Namespace;
            return xElement
                .Elements(_xNamespace.GetName("incoming"))
                .Select(incoming => incoming.Value);
        }
    }
}
