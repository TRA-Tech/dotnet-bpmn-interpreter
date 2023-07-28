﻿using System.Xml.Linq;

namespace BpmnInterpreter.Core.BpmnElements.SequenceElements
{
    public class EndEvent : BpmnSequenceElement
    {
        public static readonly string ElementTypeName = "endEvent";

        public EndEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
