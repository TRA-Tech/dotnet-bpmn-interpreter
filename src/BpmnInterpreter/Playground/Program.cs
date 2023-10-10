using System.Text;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Abstractions;
using TraTech.BpmnInterpreter.Core;
using TraTech.BpmnInterpreter.Core.Elements;
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
    <semantic:task id=""Activity_0hxdw8q"" name=""SELECT"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_0bkn3kr</semantic:incoming>
      <semantic:incoming>Flow_0c5i9gu</semantic:incoming>
      <semantic:incoming>Flow_0itf0b0</semantic:incoming>
      <semantic:incoming>Flow_1k739p1</semantic:incoming>
      <semantic:outgoing>Flow_0yu2opd</semantic:outgoing>
      <semantic:property id=""Property_1i4kf75"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_0x1rurt"">
        <semantic:sourceRef>DataObjectReference_0k0y8sw</semantic:sourceRef>
        <semantic:targetRef>Property_1i4kf75</semantic:targetRef>
      </semantic:dataInputAssociation>
      <semantic:dataInputAssociation id=""DataInputAssociation_1ihg61s"">
        <semantic:sourceRef>DataObjectReference_0wo11vb</semantic:sourceRef>
        <semantic:targetRef>Property_1i4kf75</semantic:targetRef>
      </semantic:dataInputAssociation>
      <semantic:dataInputAssociation id=""DataInputAssociation_0l2nv0g"">
        <semantic:sourceRef>DataObjectReference_1m0phwu</semantic:sourceRef>
        <semantic:targetRef>Property_1i4kf75</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:task>
    <semantic:endEvent id=""Event_0t73mtb"" name=""END"">
      <semantic:incoming>Flow_0yu2opd</semantic:incoming>
    </semantic:endEvent>
    <semantic:sequenceFlow id=""Flow_0yu2opd"" sourceRef=""Activity_0hxdw8q"" targetRef=""Event_0t73mtb"" />
    <semantic:dataStoreReference id=""DataStoreReference_02tl2lv"" name=""Kredi"" tra:tableName=""Kredi"" />
    <semantic:dataStoreReference id=""DataStoreReference_0culd5t"" name=""Kur"" tra:tableName=""Kur"" />
    <semantic:dataStoreReference id=""DataStoreReference_0xj4tqc"" name=""Mevduat"" tra:tableName=""Mevduat"" />
    <semantic:dataObjectReference id=""DataObjectReference_0k0y8sw"" name=""SB100"" dataObjectRef=""DataObject_137zsin"" tra:reportName=""SB100"" />
    <semantic:dataObject id=""DataObject_137zsin"" />
    <semantic:dataObjectReference id=""DataObjectReference_0wo11vb"" name=""ST510"" dataObjectRef=""DataObject_1s6mjbo"" tra:reportName=""ST510"" />
    <semantic:dataObject id=""DataObject_1s6mjbo"" />
    <semantic:dataObjectReference id=""DataObjectReference_1m0phwu"" name=""VK100"" dataObjectRef=""DataObject_07nfht5"" tra:reportName=""VK100"" />
    <semantic:dataObject id=""DataObject_07nfht5"" />
    <semantic:startEvent id=""Event_027wyna"">
      <semantic:outgoing>Flow_1k739p1</semantic:outgoing>
    </semantic:startEvent>
    <semantic:task id=""Activity_0v9v63i"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:outgoing>Flow_0itf0b0</semantic:outgoing>
      <semantic:property id=""Property_18cvd1y"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_1yows3b"">
        <semantic:sourceRef>DataStoreReference_02tl2lv</semantic:sourceRef>
        <semantic:targetRef>Property_18cvd1y</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:task>
    <semantic:task id=""Activity_0nxeubq"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:outgoing>Flow_0c5i9gu</semantic:outgoing>
      <semantic:property id=""Property_12892f9"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_1g8ufkx"">
        <semantic:sourceRef>DataStoreReference_0culd5t</semantic:sourceRef>
        <semantic:targetRef>Property_12892f9</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:task>
    <semantic:task id=""Activity_0ni3sui"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:outgoing>Flow_0bkn3kr</semantic:outgoing>
      <semantic:property id=""Property_0g2du9l"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_1pg6yu7"">
        <semantic:sourceRef>DataStoreReference_0xj4tqc</semantic:sourceRef>
        <semantic:targetRef>Property_0g2du9l</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:task>
    <semantic:sequenceFlow id=""Flow_0bkn3kr"" sourceRef=""Activity_0ni3sui"" targetRef=""Activity_0hxdw8q"" />
    <semantic:sequenceFlow id=""Flow_0c5i9gu"" sourceRef=""Activity_0nxeubq"" targetRef=""Activity_0hxdw8q"" />
    <semantic:sequenceFlow id=""Flow_0itf0b0"" sourceRef=""Activity_0v9v63i"" targetRef=""Activity_0hxdw8q"" />
    <semantic:sequenceFlow id=""Flow_1k739p1"" sourceRef=""Event_027wyna"" targetRef=""Activity_0hxdw8q"" />
  </semantic:process>
  <bpmndi:BPMNDiagram id=""Trisotech.Visio-_6"" name=""Untitled Diagram"" documentation="""" resolution=""96.00000267028808"">
    <bpmndi:BPMNPlane bpmnElement=""Process_0pbbn3n"">
      <bpmndi:BPMNShape id=""Activity_0hxdw8q_di"" bpmnElement=""Activity_0hxdw8q"">
        <dc:Bounds x=""570"" y=""280"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""DataObjectReference_0k0y8sw_di"" bpmnElement=""DataObjectReference_0k0y8sw"">
        <dc:Bounds x=""762"" y=""215"" width=""36"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""763"" y=""272"" width=""34"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_01ymeq6"" bpmnElement=""DataObjectReference_0wo11vb"">
        <dc:Bounds x=""762"" y=""295"" width=""36"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""764"" y=""352"" width=""33"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_1rhk6yw"" bpmnElement=""DataObjectReference_1m0phwu"">
        <dc:Bounds x=""762"" y=""375"" width=""36"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""764"" y=""432"" width=""33"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0t73mtb_di"" bpmnElement=""Event_0t73mtb"">
        <dc:Bounds x=""602"" y=""422"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""608"" y=""465"" width=""24"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_027wyna_di"" bpmnElement=""Event_027wyna"">
        <dc:Bounds x=""602"" y=""182"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""DataStoreReference_02tl2lv_di"" bpmnElement=""DataStoreReference_02tl2lv"">
        <dc:Bounds x=""255"" y=""185"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""268"" y=""242"" width=""26"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_16qfsna"" bpmnElement=""DataStoreReference_0culd5t"">
        <dc:Bounds x=""255"" y=""275"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""271"" y=""332"" width=""19"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_1lrdu73"" bpmnElement=""DataStoreReference_0xj4tqc"">
        <dc:Bounds x=""255"" y=""375"" width=""50"" height=""50"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""258"" y=""432"" width=""44"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0v9v63i_di"" bpmnElement=""Activity_0v9v63i"">
        <dc:Bounds x=""370"" y=""170"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_00h0g39"" bpmnElement=""Activity_0nxeubq"">
        <dc:Bounds x=""370"" y=""280"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""BPMNShape_13etb0a"" bpmnElement=""Activity_0ni3sui"">
        <dc:Bounds x=""370"" y=""390"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""DataInputAssociation_0x1rurt_di"" bpmnElement=""DataInputAssociation_0x1rurt"">
        <di:waypoint x=""762"" y=""249"" />
        <di:waypoint x=""670"" y=""295"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_1ihg61s_di"" bpmnElement=""DataInputAssociation_1ihg61s"">
        <di:waypoint x=""762"" y=""320"" />
        <di:waypoint x=""670"" y=""320"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_0l2nv0g_di"" bpmnElement=""DataInputAssociation_0l2nv0g"">
        <di:waypoint x=""762"" y=""391"" />
        <di:waypoint x=""670"" y=""345"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0yu2opd_di"" bpmnElement=""Flow_0yu2opd"">
        <di:waypoint x=""620"" y=""360"" />
        <di:waypoint x=""620"" y=""422"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0bkn3kr_di"" bpmnElement=""Flow_0bkn3kr"">
        <di:waypoint x=""470"" y=""430"" />
        <di:waypoint x=""520"" y=""430"" />
        <di:waypoint x=""520"" y=""320"" />
        <di:waypoint x=""570"" y=""320"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0c5i9gu_di"" bpmnElement=""Flow_0c5i9gu"">
        <di:waypoint x=""470"" y=""320"" />
        <di:waypoint x=""570"" y=""320"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0itf0b0_di"" bpmnElement=""Flow_0itf0b0"">
        <di:waypoint x=""470"" y=""210"" />
        <di:waypoint x=""520"" y=""210"" />
        <di:waypoint x=""520"" y=""320"" />
        <di:waypoint x=""570"" y=""320"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_1yows3b_di"" bpmnElement=""DataInputAssociation_1yows3b"">
        <di:waypoint x=""305"" y=""210"" />
        <di:waypoint x=""370"" y=""210"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_1g8ufkx_di"" bpmnElement=""DataInputAssociation_1g8ufkx"">
        <di:waypoint x=""305"" y=""304"" />
        <di:waypoint x=""370"" y=""313"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_1pg6yu7_di"" bpmnElement=""DataInputAssociation_1pg6yu7"">
        <di:waypoint x=""305"" y=""405"" />
        <di:waypoint x=""370"" y=""419"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1k739p1_di"" bpmnElement=""Flow_1k739p1"">
        <di:waypoint x=""620"" y=""218"" />
        <di:waypoint x=""620"" y=""280"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</semantic:definitions>


";

            var bpmnProcessReader = new Reader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                var bpmnDocument = XDocument.Load(ms);
                bpmnElements = bpmnProcessReader.Read(bpmnDocument);
            }

            var bpmnProcessSequence = new Sequence(bpmnElements);

            var bpmnSequenceProcessorBuilder = ISequenceProcessorBuilder
                .Create<SequenceProcessorBuilder>()
                .UsingElementHandler(StartEvent.ElementTypeName, new GeneralEventHandler())
                .UsingElementHandler(EndEvent.ElementTypeName, new GeneralEventHandler())
                .UsingElementHandler(BpmnSequenceElements.Task.ElementTypeName, new GeneralEventHandler())
                .UsingElementHandler(ParallelGateway.ElementTypeName, new GeneralEventHandler())
                .WithBpmnSequence(bpmnProcessSequence);

            var bpmnSequenceProcessor = bpmnSequenceProcessorBuilder.Build<SequenceProcessor>();

            bpmnSequenceProcessor.Start();
        }
    }

    public class GeneralEventHandler : ISequenceElementHandler
    {
        public void Process(BpmnSequenceElement currentElement, ISequenceElementHandlerContext context)
        {
            Console.Out.WriteLine($"{currentElement.Type}-{currentElement.Id} processed");
        }
    }
}