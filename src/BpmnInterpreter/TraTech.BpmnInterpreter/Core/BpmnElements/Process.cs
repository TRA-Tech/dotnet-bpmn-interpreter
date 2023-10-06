using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnElements
{
    public class Process : BpmnElement
    {
        public static readonly string ElementTypeName = "process";

        public Process(XElement self) : base(self)
        {

        }
    }
}
