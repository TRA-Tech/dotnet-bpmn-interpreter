using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a BPMN process element.
    /// </summary>
    public class Process : BpmnElement
    {
        /// <summary>
        /// The BPMN element type name for a process.
        /// </summary>
        public static readonly string ElementTypeName = "process";

        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the process.</param>
        public Process(XElement self) : base(self)
        {

        }
    }
}
