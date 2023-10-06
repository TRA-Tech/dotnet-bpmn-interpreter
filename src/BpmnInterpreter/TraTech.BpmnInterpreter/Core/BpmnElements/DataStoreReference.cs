using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.BpmnElements
{
    public class DataStoreReference : BpmnElement
    {
        public static readonly string ElementTypeName = "dataStoreReference";

        public DataStoreReference(XElement self) : base(self)
        {

        }
    }
}
