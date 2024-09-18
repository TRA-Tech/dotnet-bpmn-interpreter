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
    <bpmn:startEvent id=""Event_1y3wtom"">
      <bpmn:extensionElements>
        <tra:crons />
      </bpmn:extensionElements>
      <bpmn:outgoing>Flow_01ukgpq</bpmn:outgoing>
      <bpmn:timerEventDefinition id=""TimerEventDefinition_1erb8ot"" />
    </bpmn:startEvent>
    <bpmn:task id=""kredi_mevduat"" tra:subtype=""sqlBuilder"">
      <bpmn:extensionElements>
        <tra:sqlBuilderFrom dataSource=""1"" tableName=""dbo.Kredi"" />
        <tra:sqlBuilderJoin sourceColumn=""CustomerName"" targetTable=""dbo.Mevduat"" targetColumn=""CustomerName"" />
        <tra:sqlBuilderSelect>
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.Id"" type=""number"" alias=""DboKrediId"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.CustomerName"" type=""string"" alias=""DboKrediCustomerName"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.PaymentDate"" type=""date"" alias=""DboKrediPaymentDate"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.AccountNumber"" type=""number"" alias=""DboKrediAccountNumber"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Mevduat.BakiyeTL"" type=""number"" alias=""DboMevduatBakiyeTL"" />
        </tra:sqlBuilderSelect>
        <tra:sqlBuilderWhere />
        <tra:sqlBuilderLimit limit=""10"" />
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_01ukgpq</bpmn:incoming>
      <bpmn:outgoing>Flow_1r5joak</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_01ukgpq"" sourceRef=""Event_1y3wtom"" targetRef=""kredi_mevduat"" />
    <bpmn:task id=""musteri"" tra:subtype=""sqlScript"" tra:dataSource=""1"" tra:script=""SELECT TOP (500) * from musteri "">
      <bpmn:extensionElements>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:outgoing>Flow_0iizh6b</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""joined_kredi_musteri"" tra:subtype=""join"" tra:outerVariableName=""sqlbuilderKredi"" tra:outer=""kredi_mevduat"" tra:outerSelector=""data"" tra:outerField=""DboKrediCustomerName"" tra:innerVariableName=""SqlScriptMusteri"" tra:inner=""musteri"" tra:innerSelector=""data"" tra:innerField=""CustomerName"">
      <bpmn:extensionElements>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;joined&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJqb2luZWQiLCJyYXciOiJcImpvaW5lZFwiIn0sInZhbHVlIjp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6Im91dHB1dCJ9LCJzaG9ydGhhbmQiOmZhbHNlfV19"" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1r5joak</bpmn:incoming>
      <bpmn:incoming>Flow_0iizh6b</bpmn:incoming>
      <bpmn:outgoing>Flow_1lvnlok</bpmn:outgoing>
      <bpmn:outgoing>Flow_0kbfwq3</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""filter_element"" tra:subtype=""filter"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""joined_kredi_musteri.joined"" />
        <tra:filters>
          <tra:filter name=""customername"" filterType=""builder"" expression=""sqlbuilderKredi_DboKrediCustomerName %= (&#39;kuruluş&#39;)"" parsedExpression=""eyJ0eXBlIjoiQmluYXJ5RXhwcmVzc2lvbiIsIm9wZXJhdG9yIjoiJT0iLCJsZWZ0Ijp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6InNxbGJ1aWxkZXJLcmVkaV9EYm9LcmVkaUN1c3RvbWVyTmFtZSJ9LCJyaWdodCI6eyJ0eXBlIjoiTGl0ZXJhbCIsInZhbHVlIjoia3VydWx1xZ8iLCJyYXciOiIna3VydWx1xZ8nIn19"" />
        </tra:filters>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;filtered&#34;: output,&#10;  &#34;my&#34;: input.reducerelement.reducer&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJmaWx0ZXJlZCIsInJhdyI6IlwiZmlsdGVyZWRcIiJ9LCJ2YWx1ZSI6eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJvdXRwdXQifSwic2hvcnRoYW5kIjpmYWxzZX0seyJ0eXBlIjoiUHJvcGVydHkiLCJjb21wdXRlZCI6ZmFsc2UsImtleSI6eyJ0eXBlIjoiTGl0ZXJhbCIsInZhbHVlIjoibXkiLCJyYXciOiJcIm15XCIifSwidmFsdWUiOnsidHlwZSI6Ik1lbWJlckV4cHJlc3Npb24iLCJjb21wdXRlZCI6ZmFsc2UsIm9iamVjdCI6eyJ0eXBlIjoiTWVtYmVyRXhwcmVzc2lvbiIsImNvbXB1dGVkIjpmYWxzZSwib2JqZWN0Ijp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6ImlucHV0In0sInByb3BlcnR5Ijp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6InJlZHVjZXJlbGVtZW50In19LCJwcm9wZXJ0eSI6eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJyZWR1Y2VyIn19LCJzaG9ydGhhbmQiOmZhbHNlfV19"" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1lvnlok</bpmn:incoming>
      <bpmn:incoming>Flow_1b6t3oc</bpmn:incoming>
      <bpmn:outgoing>Flow_02yl0fp</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""map2_element3"" tra:subtype=""map"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""filter_element.filtered"" />
        <tra:maps>
          <tra:map variableName=""name"" formula=""sqlbuilderKredi_DboKrediCustomerName"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJzcWxidWlsZGVyS3JlZGlfRGJvS3JlZGlDdXN0b21lck5hbWUifQ=="" />
          <tra:map variableName=""accountnumber"" formula=""sqlbuilderKredi_DboKrediAccountNumber"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJzcWxidWlsZGVyS3JlZGlfRGJvS3JlZGlBY2NvdW50TnVtYmVyIn0="" />
          <tra:map variableName=""date"" formula=""sqlbuilderKredi_DboKrediPaymentDate"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJzcWxidWlsZGVyS3JlZGlfRGJvS3JlZGlQYXltZW50RGF0ZSJ9"" />
          <tra:map variableName=""bakiye"" formula=""sqlbuilderKredi_DboMevduatBakiyeTL"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJzcWxidWlsZGVyS3JlZGlfRGJvTWV2ZHVhdEJha2l5ZVRMIn0="" />
          <tra:map variableName=""musname"" formula=""SqlScriptMusteri_Name"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJTcWxTY3JpcHRNdXN0ZXJpX05hbWUifQ=="" />
          <tra:map variableName=""ulkekodu"" formula=""SqlScriptMusteri_UlkeKodu"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJTcWxTY3JpcHRNdXN0ZXJpX1Vsa2VLb2R1In0="" />
          <tra:map variableName=""organization"" formula=""SqlScriptMusteri_OrganizationClassName"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJTcWxTY3JpcHRNdXN0ZXJpX09yZ2FuaXphdGlvbkNsYXNzTmFtZSJ9"" />
        </tra:maps>
        <tra:outputMapping saveOutput=""true"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_02yl0fp</bpmn:incoming>
      <bpmn:outgoing>Flow_17vkdjm</bpmn:outgoing>
    </bpmn:task>
    <bpmn:endEvent id=""Event_0iya3f4"">
      <bpmn:incoming>Flow_17vkdjm</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:sequenceFlow id=""Flow_1r5joak"" sourceRef=""kredi_mevduat"" targetRef=""joined_kredi_musteri"" />
    <bpmn:sequenceFlow id=""Flow_0iizh6b"" sourceRef=""musteri"" targetRef=""joined_kredi_musteri"" />
    <bpmn:sequenceFlow id=""Flow_1lvnlok"" sourceRef=""joined_kredi_musteri"" targetRef=""filter_element"" />
    <bpmn:sequenceFlow id=""Flow_02yl0fp"" sourceRef=""filter_element"" targetRef=""map2_element3"" />
    <bpmn:sequenceFlow id=""Flow_17vkdjm"" sourceRef=""map2_element3"" targetRef=""Event_0iya3f4"" />
    <bpmn:task id=""reducerelement"" tra:subtype=""reducer"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""joined_kredi_musteri.joined"" />
        <tra:reducers>
          <tra:reducer name=""max"" expression=""Maximum(joined,&#39;sqlbuilderKredi_DboMevduatBakiyeTL&#39;)"" parsedExpression=""eyJ0eXBlIjoiQ2FsbEV4cHJlc3Npb24iLCJhcmd1bWVudHMiOlt7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6ImpvaW5lZCJ9LHsidHlwZSI6IkxpdGVyYWwiLCJ2YWx1ZSI6InNxbGJ1aWxkZXJLcmVkaV9EYm9NZXZkdWF0QmFraXllVEwiLCJyYXciOiInc3FsYnVpbGRlcktyZWRpX0Rib01ldmR1YXRCYWtpeWVUTCcifV0sImNhbGxlZSI6eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJNYXhpbXVtIn19"" type=""builder"" />
        </tra:reducers>
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;reducer&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJyZWR1Y2VyIiwicmF3IjoiXCJyZWR1Y2VyXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_0kbfwq3</bpmn:incoming>
      <bpmn:outgoing>Flow_1b6t3oc</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_0kbfwq3"" sourceRef=""joined_kredi_musteri"" targetRef=""reducerelement"" />
    <bpmn:sequenceFlow id=""Flow_1b6t3oc"" sourceRef=""reducerelement"" targetRef=""filter_element"" />
  </bpmn:process>
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