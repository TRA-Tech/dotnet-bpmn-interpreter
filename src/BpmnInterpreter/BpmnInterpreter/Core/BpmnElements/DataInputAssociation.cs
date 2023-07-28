﻿using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements
{
    public class DataInputAssociation : BpmnElement
    {
        public static readonly string ElementTypeName = "dataInputAssociation";

        public string SourceRef { get; private set; }
        public string TargetRef { get; private set; }

        public DataInputAssociation(XElement self) : base(self)
        {
            SourceRef = self.Attribute("sourceRef")?.Value ?? throw new InvalidOperationException("self has no sourceRef attribute");
            TargetRef = self.Attribute("targetRef")?.Value ?? throw new InvalidOperationException("self has no targetRef attribute");
        }
    }
}
