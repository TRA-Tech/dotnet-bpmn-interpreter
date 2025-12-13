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
    /// <summary>
    /// Provides extension methods for BPMN elements.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Retrieves the boundary events attached to the specified BPMN element.
        /// </summary>
        /// <param name="element">The BPMN element to check for attached boundary events.</param>
        /// <param name="boundaryEvents">The collection of boundary events to search within.</param>
        /// <returns>A list of boundary events attached to the element, or null if no boundary events are provided.</returns>
        public static List<BpmnSequenceElement>? GetBoundaries(this BpmnElement element, IEnumerable<BpmnSequenceElement>? boundaryEvents = null)
        {
            return boundaryEvents?.Where(w => w.Self.Attributes().Any(w => w.Name == "attachedToRef" && w.Value == element.Id)).ToList();
        }
    }
}
