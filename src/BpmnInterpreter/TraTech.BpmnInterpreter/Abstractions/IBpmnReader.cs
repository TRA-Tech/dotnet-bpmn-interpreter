using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.Elements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface IBpmnReader
    {
        public IEnumerable<BpmnElement> Read(XDocument bpmnDocument);
    }
}
