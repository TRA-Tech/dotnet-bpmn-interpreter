using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for reading BPMN elements from an XML document.
    /// </summary>
    public interface IBpmnReader
    {
        /// <summary>
        /// Reads BPMN elements from the specified XML document.
        /// </summary>
        /// <param name="bpmnDocument">The XML document containing the BPMN definition.</param>
        /// <returns>A collection of BPMN elements parsed from the document.</returns>
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument);
    }
}
