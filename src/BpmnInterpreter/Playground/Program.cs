using Playground.ElementHandlers;
using System.Text;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core;
using TraTech.BpmnInterpreter.Core.Elements;
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
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<definitions xmlns=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:omgdi=""http://www.omg.org/spec/DD/20100524/DI"" xmlns:omgdc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" id=""sid-38422fae-e03e-43a3-bef4-bd33b32041b2"" targetNamespace=""http://bpmn.io/bpmn"" exporter=""bpmn-js (https://demo.bpmn.io)"" exporterVersion=""18.10.1"">
  <process id=""Process_1"" isExecutable=""false"">
    <startEvent id=""Event_10ykxsi"" name=""0"">
      <outgoing>Flow_10fssez</outgoing>
    </startEvent>
    <task id=""Activity_01qugyx"" name=""1"">
      <outgoing>Flow_17eo2w8</outgoing>
    </task>
    <task id=""Activity_1a5gawu"" name=""2"">
      <incoming>Flow_10fssez</incoming>
      <outgoing>Flow_0zwy8u1</outgoing>
    </task>
    <task id=""Activity_12myfrm"" name=""3"">
      <outgoing>Flow_18jwo30</outgoing>
    </task>
    <sequenceFlow id=""Flow_10fssez"" sourceRef=""Event_10ykxsi"" targetRef=""Activity_1a5gawu"" />
    <task id=""Activity_1qcol3q"" name=""4"">
      <incoming>Flow_17eo2w8</incoming>
      <incoming>Flow_0zwy8u1</incoming>
      <incoming>Flow_18jwo30</incoming>
      <outgoing>Flow_19o1ndf</outgoing>
    </task>
    <sequenceFlow id=""Flow_17eo2w8"" sourceRef=""Activity_01qugyx"" targetRef=""Activity_1qcol3q"" />
    <sequenceFlow id=""Flow_0zwy8u1"" sourceRef=""Activity_1a5gawu"" targetRef=""Activity_1qcol3q"" />
    <sequenceFlow id=""Flow_18jwo30"" sourceRef=""Activity_12myfrm"" targetRef=""Activity_1qcol3q"" />
    <endEvent id=""Event_00niyfz"" name=""6"">
      <incoming>Flow_132c1na</incoming>
    </endEvent>
    <exclusiveGateway id=""Gateway_0ykce5k"" name=""5"">
      <incoming>Flow_19o1ndf</incoming>
      <outgoing>Flow_132c1na</outgoing>
      <outgoing>Flow_1kup94i</outgoing>
    </exclusiveGateway>
    <sequenceFlow id=""Flow_132c1na"" sourceRef=""Gateway_0ykce5k"" targetRef=""Event_00niyfz"" />
    <endEvent id=""Event_1s3g6qx"" name=""7"">
      <incoming>Flow_1kup94i</incoming>
    </endEvent>
    <sequenceFlow id=""Flow_1kup94i"" sourceRef=""Gateway_0ykce5k"" targetRef=""Event_1s3g6qx"" />
    <sequenceFlow id=""Flow_19o1ndf"" sourceRef=""Activity_1qcol3q"" targetRef=""Gateway_0ykce5k"" />
    <boundaryEvent id=""Event_0f25xku"" name=""4.1"" attachedToRef=""Activity_1qcol3q"">
      <errorEventDefinition id=""ErrorEventDefinition_1uoqmz9"" />
    </boundaryEvent>
    <boundaryEvent id=""Event_05a3ydq"" name=""4.2"" attachedToRef=""Activity_1qcol3q"" />
  </process>
  <bpmndi:BPMNDiagram id=""BpmnDiagram_1"">
    <bpmndi:BPMNPlane id=""BpmnPlane_1"" bpmnElement=""Process_1"">
      <bpmndi:BPMNShape id=""Activity_01qugyx_di"" bpmnElement=""Activity_01qugyx"">
        <omgdc:Bounds x=""280"" y=""80"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1a5gawu_di"" bpmnElement=""Activity_1a5gawu"">
        <omgdc:Bounds x=""280"" y=""210"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_12myfrm_di"" bpmnElement=""Activity_12myfrm"">
        <omgdc:Bounds x=""280"" y=""340"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_10ykxsi_di"" bpmnElement=""Event_10ykxsi"">
        <omgdc:Bounds x=""152"" y=""232"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""167"" y=""275"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1qcol3q_di"" bpmnElement=""Activity_1qcol3q"">
        <omgdc:Bounds x=""480"" y=""210"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_0ykce5k_di"" bpmnElement=""Gateway_0ykce5k"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""655"" y=""225"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""716"" y=""243"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_00niyfz_di"" bpmnElement=""Event_00niyfz"">
        <omgdc:Bounds x=""852"" y=""352"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""866"" y=""403"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_1rtlv75"" bpmnElement=""Event_1s3g6qx"">
        <omgdc:Bounds x=""852"" y=""152"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""866"" y=""198"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0f25xku_di"" bpmnElement=""Event_0f25xku"">
        <omgdc:Bounds x=""492"" y=""192"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""502"" y=""162"" width=""16"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0nsi6f5_di"" bpmnElement=""Event_05a3ydq"">
        <omgdc:Bounds x=""492"" y=""272"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""502"" y=""315"" width=""16"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_17eo2w8_di"" bpmnElement=""Flow_17eo2w8"">
        <omgdi:waypoint x=""380"" y=""120"" />
        <omgdi:waypoint x=""430"" y=""120"" />
        <omgdi:waypoint x=""430"" y=""250"" />
        <omgdi:waypoint x=""480"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_10fssez_di"" bpmnElement=""Flow_10fssez"">
        <omgdi:waypoint x=""188"" y=""250"" />
        <omgdi:waypoint x=""280"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0zwy8u1_di"" bpmnElement=""Flow_0zwy8u1"">
        <omgdi:waypoint x=""380"" y=""250"" />
        <omgdi:waypoint x=""480"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_18jwo30_di"" bpmnElement=""Flow_18jwo30"">
        <omgdi:waypoint x=""380"" y=""380"" />
        <omgdi:waypoint x=""430"" y=""380"" />
        <omgdi:waypoint x=""430"" y=""250"" />
        <omgdi:waypoint x=""480"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_19o1ndf_di"" bpmnElement=""Flow_19o1ndf"">
        <omgdi:waypoint x=""580"" y=""250"" />
        <omgdi:waypoint x=""655"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_132c1na_di"" bpmnElement=""Flow_132c1na"">
        <omgdi:waypoint x=""680"" y=""275"" />
        <omgdi:waypoint x=""680"" y=""370"" />
        <omgdi:waypoint x=""852"" y=""370"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1kup94i_di"" bpmnElement=""Flow_1kup94i"">
        <omgdi:waypoint x=""680"" y=""225"" />
        <omgdi:waypoint x=""680"" y=""170"" />
        <omgdi:waypoint x=""852"" y=""170"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</definitions>

";

            var bpmnProcessReader = new BpmnProcessReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
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
                .UsingElementHandler(ExclusiveGateway.ElementTypeName, new ExclusiveGatewayHandler())
                .UsingElementHandler(SubProcess.ElementTypeName, new SubProcessHandler())
                .UsingElementHandler(ScriptTask.ElementTypeName, new ScriptTaskHandler())
                .UsingElementHandler(BpmnSequenceElements.Task.ElementTypeName, new TaskHandler())
                .UsingElementHandler(IntermediateCatchEvent.ElementTypeName, new IntermediateCatchEventHandler())
                .UsingElementHandler(EndEvent.ElementTypeName, new EndEventHandler())
                .WithDefaultElementHandler(new DefaultElementHandler())
                .UsingBoundaryElementHandler(BoundaryEvent.ElementTypeName, new BoundaryEventHandler())
                .WithDefaultBoundaryElementHandler(new DefaultBoundaryEventHandler())
                .WithBpmnSequence(sequence)
                .Build<SequenceProcessor>();

            await sequenceProcessor.StartAsync();
        }
    }
}