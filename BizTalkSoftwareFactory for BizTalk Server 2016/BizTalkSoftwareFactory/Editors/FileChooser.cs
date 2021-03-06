#region Using directives
using System;
using System.Drawing.Design;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
#endregion

namespace BizTalkSoftwareFactory.Editors
{
    /// <summary>
    /// Editor that will choose a file when it is used
    /// It has properties that can be overrided 
    /// to use different filter, title and initialdirectory
    /// </summary>
    public class FileChooser : UITypeEditor
    {
        public virtual string InitialDirectory
        {
            get { return Properties.Settings.Default.FileChooserInitialDirectory; }
            set { Properties.Settings.Default.FileChooserInitialDirectory = value; }
        }
        
        public virtual string FileFilter
        {
            get { return "All files (*.*)|*.*"; }
        }

        public virtual string Title
        {
            get { return "Please choose a file"; }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
            IServiceProvider provider, object value)
        {
            object svc = provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc == null)
            {
                return base.EditValue(context, provider, value);
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = Title;
            fileDialog.InitialDirectory = InitialDirectory;
            fileDialog.Filter = FileFilter;
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                InitialDirectory = fileDialog.FileName;
                Properties.Settings.Default.Save();

                return fileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
        
    }
}
