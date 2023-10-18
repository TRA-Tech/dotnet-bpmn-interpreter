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
<semantic:definitions xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:bpmndi=""http://www.omg.org/spec/BPMN/20100524/DI"" xmlns:dc=""http://www.omg.org/spec/DD/20100524/DC"" xmlns:semantic=""http://www.omg.org/spec/BPMN/20100524/MODEL"" xmlns:tra=""http://tra"" xmlns:di=""http://www.omg.org/spec/DD/20100524/DI"" id=""_1275940932088"" targetNamespace=""http://www.trisotech.com/definitions/_1275940932088"" exporter=""Camunda Modeler"" exporterVersion=""1.16.0"">
  <semantic:process id=""Process_0pbbn3n"">
    <semantic:endEvent id=""Event_1b45bii"" name=""END"">
      <semantic:incoming>Flow_0qp9ug1</semantic:incoming>
    </semantic:endEvent>
    <semantic:task id=""Activity_1kqp03h"" name=""SELECT"">
      <semantic:extensionElements>
        <tra:selects dataSource=""Kur"" variableName=""KurTable"">
          <tra:select variableName=""Variable1"" formula=""Formula1"" />
          <tra:select variableName=""Variable2"" formula=""Formula2"" />
          <tra:select variableName=""Variable3"" formula=""Formula3"" />
          <tra:select variableName=""Variable4"" formula=""Formula4"" />
          <tra:select variableName=""Variable5"" formula=""Formula5"" />
        </tra:selects>
      </semantic:extensionElements>
      <semantic:incoming>Flow_0khzydu</semantic:incoming>
      <semantic:outgoing>Flow_0r1npi2</semantic:outgoing>
    </semantic:task>
    <semantic:dataStoreReference id=""DataStoreReference_088alvo"" name=""Kredi"" tra:tableName=""Kredi"" />
    <semantic:intermediateCatchEvent id=""Event_19f836e"" tra:cronExpression=""0 * * ? * * *"">
      <semantic:incoming>Flow_0r1npi2</semantic:incoming>
      <semantic:outgoing>Flow_0qp9ug1</semantic:outgoing>
      <semantic:timerEventDefinition id=""TimerEventDefinition_049497b"" />
    </semantic:intermediateCatchEvent>
    <semantic:sequenceFlow id=""Flow_0r1npi2"" sourceRef=""Activity_1kqp03h"" targetRef=""Event_19f836e"" />
    <semantic:sequenceFlow id=""Flow_0qp9ug1"" sourceRef=""Event_19f836e"" targetRef=""Event_1b45bii"" />
    <semantic:scriptTask id=""Activity_1di90xd"">
      <semantic:outgoing>Flow_0khzydu</semantic:outgoing>
      <semantic:property id=""Property_030yb2h"" name=""__targetRef_placeholder"" />
      <semantic:dataInputAssociation id=""DataInputAssociation_14812x9"">
        <semantic:extensionElements>
          <tra:tableFields>
            <tra:tableField fieldName=""AccountNumber"" />
            <tra:tableField fieldName=""AccountSuffix"" />
            <tra:tableField fieldName=""AnaparaTl"" />
          </tra:tableFields>
        </semantic:extensionElements>
        <semantic:sourceRef>DataStoreReference_088alvo</semantic:sourceRef>
        <semantic:targetRef>Property_030yb2h</semantic:targetRef>
      </semantic:dataInputAssociation>
    </semantic:scriptTask>
    <semantic:sequenceFlow id=""Flow_0khzydu"" sourceRef=""Activity_1di90xd"" targetRef=""Activity_1kqp03h"" />
  </semantic:process>
</semantic:definitions>

";

            var bpmnProcessReader = new BpmnReader("http://www.omg.org/spec/BPMN/20100524/MODEL");
            IEnumerable<BpmnElement> bpmnElements;

            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes(xml)))
            {
                var bpmnDocument = XDocument.Load(ms);
                bpmnElements = bpmnProcessReader.Read(bpmnDocument);
            }

            var scriptTask = bpmnElements.First(f => f.Type == BpmnSequenceElements.ScriptTask.ElementTypeName);

            var hasChildOfDIA = scriptTask.HasChildOf(DataInputAssociation.ElementTypeName);

            var children = scriptTask.Children.ToList();

            var sequence = new Sequence(bpmnElements);

            var sequenceProcessor = ISequenceProcessorBuilder
                .Create<SequenceProcessorBuilder>()
                .UsingElementHandler(StartEvent.ElementTypeName, new StartEventHandler())
                .UsingElementHandler(BpmnSequenceElements.Task.ElementTypeName, new TaskHandler())
                .UsingElementHandler(EndEvent.ElementTypeName, new EndEventHandler())
                .WithBpmnSequence(sequence)
                .Build<SequenceProcessor>();

            sequenceProcessor.Start();
        }
    }
}