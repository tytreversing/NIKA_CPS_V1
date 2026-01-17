using NIKA_CPS_V1.Codeplug;
using System;
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
            }
        }

        private void MoveNodeDown(TreeNodeCollection collection, int currentIndex)
        {
            if (currentIndex < collection.Count - 1)
            {
                TreeNode node = collection[currentIndex];
                collection.RemoveAt(currentIndex);
                collection.Insert(currentIndex + 1, node);
            }
        }

        private void MoveNodeUpFromMenu(object sender, EventArgs e)
        {
            TreeView activeTreeView = GetActiveTreeView();

            if (activeTreeView == null) return;

            MoveSelectedNode(activeTreeView, Direction.UP);


        }

        private void MoveNodeDownFromMenu(object sender, EventArgs e)
        {
            TreeView activeTreeView = GetActiveTreeView();

            if (activeTreeView == null) return;

            MoveSelectedNode(activeTreeView, Direction.DOWN);
        }

        private void RearrangeAll() //переупорядочиваем кодплаг согласно сделанному
        {
            RearrangeContacts();
            RearrangeChannels();
        }



        private void RearrangeContacts()
        {
            CodeplugInternal.ClearContacts();
            ushort number = 0;
            TreeNode cNode = FindTreeNodeByName(tvMain, "ContactsNode");
            foreach (TreeNode node in cNode.Nodes)
            {
                Codeplug.Contact contact = node.Tag as Codeplug.Contact; //список контактов заполняем с заменой Number по порядку
                //ДОБАВИТЬ коррекцию каналов!!!
                contact.Number = number;
                CodeplugInternal.AddContact(node.Tag as Codeplug.Contact);
                number++;
            }
        }

        private void RearrangeChannels()
        {
            CodeplugInternal.ClearChannels();
            ushort number = 0;
            TreeNode cNode = FindTreeNodeByName(tvMain, "ChannelsNode");
            foreach (TreeNode node in cNode.Nodes)
            {
                Codeplug.Channel channel = node.Tag as Codeplug.Channel; //список каналов заполняем с заменой Number по порядку
                //ДОБАВИТЬ коррекцию зон!!!
                channel.Number = number;
                CodeplugInternal.AddChannel(node.Tag as Codeplug.Channel);
                number++;
            }
        }
    }
}
