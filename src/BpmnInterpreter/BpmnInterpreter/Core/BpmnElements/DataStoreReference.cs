using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements
{
    public class DataStoreReference : BpmnElement
    {
        public static readonly string ElementTypeName = "dataStoreReference";

        public DataStoreReference(XElement self) : base(self)
        {

        }
    }
}
