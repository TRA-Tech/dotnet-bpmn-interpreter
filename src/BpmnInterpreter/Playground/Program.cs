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
    <startEvent id=""StartEvent_1y45yut"" name=""hunger noticed"">
      <outgoing>SequenceFlow_0h21x7r</outgoing>
    </startEvent>
    <task id=""Task_1hcentk"" name=""choose recipe"">
      <incoming>SequenceFlow_0h21x7r</incoming>
      <outgoing>SequenceFlow_0wnb4ke</outgoing>
    </task>
    <sequenceFlow id=""SequenceFlow_0h21x7r"" sourceRef=""StartEvent_1y45yut"" targetRef=""Task_1hcentk"" />
    <exclusiveGateway id=""ExclusiveGateway_15hu1pt"" name=""desired dish?"">
      <incoming>SequenceFlow_0wnb4ke</incoming>
      <outgoing>Flow_0xqs0xr</outgoing>
      <outgoing>Flow_1yysmed</outgoing>
    </exclusiveGateway>
    <sequenceFlow id=""SequenceFlow_0wnb4ke"" sourceRef=""Task_1hcentk"" targetRef=""ExclusiveGateway_15hu1pt"" />
    <task id=""Activity_0yfstfj"">
      <incoming>Flow_0xqs0xr</incoming>
      <outgoing>Flow_0jp50e6</outgoing>
    </task>
    <task id=""Activity_000fimm"">
      <incoming>Flow_1yysmed</incoming>
      <outgoing>Flow_04o1jrt</outgoing>
    </task>
    <task id=""Activity_1gxvn8h"">
      <incoming>Flow_0jp50e6</incoming>
      <outgoing>Flow_0jk3fqx</outgoing>
    </task>
    <task id=""Activity_0w0wsfo"">
      <incoming>Flow_04o1jrt</incoming>
      <outgoing>Flow_0jic6n1</outgoing>
    </task>
    <task id=""Activity_15s4v3b"">
      <incoming>Flow_11a9jin</incoming>
      <outgoing>Flow_0amqn00</outgoing>
    </task>
    <exclusiveGateway id=""Gateway_0bqsdf1"">
      <incoming>Flow_0jk3fqx</incoming>
      <outgoing>Flow_1avaedw</outgoing>
      <outgoing>Flow_09f1it5</outgoing>
    </exclusiveGateway>
    <exclusiveGateway id=""Gateway_19padlp"">
      <incoming>Flow_0jic6n1</incoming>
      <outgoing>Flow_11a9jin</outgoing>
      <outgoing>Flow_05snqvc</outgoing>
    </exclusiveGateway>
    <task id=""Activity_0wmgl9n"">
      <incoming>Flow_1avaedw</incoming>
      <outgoing>Flow_1hri896</outgoing>
    </task>
    <task id=""Activity_1hiiha0"">
      <incoming>Flow_09f1it5</incoming>
      <outgoing>Flow_0wt7bm4</outgoing>
    </task>
    <task id=""Activity_1lwsrvx"">
      <incoming>Flow_05snqvc</incoming>
      <outgoing>Flow_03npq6q</outgoing>
    </task>
    <task id=""Activity_1v5ehzw"">
      <incoming>Flow_0wt7bm4</incoming>
      <incoming>Flow_1hri896</incoming>
      <outgoing>Flow_1t34rum</outgoing>
    </task>
    <task id=""Activity_1j5jywg"">
      <incoming>Flow_03npq6q</incoming>
      <incoming>Flow_0amqn00</incoming>
      <outgoing>Flow_1v7h1zy</outgoing>
    </task>
    <endEvent id=""Event_1m6au4t"">
      <incoming>Flow_1v7h1zy</incoming>
      <incoming>Flow_1t34rum</incoming>
    </endEvent>
    <sequenceFlow id=""Flow_0xqs0xr"" sourceRef=""ExclusiveGateway_15hu1pt"" targetRef=""Activity_0yfstfj"" />
    <sequenceFlow id=""Flow_0jp50e6"" sourceRef=""Activity_0yfstfj"" targetRef=""Activity_1gxvn8h"" />
    <sequenceFlow id=""Flow_0jk3fqx"" sourceRef=""Activity_1gxvn8h"" targetRef=""Gateway_0bqsdf1"" />
    <sequenceFlow id=""Flow_1avaedw"" sourceRef=""Gateway_0bqsdf1"" targetRef=""Activity_0wmgl9n"" />
    <sequenceFlow id=""Flow_09f1it5"" sourceRef=""Gateway_0bqsdf1"" targetRef=""Activity_1hiiha0"" />
    <sequenceFlow id=""Flow_0wt7bm4"" sourceRef=""Activity_1hiiha0"" targetRef=""Activity_1v5ehzw"" />
    <sequenceFlow id=""Flow_1hri896"" sourceRef=""Activity_0wmgl9n"" targetRef=""Activity_1v5ehzw"" />
    <sequenceFlow id=""Flow_1yysmed"" sourceRef=""ExclusiveGateway_15hu1pt"" targetRef=""Activity_000fimm"" />
    <sequenceFlow id=""Flow_04o1jrt"" sourceRef=""Activity_000fimm"" targetRef=""Activity_0w0wsfo"" />
    <sequenceFlow id=""Flow_0jic6n1"" sourceRef=""Activity_0w0wsfo"" targetRef=""Gateway_19padlp"" />
    <sequenceFlow id=""Flow_11a9jin"" sourceRef=""Gateway_19padlp"" targetRef=""Activity_15s4v3b"" />
    <sequenceFlow id=""Flow_05snqvc"" sourceRef=""Gateway_19padlp"" targetRef=""Activity_1lwsrvx"" />
    <sequenceFlow id=""Flow_03npq6q"" sourceRef=""Activity_1lwsrvx"" targetRef=""Activity_1j5jywg"" />
    <sequenceFlow id=""Flow_0amqn00"" sourceRef=""Activity_15s4v3b"" targetRef=""Activity_1j5jywg"" />
    <sequenceFlow id=""Flow_1v7h1zy"" sourceRef=""Activity_1j5jywg"" targetRef=""Event_1m6au4t"" />
    <sequenceFlow id=""Flow_1t34rum"" sourceRef=""Activity_1v5ehzw"" targetRef=""Event_1m6au4t"" />
  </process>
  <bpmndi:BPMNDiagram id=""BpmnDiagram_1"">
    <bpmndi:BPMNPlane id=""BpmnPlane_1"" bpmnElement=""Process_1"">
      <bpmndi:BPMNShape id=""StartEvent_1y45yut_di"" bpmnElement=""StartEvent_1y45yut"">
        <omgdc:Bounds x=""172"" y=""382"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""154"" y=""425"" width=""74"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Task_1hcentk_di"" bpmnElement=""Task_1hcentk"">
        <omgdc:Bounds x=""264"" y=""360"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""ExclusiveGateway_15hu1pt_di"" bpmnElement=""ExclusiveGateway_15hu1pt"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""425"" y=""375"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""485"" y=""393"" width=""66"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0yfstfj_di"" bpmnElement=""Activity_0yfstfj"">
        <omgdc:Bounds x=""480"" y=""170"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_000fimm_di"" bpmnElement=""Activity_000fimm"">
        <omgdc:Bounds x=""480"" y=""530"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1gxvn8h_di"" bpmnElement=""Activity_1gxvn8h"">
        <omgdc:Bounds x=""680"" y=""170"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0w0wsfo_di"" bpmnElement=""Activity_0w0wsfo"">
        <omgdc:Bounds x=""680"" y=""530"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_19padlp_di"" bpmnElement=""Gateway_19padlp"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""855"" y=""535"" width=""50"" height=""50"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_0bqsdf1_di"" bpmnElement=""Gateway_0bqsdf1"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""845"" y=""185"" width=""50"" height=""50"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0wmgl9n_di"" bpmnElement=""Activity_0wmgl9n"">
        <omgdc:Bounds x=""960"" y=""80"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1hiiha0_di"" bpmnElement=""Activity_1hiiha0"">
        <omgdc:Bounds x=""960"" y=""240"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_15s4v3b_di"" bpmnElement=""Activity_15s4v3b"">
        <omgdc:Bounds x=""960"" y=""410"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1lwsrvx_di"" bpmnElement=""Activity_1lwsrvx"">
        <omgdc:Bounds x=""960"" y=""610"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1v5ehzw_di"" bpmnElement=""Activity_1v5ehzw"">
        <omgdc:Bounds x=""1150"" y=""170"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1j5jywg_di"" bpmnElement=""Activity_1j5jywg"">
        <omgdc:Bounds x=""1140"" y=""520"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_1m6au4t_di"" bpmnElement=""Event_1m6au4t"">
        <omgdc:Bounds x=""1362"" y=""342"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""SequenceFlow_0h21x7r_di"" bpmnElement=""SequenceFlow_0h21x7r"">
        <omgdi:waypoint x=""208"" y=""400"" />
        <omgdi:waypoint x=""264"" y=""400"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""SequenceFlow_0wnb4ke_di"" bpmnElement=""SequenceFlow_0wnb4ke"">
        <omgdi:waypoint x=""364"" y=""400"" />
        <omgdi:waypoint x=""425"" y=""400"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0xqs0xr_di"" bpmnElement=""Flow_0xqs0xr"">
        <omgdi:waypoint x=""450"" y=""375"" />
        <omgdi:waypoint x=""450"" y=""210"" />
        <omgdi:waypoint x=""480"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jp50e6_di"" bpmnElement=""Flow_0jp50e6"">
        <omgdi:waypoint x=""580"" y=""210"" />
        <omgdi:waypoint x=""680"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jk3fqx_di"" bpmnElement=""Flow_0jk3fqx"">
        <omgdi:waypoint x=""780"" y=""210"" />
        <omgdi:waypoint x=""845"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1avaedw_di"" bpmnElement=""Flow_1avaedw"">
        <omgdi:waypoint x=""870"" y=""185"" />
        <omgdi:waypoint x=""870"" y=""120"" />
        <omgdi:waypoint x=""960"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_09f1it5_di"" bpmnElement=""Flow_09f1it5"">
        <omgdi:waypoint x=""870"" y=""235"" />
        <omgdi:waypoint x=""870"" y=""280"" />
        <omgdi:waypoint x=""960"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0wt7bm4_di"" bpmnElement=""Flow_0wt7bm4"">
        <omgdi:waypoint x=""1060"" y=""280"" />
        <omgdi:waypoint x=""1105"" y=""280"" />
        <omgdi:waypoint x=""1105"" y=""210"" />
        <omgdi:waypoint x=""1150"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1hri896_di"" bpmnElement=""Flow_1hri896"">
        <omgdi:waypoint x=""1060"" y=""120"" />
        <omgdi:waypoint x=""1105"" y=""120"" />
        <omgdi:waypoint x=""1105"" y=""210"" />
        <omgdi:waypoint x=""1150"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1yysmed_di"" bpmnElement=""Flow_1yysmed"">
        <omgdi:waypoint x=""450"" y=""425"" />
        <omgdi:waypoint x=""450"" y=""570"" />
        <omgdi:waypoint x=""480"" y=""570"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_04o1jrt_di"" bpmnElement=""Flow_04o1jrt"">
        <omgdi:waypoint x=""580"" y=""570"" />
        <omgdi:waypoint x=""680"" y=""570"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jic6n1_di"" bpmnElement=""Flow_0jic6n1"">
        <omgdi:waypoint x=""780"" y=""570"" />
        <omgdi:waypoint x=""818"" y=""570"" />
        <omgdi:waypoint x=""818"" y=""560"" />
        <omgdi:waypoint x=""855"" y=""560"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_11a9jin_di"" bpmnElement=""Flow_11a9jin"">
        <omgdi:waypoint x=""880"" y=""535"" />
        <omgdi:waypoint x=""880"" y=""450"" />
        <omgdi:waypoint x=""960"" y=""450"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_05snqvc_di"" bpmnElement=""Flow_05snqvc"">
        <omgdi:waypoint x=""880"" y=""585"" />
        <omgdi:waypoint x=""880"" y=""650"" />
        <omgdi:waypoint x=""960"" y=""650"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_03npq6q_di"" bpmnElement=""Flow_03npq6q"">
        <omgdi:waypoint x=""1060"" y=""650"" />
        <omgdi:waypoint x=""1100"" y=""650"" />
        <omgdi:waypoint x=""1100"" y=""560"" />
        <omgdi:waypoint x=""1140"" y=""560"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0amqn00_di"" bpmnElement=""Flow_0amqn00"">
        <omgdi:waypoint x=""1060"" y=""450"" />
        <omgdi:waypoint x=""1100"" y=""450"" />
        <omgdi:waypoint x=""1100"" y=""560"" />
        <omgdi:waypoint x=""1140"" y=""560"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1v7h1zy_di"" bpmnElement=""Flow_1v7h1zy"">
        <omgdi:waypoint x=""1240"" y=""560"" />
        <omgdi:waypoint x=""1301"" y=""560"" />
        <omgdi:waypoint x=""1301"" y=""360"" />
        <omgdi:waypoint x=""1362"" y=""360"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1t34rum_di"" bpmnElement=""Flow_1t34rum"">
        <omgdi:waypoint x=""1250"" y=""210"" />
        <omgdi:waypoint x=""1306"" y=""210"" />
        <omgdi:waypoint x=""1306"" y=""360"" />
        <omgdi:waypoint x=""1362"" y=""360"" />
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
                .UsingElementHandler(EndEvent.ElementTypeName, new EndEventHandler())
                .WithDefaultElementHandler(new DefaultElementHandler())
                .WithBpmnSequence(sequence)
                .Build<SequenceProcessor>();

            sequenceProcessor.Start();
        }
    }
}