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
    public partial class Grouplist : Form
    {

        private CodeplugGroupList _list;
        public Grouplist(CodeplugGroupList list)
        {
            InitializeComponent();
            _list = list;
        }


        private void RefreshUsed()
        {
            if (_list.Contacts != null && _list.Contacts.Count != 0)
            {
                lbUsedContacts.BeginUpdate();
                foreach (ushort contact in _list.Contacts)
                {
                    foreach (CodeplugContact c in MainForm.CodeplugInternal.Contacts)
                    {
                        if (c.Number == contact)
                        {
                            lbUsedContacts.Items.Add(c);
                        }
                    }
                }
                lbUsedContacts.EndUpdate();
                lCounter.Text = "Группы в зоне (Свободных ячеек: " + (CodeplugData.MAX_CONTACTS_IN_LIST_COUNT - lbUsedContacts.Items.Count).ToString() + "):";
            }
        }

        private void RefreshAvailable()
        {
            lbAvailableContacts.BeginUpdate();
            lbAvailableContacts.Items.Clear();
            foreach (CodeplugContact c in MainForm.CodeplugInternal.Contacts)
            {
                if (!lbUsedContacts.Items.Contains(c) && c.Type == CodeplugContact.ContactType.GROUP)
                {
                    lbAvailableContacts.Items.Add(c);
                }
            }
            lbAvailableContacts.EndUpdate();

        }

        private void Grouplist_Load(object sender, EventArgs e)
        {
            lbUsedContacts.Items.Clear();
            if (_list != null)
            {
                tbName.Text = _list.Name;
                Text = "Редактирование списка #" + _list.Number.ToString() + " (" + _list.Name + ")";
                RefreshUsed();
                RefreshAvailable();
                lbUsedContacts.TopIndex = 0;
                lbAvailableContacts.TopIndex = 0;
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (lbAvailableContacts.SelectedItems.Count > 0) // выбран хотя бы один элемент, разрешено множественное выделение
            {
                if (lbAvailableContacts.SelectedItems.Count <= CodeplugData.MAX_CONTACTS_IN_LIST_COUNT - lbUsedContacts.Items.Count)
                {
                    foreach (CodeplugContact c in lbAvailableContacts.SelectedItems)
                    {
                        lbUsedContacts.Items.Add(c);
                    }
                    RefreshAvailable();
                }
                else
                    MessageBox.Show("Невозможно добавить выбранные группы в этот список. Их количество превышает число свободных ячеек в списке!", "Список заполнен", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Выберите как минимум одну группу в правом списке для добавления в список!", "Выберите группу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            if (lbUsedContacts.SelectedItems.Count > 0)
            {
                var itemsToRemove = lbUsedContacts.SelectedItems.Cast<CodeplugContact>().ToList();

                // Удаляем элементы
                foreach (var contact in itemsToRemove)
                {
                    lbUsedContacts.Items.Remove(contact);
                }
                RefreshAvailable();

            }
            else
                MessageBox.Show("Выберите как минимум одну группу для удаления!", "Выберите группу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void MoveSelectedItem(int direction) // direction: -1 = вверх, 1 = вниз
        {
            if (lbUsedContacts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите группу для перемещения ее в списке!", "Выберите группу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lbUsedContacts.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выберите только одну группу для перемещения!", "Выберите группу", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int selectedIndex = lbUsedContacts.SelectedIndex;
            int newIndex = selectedIndex + direction;

            if (newIndex < 0 || newIndex >= lbUsedContacts.Items.Count) return;

            CodeplugChannel selectedItem = (CodeplugChannel)lbUsedContacts.SelectedItem;
            lbUsedContacts.Items.RemoveAt(selectedIndex);
            lbUsedContacts.Items.Insert(newIndex, selectedItem);
            lbUsedContacts.SelectedIndex = newIndex;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            List<ushort> newContacts = new List<ushort>();
            foreach (CodeplugContact contact in lbUsedContacts.Items)
            {
                newContacts.Add(contact.Number);
            }
            MainForm.CodeplugInternal.UpdateGrouplistByNumber(_list.Number, tbName.Text, newContacts);
            Close();
        }

        private void bUp_Click(object sender, EventArgs e) => MoveSelectedItem(-1);
        private void bDown_Click(object sender, EventArgs e) => MoveSelectedItem(1);
    }
}
