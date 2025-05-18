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
    public partial class FirmwareUploader: Form
    {
        public FirmwareUploader()
        {
            InitializeComponent();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            ofdOpenFirmware.ShowDialog();
        }
    }
}
