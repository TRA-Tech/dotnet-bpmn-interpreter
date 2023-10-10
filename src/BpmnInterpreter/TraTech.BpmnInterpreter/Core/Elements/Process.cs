using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class Process : BpmnElement
    {
        public static readonly string ElementTypeName = "process";

        public Process(XElement self) : base(self)
        {

        }
    }
}
