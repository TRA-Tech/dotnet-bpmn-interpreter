using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface IReader
    {
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument);
    }
}
