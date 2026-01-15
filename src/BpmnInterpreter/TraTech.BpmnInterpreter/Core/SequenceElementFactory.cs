using System.Reflection;
using System.Xml.Linq;
using TraTech.BpmnInterpreter.Core.SequenceElements;

namespace TraTech.BpmnInterpreter.Core
{
    /// <summary>
    /// Creates <see cref="BpmnSequenceElement"/> instances from BPMN XML elements.
    /// </summary>
    /// <remarks>
    /// The factory discovers concrete <see cref="BpmnSequenceElement"/> types via reflection.
    /// For a type to be eligible it must:
    /// <list type="bullet">
    /// <item><description>Define a public static string field named <c>ElementTypeName</c>.</description></item>
    /// <item><description>Expose a public constructor whose first parameter is <see cref="XElement"/>, with any remaining parameters being optional.</description></item>
    /// </list>
    /// If no matching type is found, a plain <see cref="BpmnSequenceElement"/> is created.
    /// </remarks>
    internal static class SequenceElementFactory
    {
        private static readonly Lazy<IReadOnlyDictionary<string, Func<XElement, BpmnSequenceElement>>> _factories =
            new(BuildFactories);

        /// <summary>
        /// Creates a <see cref="BpmnSequenceElement"/> for the given BPMN XML element.
        /// </summary>
        /// <param name="element">The BPMN XML element.</param>
        /// <returns>A concrete <see cref="BpmnSequenceElement"/> when a matching factory is found; otherwise a base <see cref="BpmnSequenceElement"/>.</returns>
        public static BpmnSequenceElement Create(XElement element)
        {
            var typeName = element.Name.LocalName;
            if (_factories.Value.TryGetValue(typeName, out var factory))
            {
                return factory(element);
            }

            return new BpmnSequenceElement(element);
        }

        /// <summary>
        /// Builds the mapping from BPMN element type names to their corresponding factory delegates.
        /// </summary>
        private static IReadOnlyDictionary<string, Func<XElement, BpmnSequenceElement>> BuildFactories()
        {
            var baseType = typeof(BpmnSequenceElement);
            var types = baseType.Assembly
                .GetTypes()
                .Where(t =>
                    t is { IsAbstract: false, IsInterface: false } &&
                    baseType.IsAssignableFrom(t));

            var map = new Dictionary<string, Func<XElement, BpmnSequenceElement>>(StringComparer.Ordinal);

            foreach (var type in types)
            {
                var field = type.GetField("ElementTypeName", BindingFlags.Public | BindingFlags.Static);
                if (field is null || field.FieldType != typeof(string))
                {
                    continue;
                }

                var elementTypeName = field.GetValue(null) as string;
                if (string.IsNullOrWhiteSpace(elementTypeName))
                {
                    continue;
                }

                var ctor = type
                    .GetConstructors()
                    .Select(c => new { Ctor = c, Params = c.GetParameters() })
                    .Where(x =>
                        x.Params.Length >= 1 &&
                        x.Params[0].ParameterType == typeof(XElement) &&
                        x.Params.Skip(1).All(p => p.IsOptional))
                    .Select(x => x.Ctor)
                    .FirstOrDefault();

                if (ctor is null)
                {
                    continue;
                }

                map[elementTypeName] = (x) =>
                {
                    var parameters = ctor.GetParameters();
                    var args = new object?[parameters.Length];
                    args[0] = x;
                    for (var i = 1; i < parameters.Length; i++)
                    {
                        args[i] = parameters[i].DefaultValue;
                    }

                    return (BpmnSequenceElement)ctor.Invoke(args);
                };
            }

            return map;
        }
    }
}
