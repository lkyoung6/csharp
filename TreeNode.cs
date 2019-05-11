using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerachTreeLibrary
{
    public class TreeNode
    {
        public int _key;
        public int _value;
        public TreeNode _leftChild;
        public TreeNode _rightChild;

        public TreeNode(int key, int value)
        {
            _key = key;
            _value = value;
        }
    }
}
