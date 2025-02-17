using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Extensions
{
    public static class ElementExtensions
    {
        public static List<BpmnSequenceElement>? GetBoundaries(this BpmnElement element, IEnumerable<BpmnSequenceElement>? boundaryEvents = null)
        {
            return boundaryEvents?.Where(w => w.Self.Attributes().Any(w => w.Name == "attachedToRef" && w.Value == element.Id)).ToList();
        }
    }
}
