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
<bpmn:definitions xmlns:bpmn=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:tra=""http://tra"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"">
  <bpmn:process id=""Process_main"">
    <bpmn:startEvent id=""Event_0r87j0m"">
      <bpmn:extensionElements>
        <tra:crons />
      </bpmn:extensionElements>
      <bpmn:outgoing>Flow_0tp1fjo</bpmn:outgoing>
      <bpmn:outgoing>Flow_12glj21</bpmn:outgoing>
      <bpmn:outgoing>Flow_1jwj68j</bpmn:outgoing>
      <bpmn:timerEventDefinition id=""TimerEventDefinition_17eefzv"" />
    </bpmn:startEvent>
    <bpmn:endEvent id=""Event_16j3dzi"">
      <bpmn:incoming>Flow_1emty08</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:task id=""Activity_0elw6fh"" tra:subtype=""sqlScript"" tra:dataSource=""3"" tra:script=""SELECT TOP (1000) [Id]&#10;      ,[CustomerName]&#10;      ,[FecCode]&#10;      ,[DebtFER]&#10;      ,[ProjectDate]&#10;	  ,[AnaparaTL]&#10;  FROM [LegalRegulationDataSource].[dbo].[Kredi] where ProjectDate = &#39;2022-11-23 00:00:00&#39;"">
      <bpmn:extensionElements>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_12glj21</bpmn:incoming>
      <bpmn:outgoing>Flow_04eaabo</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""Activity_1gi1unn"" tra:subtype=""sqlBuilder"">
      <bpmn:extensionElements>
        <tra:sqlBuilderFrom dataSource=""3"" tableName=""dbo.Kur"" />
        <tra:sqlBuilderJoin sourceColumn="""" targetTable="""" targetColumn="""" />
        <tra:sqlBuilderSelect>
          <tra:sqlBuilderSelectColumn column=""dbo.Kur.FecCode"" type=""string"" alias=""DboKurFecCode"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kur.FecName"" type=""string"" alias=""DboKurFecName"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kur.CurrencyBid"" type=""number"" alias=""DboKurCurrencyBid"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kur.Trandate"" type=""date"" alias=""DboKurTrandate"" />
        </tra:sqlBuilderSelect>
        <tra:sqlBuilderWhere>
          <tra:sqlBuilderWhereCondition column=""dbo.Kur.Trandate"" operator=""equal"" value=""&#39;2022-11-23 00:00:00&#39;"" />
        </tra:sqlBuilderWhere>
        <tra:sqlBuilderLimit limit="""" />
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_0tp1fjo</bpmn:incoming>
      <bpmn:outgoing>Flow_1yc47e6</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""Activity_1oiyujw"" tra:subtype=""join"" tra:outerVariableName=""Kredi"" tra:outer=""Activity_0elw6fh"" tra:outerSelector=""data"" tra:outerField=""FecCode"" tra:innerVariableName=""Kur"" tra:inner=""Activity_1gi1unn"" tra:innerSelector=""data"" tra:innerField=""DboKurFecCode"">
      <bpmn:extensionElements>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_04eaabo</bpmn:incoming>
      <bpmn:incoming>Flow_1yc47e6</bpmn:incoming>
      <bpmn:incoming>Flow_0brrc1j</bpmn:incoming>
      <bpmn:outgoing>Flow_1mdfg0c</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""Activity_1oqob5c"" tra:subtype=""map"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""data"" />
        <tra:maps>
          <tra:map variableName=""AnaparaDoviz"" formula=""Kredi_AnaparaTL * Kur_DboKurCurrencyBid"" formulaExpression=""eyJ0eXBlIjoiQmluYXJ5RXhwcmVzc2lvbiIsIm9wZXJhdG9yIjoiKiIsImxlZnQiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoiS3JlZGlfQW5hcGFyYVRMIn0sInJpZ2h0Ijp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6Ikt1cl9EYm9LdXJDdXJyZW5jeUJpZCJ9fQ=="" />
          <tra:map variableName=""AnaparaTLBinlik"" formula=""Kredi_AnaparaTL / 1000"" formulaExpression=""eyJ0eXBlIjoiQmluYXJ5RXhwcmVzc2lvbiIsIm9wZXJhdG9yIjoiLyIsImxlZnQiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoiS3JlZGlfQW5hcGFyYVRMIn0sInJpZ2h0Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOjEwMDAsInJhdyI6IjEwMDAifX0="" />
        </tra:maps>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1mdfg0c</bpmn:incoming>
      <bpmn:outgoing>Flow_0y8uenm</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""Activity_1foj578"" tra:subtype=""reducer"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""data"" />
        <tra:reducers>
          <tra:reducer name=""AnaparaTLBinlikTotal"" expression=""Sum(data,&#39;AnaparaTLBinlik&#39;)"" parsedExpression=""eyJ0eXBlIjoiQ2FsbEV4cHJlc3Npb24iLCJhcmd1bWVudHMiOlt7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6ImRhdGEifSx7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJBbmFwYXJhVExCaW5saWsiLCJyYXciOiInQW5hcGFyYVRMQmlubGlrJyJ9XSwiY2FsbGVlIjp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6IlN1bSJ9fQ=="" type=""builder"" />
          <tra:reducer name=""AnaparaDovizTotal"" expression=""Sum(data,&#39;AnaparaDoviz&#39;)"" parsedExpression=""eyJ0eXBlIjoiQ2FsbEV4cHJlc3Npb24iLCJhcmd1bWVudHMiOlt7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6ImRhdGEifSx7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJBbmFwYXJhRG92aXoiLCJyYXciOiInQW5hcGFyYURvdml6JyJ9XSwiY2FsbGVlIjp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6IlN1bSJ9fQ=="" type=""builder"" />
        </tra:reducers>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;result&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJyZXN1bHQiLCJyYXciOiJcInJlc3VsdFwiIn0sInZhbHVlIjp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6Im91dHB1dCJ9LCJzaG9ydGhhbmQiOmZhbHNlfV19"" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_0y8uenm</bpmn:incoming>
      <bpmn:outgoing>Flow_1emty08</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_0tp1fjo"" sourceRef=""Event_0r87j0m"" targetRef=""Activity_1gi1unn"" />
    <bpmn:sequenceFlow id=""Flow_04eaabo"" sourceRef=""Activity_0elw6fh"" targetRef=""Activity_1oiyujw"" />
    <bpmn:sequenceFlow id=""Flow_1yc47e6"" sourceRef=""Activity_1gi1unn"" targetRef=""Activity_1oiyujw"" />
    <bpmn:sequenceFlow id=""Flow_1emty08"" sourceRef=""Activity_1foj578"" targetRef=""Event_16j3dzi"" />
    <bpmn:sequenceFlow id=""Flow_1mdfg0c"" sourceRef=""Activity_1oiyujw"" targetRef=""Activity_1oqob5c"" />
    <bpmn:sequenceFlow id=""Flow_0y8uenm"" sourceRef=""Activity_1oqob5c"" targetRef=""Activity_1foj578"" />
    <bpmn:task id=""Activity_1w1rwwd"" tra:subtype=""sqlBuilder"">
      <bpmn:extensionElements>
        <tra:sqlBuilderFrom dataSource="""" tableName="""" />
        <tra:sqlBuilderJoin sourceColumn="""" targetTable="""" targetColumn="""" />
        <tra:sqlBuilderSelect />
        <tra:sqlBuilderWhere />
        <tra:sqlBuilderLimit limit="""" />
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1jwj68j</bpmn:incoming>
      <bpmn:outgoing>Flow_0brrc1j</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_0brrc1j"" sourceRef=""Activity_1w1rwwd"" targetRef=""Activity_1oiyujw"" />
    <bpmn:sequenceFlow id=""Flow_12glj21"" sourceRef=""Event_0r87j0m"" targetRef=""Activity_0elw6fh"" />
    <bpmn:sequenceFlow id=""Flow_1jwj68j"" sourceRef=""Event_0r87j0m"" targetRef=""Activity_1w1rwwd"" />
  </bpmn:process>
  <bpmndi:BPMNDiagram id=""BPMNDiagram_1"">
    <bpmndi:BPMNPlane id=""BPMNPlane_1"" bpmnElement=""Process_main"">
      <bpmndi:BPMNShape id=""Event_16j3dzi_di"" bpmnElement=""Event_16j3dzi"">
        <dc:Bounds x=""842"" y=""102"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_0elw6fh_di"" bpmnElement=""Activity_0elw6fh"">
        <dc:Bounds x=""110"" y=""0"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1gi1unn_di"" bpmnElement=""Activity_1gi1unn"">
        <dc:Bounds x=""110"" y=""150"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1oiyujw_di"" bpmnElement=""Activity_1oiyujw"">
        <dc:Bounds x=""260"" y=""80"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1oqob5c_di"" bpmnElement=""Activity_1oqob5c"">
        <dc:Bounds x=""460"" y=""80"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1foj578_di"" bpmnElement=""Activity_1foj578"">
        <dc:Bounds x=""650"" y=""80"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_1w1rwwd_di"" bpmnElement=""Activity_1w1rwwd"">
        <dc:Bounds x=""110"" y=""280"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0r87j0m_di"" bpmnElement=""Event_0r87j0m"">
        <dc:Bounds x=""-38"" y=""172"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_0tp1fjo_di"" bpmnElement=""Flow_0tp1fjo"">
        <di:waypoint x=""-2"" y=""190"" />
        <di:waypoint x=""110"" y=""190"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_04eaabo_di"" bpmnElement=""Flow_04eaabo"">
        <di:waypoint x=""210"" y=""40"" />
        <di:waypoint x=""235"" y=""40"" />
        <di:waypoint x=""235"" y=""120"" />
        <di:waypoint x=""260"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1yc47e6_di"" bpmnElement=""Flow_1yc47e6"">
        <di:waypoint x=""210"" y=""190"" />
        <di:waypoint x=""235"" y=""190"" />
        <di:waypoint x=""235"" y=""120"" />
        <di:waypoint x=""260"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1emty08_di"" bpmnElement=""Flow_1emty08"">
        <di:waypoint x=""750"" y=""120"" />
        <di:waypoint x=""842"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1mdfg0c_di"" bpmnElement=""Flow_1mdfg0c"">
        <di:waypoint x=""360"" y=""120"" />
        <di:waypoint x=""460"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0y8uenm_di"" bpmnElement=""Flow_0y8uenm"">
        <di:waypoint x=""560"" y=""120"" />
        <di:waypoint x=""650"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_0brrc1j_di"" bpmnElement=""Flow_0brrc1j"">
        <di:waypoint x=""210"" y=""320"" />
        <di:waypoint x=""235"" y=""320"" />
        <di:waypoint x=""235"" y=""120"" />
        <di:waypoint x=""260"" y=""120"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_12glj21_di"" bpmnElement=""Flow_12glj21"">
        <di:waypoint x=""-2"" y=""190"" />
        <di:waypoint x=""54"" y=""190"" />
        <di:waypoint x=""54"" y=""40"" />
        <di:waypoint x=""110"" y=""40"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1jwj68j_di"" bpmnElement=""Flow_1jwj68j"">
        <di:waypoint x=""-2"" y=""190"" />
        <di:waypoint x=""54"" y=""190"" />
        <di:waypoint x=""54"" y=""320"" />
        <di:waypoint x=""110"" y=""320"" />
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