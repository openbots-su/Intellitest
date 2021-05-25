using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;

namespace Intellitest
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            var workspace = new AdhocWorkspace(host);
        
            var scriptCode = "Guid.N";

            // Need to pass in an array of all usings statements
            var compilationOptions = new CSharpCompilationOptions(
               OutputKind.DynamicallyLinkedLibrary,
               usings: new[] { "System" });

            var scriptProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "Script", "Script", LanguageNames.CSharp, isSubmission: true)
               .WithMetadataReferences(new[]
               {
       MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
               })
               .WithCompilationOptions(compilationOptions);

            var scriptProject = workspace.AddProject(scriptProjectInfo);
            var scriptDocumentInfo = DocumentInfo.Create(
                DocumentId.CreateNewId(scriptProject.Id), "Script",
                sourceCodeKind: SourceCodeKind.Script,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(scriptCode), VersionStamp.Create())));
            var scriptDocument = workspace.AddDocument(scriptDocumentInfo);
            // scriptProject.GetCompilationAsync().GetAwaiter().GetResult();
            // cursor position is at the end
            var position = scriptCode.Length - 1;

            var completionService = CompletionService.GetService(scriptDocument);
            var results = completionService.GetCompletionsAsync(scriptDocument, position).GetAwaiter().GetResult();

            foreach (var i in results.Items)
            {
                Console.WriteLine(i.DisplayText);

                foreach (var prop in i.Properties)
                {
                    Console.Write($"{prop.Key}:{prop.Value}  ");
                }

                Console.WriteLine();
                foreach (var tag in i.Tags)
                {
                    Console.Write($"{tag}  ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
            Console.Read();
            
            //SyntaxTree tree = CSharpSyntaxTree.ParseText("int a = 1+1;");
            //tree.GetDiagnostics();
        }

        public static async Task MainAsync()
        {

        }
    }
}
