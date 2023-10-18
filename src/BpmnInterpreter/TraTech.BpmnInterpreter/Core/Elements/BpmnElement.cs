using System.Reflection;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class BpmnElement
    {
        public const string ExtensionElementsName = "extensionElements";
        public const string EventDefinitionName = "EventDefinition";

        private readonly string _type;
        private readonly string _id;
        private readonly string? _name;
        private readonly XElement _self;
        private readonly XNamespace _namespace;

        public string Type { get { return _type; } }
        public string Id { get { return _id; } }
        public string? Name { get { return _name; } }
        public XElement Self { get { return _self; } }
        public XNamespace Namespace { get { return _namespace; } }
        public bool HasParent { get { return _self.Parent != null; } }
        public bool HasChildren { get { return _self.HasElements; } }
        public bool HasExtensionElements { get { return _self.Elements(_namespace.GetName(ExtensionElementsName)).Any(); } }
        public bool HasEventDefinitions
        {
            get
            {
                return _self
                    .Elements()
                    .Any(a => a.Name.LocalName.Contains(
                        EventDefinitionName,
                        StringComparison.InvariantCultureIgnoreCase)
                    );
            }
        }
        public IEnumerable<XElement> Children
        {
            get
            {
                return _self.Elements();
            }
        }
        public IEnumerable<XElement> ExtensionElements
        {
            get
            {
                if (HasExtensionElements)
                    return _self.Element(_namespace.GetName(ExtensionElementsName))?.Elements() ?? Enumerable.Empty<XElement>();

                return Enumerable.Empty<XElement>();
            }
        }
        public IEnumerable<XElement> EventDefinitions
        {
            get
            {
                if (HasEventDefinitions)
                    return _self.Elements()
                    .Where(a => a.Name.LocalName.Contains(
                        EventDefinitionName,
                        StringComparison.InvariantCultureIgnoreCase)
                    );

                return Enumerable.Empty<XElement>();
            }
        }

        public BpmnElement(XElement self)
        {
            _self = self ?? throw new ArgumentNullException(nameof(self));
            _type = _self.Name.LocalName;
            _id = _self.Attribute("id")?.Value ?? throw new InvalidOperationException("self has no id attribute");
            _name = _self.Attribute("name")?.Value;
            _namespace = _self.Name.Namespace;
        }

        public T ParseExtensionElement<T>()
            where T : class
        {
            var type = typeof(T);
            var methodName = "Parse";
            var fieldName = "ElementTypeName";

            var extensionElementTypeNameFieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static)
                ?? throw new Exception($"(field){fieldName} could not found in the (class){type.Name}");

            var extensionElementTypeName = (string?)extensionElementTypeNameFieldInfo.GetValue(null)
                ?? throw new Exception($"(field){fieldName} could not get the value");

            var extensionElement = ExtensionElements.First(f => f.Name.LocalName == extensionElementTypeName)
                ?? throw new Exception($"{extensionElementTypeName} could not found in the extension elements of the element");

            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, new[] { typeof(XElement) })
                ?? throw new Exception($"(method){methodName} could not found in the (class){type.Name}");

            var result = method!.Invoke(null, new object?[] { extensionElement })!;

            if (result is T resultAsT)
            {
                return resultAsT;
            }

            throw new InvalidOperationException($"Failed to invoke the '{methodName}' method");
        }

        public T ParseEventDefinition<T>()
            where T : class
        {
            var type = typeof(T);
            var methodName = "Parse";
            var fieldName = "EventDefinitionTypeName";

            var eventDefinitionTypeNameFieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static)
                ?? throw new Exception($"(field){fieldName} could not found in the (class){type.Name}");

            var eventDefinitionTypeName = (string?)eventDefinitionTypeNameFieldInfo.GetValue(null)
                ?? throw new Exception($"(field){fieldName} could not get the value");

            var eventDefinitionElement = EventDefinitions.First(f => f.Name.LocalName == eventDefinitionTypeName)
                ?? throw new Exception($"{eventDefinitionTypeName} could not found in {nameof(EventDefinitions)}");

            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, new[] { typeof(XElement) })
                ?? throw new Exception($"(method){methodName} could not found in the (class){type.Name}");

            var result = (T)method!.Invoke(null, new object?[] { eventDefinitionElement })!;

            return result;
        }

        public bool HasExtensionElementOf(string extensionElementName, XNamespace? xNamespace = null)
        {
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(extensionElementName);
            return ExtensionElements.Any(a => a.Name == name);
        }

        public bool HasEventDefinitionOf(string eventDefinitionName, XNamespace? xNamespace = null)
        {
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(eventDefinitionName);
            return EventDefinitions.Any(a => a.Name == name);
        }

        public bool HasChildOf(string childType, XNamespace? xNamespace = null)
        {
            if (!HasChildren) return false;
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(childType);
            return Self.Elements(name).Any();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (obj is not BpmnElement bpmnElementObj)
                return false;

            if (bpmnElementObj.Id == Id)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
