using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static NalkaTools.CodeAnalysis.Resources;
using static NalkaTools.CodeAnalysis.DiagnosticDescriptors;

namespace NalkaTools.CodeAnalysis
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(NalkaToolsCodeFixProvider)), Shared]
    public class NalkaToolsCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(UseInstantiaterRule.Id, UseDestroyerRule.Id);

        public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            foreach (Diagnostic diagnostic in context.Diagnostics)
            {
                InvocationExpressionSyntax invocation = root.FindToken(diagnostic.Location.SourceSpan.Start).Parent.AncestorsAndSelf().OfType<InvocationExpressionSyntax>().First();
                if (diagnostic.Id == UseInstantiaterRule.Id)
                {
                    context.RegisterCodeFix(CodeAction.Create(UseInstantiaterCodeFix, token => ReplaceStaticMethodCall(context.Document, invocation, token, "Instantiate", "Instantiater"), UseInstantiaterCodeFix), diagnostic);
                }
                else if (diagnostic.Id == UseDestroyerRule.Id)
                {
                    context.RegisterCodeFix(CodeAction.Create(UseDestroyerCodeFix, token => ReplaceStaticMethodCall(context.Document, invocation, token, "Destroy", "Destroyer"), UseDestroyerCodeFix), diagnostic);
                }
            }
        }

        private async Task<Document> ReplaceStaticMethodCall(Document document, InvocationExpressionSyntax replacedCall, CancellationToken cancellationToken, string methodName, string className)
        {
            // Get the expression that represents the entity that the invocation happens on
            // (In this case this is the method name including possible class name, namespace, etc)
            ExpressionSyntax replacedExpression = replacedCall.Expression;

            // Construct an expression for the new classname and methodname
            IdentifierNameSyntax classNameSyntax = SyntaxFactory.IdentifierName(className);
            IdentifierNameSyntax methodNameSyntax = SyntaxFactory.IdentifierName(methodName);
            MemberAccessExpressionSyntax methodAccessSyntax = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, classNameSyntax, methodNameSyntax);

            // Add the surrounding trivia from the original expression
            MemberAccessExpressionSyntax replaced = methodAccessSyntax;
            replaced = replaced.WithLeadingTrivia(replacedExpression.GetLeadingTrivia());
            replaced = replaced.WithTrailingTrivia(replacedExpression.GetTrailingTrivia());

            // Replace the whole original expression with the new expression
            return document.WithSyntaxRoot((await document.GetSyntaxRootAsync()).ReplaceNode(replacedExpression, replaced));
        }
    }
}