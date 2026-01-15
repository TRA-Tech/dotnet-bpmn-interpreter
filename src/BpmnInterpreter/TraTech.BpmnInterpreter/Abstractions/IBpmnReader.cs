using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    /// <summary>
    /// Defines a contract for reading BPMN elements from a BPMN XML document.
    /// </summary>
    public interface IBpmnReader
    {
        /// <summary>
        /// Reads BPMN elements from the specified XML document.
        /// </summary>
        /// <param name="bpmnDocument">The XML document containing the BPMN definition.</param>
        /// <returns>The BPMN elements parsed from the document.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bpmnDocument"/> is <see langword="null"/>.</exception>
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument);
    }
}
