using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace IntelliForm
{
    public partial class Form1 : Form
    {
        public AutoCompleteStringCollection suggestionSource;
        private bool wordMatched = false;
        private ProjectInfo scriptProjectInfo;
        private CSharpCompilationOptions compilationOptions;
        private AdhocWorkspace workspace;
        private List<string> completionList;
        private List<int> charsToRemoveList;
        private bool keepHidden = false;
        private TextBox activeCodeInput;
        private CodeGridView activeGridView;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBoxAutoComplete_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Ignore any keys being pressed on the listview
            activeCodeInput.Focus();
        }

        private void listBoxAutoComplete_DoubleClick(object sender, System.EventArgs e)
        {
            GListBox currentListBox = (GListBox)sender;
            // Item double clicked, select it
            if (currentListBox.SelectedItems.Count == 1)
            {
                this.wordMatched = true;
                this.selectItem();
                currentListBox.Hide();
                if (activeGridView != null)
                    activeGridView.listBoxShown = false;
                activeCodeInput.Focus();
                this.wordMatched = false;
            }
        }

        private void selectItem()
        {
            if (this.wordMatched)
            {
                // insert selected item
                string fill = this.gListBox1.SelectedItem.ToString();

                string precursorText = activeCodeInput.Text.Substring(0, activeCodeInput.SelectionStart);
                string postcursorText = activeCodeInput.Text.Substring(activeCodeInput.SelectionStart);
                string initialText = precursorText.Substring(0, precursorText.Length - charsToRemoveList[this.gListBox1.SelectedIndex]);
                string postInsertionText = initialText.Insert(precursorText.Length - charsToRemoveList[this.gListBox1.SelectedIndex], completionList[this.gListBox1.SelectedIndex]);
                activeCodeInput.Text = postInsertionText + postcursorText;
                if (activeGridView != null)
                    activeGridView.CurrentCell.Value = activeCodeInput.Text;

                activeCodeInput.SelectionStart = activeCodeInput.Text.Length;
            }
        }

        private void listBoxAutoComplete_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Make sure when an item is selected, control is returned back to the richtext
            activeCodeInput.Focus();
        }

        private bool populateListBox()
        {
            this.gListBox1.Items.Clear();
            workspace.ClearSolution();
            completionList = new List<string>();
            charsToRemoveList = new List<int>();

            string dec = "List<string> axeys = new List<string>();";

            var scriptCode = dec + activeCodeInput.Text;
            

            var scriptProject = workspace.AddProject(scriptProjectInfo);
            var scriptDocumentInfo = DocumentInfo.Create(
                DocumentId.CreateNewId(scriptProject.Id), "Script",
                sourceCodeKind: SourceCodeKind.Script,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(scriptCode), VersionStamp.Create())));
            var scriptDocument = workspace.AddDocument(scriptDocumentInfo);

            // cursor position is at the end
            var position = dec.Length + activeCodeInput.SelectionStart;

            var completionService = CompletionService.GetService(scriptDocument);
            var results = completionService.GetCompletionsAsync(scriptDocument, position).GetAwaiter().GetResult();
            
            if (results == null)
                return false;
            foreach (var i in results.Items)
            {
                int image = 0;
                if (i.Tags.Contains("Method"))
                    image = 1;
                else if (i.Tags.Contains("ExtensionMethod"))
                    image = 2;
                else if (i.Tags.Contains("Property"))
                    image = 3;
                else if (i.Tags.Contains("Class"))
                    image = 4;
                else if (i.Tags.Contains("Enumerator"))
                    image = 5;
                else if (i.Tags.Contains("Event"))
                    image = 6;
                else if (i.Tags.Contains("Interface"))
                    image = 7;
                else if (i.Tags.Contains("Namespace"))
                    image = 8;
                else if (i.Tags.Contains("Structure"))
                    image = 9;
                string recString = activeCodeInput.Text.Substring(i.Span.Start - dec.Length, i.Span.Length);
                if (scriptCode[dec.Length + activeCodeInput.SelectionStart - 1] != '.')
                {
                    if (recString != "" && i.DisplayText.Length >= recString.Length && i.DisplayText.Substring(0, recString.Length).ToLower().Equals(recString.ToLower()))
                    {
                        if (recString != "")
                        {
                            completionList.Add(i.DisplayText);
                            charsToRemoveList.Add(recString.Length);
                        }
                        else
                        {
                            completionList.Add(i.DisplayText);
                            charsToRemoveList.Add(0);
                        }
                        this.gListBox1.Items.Add(new GListBoxItem(i.DisplayText, image));
                    }
                }
                else
                {
                    completionList.Add(i.DisplayText);
                    charsToRemoveList.Add(0);
                    this.gListBox1.Items.Add(new GListBoxItem(i.DisplayText, image));
                }
            }
            if (this.gListBox1.Items.Count <= 0)
                return false;
            return true;
        }

        private void textBoxCodeInput_textChanged(object sender, EventArgs e)
        {
            activeCodeInput = (TextBox)sender;
            if (populateListBox() && !keepHidden)
            {
                // Find the position of the caret
                Point point = activeCodeInput.GetPositionFromCharIndex(activeCodeInput.SelectionStart);

                point.Y += activeCodeInput.Location.Y + (int)Math.Ceiling(activeCodeInput.Font.GetHeight()) + 2;
                point.X += activeCodeInput.Location.X + TextRenderer.MeasureText(activeCodeInput.Text, activeCodeInput.Font).Width;
                this.gListBox1.Location = point;
                this.gListBox1.BringToFront();
                this.gListBox1.Show();
            }
            else
            {
                this.gListBox1.Hide();
            }
        }
        private void codeInput_textChanged(object sender, EventArgs e)
        {
            activeCodeInput = (TextBox)sender;
            if (populateListBox() && !keepHidden)
            {
                // Find the position of the caret
                Point point = activeCodeInput.GetPositionFromCharIndex(activeCodeInput.SelectionStart);
                point = activeGridView.Location;
                var cellColMultiplier = activeGridView.CurrentCellAddress.Y + 1;
                List<int> colWidths = new List<int>();
                foreach (DataGridViewColumn col in activeGridView.Columns)
                {
                    colWidths.Add(col.Width);
                }
                Point currentCellAddress = activeGridView.CurrentCellAddress;
                int widthToAdd = 0;
                for(int i = 0; i < currentCellAddress.X; i++)
                {
                    widthToAdd += colWidths[i];
                }
                point.Y += activeCodeInput.Location.Y + (int)Math.Ceiling(activeCodeInput.Font.GetHeight()) + 2 + (activeGridView.RowTemplate.Height * cellColMultiplier);
                point.X += activeCodeInput.Location.X + TextRenderer.MeasureText(activeCodeInput.Text, activeCodeInput.Font).Width + activeGridView.RowHeadersWidth + widthToAdd;
                this.gListBox1.Location = point;
                this.gListBox1.BringToFront();
                activeGridView.listBoxShown = true;
                this.gListBox1.Show();
            }
            else
            {
                this.gListBox1.Hide();
                activeGridView.listBoxShown = false;
            }
        }

        public void codeInput_keyDown(object sender, KeyEventArgs e)
        {
            keepHidden = false;
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Space)
            {
                //this.textBoxTooltip.Hide();

                // Autocomplete
                this.selectItem();

                //this.typed = "";
                this.wordMatched = false;
                e.Handled = true;
                this.gListBox1.Hide();
                if (activeGridView != null)
                    activeGridView.listBoxShown = false;
                keepHidden = true;
            }
            else if(e.KeyCode == Keys.Back || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                this.gListBox1.Hide();
                if (activeGridView != null)
                    activeGridView.listBoxShown = false;
                keepHidden = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                // The up key moves up our member list, if
                // the list is visible

                //this.textBoxTooltip.Hide();

                if (this.gListBox1.Visible)
                {
                    this.wordMatched = true;
                    if (this.gListBox1.SelectedIndex > 0)
                        this.gListBox1.SelectedIndex--;

                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // The up key moves down our member list, if
                // the list is visible

                //this.textBoxTooltip.Hide();

                if (this.gListBox1.Visible)
                {
                    this.wordMatched = true;
                    if (this.gListBox1.SelectedIndex < this.gListBox1.Items.Count - 1)
                        this.gListBox1.SelectedIndex++;

                    e.Handled = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Assembly> assemblyImports = new List<Assembly>();
            assemblyImports.AddRange(MefHostServices.DefaultAssemblies);
            assemblyImports.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            var DefaultReferences = assemblyImports.Select(x => (MetadataReference)MetadataReference.CreateFromFile(x.Location)).ToList();

            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            workspace = new AdhocWorkspace(host);

            // Need to pass in an array of all usings statements
            compilationOptions = new CSharpCompilationOptions(
               OutputKind.DynamicallyLinkedLibrary,
               usings: new[] { "System", "System.Collections.Generic", "System.Linq" },
               allowUnsafe: true);

            scriptProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "Script", "Script", LanguageNames.CSharp, isSubmission: true)
               .WithMetadataReferences(DefaultReferences
                /*new[]
               {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
               }*/)
               .WithCompilationOptions(compilationOptions);
            var scriptProject = workspace.AddProject(scriptProjectInfo);
            var scriptCode = "";
            var scriptDocumentInfo = DocumentInfo.Create(
                DocumentId.CreateNewId(scriptProject.Id), "Script",
                sourceCodeKind: SourceCodeKind.Script,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(scriptCode), VersionStamp.Create())));
            var scriptDocument = workspace.AddDocument(scriptDocumentInfo);

            // cursor position is at the end
            var position = scriptCode.Length;

            var completionService = CompletionService.GetService(scriptDocument);
            var results = completionService.GetCompletionsAsync(scriptDocument, position).GetAwaiter().GetResult();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            activeGridView = (CodeGridView)sender;
            var txtBox = e.Control as TextBox;
            if (txtBox != null)
            {
                // Remove an existing event-handler, if present, to avoid
                // adding multiple handlers when the editing control is reused.
                txtBox.KeyDown -= new KeyEventHandler(codeInput_keyDown);
                txtBox.TextChanged -= new EventHandler(codeInput_textChanged);

                // Add the event handler. 
                txtBox.KeyDown += new KeyEventHandler(codeInput_keyDown);
                txtBox.TextChanged += new EventHandler(codeInput_textChanged);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
