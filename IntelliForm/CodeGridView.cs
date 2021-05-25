using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntelliForm
{
    class CodeGridView : DataGridView
    {
        public bool listBoxShown = false;
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (this.EditingControl != null && listBoxShown == true && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Space || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                
                return false;
            }
            return base.ProcessDataGridViewKey(e);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (this.EditingControl != null && (key == Keys.Enter || key == Keys.Tab || key == Keys.Space || key == Keys.Down || key == Keys.Up))
            {

                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
