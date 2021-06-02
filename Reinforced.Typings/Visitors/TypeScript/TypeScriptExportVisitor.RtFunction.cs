using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.TypeScript
{
    partial class TypeScriptExportVisitor
    {
        public override void Visit(RtFunction node)
        {
            if (node == null) return;
            Visit(node.Documentation);
            AppendTabs();
            if (Context != WriterContext.Interface)
            {
                Decorators(node);
                Modifiers(node);
                if (node.IsAsync)
                {
                    Write("async ");
                }
            }
            Visit(node.Identifier);
            Write("(");
            SequentialVisit(node.Arguments, ", ");
            Write(") ");
            if (node.ReturnType != null)
            {
                Write(": ");
                Visit(node.ReturnType);
            }

            if (Context == WriterContext.Interface)
            {
                WriteLine(";");
            }
            else
            {
                if (node.Body != null && !string.IsNullOrEmpty(node.Body.RawContent))
                {
                    CodeBlock(node.Body);
                }
                else
                {
                    EmptyBody(node.ReturnType, node.IsAsync);
                }
            }

            if (!string.IsNullOrEmpty(node.LineAfter))
            {
                AppendTabs();
                Write(node.LineAfter);
                Br();
            }
        }
    }
}