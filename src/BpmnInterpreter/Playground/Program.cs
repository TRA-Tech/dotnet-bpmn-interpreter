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
    <startEvent id=""StartEvent_1y45yut"" name=""start"">
      <outgoing>SequenceFlow_0h21x7r</outgoing>
      <outgoing>Flow_07erx3m</outgoing>
    </startEvent>
    <task id=""Task_1hcentk"" name=""1"">
      <incoming>SequenceFlow_0h21x7r</incoming>
      <outgoing>SequenceFlow_0wnb4ke</outgoing>
    </task>
    <sequenceFlow id=""SequenceFlow_0h21x7r"" sourceRef=""StartEvent_1y45yut"" targetRef=""Task_1hcentk"" />
    <exclusiveGateway id=""ExclusiveGateway_15hu1pt"" name=""3"">
      <incoming>SequenceFlow_0wnb4ke</incoming>
      <outgoing>Flow_0xqs0xr</outgoing>
      <outgoing>Flow_1315kux</outgoing>
    </exclusiveGateway>
    <sequenceFlow id=""SequenceFlow_0wnb4ke"" sourceRef=""Task_1hcentk"" targetRef=""ExclusiveGateway_15hu1pt"" />
    <task id=""Activity_0yfstfj"" name=""4"">
      <incoming>Flow_0xqs0xr</incoming>
      <outgoing>Flow_0jp50e6</outgoing>
    </task>
    <task id=""Activity_1gxvn8h"" name=""6"">
      <incoming>Flow_0jp50e6</incoming>
      <outgoing>Flow_0jk3fqx</outgoing>
    </task>
    <task id=""Activity_0w0wsfo"" name=""7"">
      <incoming>Flow_0mc9y8f</incoming>
      <outgoing>Flow_0jic6n1</outgoing>
    </task>
    <task id=""Activity_15s4v3b"" name=""12"">
      <incoming>Flow_11a9jin</incoming>
      <outgoing>Flow_0amqn00</outgoing>
    </task>
    <exclusiveGateway id=""Gateway_0bqsdf1"" name=""8"">
      <incoming>Flow_0jk3fqx</incoming>
      <outgoing>Flow_1avaedw</outgoing>
      <outgoing>Flow_09f1it5</outgoing>
    </exclusiveGateway>
    <exclusiveGateway id=""Gateway_19padlp"" name=""9"">
      <incoming>Flow_0jic6n1</incoming>
      <outgoing>Flow_11a9jin</outgoing>
      <outgoing>Flow_05snqvc</outgoing>
    </exclusiveGateway>
    <task id=""Activity_0wmgl9n"" name=""10"">
      <incoming>Flow_1avaedw</incoming>
      <outgoing>Flow_1hri896</outgoing>
    </task>
    <task id=""Activity_1hiiha0"" name=""11"">
      <incoming>Flow_09f1it5</incoming>
      <outgoing>Flow_0wt7bm4</outgoing>
    </task>
    <task id=""Activity_1lwsrvx"" name=""13"">
      <incoming>Flow_05snqvc</incoming>
      <outgoing>Flow_03npq6q</outgoing>
    </task>
    <task id=""Activity_1v5ehzw"" name=""14"">
      <incoming>Flow_0wt7bm4</incoming>
      <incoming>Flow_1hri896</incoming>
      <outgoing>Flow_1t34rum</outgoing>
    </task>
    <task id=""Activity_1j5jywg"" name=""15"">
      <incoming>Flow_03npq6q</incoming>
      <incoming>Flow_0amqn00</incoming>
      <outgoing>Flow_1v7h1zy</outgoing>
    </task>
    <endEvent id=""Event_1m6au4t"" name=""end"">
      <incoming>Flow_1v7h1zy</incoming>
      <incoming>Flow_1t34rum</incoming>
      <incoming>Flow_1gylt3y</incoming>
      <incoming>Flow_10sx9nc</incoming>
    </endEvent>
    <sequenceFlow id=""Flow_0xqs0xr"" sourceRef=""ExclusiveGateway_15hu1pt"" targetRef=""Activity_0yfstfj"" />
    <sequenceFlow id=""Flow_0jp50e6"" sourceRef=""Activity_0yfstfj"" targetRef=""Activity_1gxvn8h"" />
    <sequenceFlow id=""Flow_0jk3fqx"" sourceRef=""Activity_1gxvn8h"" targetRef=""Gateway_0bqsdf1"" />
    <sequenceFlow id=""Flow_1avaedw"" sourceRef=""Gateway_0bqsdf1"" targetRef=""Activity_0wmgl9n"" />
    <sequenceFlow id=""Flow_09f1it5"" sourceRef=""Gateway_0bqsdf1"" targetRef=""Activity_1hiiha0"" />
    <sequenceFlow id=""Flow_0wt7bm4"" sourceRef=""Activity_1hiiha0"" targetRef=""Activity_1v5ehzw"" />
    <sequenceFlow id=""Flow_1hri896"" sourceRef=""Activity_0wmgl9n"" targetRef=""Activity_1v5ehzw"" />
    <sequenceFlow id=""Flow_0jic6n1"" sourceRef=""Activity_0w0wsfo"" targetRef=""Gateway_19padlp"" />
    <sequenceFlow id=""Flow_11a9jin"" sourceRef=""Gateway_19padlp"" targetRef=""Activity_15s4v3b"" />
    <sequenceFlow id=""Flow_05snqvc"" sourceRef=""Gateway_19padlp"" targetRef=""Activity_1lwsrvx"" />
    <sequenceFlow id=""Flow_03npq6q"" sourceRef=""Activity_1lwsrvx"" targetRef=""Activity_1j5jywg"" />
    <sequenceFlow id=""Flow_0amqn00"" sourceRef=""Activity_15s4v3b"" targetRef=""Activity_1j5jywg"" />
    <sequenceFlow id=""Flow_1v7h1zy"" sourceRef=""Activity_1j5jywg"" targetRef=""Event_1m6au4t"" />
    <sequenceFlow id=""Flow_1t34rum"" sourceRef=""Activity_1v5ehzw"" targetRef=""Event_1m6au4t"" />
    <task id=""Activity_1q92vyt"" name=""2"">
      <incoming>Flow_07erx3m</incoming>
      <outgoing>Flow_1gylt3y</outgoing>
    </task>
    <sequenceFlow id=""Flow_07erx3m"" sourceRef=""StartEvent_1y45yut"" targetRef=""Activity_1q92vyt"" />
    <sequenceFlow id=""Flow_1gylt3y"" sourceRef=""Activity_1q92vyt"" targetRef=""Event_1m6au4t"" />
    <exclusiveGateway id=""Gateway_19cbia3"" name=""17"">
      <incoming>Flow_1315kux</incoming>
      <outgoing>Flow_1dl98ic</outgoing>
      <outgoing>Flow_1o8lz9y</outgoing>
    </exclusiveGateway>
    <sequenceFlow id=""Flow_1315kux"" sourceRef=""ExclusiveGateway_15hu1pt"" targetRef=""Gateway_19cbia3"" />
    <task id=""Activity_0glxc6b"" name=""5"">
      <incoming>Flow_1o8lz9y</incoming>
      <outgoing>Flow_10sx9nc</outgoing>
    </task>
    <task id=""Activity_1hii420"" name=""16"">
      <incoming>Flow_1dl98ic</incoming>
      <outgoing>Flow_0mc9y8f</outgoing>
    </task>
    <sequenceFlow id=""Flow_0mc9y8f"" sourceRef=""Activity_1hii420"" targetRef=""Activity_0w0wsfo"" />
    <sequenceFlow id=""Flow_10sx9nc"" sourceRef=""Activity_0glxc6b"" targetRef=""Event_1m6au4t"" />
    <sequenceFlow id=""Flow_1dl98ic"" sourceRef=""Gateway_19cbia3"" targetRef=""Activity_1hii420"" />
    <sequenceFlow id=""Flow_1o8lz9y"" sourceRef=""Gateway_19cbia3"" targetRef=""Activity_0glxc6b"" />
  </process>
  <bpmndi:BPMNDiagram id=""BpmnDiagram_1"">
    <bpmndi:BPMNPlane id=""BpmnPlane_1"" bpmnElement=""Process_1"">
      <bpmndi:BPMNShape id=""StartEvent_1y45yut_di"" bpmnElement=""StartEvent_1y45yut"">
        <omgdc:Bounds x=""172"" y=""452"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""180"" y=""495"" width=""23"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Task_1hcentk_di"" bpmnElement=""Task_1hcentk"">
        <omgdc:Bounds x=""264"" y=""430"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""ExclusiveGateway_15hu1pt_di"" bpmnElement=""ExclusiveGateway_15hu1pt"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""425"" y=""445"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""446"" y=""505"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0yfstfj_di"" bpmnElement=""Activity_0yfstfj"">
        <omgdc:Bounds x=""480"" y=""240"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1gxvn8h_di"" bpmnElement=""Activity_1gxvn8h"">
        <omgdc:Bounds x=""680"" y=""240"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0w0wsfo_di"" bpmnElement=""Activity_0w0wsfo"">
        <omgdc:Bounds x=""680"" y=""600"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_15s4v3b_di"" bpmnElement=""Activity_15s4v3b"">
        <omgdc:Bounds x=""960"" y=""480"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_0bqsdf1_di"" bpmnElement=""Gateway_0bqsdf1"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""845"" y=""255"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""947"" y=""270"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_19padlp_di"" bpmnElement=""Gateway_19padlp"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""855"" y=""605"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""957"" y=""620"" width=""7"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0wmgl9n_di"" bpmnElement=""Activity_0wmgl9n"">
        <omgdc:Bounds x=""960"" y=""150"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1hiiha0_di"" bpmnElement=""Activity_1hiiha0"">
        <omgdc:Bounds x=""960"" y=""310"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1v5ehzw_di"" bpmnElement=""Activity_1v5ehzw"">
        <omgdc:Bounds x=""1150"" y=""240"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1j5jywg_di"" bpmnElement=""Activity_1j5jywg"">
        <omgdc:Bounds x=""1140"" y=""590"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_1m6au4t_di"" bpmnElement=""Event_1m6au4t"">
        <omgdc:Bounds x=""1362"" y=""412"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""1371"" y=""455"" width=""19"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1lwsrvx_di"" bpmnElement=""Activity_1lwsrvx"">
        <omgdc:Bounds x=""960"" y=""690"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_02sb1g8"" bpmnElement=""Activity_1q92vyt"">
        <omgdc:Bounds x=""530"" y=""80"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_19cbia3_di"" bpmnElement=""Gateway_19cbia3"" isMarkerVisible=""true"">
        <omgdc:Bounds x=""545"" y=""445"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x=""564"" y=""415"" width=""13"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0glxc6b_di"" bpmnElement=""Activity_0glxc6b"">
        <omgdc:Bounds x=""670"" y=""410"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1hii420_di"" bpmnElement=""Activity_1hii420"">
        <omgdc:Bounds x=""500"" y=""670"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""SequenceFlow_0h21x7r_di"" bpmnElement=""SequenceFlow_0h21x7r"">
        <omgdi:waypoint x=""208"" y=""470"" />
        <omgdi:waypoint x=""264"" y=""470"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_07erx3m_di"" bpmnElement=""Flow_07erx3m"">
        <omgdi:waypoint x=""208"" y=""470"" />
        <omgdi:waypoint x=""230"" y=""470"" />
        <omgdi:waypoint x=""230"" y=""120"" />
        <omgdi:waypoint x=""530"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""SequenceFlow_0wnb4ke_di"" bpmnElement=""SequenceFlow_0wnb4ke"">
        <omgdi:waypoint x=""364"" y=""470"" />
        <omgdi:waypoint x=""425"" y=""470"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0xqs0xr_di"" bpmnElement=""Flow_0xqs0xr"">
        <omgdi:waypoint x=""450"" y=""445"" />
        <omgdi:waypoint x=""450"" y=""280"" />
        <omgdi:waypoint x=""480"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1315kux_di"" bpmnElement=""Flow_1315kux"">
        <omgdi:waypoint x=""475"" y=""470"" />
        <omgdi:waypoint x=""545"" y=""470"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jp50e6_di"" bpmnElement=""Flow_0jp50e6"">
        <omgdi:waypoint x=""580"" y=""280"" />
        <omgdi:waypoint x=""680"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jk3fqx_di"" bpmnElement=""Flow_0jk3fqx"">
        <omgdi:waypoint x=""780"" y=""280"" />
        <omgdi:waypoint x=""845"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0mc9y8f_di"" bpmnElement=""Flow_0mc9y8f"">
        <omgdi:waypoint x=""600"" y=""710"" />
        <omgdi:waypoint x=""650"" y=""710"" />
        <omgdi:waypoint x=""650"" y=""640"" />
        <omgdi:waypoint x=""680"" y=""640"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0jic6n1_di"" bpmnElement=""Flow_0jic6n1"">
        <omgdi:waypoint x=""780"" y=""640"" />
        <omgdi:waypoint x=""818"" y=""640"" />
        <omgdi:waypoint x=""818"" y=""630"" />
        <omgdi:waypoint x=""855"" y=""630"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_11a9jin_di"" bpmnElement=""Flow_11a9jin"">
        <omgdi:waypoint x=""880"" y=""605"" />
        <omgdi:waypoint x=""880"" y=""520"" />
        <omgdi:waypoint x=""960"" y=""520"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0amqn00_di"" bpmnElement=""Flow_0amqn00"">
        <omgdi:waypoint x=""1060"" y=""520"" />
        <omgdi:waypoint x=""1100"" y=""520"" />
        <omgdi:waypoint x=""1100"" y=""630"" />
        <omgdi:waypoint x=""1140"" y=""630"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1avaedw_di"" bpmnElement=""Flow_1avaedw"">
        <omgdi:waypoint x=""870"" y=""255"" />
        <omgdi:waypoint x=""870"" y=""190"" />
        <omgdi:waypoint x=""960"" y=""190"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_09f1it5_di"" bpmnElement=""Flow_09f1it5"">
        <omgdi:waypoint x=""870"" y=""305"" />
        <omgdi:waypoint x=""870"" y=""350"" />
        <omgdi:waypoint x=""960"" y=""350"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_05snqvc_di"" bpmnElement=""Flow_05snqvc"">
        <omgdi:waypoint x=""880"" y=""655"" />
        <omgdi:waypoint x=""880"" y=""730"" />
        <omgdi:waypoint x=""960"" y=""730"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1hri896_di"" bpmnElement=""Flow_1hri896"">
        <omgdi:waypoint x=""1060"" y=""190"" />
        <omgdi:waypoint x=""1105"" y=""190"" />
        <omgdi:waypoint x=""1105"" y=""280"" />
        <omgdi:waypoint x=""1150"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0wt7bm4_di"" bpmnElement=""Flow_0wt7bm4"">
        <omgdi:waypoint x=""1060"" y=""350"" />
        <omgdi:waypoint x=""1105"" y=""350"" />
        <omgdi:waypoint x=""1105"" y=""280"" />
        <omgdi:waypoint x=""1150"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1t34rum_di"" bpmnElement=""Flow_1t34rum"">
        <omgdi:waypoint x=""1250"" y=""280"" />
        <omgdi:waypoint x=""1300"" y=""280"" />
        <omgdi:waypoint x=""1300"" y=""430"" />
        <omgdi:waypoint x=""1362"" y=""430"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_03npq6q_di"" bpmnElement=""Flow_03npq6q"">
        <omgdi:waypoint x=""1060"" y=""730"" />
        <omgdi:waypoint x=""1100"" y=""730"" />
        <omgdi:waypoint x=""1100"" y=""630"" />
        <omgdi:waypoint x=""1140"" y=""630"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1v7h1zy_di"" bpmnElement=""Flow_1v7h1zy"">
        <omgdi:waypoint x=""1240"" y=""630"" />
        <omgdi:waypoint x=""1301"" y=""630"" />
        <omgdi:waypoint x=""1301"" y=""430"" />
        <omgdi:waypoint x=""1362"" y=""430"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1gylt3y_di"" bpmnElement=""Flow_1gylt3y"">
        <omgdi:waypoint x=""630"" y=""120"" />
        <omgdi:waypoint x=""1380"" y=""120"" />
        <omgdi:waypoint x=""1380"" y=""412"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_10sx9nc_di"" bpmnElement=""Flow_10sx9nc"">
        <omgdi:waypoint x=""770"" y=""450"" />
        <omgdi:waypoint x=""1066"" y=""450"" />
        <omgdi:waypoint x=""1066"" y=""430"" />
        <omgdi:waypoint x=""1362"" y=""430"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1dl98ic_di"" bpmnElement=""Flow_1dl98ic"">
        <omgdi:waypoint x=""570"" y=""495"" />
        <omgdi:waypoint x=""570"" y=""583"" />
        <omgdi:waypoint x=""550"" y=""583"" />
        <omgdi:waypoint x=""550"" y=""670"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1o8lz9y_di"" bpmnElement=""Flow_1o8lz9y"">
        <omgdi:waypoint x=""595"" y=""470"" />
        <omgdi:waypoint x=""633"" y=""470"" />
        <omgdi:waypoint x=""633"" y=""450"" />
        <omgdi:waypoint x=""670"" y=""450"" />
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