using Playground.ElementHandlers;
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
    public class Data
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

    }

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
    <semantic:startEvent id=""Event_1rfnzok"" name=""START"">
      <semantic:outgoing>Flow_0kszezh</semantic:outgoing>
    </semantic:startEvent>
    <semantic:task id=""Activity_17vne42"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_0kszezh</semantic:incoming>
      <semantic:outgoing>Flow_1nvb38w</semantic:outgoing>
    </semantic:task>
    <semantic:endEvent id=""Event_1y9un6p"">
      <semantic:incoming>Flow_1nvb38w</semantic:incoming>
    </semantic:endEvent>
    <semantic:sequenceFlow id=""Flow_0kszezh"" sourceRef=""Event_1rfnzok"" targetRef=""Activity_17vne42"" />
    <semantic:sequenceFlow id=""Flow_1nvb38w"" sourceRef=""Activity_17vne42"" targetRef=""Event_1y9un6p"" />
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

            var sequence = new Sequence(bpmnElements);

            var sequenceProcessor = ISequenceProcessorBuilder
                .Create<SequenceProcessorBuilder>()
                .UsingElementHandler(StartEvent.ElementTypeName, new StartEventHandler())
                .UsingElementHandler(BpmnSequenceElements.Task.ElementTypeName, new TaskHandler())
                .UsingElementHandler(EndEvent.ElementTypeName, new EndEventHandler())
                .WithBpmnSequence(sequence)
                .Build<SequenceProcessor>();

            sequenceProcessor.Start();
        }
    }
}