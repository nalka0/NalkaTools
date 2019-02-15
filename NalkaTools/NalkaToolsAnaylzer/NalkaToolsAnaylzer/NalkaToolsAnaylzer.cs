using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;
using static NalkaTools.CodeAnalysis.DiagnosticDescriptors;

namespace NalkaTools.CodeAnalysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NalkaToolsAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DontReadSelfInGetterRule, UseDestroyerRule, UseInstantiaterRule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterOperationAction(ApplyUseDestroyerRule, OperationKind.Invocation);
            context.RegisterOperationAction(ApplyUseInstantiaterRule, OperationKind.Invocation);
            context.RegisterOperationAction(ApplyDontReadSelfInGetterRule, OperationKind.PropertyReference);
        }

        private void ApplyDontReadSelfInGetterRule(OperationAnalysisContext context)
        {
            if (context.Operation is IPropertyReferenceOperation propertyReference && context.ContainingSymbol is IMethodSymbol getter && SymbolEqualityComparer.Default.Equals(propertyReference.Property, getter.AssociatedSymbol))
            {
                context.ReportDiagnostic(Diagnostic.Create(DontReadSelfInGetterRule, propertyReference.Syntax.GetLocation(), propertyReference.Property.Name));
            }
        }

        private void ApplyUseDestroyerRule(OperationAnalysisContext context)
        {
            IInvocationOperation operation = (IInvocationOperation)context.Operation;
            if (string.Join(".", operation.TargetMethod.ContainingNamespace.Name, operation.TargetMethod.ContainingType.Name, operation.TargetMethod.Name) == "UnityEngine.Object.Destroy")
            {
                context.ReportDiagnostic(Diagnostic.Create(UseDestroyerRule, operation.Syntax.GetLocation()));
            }
        }

        private void ApplyUseInstantiaterRule(OperationAnalysisContext context)
        {
            IInvocationOperation operation = (IInvocationOperation)context.Operation;
            if (string.Join(".", operation.TargetMethod.ContainingNamespace.Name, operation.TargetMethod.ContainingType.Name, operation.TargetMethod.Name) == "UnityEngine.Object.Instantiate")
            {
                context.ReportDiagnostic(Diagnostic.Create(UseInstantiaterRule, operation.Syntax.GetLocation()));
            }
        }
    }
}