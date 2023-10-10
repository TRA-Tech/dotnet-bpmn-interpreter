using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class DataObject : BpmnElement
    {
        public static readonly string ElementTypeName = "dataObject";

        public DataObject(XElement self) : base(self)
        {

        }
    }
}
