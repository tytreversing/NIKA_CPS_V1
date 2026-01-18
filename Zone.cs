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
                            lbUsedChannels.Items.Add(c);
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
            lbAvailableChannels.Items.Clear();
            foreach (Codeplug.Channel channel in MainForm.CodeplugInternal.Channels)
            {
                if (!lbUsedChannels.Items.Contains(channel))
                {
                    lbAvailableChannels.Items.Add(channel);
                }
            }
            lbAvailableChannels.EndUpdate();   

        }

        private void Zone_Load(object sender, EventArgs e)
        {
            lbUsedChannels.Items.Clear();
            if (_zone != null)
            {
                tbName.Text = _zone.Name;
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
                    foreach (Codeplug.Channel c in lbAvailableChannels.SelectedItems)
                    {
                        lbUsedChannels.Items.Add(c);
                    }
                    RefreshAvailable(); 
                }
                else
                    MessageBox.Show("Невозможно добавить выбранные каналы в эту зону. Их количество превышает число свободных ячеек в зоне!", "Зона заполнена", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Выберите как минимум один канал в правом списке для добавления в зону!", "Выберите канал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            if (lbUsedChannels.SelectedItems.Count > 0)
            {
                var itemsToRemove = lbUsedChannels.SelectedItems.Cast<Codeplug.Channel>().ToList();

                // Удаляем элементы
                foreach (var channel in itemsToRemove)
                {
                    lbUsedChannels.Items.Remove(channel);
                }
                RefreshAvailable();

            }
            else
                MessageBox.Show("Выберите как минимум один канал для удаления!", "Выберите канал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void MoveSelectedItem(int direction) // direction: -1 = вверх, 1 = вниз
        {
            if (lbUsedChannels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите канал для перемещения его в списке!", "Выберите канал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lbUsedChannels.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выберите только один канал для перемещения!", "Выберите канал", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int selectedIndex = lbUsedChannels.SelectedIndex;
            int newIndex = selectedIndex + direction;

            if (newIndex < 0 || newIndex >= lbUsedChannels.Items.Count) return;

            Codeplug.Channel selectedItem = (Codeplug.Channel)lbUsedChannels.SelectedItem;
            lbUsedChannels.Items.RemoveAt(selectedIndex);
            lbUsedChannels.Items.Insert(newIndex, selectedItem);
            lbUsedChannels.SelectedIndex = newIndex;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            List<ushort> newChannels = new List<ushort>();
            foreach (Codeplug.Channel channel in lbUsedChannels.Items)
            {
                newChannels.Add(channel.Number);
            }
            MainForm.CodeplugInternal.UpdateZoneByNumber(_zone.Number, tbName.Text, newChannels);
            Close();
        }

        private void bUp_Click(object sender, EventArgs e) => MoveSelectedItem(-1);
        private void bDown_Click(object sender, EventArgs e) => MoveSelectedItem(1);
    }
}
