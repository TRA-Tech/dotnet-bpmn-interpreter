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
<bpmn:definitions xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:bpmn=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"" id=""Definitions_1sb21a0"" targetNamespace=""http://bpmn.io/schema/bpmn"" exporter=""bpmn-js (https://demo.bpmn.io)"" exporterVersion=""18.6.1"">
  <bpmn:process id=""Process_1dj2y4l"" isExecutable=""false"">
    <bpmn:startEvent id=""StartEvent_1acrn3m"">
      <bpmn:outgoing>Flow_0g8wv39</bpmn:outgoing>
    </bpmn:startEvent>
    <bpmn:endEvent id=""Event_16pofqx"">
      <bpmn:incoming>Flow_1vdwbwx</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:subProcess id=""Activity_10s84on"">
      <bpmn:incoming>Flow_0g8wv39</bpmn:incoming>
      <bpmn:outgoing>Flow_1vdwbwx</bpmn:outgoing>
      <bpmn:startEvent id=""Event_19p8um1"">
        <bpmn:outgoing>Flow_11x64re</bpmn:outgoing>
      </bpmn:startEvent>
      <bpmn:endEvent id=""Event_1cgqww1"">
        <bpmn:incoming>Flow_0h4nf3f</bpmn:incoming>
      </bpmn:endEvent>
      <bpmn:task id=""Activity_13n23id"">
        <bpmn:incoming>Flow_11x64re</bpmn:incoming>
        <bpmn:outgoing>Flow_0h4nf3f</bpmn:outgoing>
      </bpmn:task>
      <bpmn:sequenceFlow id=""Flow_11x64re"" sourceRef=""Event_19p8um1"" targetRef=""Activity_13n23id"" />
      <bpmn:sequenceFlow id=""Flow_0h4nf3f"" sourceRef=""Activity_13n23id"" targetRef=""Event_1cgqww1"" />
    </bpmn:subProcess>
    <bpmn:sequenceFlow id=""Flow_0g8wv39"" sourceRef=""StartEvent_1acrn3m"" targetRef=""Activity_10s84on"" />
    <bpmn:sequenceFlow id=""Flow_1vdwbwx"" sourceRef=""Activity_10s84on"" targetRef=""Event_16pofqx"" />
  </bpmn:process>
  <bpmndi:BPMNDiagram id=""BPMNDiagram_1"">
    <bpmndi:BPMNPlane id=""BPMNPlane_1"" bpmnElement=""Process_1dj2y4l"">
      <bpmndi:BPMNShape id=""_BPMNShape_StartEvent_2"" bpmnElement=""StartEvent_1acrn3m"">
        <dc:Bounds x=""156"" y=""82"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_16pofqx_di"" bpmnElement=""Event_16pofqx"">
        <dc:Bounds x=""702"" y=""72"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_10s84on_di"" bpmnElement=""Activity_10s84on"" isExpanded=""true"">
        <dc:Bounds x=""230"" y=""40"" width=""350"" height=""200"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_19p8um1_di"" bpmnElement=""Event_19p8um1"">
        <dc:Bounds x=""270"" y=""122"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_1cgqww1_di"" bpmnElement=""Event_1cgqww1"">
        <dc:Bounds x=""502"" y=""122"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_13n23id_di"" bpmnElement=""Activity_13n23id"">
        <dc:Bounds x=""350"" y=""100"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_11x64re_di"" bpmnElement=""Flow_11x64re"">
        <di:waypoint x=""306"" y=""140"" />
        <di:waypoint x=""350"" y=""140"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0h4nf3f_di"" bpmnElement=""Flow_0h4nf3f"">
        <di:waypoint x=""450"" y=""140"" />
        <di:waypoint x=""502"" y=""140"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0g8wv39_di"" bpmnElement=""Flow_0g8wv39"">
        <di:waypoint x=""192"" y=""100"" />
        <di:waypoint x=""230"" y=""100"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1vdwbwx_di"" bpmnElement=""Flow_1vdwbwx"">
        <di:waypoint x=""580"" y=""90"" />
        <di:waypoint x=""702"" y=""90"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</bpmn:definitions>

";

            var bpmnProcessReader = new BpmnReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                var bpmnDocument = XDocument.Load(ms);
                bpmnElements = bpmnProcessReader.Read(bpmnDocument);
            }

            //var scriptTask = bpmnElements.First(f => f.Type == BpmnSequenceElements.ScriptTask.ElementTypeName);

            //var hasChildOfDIA = scriptTask.HasChildOf(DataInputAssociation.ElementTypeName);

            //var children = scriptTask.Children.ToList();

            var sequence = new Sequence(bpmnElements);

            var sequenceProcessor = ISequenceProcessorBuilder
                .Create<SequenceProcessorBuilder>()
                .UsingElementHandler(StartEvent.ElementTypeName, new StartEventHandler())
                .UsingElementHandler(ExclusiveGateway.ElementTypeName, new ExclusiveGatewayHandler())
                .UsingElementHandler(SubProcess.ElementTypeName, new SubProcessHandler())
                .UsingElementHandler(ScriptTask.ElementTypeName, new ScriptTaskHandler())
                .UsingElementHandler(BpmnSequenceElements.Task.ElementTypeName, new TaskHandler())
                .UsingElementHandler(IntermediateCatchEvent.ElementTypeName, new IntermediateCatchEventHandler())
                .UsingElementHandler(BoundaryEvent.ElementTypeName, new BoundaryEventHandler())
                .UsingElementHandler(EndEvent.ElementTypeName, new EndEventHandler())
                .WithDefaultElementHandler(new DefaultElementHandler())
                .WithBpmnSequence(sequence)
                .Build<SequenceProcessor>();

            sequenceProcessor.Start();
        }
    }
}