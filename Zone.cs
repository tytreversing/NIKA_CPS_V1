using NIKA_CPS_V1.Codeplug;
using System;
using System.Collections;
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
    public partial class Zone : Form
    {

        private Codeplug.Zone _zone;
        public Zone(Codeplug.Zone zone)
        {
            InitializeComponent();
            _zone = zone;
        }


        private void RefreshUsed()
        {
            if (_zone.Channels != null && _zone.Channels.Count != 0)
            {
                lbUsedChannels.BeginUpdate();
                foreach (ushort channel in _zone.Channels)
                {
                    foreach (Codeplug.Channel c in MainForm.CodeplugInternal.Channels)
                    {
                        if (c.Number == channel)
                        {
                            lbUsedChannels.Items.Add(c.Name);
                        }
                    }
                }
                lbUsedChannels.EndUpdate();
                lCounter.Text = "Каналы в зоне (Свободных ячеек: " + (CodeplugData.MAX_CHANNELS_IN_ZONE_COUNT - lbUsedChannels.Items.Count).ToString() + "):";
            }
        }

        private void RefreshAvailable()
        {
            lbAvailableChannels.BeginUpdate();
            foreach (Codeplug.Channel channel in MainForm.CodeplugInternal.Channels)
            {
                if (!_zone.Channels.Contains(channel.Number))
                {
                    lbAvailableChannels.Items.Add(channel.Name);
                }
            }
            lbAvailableChannels.EndUpdate();   

        }

        private void Zone_Load(object sender, EventArgs e)
        {
            lbUsedChannels.Items.Clear();
            if (_zone != null)
            {
                Text = "Редактирование зоны #" + _zone.Number.ToString() + " (" + _zone.Name + ")";
                RefreshUsed();
                RefreshAvailable(); 
                lbUsedChannels.TopIndex = 0;
                lbAvailableChannels.TopIndex = 0;
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (lbAvailableChannels.SelectedItems.Count > 0) // выбран хотя бы один элемент, разрешено множественное выделение
            {
                if (lbAvailableChannels.SelectedItems.Count <= CodeplugData.MAX_CHANNELS_IN_ZONE_COUNT - lbUsedChannels.Items.Count)
                {
                }
                else
                    MessageBox.Show("Невозможно добавить выбранные каналы в эту зону. Их количество превышает число свободных ячеек в зоне!", "Зона заполнена", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Выберите как минимум один канал в правом списке для добавления в зону!", "Выберите канал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }
    }
}
