using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1
{
    public partial class DMRID : Form
    {
        public DMRID()
        {
            InitializeComponent();
            tbDMRID.Text = MainForm.CodeplugInternal.DMRID.DMRID.ToString();
            tbAlias.Text = MainForm.CodeplugInternal.DMRID.Alias;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            MainForm.CodeplugInternal.DMRID.DMRID = uint.Parse(tbDMRID.Text);
            MainForm.CodeplugInternal.DMRID.Alias = tbAlias.Text;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
