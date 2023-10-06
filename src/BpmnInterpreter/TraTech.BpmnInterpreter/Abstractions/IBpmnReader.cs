using TraTech.BpmnInterpreter.Core.BpmnElements;

namespace TraTech.BpmnInterpreter.Abstractions
{
    public interface IBpmnReader
    {
        public void Load(Stream bpmnStream);
        public IEnumerable<BpmnElement> Read();
    }
}
