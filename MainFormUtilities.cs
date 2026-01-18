using NIKA_CPS_V1.Codeplug;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1
{
    public partial class MainForm : Form
    {

        private enum Direction
        {
            UP,
            DOWN
        }

        private void MoveSelectedNode(TreeView treeView, Direction direction)
        {
            if (treeView.SelectedNode == null)
            {
                return;
            }

            TreeNode selectedNode = treeView.SelectedNode;
            TreeNodeCollection parentCollection = GetParentNodeCollection(selectedNode);

            if (parentCollection == null) return;

            int currentIndex = parentCollection.IndexOf(selectedNode);

            try
            {
                treeView.BeginUpdate(); // Отключаем перерисовку для плавного перемещения

                if (direction == Direction.UP)
                {
                    MoveNodeUp(parentCollection, currentIndex);
                }
                else if (direction == Direction.DOWN)
                {
                    MoveNodeDown(parentCollection, currentIndex);
                }

                // После перемещения обновляем UI
                treeView.SelectedNode = selectedNode;
                treeView.SelectedNode.EnsureVisible();
            }
            catch
            {
            }
            finally
            {
                treeView.EndUpdate(); // Включаем перерисовку обратно
            }
        }

        private TreeNodeCollection GetParentNodeCollection(TreeNode node)
        {
            return node.Parent?.Nodes ?? node.TreeView?.Nodes;
        }

        private void MoveNodeUp(TreeNodeCollection collection, int currentIndex)
        {
            if (currentIndex > 0)
            {
                TreeNode node = collection[currentIndex];
                collection.RemoveAt(currentIndex);
                collection.Insert(currentIndex - 1, node);
                RearrangeAll();
            }
        }

        private void MoveNodeDown(TreeNodeCollection collection, int currentIndex)
        {
            if (currentIndex < collection.Count - 1)
            {
                TreeNode node = collection[currentIndex];
                collection.RemoveAt(currentIndex);
                collection.Insert(currentIndex + 1, node);
                RearrangeAll();
            }
        }

        private void MoveNodeUpFromMenu(object sender, EventArgs e)
        {
            TreeView activeTreeView = GetActiveTreeView();

            if (activeTreeView == null) return;

            MoveSelectedNode(activeTreeView, Direction.UP);
            RearrangeAll();
        }

        private void MoveNodeDownFromMenu(object sender, EventArgs e)
        {
            TreeView activeTreeView = GetActiveTreeView();

            if (activeTreeView == null) return;

            MoveSelectedNode(activeTreeView, Direction.DOWN);
            RearrangeAll();
        }

        private void RearrangeAll() //переупорядочиваем кодплаг согласно сделанному
        {
            RearrangeContacts();
            RearrangeZones();
            RearrangeChannels();
        }



        private void RearrangeContacts()
        {
            CodeplugInternal.ClearContacts();
            ushort number = 0;
            TreeNode cNode = FindTreeNodeByName(tvMain, "ContactsNode");
            foreach (TreeNode node in cNode.Nodes)
            {
                CodeplugContact contact = node.Tag as CodeplugContact; //список контактов заполняем с заменой Number по порядку
                //TODO ДОБАВИТЬ коррекцию каналов!!!
                contact.Number = number;
                CodeplugInternal.AddContact(node.Tag as CodeplugContact);
                number++;
            }
        }

        private void RearrangeZones()
        {
            CodeplugInternal.ClearZones();
            ushort number = 0;
            TreeNode cNode = FindTreeNodeByName(tvMain, "ZonesNode");
            foreach (TreeNode node in cNode.Nodes)
            {
                CodeplugZone zone = node.Tag as CodeplugZone; //список зон заполняем с заменой Number по порядку
                zone.Number = (byte)number;
                CodeplugInternal.AddZone(node.Tag as CodeplugZone);
                number++;
            }
        }

        private void RearrangeChannels()
        {
            Dictionary<ushort, ushort> mapping = new Dictionary<ushort, ushort>(); //сопоставитель старых номеров каналов новым

            CodeplugInternal.ClearChannels();
            ushort number = 0;
            TreeNode cNode = FindTreeNodeByName(tvMain, "ChannelsNode");
            if (cNode == null) return;
            foreach (TreeNode node in cNode.Nodes)
            {
                CodeplugChannel channel = node.Tag as CodeplugChannel; //список каналов заполняем с заменой Number по порядку
                mapping.Add(channel.Number, number); //старый номер канала - ключ, значение - новый номер
                channel.Number = number;
                CodeplugInternal.AddChannel(node.Tag as CodeplugChannel);
                number++;
            }
            foreach (CodeplugZone zone in CodeplugInternal.Zones)
            {
                for (int i = 0; i < zone.Channels.Count; i++)
                {
                    ushort newNum;
                    if (mapping.ContainsKey(zone.Channels[i]) && mapping.TryGetValue(zone.Channels[i], out newNum)) //ключ найден и получено соответствующее значение
                    { 
                        zone.Channels[i] = newNum;
                    }

                }
            }
        }
    }
}
