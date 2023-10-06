using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core.BpmnElements;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnReaders
{
    public class BpmnProcessReader : IBpmnReader
    {
        public readonly XNamespace BpmnNameSpace;

        public BpmnProcessReader(XNamespace bpmnNameSpace)
        {
            BpmnNameSpace = bpmnNameSpace;
        }

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
