using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements
{
    public class DataObject : BpmnElement
    {
        public static readonly string ElementTypeName = "dataObject";

        public DataObject(XElement self) : base(self)
        {

        }
    }
}
