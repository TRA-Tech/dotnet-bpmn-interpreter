﻿using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.SequenceElements
{
    public class EndEvent : BpmnSequenceElement
    {
        public static readonly string ElementTypeName = "endEvent";

        public EndEvent(XElement self, IEnumerable<BpmnSequenceElement>? nextElements = null) : base(self, null, nextElements)
        {

        }
    }
}
