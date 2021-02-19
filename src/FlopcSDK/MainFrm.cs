using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using PR.Flopcsharp;

namespace FlopcSDK
{
    public partial class MainFrm : Form
    {
        private ModelContainer _cont;
        
        DataTable errorsTables;
        DataSet varTables;
        public MainFrm()
        {
            InitializeComponent();
            TemplateNewModel();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            Compile();
            
        }

        private bool Compile()
        {
            Microsoft.CSharp.CSharpCodeProvider cp
                    = new Microsoft.CSharp.CSharpCodeProvider();
            
            //System.CodeDom.Compiler.ICodeCompiler ic = cp.CreateCompiler();
            System.CodeDom.Compiler.CompilerParameters cpar
                                  = new System.CodeDom.Compiler.CompilerParameters();
            cpar.GenerateInMemory = true;
            cpar.GenerateExecutable = false;
            cpar.ReferencedAssemblies.Add("system.dll");
            cpar.ReferencedAssemblies.Add("Flopcsharp.dll");
            cpar.ReferencedAssemblies.Add("FlopcSDK.exe");
            string src = "using System;\nusing PR.Flopcsharp;\n" +
             "class myclass:FlopcSDK.ModelContainer\n" +
             "{\n" +
             "public myclass(){}\n" +
             rtbCode.Text+
             "\n}\n";
            System.CodeDom.Compiler.CompilerResults cr
                                            = cp.CompileAssemblyFromSource(cpar, src);
            errorsTables = new DataTable();
            errorsTables.Columns.Add("Line");
            errorsTables.Columns.Add("Message");
            foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
            {
                errorsTables.Rows.Add(ce.Line, ce.ErrorText);
            }
            dgvErrors.DataSource = errorsTables;

            if (cr.Errors.Count == 0 && cr.CompiledAssembly != null)
            {
                Type ObjType = cr.CompiledAssembly.GetType("myclass");
                try
                {
                    if (ObjType != null)
                    {
                        _cont = (ModelContainer)Activator.CreateInstance(ObjType);
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                tsblStatus.Text = "Compilation terminée.";
                return true;

            }
            else
            {
                tsblStatus.Text = "Erreurs!";
                return false;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            _cont.Execute();

            varTables=new DataSet();
            Type objType = _cont.GetType();
            FieldInfo[] fi = objType.GetFields();
            for (int i = 0; i < fi.Length; i++)
            {
                string fname=fi[i].Name;
                DataTable table=new DataTable(fname);
                cbTables.Items.Add(fname);
                object o=fi[i].GetValue(_cont);
                if (o is FCS_Set)
                {
                    table.Columns.Add("Items");
                    FCS_Set s = (FCS_Set)o;
                    foreach (string item in s.Elements())
                    {
                        table.Rows.Add(item);
                    }
                }
                varTables.Tables.Add(table);
            }
            cbTables.SelectedIndex = 0;
            tsblStatus.Text = "Execution terminée.";
        }

        private void TemplateNewModel()
        {
            string s = "//Mettre ici les variables que l'on veut voir dans le SDK.\n"
                + "//Elles seront déclarées publiques\n"
                + "//Ex:\n"
                + "// public FCS_Set SITES=new FCS_Set();"
                + "\n\n\n"
                + "public override void Execute()\n"
             + "{\n"
             + "//mettre dans cette fonction le code du modèle."
             +"\n\n"
             + "}\n"
            + "//ne rien mettre après cette ligne.";
            rtbCode.Text = s;

        }

        private void btnNewModel_Click(object sender, EventArgs e)
        {
            TemplateNewModel();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            switch (ofd.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        StreamReader sr=new StreamReader(ofd.FileName);
                        rtbCode.Text= sr.ReadToEnd();
                        sr.Close();
                        break;
                    }
                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void btnSauver_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            switch (ofd.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        StreamWriter sr = new StreamWriter(ofd.FileName);
                        sr.Write( rtbCode.Text);
                        sr.Close();
                        break;
                    }
                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTables.DataSource = varTables.Tables[(string)cbTables.SelectedItem];
        }
    }
}