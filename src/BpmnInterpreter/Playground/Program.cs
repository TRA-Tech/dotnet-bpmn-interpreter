using System.Text;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core;
using TraTech.BpmnInterpreter.Core.Elements;
using TraTech.BpmnInterpreter.Core.EventDefinitions;
using TraTech.BpmnInterpreter.Core.SequenceElements;
using BpmnSequenceElements = TraTech.BpmnInterpreter.Core.SequenceElements;

namespace Playground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<semantic:definitions xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:semantic=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:tra=""http://tra"" xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"" id=""_1275940932088"" targetNamespace=""http://www.trisotech.com/definitions/_1275940932088"" exporter=""Camunda Modeler"" exporterVersion=""1.16.0"">
  <semantic:message id=""_1275940932310"" />
  <semantic:message id=""_1275940932433"" />
  <semantic:message id=""_1275940932198"" />
  <semantic:process id=""Process_0pbbn3n"">
    <semantic:startEvent id=""Event_0yh5iyc"" />
    <semantic:startEvent id=""Event_10durh9"">
      <semantic:messageEventDefinition id=""MessageEventDefinition_1yt1l3a"" />
    </semantic:startEvent>
    <semantic:startEvent id=""Event_07a52kb"">
      <semantic:timerEventDefinition id=""TimerEventDefinition_11hhiqh"" />
    </semantic:startEvent>
    <semantic:startEvent id=""Event_02c4dmy"">
      <semantic:conditionalEventDefinition id=""ConditionalEventDefinition_03qoudr"">
        <semantic:condition xsi:type=""semantic:tFormalExpression"" />
      </semantic:conditionalEventDefinition>
    </semantic:startEvent>
    <semantic:startEvent id=""Event_0q94alq"">
      <semantic:signalEventDefinition id=""SignalEventDefinition_0a6vsz8"" />
    </semantic:startEvent>
  </semantic:process>
</semantic:definitions>


";

            var bpmnProcessReader = new BpmnReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                var bpmnDocument = XDocument.Load(ms);
                bpmnElements = bpmnProcessReader.Read(bpmnDocument);
            }


            foreach (var element in bpmnElements.Where(f => f.Type == StartEvent.ElementTypeName))
            {
                Console.Out.Write("element.HasEventDefinitions: ");
                Console.Out.WriteLine(element.HasEventDefinitions);

                Console.Out.Write("element.HasEventDefinitionOf(TimerEventDefinition.EventDefinitionTypeName): ");
                Console.Out.WriteLine(element.HasEventDefinitionOf(TimerEventDefinition.EventDefinitionTypeName));

                foreach (var eventDefinition in element.EventDefinitions)
                    Console.Out.Write(eventDefinition.Name.LocalName + ", ");
                Console.Out.WriteLine();

                if (element.HasEventDefinitionOf(TimerEventDefinition.EventDefinitionTypeName))
                {
                    var timerEventDefinition = element.ParseEventDefinition<TimerEventDefinition>();

                }
                Console.Out.WriteLine();
                Console.Out.WriteLine("------------------------------------");
                Console.Out.WriteLine();
            }
        }
    }
}