using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class DataStoreReference : BpmnElement
    {
        public static readonly string ElementTypeName = "dataStoreReference";

        public DataStoreReference(XElement self) : base(self)
        {

        }
    }
}
