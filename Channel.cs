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
    public partial class Channel : Form
    {

        private CodeplugChannel _channel;
        public Channel(CodeplugChannel channel)
        {
            InitializeComponent();
            _channel = channel;
        }

        private void Channel_Load(object sender, EventArgs e)
        {
            if (_channel != null)
            {
                Text = "Редактирование канала #" + _channel.Number.ToString() + " (" + _channel.Name + ")";
                // TODO заполнение полей канала
            }
        }
    }
}
