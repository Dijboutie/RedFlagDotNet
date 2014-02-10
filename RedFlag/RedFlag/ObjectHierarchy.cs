using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RedFlag
{
    public partial class ObjectHierarchy : Form
    {
        private int m_ObjectId = 0;
        private Method m_SelectedMethod = null;
        /// <summary>
        /// Construct a treelist of the selected object hierarchy
        /// </summary>
        /// <param name="SelectedMethod">The method that the object appears in</param>
        /// <param name="ObjectId">The sequence number of the object</param>
        public ObjectHierarchy(Method SelectedMethod, int ObjectId)
        {
            InitializeComponent();
            m_ObjectId = ObjectId;
            m_SelectedMethod = SelectedMethod;

        }
        private void AddNodesFlat(StackObject TopObject, IEnumerator memberEnumerator, TreeNode LastNode, TreeNodeCollection Nodes, int CurrentId)
        {
            TreeNode Parentnode;
            if (LastNode == null)
            {
                Parentnode = new TreeNode(TopObject.ToString());
                Parentnode.Name = TopObject.ToString();
                Parentnode.Tag = CurrentId;
                Nodes.Add(Parentnode);
                CurrentId++;
               // AddNodesFlat(TopObject,memberEnumerator,Parentnode,Nodes,CurrentId);
            }
            else
            {
                Parentnode = LastNode;
            }
            TreeNode currentNode = Parentnode;
            while (memberEnumerator.MoveNext() == true)
            {
                StackObject currentSo = (StackObject)memberEnumerator.Current;
                if (currentSo.ObjectDepth == 0) break;
                TreeNode node = new TreeNode(currentSo.ToString());
                node.Name = currentSo.ToString();
                node.Tag = CurrentId;
                // where do we add this node?
                int nodeOffset=currentSo.ObjectDepth-currentNode.Level;
                // normally, this is + going down the tree, and - going up the tree...
                if (nodeOffset<=0){
                nodeOffset = Math.Abs(nodeOffset);
                for (int i=0;i<=nodeOffset;i++)
                {
                  currentNode = currentNode.Parent;
                }
                }
                currentNode.Nodes.Add(node);
                CurrentId++;
                currentNode = node;
            }
        }
        
        private void AddNodesRecursive(StackObject ParentObject, IEnumerator memberEnumerator, TreeNodeCollection Nodes, int CurrentId)
        {       
            TreeNode Parentnode=new TreeNode(ParentObject.ToString());
            Parentnode.Name = ParentObject.ToString();
            Parentnode.Tag = CurrentId;
            Nodes.Add(Parentnode);
            CurrentId++;
            while (memberEnumerator.MoveNext() == true)
            {  
                StackObject currentSo=(StackObject) memberEnumerator.Current;
                if (currentSo.ObjectDepth == 0) break;
                if (currentSo.ObjectDepth == ParentObject.ObjectDepth+1) AddNodesRecursive(currentSo, memberEnumerator, Parentnode.Nodes, CurrentId);
                else
                {
                    TreeNode node = new TreeNode(currentSo.ToString());
                    node.Name = currentSo.ToString();
                    node.Tag = CurrentId;
                    Nodes.Add(node);
                    CurrentId++;
                }

            }
            

        }

        private void ObjectHierarchy_Load(object sender, EventArgs e)
        {
            StackObject topSo = m_SelectedMethod.PrivateMembers[m_ObjectId];
            int iterator = m_ObjectId;
            int offsetFromRoot = 0;
            while (topSo.ObjectDepth > 0 && iterator > 0)
            {
                iterator--;
                offsetFromRoot++;
                topSo = m_SelectedMethod.PrivateMembers[iterator];
            }
            // create an enumerator and move to top so
            IEnumerator membersEnum = m_SelectedMethod.PrivateMembers.GetEnumerator();
            //membersEnum.Reset();
            for (int i = -1; i < iterator; i++)
            {
                membersEnum.MoveNext();
            }
            // Now, add nodes recursively until we reach another L0 entry
            //AddNodesRecursive(topSo, membersEnum, tvHierarchy.Nodes, 0);
            AddNodesFlat(topSo, membersEnum, null, tvHierarchy.Nodes, 0);
            TreeNode[] select=tvHierarchy.Nodes.Find(m_SelectedMethod.PrivateMembers[m_ObjectId].ToString(), true);
            foreach (TreeNode node in select)
            {
                if (Convert.ToInt32(node.Tag) == offsetFromRoot)
                {
                    node.BackColor = SystemColors.Highlight;
                    tvHierarchy.SelectedNode = node;
                }
            
            }
           

        }
    }
}
