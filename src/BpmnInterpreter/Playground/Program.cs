using TraTech.BpmnInterpreter.Core.BpmnElements;
using TraTech.BpmnInterpreter.Core.BpmnReaders;
using System.Text;
using System.Xml.Linq;

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
    <semantic:startEvent id=""Event_1rfnzok"" name=""START"">
      <semantic:outgoing>Flow_0f1ar8w</semantic:outgoing>
    </semantic:startEvent>
    <semantic:task id=""Activity_047m6uo"">
      <semantic:incoming>Flow_0f1ar8w</semantic:incoming>
      <semantic:outgoing>Flow_16vax8m</semantic:outgoing>
      <semantic:property id=""Property_1vkyhx0"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_06jr67c"">
        <semantic:extensionElements>
          <tra:databaseInputs>
            <tra:databaseInput table=""Kredi"" column=""BankRate"" />
            <tra:databaseInput table=""Kur"" column=""CurrencyCode"" />
            <tra:databaseInput table=""Kur"" column=""CurrencyName"" />
          </tra:databaseInputs>
        </semantic:extensionElements>
        <semantic:sourceRef>DataStoreReference_0d3ngom</semantic:sourceRef>
        <semantic:targetRef>Property_1vkyhx0</semantic:targetRef>
      </semantic:dataInputAssociation>
      <semantic:dataInputAssociation id=""DataInputAssociation_0t9pmi7"">
        <semantic:extensionElements>
          <tra:linkedReportInputs>
            <tra:linkedReportInput report=""SB100"" column=""Ortalama Maliyet"" />
            <tra:linkedReportInput report=""ST510"" column=""Kategori"" />
            <tra:linkedReportInput report=""SB100"" column=""Banka Puanı"" />
          </tra:linkedReportInputs>
        </semantic:extensionElements>
        <semantic:sourceRef>DataObjectReference_1aawayj</semantic:sourceRef>
        <semantic:targetRef>Property_1vkyhx0</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:task>
    <semantic:dataObjectReference id=""DataObjectReference_1aawayj"" dataObjectRef=""DataObject_0vbt3xe"" />
    <semantic:dataObject id=""DataObject_0vbt3xe"" />
    <semantic:dataStoreReference id=""DataStoreReference_0d3ngom"" />
    <semantic:endEvent id=""Event_0zz5c74"">
      <semantic:incoming>Flow_16vax8m</semantic:incoming>
    </semantic:endEvent>
    <semantic:sequenceFlow id=""Flow_0f1ar8w"" sourceRef=""Event_1rfnzok"" targetRef=""Activity_047m6uo"" />
    <semantic:sequenceFlow id=""Flow_16vax8m"" sourceRef=""Activity_047m6uo"" targetRef=""Event_0zz5c74"" />
  </semantic:process>
  <bpmndi:BPMNDiagram id=""Trisotech.Visio-_6"" name=""Untitled Diagram"" documentation="""" resolution=""96.00000267028808"">
    <bpmndi:BPMNPlane bpmnElement=""Process_0pbbn3n"">
      <bpmndi:BPMNShape id=""Event_1rfnzok_di"" bpmnElement=""Event_1rfnzok"">
        <dc:Bounds x=""352"" y=""272"" width=""36"" height=""36"" />
        <bpmndi:BPMNLabel>
          <dc:Bounds x=""352"" y=""315"" width=""36"" height=""14"" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_047m6uo_di"" bpmnElement=""Activity_047m6uo"">
        <dc:Bounds x=""470"" y=""250"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""DataObjectReference_1aawayj_di"" bpmnElement=""DataObjectReference_1aawayj"">
        <dc:Bounds x=""502"" y=""405"" width=""36"" height=""50"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""DataStoreReference_0d3ngom_di"" bpmnElement=""DataStoreReference_0d3ngom"">
        <dc:Bounds x=""495"" y=""135"" width=""50"" height=""50"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0zz5c74_di"" bpmnElement=""Event_0zz5c74"">
        <dc:Bounds x=""642"" y=""272"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_0f1ar8w_di"" bpmnElement=""Flow_0f1ar8w"">
        <di:waypoint x=""388"" y=""290"" />
        <di:waypoint x=""470"" y=""290"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_16vax8m_di"" bpmnElement=""Flow_16vax8m"">
        <di:waypoint x=""570"" y=""290"" />
        <di:waypoint x=""642"" y=""290"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_06jr67c_di"" bpmnElement=""DataInputAssociation_06jr67c"">
        <di:waypoint x=""520"" y=""185"" />
        <di:waypoint x=""520"" y=""250"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""DataInputAssociation_0t9pmi7_di"" bpmnElement=""DataInputAssociation_0t9pmi7"">
        <di:waypoint x=""520"" y=""405"" />
        <di:waypoint x=""520"" y=""330"" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</semantic:definitions>
";

            var bpmnProcessReader = new BpmnProcessReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                bpmnProcessReader.Load(ms);
                bpmnElements = bpmnProcessReader.Read();
            }

            var dataInputAssociation = bpmnElements
                .Where(w => w.Type == DataInputAssociation.ElementTypeName)
                .Select(element => new DataInputAssociation(element.Self))
                .First();

            Console.WriteLine(dataInputAssociation.HasExtensionElementOf(DatabaseInputs.XName));
            Console.WriteLine(dataInputAssociation.HasExtensionElementOf(DatabaseInputs.XNamespace, DatabaseInputs.ElementTypeName));

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