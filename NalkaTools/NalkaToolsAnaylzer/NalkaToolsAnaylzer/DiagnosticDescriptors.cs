using Microsoft.CodeAnalysis;
using static NalkaTools.CodeAnalysis.Resources;

namespace NalkaTools.CodeAnalysis
{
    public class DiagnosticDescriptors
    {
        static DiagnosticDescriptors()
        {
            DontReadSelfInGetterRule = CreateDescriptor(CreateLocalizableString(nameof(DontReadSelfInGetterTitle)), "NA001", CreateLocalizableString(nameof(DontReadSelfInGetterMessageFormat)), DiagnosticSeverity.Warning, CreateLocalizableString(nameof(DontReadSelfInGetterDescription)));
            UseDestroyerRule = CreateDescriptor(CreateLocalizableString(nameof(UseDestroyerTitle)), "NA002", CreateLocalizableString(nameof(UseDestroyerMessageFormat)), DiagnosticSeverity.Warning, CreateLocalizableString(nameof(UseDestroyerDescription)));
            UseInstantiaterRule = CreateDescriptor(CreateLocalizableString(nameof(UseInstantiaterTitle)), "NA003", CreateLocalizableString(nameof(UseInstantiaterMessageFormat)), DiagnosticSeverity.Warning, CreateLocalizableString(nameof(UseInstantiaterDescription)));
        }

        private static LocalizableString CreateLocalizableString(string resourceName)
        {
            return new LocalizableResourceString(resourceName, ResourceManager, typeof(Resources));
        }

        private static DiagnosticDescriptor CreateDescriptor(LocalizableString title, string id, LocalizableString messageFormat, DiagnosticSeverity severity, LocalizableString description)
        {
            return new DiagnosticDescriptor(id, title, messageFormat, "NalkaTools", severity, true, description);
        }

        public static DiagnosticDescriptor DontReadSelfInGetterRule { get; }

        public static DiagnosticDescriptor UseDestroyerRule { get; }

        public static DiagnosticDescriptor UseInstantiaterRule { get; }
    }
}