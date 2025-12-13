using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Reads BPMN elements from an XML document.
    /// </summary>
    public class BpmnReader : IBpmnReader
    {
        /// <summary>
        /// The XML namespace for the BPMN elements.
        /// </summary>
        public readonly XNamespace BpmnNameSpace;

        /// <summary>
        /// Initializes a new instance of the <see cref="BpmnReader"/> class.
        /// </summary>
        /// <param name="bpmnNameSpace">The XML namespace for the BPMN elements.</param>
        public BpmnReader(XNamespace bpmnNameSpace)
        {
            BpmnNameSpace = bpmnNameSpace;
        }

        /// <summary>
        /// Reads BPMN elements from the specified XML document.
        /// </summary>
        /// <param name="bpmnDocument">The XML document containing the BPMN definition.</param>
        /// <returns>A collection of BPMN elements parsed from the document.</returns>
        /// <exception cref="NullReferenceException">Thrown when the process element is not found in the document.</exception>
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument)
        {
            var processElement = bpmnDocument.Root?.Element(BpmnNameSpace.GetName(Process.ElementTypeName));

            if (processElement == null) throw new NullReferenceException(nameof(processElement));

            List<BpmnElement> bpmnElementList = new()
            {
                new BpmnElement(processElement)
            };

            foreach (XElement element in processElement.Elements())
            {
                bpmnElementList.AddRange(
                    element.DescendantsAndSelf()
                        .Where(x => x.Attribute("id") != null)
                        .Select(s => new BpmnElement(s))
                );
            }

            return bpmnElementList;
        }
    }
}
