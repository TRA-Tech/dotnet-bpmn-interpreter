﻿using System.Reflection;
using System.Xml.Linq;

namespace TraTech.BpmnInterpreter.Core.Elements
{
    public class BpmnElement
    {
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
        public bool HasExtensionElements { get { return _self.Elements(_namespace.GetName("extensionElements")).Any(); } }
        public IEnumerable<XElement> ExtensionElements
        {
            get
            {
                if (HasExtensionElements)
                    return _self.Element(_namespace.GetName("extensionElements"))?.Elements() ?? Enumerable.Empty<XElement>();

                return Enumerable.Empty<XElement>();
            }
        }

        public T ParseExtensionElement<T>()
            where T : class
        {
            var type = typeof(T);
            var methodName = "Parse";
            var fieldName = "ElementTypeName";

            var extensionElementTypeFieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static)
                ?? throw new Exception($"(field){fieldName} could not found in the (class){type.Name}");

            var extensionElementTypeName = (string?)extensionElementTypeFieldInfo.GetValue(null)
                ?? throw new Exception($"(field){fieldName} could not get the value");

            var extensionElement = ExtensionElements.First(f => f.Name.LocalName == extensionElementTypeName)
                ?? throw new Exception($"{extensionElementTypeName} could not found in the extension elements of the element");

            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, new[] { typeof(XElement) })
                ?? throw new Exception($"(method){methodName} could not found in the (class){type.Name}");

            var result = (T)method!.Invoke(null, new object?[] { extensionElement })!;

            return result;
        }

        public BpmnElement(XElement self)
        {
            _self = self ?? throw new ArgumentNullException(nameof(self));
            _type = _self.Name.LocalName;
            _id = _self.Attribute("id")?.Value ?? throw new InvalidOperationException("self has no id attribute");
            _name = _self.Attribute("name")?.Value;
            _namespace = _self.Name.Namespace;

        }

        public bool HasExtensionElementOf(XName extensionElementName)
        {
            return ExtensionElements.Any(a => a.Name == extensionElementName);
        }

        public bool HasExtensionElementOf(XNamespace xNamespace, string extensionElementName)
        {
            var name = xNamespace.GetName(extensionElementName);
            return ExtensionElements.Any(a => a.Name == name);
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