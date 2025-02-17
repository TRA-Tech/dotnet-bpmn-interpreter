using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class BoundaryEvent : BpmnSequenceElement
    {
        public static readonly string ElementTypeName = "boundaryEvent";
        public string AttachedToRef { get; private set; }
        public BoundaryEvent(
            XElement self,
            IEnumerable<BpmnSequenceElement>? previousElements = null,
            IEnumerable<BpmnSequenceElement>? nextElements = null
        ) : base(self, previousElements, nextElements)
        {
            AttachedToRef = self.Attribute("attachedToRef")?.Value ?? throw new InvalidOperationException("self has no dataObjectRef attribute");
        }
    }
}
