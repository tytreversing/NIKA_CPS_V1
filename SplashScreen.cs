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
    public partial class SplashScreen: Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            tSplash.Start();
        }

        private void tSplash_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void SplashScreen_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
