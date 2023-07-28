using BpmnInterpreter.Core.BpmnElements;

namespace BpmnInterpreter.Abstractions
{
    public interface IBpmnReader
    {
        public void Load(Stream bpmnStream);
        public IEnumerable<BpmnElement> Read();
    }
}
