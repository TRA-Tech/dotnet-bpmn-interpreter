using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.BpmnElements;
using TraTech.BpmnInterpreter.Core.BpmnReaders;
using TraTech.BpmnInterpreter.Core.BpmnSequences;
using BpmnProcess = TraTech.BpmnInterpreter.Core.BpmnElements.Process;

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
    <semantic:startEvent id=""Event_0xfl0hc"">
      <semantic:outgoing>Flow_1c5nc61</semantic:outgoing>
    </semantic:startEvent>
    <semantic:task id=""Activity_1wejw3i"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_1c5nc61</semantic:incoming>
      <semantic:outgoing>Flow_02j5fb8</semantic:outgoing>
      <semantic:outgoing>Flow_1umsan9</semantic:outgoing>
    </semantic:task>
    <semantic:sequenceFlow id=""Flow_1c5nc61"" sourceRef=""Event_0xfl0hc"" targetRef=""Activity_1wejw3i"" />
    <semantic:task id=""Activity_0wk076j"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_02j5fb8</semantic:incoming>
      <semantic:outgoing>Flow_15e65ba</semantic:outgoing>
    </semantic:task>
    <semantic:task id=""Activity_1re52l3"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_1umsan9</semantic:incoming>
      <semantic:outgoing>Flow_1cwf3nz</semantic:outgoing>
    </semantic:task>
    <semantic:sequenceFlow id=""Flow_02j5fb8"" sourceRef=""Activity_1wejw3i"" targetRef=""Activity_0wk076j"" />
    <semantic:sequenceFlow id=""Flow_1umsan9"" sourceRef=""Activity_1wejw3i"" targetRef=""Activity_1re52l3"" />
    <semantic:exclusiveGateway id=""Gateway_07c1h1y"">
      <semantic:incoming>Flow_1cwf3nz</semantic:incoming>
      <semantic:incoming>Flow_15e65ba</semantic:incoming>
      <semantic:outgoing>Flow_1tj0zgr</semantic:outgoing>
      <semantic:outgoing>Flow_0aoqljy</semantic:outgoing>
    </semantic:exclusiveGateway>
    <semantic:sequenceFlow id=""Flow_1cwf3nz"" sourceRef=""Activity_1re52l3"" targetRef=""Gateway_07c1h1y"" />
    <semantic:sequenceFlow id=""Flow_15e65ba"" sourceRef=""Activity_0wk076j"" targetRef=""Gateway_07c1h1y"" />
    <semantic:task id=""Activity_09lx777"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_0aoqljy</semantic:incoming>
      <semantic:outgoing>Flow_1ev91p1</semantic:outgoing>
    </semantic:task>
    <semantic:task id=""Activity_14aa10v"">
      <semantic:extensionElements>
        <tra:selects dataSource="""" variableName="""" />
      </semantic:extensionElements>
      <semantic:incoming>Flow_1tj0zgr</semantic:incoming>
      <semantic:outgoing>Flow_10jm4kk</semantic:outgoing>
    </semantic:task>
    <semantic:sequenceFlow id=""Flow_1tj0zgr"" sourceRef=""Gateway_07c1h1y"" targetRef=""Activity_14aa10v"" />
    <semantic:sequenceFlow id=""Flow_0aoqljy"" sourceRef=""Gateway_07c1h1y"" targetRef=""Activity_09lx777"" />
    <semantic:endEvent id=""Event_18pzjtf"">
      <semantic:incoming>Flow_1ev91p1</semantic:incoming>
      <semantic:incoming>Flow_10jm4kk</semantic:incoming>
    </semantic:endEvent>
    <semantic:sequenceFlow id=""Flow_1ev91p1"" sourceRef=""Activity_09lx777"" targetRef=""Event_18pzjtf"" />
    <semantic:sequenceFlow id=""Flow_10jm4kk"" sourceRef=""Activity_14aa10v"" targetRef=""Event_18pzjtf"" />
  </semantic:process>
  <bpmndi:BPMNDiagram id=""Trisotech.Visio-_6"" name=""Untitled Diagram"" documentation="""" resolution=""96.00000267028808"">
    <bpmndi:BPMNPlane bpmnElement=""Process_0pbbn3n"">
      <bpmndi:BPMNShape id=""Event_0xfl0hc_di"" bpmnElement=""Event_0xfl0hc"">
        <dc:Bounds x=""262"" y=""262"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1wejw3i_di"" bpmnElement=""Activity_1wejw3i"">
        <dc:Bounds x=""360"" y=""240"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0wk076j_di"" bpmnElement=""Activity_0wk076j"">
        <dc:Bounds x=""550"" y=""180"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1re52l3_di"" bpmnElement=""Activity_1re52l3"">
        <dc:Bounds x=""550"" y=""310"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Gateway_07c1h1y_di"" bpmnElement=""Gateway_07c1h1y"" isMarkerVisible=""true"">
        <dc:Bounds x=""705"" y=""255"" width=""50"" height=""50"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_09lx777_di"" bpmnElement=""Activity_09lx777"">
        <dc:Bounds x=""800"" y=""180"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_14aa10v_di"" bpmnElement=""Activity_14aa10v"">
        <dc:Bounds x=""800"" y=""310"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_18pzjtf_di"" bpmnElement=""Event_18pzjtf"">
        <dc:Bounds x=""962"" y=""262"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_1c5nc61_di"" bpmnElement=""Flow_1c5nc61"">
        <di:waypoint x=""298"" y=""280"" />
        <di:waypoint x=""360"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_02j5fb8_di"" bpmnElement=""Flow_02j5fb8"">
        <di:waypoint x=""460"" y=""280"" />
        <di:waypoint x=""505"" y=""280"" />
        <di:waypoint x=""505"" y=""220"" />
        <di:waypoint x=""550"" y=""220"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1umsan9_di"" bpmnElement=""Flow_1umsan9"">
        <di:waypoint x=""460"" y=""280"" />
        <di:waypoint x=""505"" y=""280"" />
        <di:waypoint x=""505"" y=""350"" />
        <di:waypoint x=""550"" y=""350"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1cwf3nz_di"" bpmnElement=""Flow_1cwf3nz"">
        <di:waypoint x=""650"" y=""350"" />
        <di:waypoint x=""730"" y=""350"" />
        <di:waypoint x=""730"" y=""305"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_15e65ba_di"" bpmnElement=""Flow_15e65ba"">
        <di:waypoint x=""650"" y=""220"" />
        <di:waypoint x=""730"" y=""220"" />
        <di:waypoint x=""730"" y=""255"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1tj0zgr_di"" bpmnElement=""Flow_1tj0zgr"">
        <di:waypoint x=""750"" y=""285"" />
        <di:waypoint x=""750"" y=""350"" />
        <di:waypoint x=""800"" y=""350"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0aoqljy_di"" bpmnElement=""Flow_0aoqljy"">
        <di:waypoint x=""750"" y=""275"" />
        <di:waypoint x=""750"" y=""220"" />
        <di:waypoint x=""800"" y=""220"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1ev91p1_di"" bpmnElement=""Flow_1ev91p1"">
        <di:waypoint x=""900"" y=""220"" />
        <di:waypoint x=""931"" y=""220"" />
        <di:waypoint x=""931"" y=""280"" />
        <di:waypoint x=""962"" y=""280"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_10jm4kk_di"" bpmnElement=""Flow_10jm4kk"">
        <di:waypoint x=""900"" y=""350"" />
        <di:waypoint x=""931"" y=""350"" />
        <di:waypoint x=""931"" y=""280"" />
        <di:waypoint x=""962"" y=""280"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</semantic:definitions>

            
            ";

            var bpmnProcessReader = new BpmnProcessReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                var bpmnDocument = XDocument.Load(ms);
                bpmnElements = bpmnProcessReader.Read(bpmnDocument);
            }

            var bpmnProcessSequence = new BpmnProcessSequence(bpmnElements);

            foreach (var element in bpmnProcessSequence.BpmnSequenceElements)
            {
                Console.WriteLine(element.Type);
            }

            Console.ReadKey();
        }
    }

    public class DatabaseInputs
    {
        public static readonly string ElementTypeName = "databaseInputs";
        public static readonly XNamespace XNamespace = @"http://tra";
        public static readonly XName XName = XNamespace.GetName(ElementTypeName);

        public readonly IEnumerable<DatabaseInput> Parameters;

        public DatabaseInputs(IEnumerable<DatabaseInput> parameters)
        {
            Parameters = parameters;
        }

        public static DatabaseInputs Parse(XElement element)
        {
            var parameters = element.Elements(XNamespace.GetName(DatabaseInput.ElementTypeName));

            return new DatabaseInputs(
                parameters
                    .Select(s =>
                    new DatabaseInput(
                        s.Attribute("table")?.Value ?? string.Empty,
                        s.Attribute("column")?.Value ?? string.Empty)
                    )
            );
        }

        public class DatabaseInput
        {
            public static readonly string ElementTypeName = "databaseInput";

            private readonly string _table;
            private readonly string _column;

            public string Table { get => _table; }
            public string Column { get => _column; }

            public DatabaseInput(string table, string column)
            {
                _table = table;
                _column = column;
            }
        }
    }
}