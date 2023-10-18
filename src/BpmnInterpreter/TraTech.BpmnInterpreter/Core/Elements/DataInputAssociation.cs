using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class DataInputAssociation : BpmnElement
    {
        public static readonly string ElementTypeName = "dataInputAssociation";

        private readonly string sourceRef = null!;
        private readonly string targetRef = null!;

        public string SourceRef { get => sourceRef; }
        public string TargetRef { get => targetRef; }

        public DataInputAssociation(XElement self) : base(self)
        {
            sourceRef = self.Element(Namespace.GetName(nameof(sourceRef)))?.Value ?? throw new InvalidOperationException($"self has no {nameof(sourceRef)} element");
            targetRef = self.Element(Namespace.GetName(nameof(targetRef)))?.Value ?? throw new InvalidOperationException($"self has no {nameof(targetRef)} element");
        }
    }
}
