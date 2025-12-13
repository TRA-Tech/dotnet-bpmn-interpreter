using System.Reflection;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    /// <summary>
    /// Represents a generic BPMN element parsed from XML.
    /// </summary>
    public class BpmnElement
    {
        /// <summary>
        /// The name of the extension elements XML element.
        /// </summary>
        public const string ExtensionElementsName = "extensionElements";

        /// <summary>
        /// The name of the event definition XML element.
        /// </summary>
        public const string EventDefinitionName = "EventDefinition";

        private readonly string _type;
        private readonly string _id;
        private readonly string? _name;
        private readonly XElement _self;
        private readonly XNamespace _namespace;

        /// <summary>
        /// Gets the type of the BPMN element.
        /// </summary>
        public string Type { get { return _type; } }

        /// <summary>
        /// Gets the ID of the BPMN element.
        /// </summary>
        public string Id { get { return _id; } }

        /// <summary>
        /// Gets the name of the BPMN element.
        /// </summary>
        public string? Name { get { return _name; } }

        /// <summary>
        /// Gets the underlying XML element.
        /// </summary>
        public XElement Self { get { return _self; } }

        /// <summary>
        /// Gets the XML namespace of the element.
        /// </summary>
        public XNamespace Namespace { get { return _namespace; } }

        /// <summary>
        /// Gets a value indicating whether the element has a parent.
        /// </summary>
        public bool HasParent { get { return _self.Parent != null; } }

        /// <summary>
        /// Gets a value indicating whether the element has children.
        /// </summary>
        public bool HasChildren { get { return _self.HasElements; } }

        /// <summary>
        /// Gets a value indicating whether the element has extension elements.
        /// </summary>
        public bool HasExtensionElements { get { return _self.Elements(_namespace.GetName(ExtensionElementsName)).Any(); } }

        /// <summary>
        /// Gets a value indicating whether the element has event definitions.
        /// </summary>
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

        /// <summary>
        /// Gets the children of the element.
        /// </summary>
        public IEnumerable<XElement> Children
        {
            get
            {
                return _self.Elements();
            }
        }

        /// <summary>
        /// Gets the extension elements of the element.
        /// </summary>
        public IEnumerable<XElement> ExtensionElements
        {
            get
            {
                if (HasExtensionElements)
                    return _self.Element(_namespace.GetName(ExtensionElementsName))?.Elements() ?? Enumerable.Empty<XElement>();

                return Enumerable.Empty<XElement>();
            }
        }

        /// <summary>
        /// Gets the event definitions of the element.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BpmnElement"/> class.
        /// </summary>
        /// <param name="self">The XML element representing the BPMN element.</param>
        /// <exception cref="ArgumentNullException">Thrown when self is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when self has no id attribute.</exception>
        public BpmnElement(XElement self)
        {
            _self = self ?? throw new ArgumentNullException(nameof(self));
            _type = _self.Name.LocalName;
            _id = _self.Attribute("id")?.Value ?? throw new InvalidOperationException("self has no id attribute");
            _name = _self.Attribute("name")?.Value;
            _namespace = _self.Name.Namespace;
        }

        /// <summary>
        /// Parses an extension element of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the extension element to parse.</typeparam>
        /// <returns>The parsed extension element.</returns>
        /// <exception cref="Exception">Thrown when the extension element cannot be found or parsed.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the parsing method invocation fails.</exception>
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

        /// <summary>
        /// Parses an event definition of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the event definition to parse.</typeparam>
        /// <returns>The parsed event definition.</returns>
        /// <exception cref="Exception">Thrown when the event definition cannot be found or parsed.</exception>
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

        /// <summary>
        /// Checks if the element has an extension element with the specified name.
        /// </summary>
        /// <param name="extensionElementName">The name of the extension element.</param>
        /// <param name="xNamespace">The XML namespace. If null, the element's namespace is used.</param>
        /// <returns>True if the extension element exists; otherwise, false.</returns>
        public bool HasExtensionElementOf(string extensionElementName, XNamespace? xNamespace = null)
        {
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(extensionElementName);
            return ExtensionElements.Any(a => a.Name == name);
        }

        /// <summary>
        /// Checks if the element has an event definition with the specified name.
        /// </summary>
        /// <param name="eventDefinitionName">The name of the event definition.</param>
        /// <param name="xNamespace">The XML namespace. If null, the element's namespace is used.</param>
        /// <returns>True if the event definition exists; otherwise, false.</returns>
        public bool HasEventDefinitionOf(string eventDefinitionName, XNamespace? xNamespace = null)
        {
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(eventDefinitionName);
            return EventDefinitions.Any(a => a.Name == name);
        }

        /// <summary>
        /// Checks if the element has a child element of the specified type.
        /// </summary>
        /// <param name="childType">The type of the child element.</param>
        /// <param name="xNamespace">The XML namespace. If null, the element's namespace is used.</param>
        /// <returns>True if the child element exists; otherwise, false.</returns>
        public bool HasChildOf(string childType, XNamespace? xNamespace = null)
        {
            if (!HasChildren) return false;
            var nameSpace = xNamespace ?? _namespace;
            var name = nameSpace.GetName(childType);
            return Self.Elements(name).Any();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
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

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
