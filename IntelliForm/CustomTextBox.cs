using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelliForm
{
    class CustomTextBox : TextBox
    {
        public bool listBoxShown;
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);
            if (listBoxShown && key == Keys.Enter)
            {
                this.OnKeyDown(new KeyEventArgs(key));
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
