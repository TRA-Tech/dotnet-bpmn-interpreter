using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Reads BPMN elements from a BPMN XML document.
    /// </summary>
    public class BpmnReader : IBpmnReader
    {
        /// <summary>
        /// The XML namespace used for BPMN elements.
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
        /// <param name="bpmnDocument">The BPMN XML document.</param>
        /// <returns>
        /// A collection containing the <c>process</c> element itself and the elements under it that have an <c>id</c> attribute.
        /// </returns>
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument)
        {
            var processElement = bpmnDocument.Root?.Element(BpmnNameSpace.GetName(Process.ElementTypeName));

            if (processElement == null) throw new NullReferenceException(nameof(processElement));

            List<BpmnElement> bpmnElementList =
            [
                new BpmnElement(processElement)
            ];

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
