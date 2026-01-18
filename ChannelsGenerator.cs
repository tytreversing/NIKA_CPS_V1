using NIKA_CPS_V1.Codeplug;
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
    public partial class ChannelsGenerator : Form
    {

        private const uint LPDChannel1 = 433075000;
        private const uint LPDStep = 25000;
        private const uint PMRChannel1 = 446006250;
        private const uint PMRStep = 12500;
        public ChannelsGenerator()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (cbLPD.Checked)
            {
                int steps = 0;
                if (rbLPD16.Checked)
                    steps = 16;
                else if (rbLPD32.Checked)
                    steps = 32;
                else if (rbLPDFull.Checked)
                    steps = 69;
                for (int i = 0; i < steps; i++)
                {
                    MainForm.CodeplugInternal.AddChannel(new CodeplugChannel(MainForm.CodeplugInternal.GetFirstChannelFreeNumber(), "LPD " + (i+1).ToString("00"), Codeplug.CodeplugChannel.ChannelType.ANALOG, LPDChannel1 + (uint)(LPDStep * i)));
                }
            }
            if (cbPMR.Checked)
            {
                for (int i = 0; i < 8; i++)
                {
                    MainForm.CodeplugInternal.AddChannel(new CodeplugChannel(MainForm.CodeplugInternal.GetFirstChannelFreeNumber(), "PMR " + (i + 1).ToString(), Codeplug.CodeplugChannel.ChannelType.ANALOG, PMRChannel1 + (uint)(PMRStep * i)));
                }
            }
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
