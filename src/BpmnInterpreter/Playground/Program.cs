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
<definitions xmlns=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:omgdi=""http://www.omg.org/spec/DD/20100524/DI"" xmlns:omgdc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" id=""sid-38422fae-e03e-43a3-bef4-bd33b32041b2"" targetNamespace=""http://bpmn.io/bpmn"" exporter=""bpmn-js (https://demo.bpmn.io)"" exporterVersion=""18.1.1"">
  <process id=""Process_1"" isExecutable=""false"">
    <task id=""Activity_0k2c2n3"" name=""task1"">
      <incoming>Flow_1yy49am</incoming>
      <outgoing>Flow_1jlxq41</outgoing>
    </task>
    <intermediateCatchEvent id=""Event_1lspmdt"" name=""message2"">
      <incoming>Flow_04sl41u</incoming>
      <outgoing>Flow_1yy49am</outgoing>
      <messageEventDefinition id=""MessageEventDefinition_0nx91qu"" />
    </intermediateCatchEvent>
    <sequenceFlow id=""Flow_1yy49am"" sourceRef=""Event_1lspmdt"" targetRef=""Activity_0k2c2n3"" />
    <boundaryEvent id=""Event_0n7ntzr"" name=""message3"" attachedToRef=""Activity_0k2c2n3"">
      <messageEventDefinition id=""MessageEventDefinition_0gvdvzu"" />
    </boundaryEvent>
    <endEvent id=""Event_1ph9361"" name=""end event"">
      <incoming>Flow_1jlxq41</incoming>
    </endEvent>
    <sequenceFlow id=""Flow_1jlxq41"" sourceRef=""Activity_0k2c2n3"" targetRef=""Event_1ph9361"" />
    <startEvent id=""Event_0t8gvnd"" name=""start event"">
      <outgoing>Flow_04sl41u</outgoing>
    </startEvent>
    <sequenceFlow id=""Flow_04sl41u"" sourceRef=""Event_0t8gvnd"" targetRef=""Event_1lspmdt"" />
  </process>
  <bpmndi:BPMNDiagram id=""BpmnDiagram_1"">
    <bpmndi:BPMNPlane id=""BpmnPlane_1"" bpmnElement=""Process_1"">
      <bpmndi:BPMNShape id=""Activity_0k2c2n3_di"" bpmnElement=""Activity_0k2c2n3"">
        <omgdc:Bounds x=""350"" y=""80"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0ixb6m0_di"" bpmnElement=""Event_1lspmdt"">
        <omgdc:Bounds x=""242"" y=""102"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""234"" y=""145"" width=""52"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_1ph9361_di"" bpmnElement=""Event_1ph9361"">
        <omgdc:Bounds x=""562"" y=""102"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""555"" y=""145"" width=""50"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0t8gvnd_di"" bpmnElement=""Event_0t8gvnd"">
        <omgdc:Bounds x=""152"" y=""102"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""144"" y=""145"" width=""53"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0n7ntzr_di"" bpmnElement=""Event_0n7ntzr"">
        <omgdc:Bounds x=""382"" y=""142"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""374"" y=""185"" width=""52"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_1yy49am_di"" bpmnElement=""Flow_1yy49am"">
        <omgdi:waypoint x=""278"" y=""120"" />
        <omgdi:waypoint x=""350"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1jlxq41_di"" bpmnElement=""Flow_1jlxq41"">
        <omgdi:waypoint x=""450"" y=""120"" />
        <omgdi:waypoint x=""562"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_04sl41u_di"" bpmnElement=""Flow_04sl41u"">
        <omgdi:waypoint x=""188"" y=""120"" />
        <omgdi:waypoint x=""242"" y=""120"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</definitions>

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