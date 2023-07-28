using BpmnInterpreter.Abstractions;
using BpmnInterpreter.Core.BpmnElements;
using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnReaders
{
    public class BpmnProcessReader : IBpmnReader
    {
        private readonly XNamespace _bpmnNameSpace;

        private XDocument? _doc;

        private XElement? _processElement;


        public BpmnProcessReader(XNamespace BpmnNameSpace)
        {
            _bpmnNameSpace = BpmnNameSpace;
        }

        public void Load(Stream bpmnStream)
        {
            _doc = XDocument.Load(bpmnStream);
            _processElement = _doc.Root?.Element(_bpmnNameSpace.GetName("process"))
                ?? throw new NullReferenceException("no root or process element");
        }

        public IEnumerable<BpmnElement> Read()
        {
            if (_processElement == null) throw new NullReferenceException(nameof(_processElement));

            List<BpmnElement> bpmnElementList = new()
            {
                new BpmnElement(_processElement)
            };

            foreach (XElement element in _processElement.Elements())
            {
                bpmnElementList.AddRange(
                    element.DescendantsAndSelf()
                        .TakeWhile(x => x.Attribute("id") != null)
                        .Select(s => new BpmnElement(s))
                );
            }

            return bpmnElementList;
        }
    }
}
