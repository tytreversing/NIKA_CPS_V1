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
    public partial class Contact : Form
    {
        private ushort _number;
        private uint _id;
        private CodeplugContact backup;
        public Contact(CodeplugContact contact)
        {
            InitializeComponent();
            backup = contact;
            _number = contact.Number;
            Text = "Контакт #" + _number.ToString() + ": " + contact.Alias;
            tbAlias.Text = contact.Alias;
            tbData.Text = contact.UserData;
            tbDMRID.Text = contact.DMR_ID.ToString();
            _id = contact.DMR_ID;
            switch (contact.Type)
            {
                case CodeplugContact.ContactType.PRIVATE:
                    rbPrivateCall.Checked = true;
                    break;
                case CodeplugContact.ContactType.GROUP:
                    rbGroupCall.Checked = true;
                    break;
                case CodeplugContact.ContactType.ALL_CALL:
                    rbAllCall.Checked = true;
                    break;
            }
            cbTimeslot.SelectedIndex = (int)contact.TimeSlot;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            CodeplugContact.ContactType type;
            CodeplugContact.Timeslot timeslot;
            if (tbDMRID.Text == "")
                tbDMRID.Text = "0";
            if (rbPrivateCall.Checked)
                type = CodeplugContact.ContactType.PRIVATE;
            else if (rbGroupCall.Checked)
                type = CodeplugContact.ContactType.GROUP;
            else type = CodeplugContact.ContactType.ALL_CALL;
            switch ((string)cbTimeslot.SelectedItem)
            {
                case "TS1":
                    timeslot = CodeplugContact.Timeslot.TS1;
                    break;
                case "TS2":
                    timeslot = CodeplugContact.Timeslot.TS2;
                    break;
                default:
                    timeslot = CodeplugContact.Timeslot.NONE;
                    break;
            }
            MainForm.CodeplugInternal.UpdateContactByID(_number, tbAlias.Text, uint.Parse(tbDMRID.Text), tbData.Text, type, timeslot);
            Close();
        }

        private void rbAllCall_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllCall.Checked)
                tbDMRID.Text = "16777215";
            else
                tbDMRID.Text = _id.ToString();
        }

        private void tbDMRID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры, Backspace и Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }

            // Дополнительная проверка длины (если уже 7 символов и не управляющий символ)
            if (((TextBox)sender).Text.Length >= 7 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbDMRID_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (uint.TryParse(textBox.Text, out uint result) && textBox.Text.Length <= 7)
            {
                _id = result;
            }
            else if (string.IsNullOrEmpty(textBox.Text))
            {
                _id = 0;
                tbDMRID.Text = "";
            }
        }

        private void tbAlias_TextChanged(object sender, EventArgs e)
        {
            Text = "Контакт #" + _number.ToString() + ": " + tbAlias.Text;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
