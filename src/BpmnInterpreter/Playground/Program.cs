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
    <bpmn:startEvent id=""Event_1a20tjs"">
      <bpmn:extensionElements>
        <tra:crons>
          <tra:cron expression=""0 0/30 * ? * * *"" />
        </tra:crons>
      </bpmn:extensionElements>
      <bpmn:outgoing>Flow_17n3a2y</bpmn:outgoing>
      <bpmn:timerEventDefinition id=""TimerEventDefinition_0mxmwc9"" />
    </bpmn:startEvent>
    <bpmn:task id=""kredisqlbuilder"" tra:subtype=""sqlBuilder"" name=""kredisqlbuilder"">
      <bpmn:extensionElements>
        <tra:sqlBuilderFrom dataSource=""1"" tableName=""dbo.Kredi"" />
        <tra:sqlBuilderJoin sourceColumn="""" targetTable="""" targetColumn="""" />
        <tra:sqlBuilderSelect>
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.CustomerName"" type=""string"" alias=""DboKrediCustomerName"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.OrderNumber"" type=""number"" alias=""DboKrediOrderNumber"" />
          <tra:sqlBuilderSelectColumn column=""dbo.Kredi.TotalCredit"" type=""number"" alias=""DboKrediTotalCredit"" />
        </tra:sqlBuilderSelect>
        <tra:sqlBuilderWhere />
        <tra:sqlBuilderLimit limit="""" />
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_17n3a2y</bpmn:incoming>
      <bpmn:outgoing>Flow_1f35s69</bpmn:outgoing>
    </bpmn:task>
    <bpmn:task id=""krediordernumberfilter"" tra:subtype=""filter"" name=""krediordernumberfilter"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""kredisqlbuilder.data"" />
        <tra:filters>
          <tra:filter name=""order"" filterType=""builder"" expression=""DboKrediOrderNumber == (18)"" parsedExpression=""eyJ0eXBlIjoiQmluYXJ5RXhwcmVzc2lvbiIsIm9wZXJhdG9yIjoiPT0iLCJsZWZ0Ijp7InR5cGUiOiJJZGVudGlmaWVyIiwibmFtZSI6IkRib0tyZWRpT3JkZXJOdW1iZXIifSwicmlnaHQiOnsidHlwZSI6IkxpdGVyYWwiLCJ2YWx1ZSI6MTgsInJhdyI6IjE4In19"" />
        </tra:filters>
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1f35s69</bpmn:incoming>
      <bpmn:outgoing>Flow_05kznqg</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_17n3a2y"" sourceRef=""Event_1a20tjs"" targetRef=""kredisqlbuilder"" />
    <bpmn:sequenceFlow id=""Flow_1f35s69"" sourceRef=""kredisqlbuilder"" targetRef=""krediordernumberfilter"" />
    <bpmn:task id=""map2items"" tra:subtype=""map"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""MessageEventId.krediordernumberfilter.data"" />
        <tra:maps>
          <tra:map variableName=""DboKrediCustomerName"" formula=""DboKrediCustomerName"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJEYm9LcmVkaUN1c3RvbWVyTmFtZSJ9"" />
          <tra:map variableName=""DboKrediOrderNumber"" formula=""DboKrediOrderNumber"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJEYm9LcmVkaU9yZGVyTnVtYmVyIn0="" />
          <tra:map variableName=""DboKrediTotalCredit"" formula="""" formulaExpression=""eyJ0eXBlIjoiQ29tcG91bmQiLCJib2R5IjpbXX0="" />
        </tra:maps>
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_13vyc9a</bpmn:incoming>
      <bpmn:outgoing>Flow_1qj8tpk</bpmn:outgoing>
    </bpmn:task>
    <bpmn:intermediateCatchEvent id=""MessageEventId"">
      <bpmn:extensionElements>
        <tra:mail to=""ykacar@trabilisim.com; yarenozlemkacar@gmail.com; "" cc=""syazici@trabilisim.com; "" subject=""Konu"" body=""Body"" output=""krediordernumberfilter.data"" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_05kznqg</bpmn:incoming>
      <bpmn:outgoing>Flow_13vyc9a</bpmn:outgoing>
      <bpmn:messageEventDefinition id=""MessageEventDefinition_0o53lno"" />
    </bpmn:intermediateCatchEvent>
    <bpmn:sequenceFlow id=""Flow_05kznqg"" sourceRef=""krediordernumberfilter"" targetRef=""MessageEventId"" />
    <bpmn:task id=""sontask"" tra:subtype=""map"">
      <bpmn:extensionElements>
        <tra:inputSelector selectedInput=""map2items.data"" />
        <tra:maps>
          <tra:map variableName=""customer"" formula=""DboKrediCustomerName"" formulaExpression=""eyJ0eXBlIjoiSWRlbnRpZmllciIsIm5hbWUiOiJEYm9LcmVkaUN1c3RvbWVyTmFtZSJ9"" />
        </tra:maps>
        <tra:outputMapping saveOutput=""false"" expression=""{&#10;  &#34;data&#34;: output&#10;}"" parsedExpression=""eyJ0eXBlIjoiT2JqZWN0RXhwcmVzc2lvbiIsInByb3BlcnRpZXMiOlt7InR5cGUiOiJQcm9wZXJ0eSIsImNvbXB1dGVkIjpmYWxzZSwia2V5Ijp7InR5cGUiOiJMaXRlcmFsIiwidmFsdWUiOiJkYXRhIiwicmF3IjoiXCJkYXRhXCIifSwidmFsdWUiOnsidHlwZSI6IklkZW50aWZpZXIiLCJuYW1lIjoib3V0cHV0In0sInNob3J0aGFuZCI6ZmFsc2V9XX0="" />
      </bpmn:extensionElements>
      <bpmn:incoming>Flow_1qj8tpk</bpmn:incoming>
      <bpmn:outgoing>Flow_1n6d1n8</bpmn:outgoing>
    </bpmn:task>
    <bpmn:sequenceFlow id=""Flow_13vyc9a"" sourceRef=""MessageEventId"" targetRef=""map2items"" />
    <bpmn:sequenceFlow id=""Flow_1qj8tpk"" sourceRef=""map2items"" targetRef=""sontask"" />
    <bpmn:endEvent id=""Event_0ss5h3c"">
      <bpmn:incoming>Flow_1n6d1n8</bpmn:incoming>
    </bpmn:endEvent>
    <bpmn:sequenceFlow id=""Flow_1n6d1n8"" sourceRef=""sontask"" targetRef=""Event_0ss5h3c"" />
    <bpmn:boundaryEvent id=""Event_085mbwq"" attachedToRef=""map2items"">
      <bpmn:extensionElements>
        <tra:mail to=""ykacar@trabilisim.com; syazici@trabilisim.com; "" cc=""cturkan@trabilisim.com; "" subject=""Konu"" body=""gövde"" output=""map2items.data"" />
      </bpmn:extensionElements>
      <bpmn:messageEventDefinition id=""MessageEventDefinition_0aky2kp"" />
    </bpmn:boundaryEvent>
  </bpmn:process>
  <bpmndi:BPMNDiagram id=""BPMNDiagram_1"">
    <bpmndi:BPMNPlane id=""BPMNPlane_1"" bpmnElement=""Process_main"">
      <bpmndi:BPMNShape id=""Event_1a20tjs_di"" bpmnElement=""Event_1a20tjs"">
        <dc:Bounds x=""272"" y=""112"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_11snd2o_di"" bpmnElement=""kredisqlbuilder"">
        <dc:Bounds x=""240"" y=""190"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_140fyfl_di"" bpmnElement=""krediordernumberfilter"">
        <dc:Bounds x=""240"" y=""300"" width=""100"" height=""80"" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0dt2py5_di"" bpmnElement=""MessageEventId"">
        <dc:Bounds x=""272"" y=""402"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_11o6o59_di"" bpmnElement=""sontask"">
        <dc:Bounds x=""440"" y=""460"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Activity_13ygffw_di"" bpmnElement=""map2items"">
        <dc:Bounds x=""240"" y=""460"" width=""100"" height=""80"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_0ss5h3c_di"" bpmnElement=""Event_0ss5h3c"">
        <dc:Bounds x=""612"" y=""482"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id=""Event_085mbwq_di"" bpmnElement=""Event_085mbwq"">
        <dc:Bounds x=""322"" y=""482"" width=""36"" height=""36"" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id=""Flow_17n3a2y_di"" bpmnElement=""Flow_17n3a2y"">
        <di:waypoint x=""290"" y=""148"" />
        <di:waypoint x=""290"" y=""190"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1f35s69_di"" bpmnElement=""Flow_1f35s69"">
        <di:waypoint x=""290"" y=""270"" />
        <di:waypoint x=""290"" y=""300"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_05kznqg_di"" bpmnElement=""Flow_05kznqg"">
        <di:waypoint x=""290"" y=""380"" />
        <di:waypoint x=""290"" y=""402"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_13vyc9a_di"" bpmnElement=""Flow_13vyc9a"">
        <di:waypoint x=""290"" y=""438"" />
        <di:waypoint x=""290"" y=""460"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1qj8tpk_di"" bpmnElement=""Flow_1qj8tpk"">
        <di:waypoint x=""290"" y=""540"" />
        <di:waypoint x=""290"" y=""570"" />
        <di:waypoint x=""480"" y=""570"" />
        <di:waypoint x=""480"" y=""540"" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id=""Flow_1n6d1n8_di"" bpmnElement=""Flow_1n6d1n8"">
        <di:waypoint x=""540"" y=""500"" />
        <di:waypoint x=""612"" y=""500"" />
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