using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Reads BPMN elements from the first <c>process</c> element found in a BPMN XML document.
    /// </summary>
    public sealed class BpmnProcessReader : IBpmnReader
    {
        private readonly XNamespace _bpmnNs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BpmnProcessReader"/> class.
        /// </summary>
        /// <param name="bpmnNs">The XML namespace used for BPMN elements.</param>
        public BpmnProcessReader(XNamespace bpmnNs)
        {
            _bpmnNs = bpmnNs;
        }

        /// <summary>
        /// Reads BPMN elements from the given document.
        /// </summary>
        /// <param name="document">The BPMN XML document.</param>
        /// <returns>
        /// A collection containing the <c>process</c> element itself and its direct child elements that have an <c>id</c> attribute.
        /// </returns>
        /// <remarks>
        /// Only the first <c>process</c> element in the document is considered.
        /// </remarks>
        public IEnumerable<BpmnElement> Read(XDocument document)
        {
            var processElement = document
                .Descendants(_bpmnNs.GetName(Process.ElementTypeName))
                .FirstOrDefault();

            if (processElement is null)
            {
                throw new NullReferenceException(nameof(processElement));
            }

            var bpmnElements = new List<BpmnElement>
            {
                new(processElement)
            };

            var childElements = processElement
                    .Elements()
                    .Where(x => x.Attribute("id") != null)
                    .Select(s => new BpmnElement(s));

            bpmnElements.AddRange(childElements);

            return bpmnElements;
        }
    }
}
