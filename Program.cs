using BinarySerachTreeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBinarySerachTree binarySearchTree = CreateBinarySearchTree();

            bool isSuccess = InsertData(binarySearchTree, 70, 70);
            isSuccess = InsertData(binarySearchTree, 40, 40);
            isSuccess = InsertData(binarySearchTree, 90, 90);
            isSuccess = InsertData(binarySearchTree, 20, 20);
            InsertData(binarySearchTree, 50, 50);
            InsertData(binarySearchTree, 80, 80);
            InsertData(binarySearchTree, 100, 100);
            InsertData(binarySearchTree, 10, 10);
            InsertData(binarySearchTree, 30, 30);
            //InsertData(binarySearchTree, 45, 45);
            //DeleteData(binarySearchTree, 20);
            //DeleteData(binarySearchTree, 10);
            DeleteData(binarySearchTree, 70);

            DeleteBinarySearchTree(ref binarySearchTree);


        }

        private static void DeleteBinarySearchTree(ref MyBinarySerachTree binarySerachTree)
        {
            if (binarySerachTree._rootNode != null)
            {
                DeleteTreeRecurSive(ref binarySerachTree._rootNode);
                binarySerachTree = null;
            }
        }

        private static void DeleteTreeRecurSive(ref TreeNode currentNode)
        {
            if (currentNode._leftChild != null)
            {
                DeleteTreeRecurSive(ref currentNode._leftChild);
            }
            if (currentNode._rightChild != null)
            {
                DeleteTreeRecurSive(ref currentNode._rightChild);
            }
            currentNode = null;
        }

        private static TreeNode Search(MyBinarySerachTree binarySerachTree, int key)
        {
            TreeNode nodeToFind = SearchRecursive(binarySerachTree._rootNode, key);
            return nodeToFind;
        }

        private static TreeNode SearchParent(MyBinarySerachTree tree, TreeNode currentNode, int key)
        {
            TreeNode foundParentNode = SearchParentRecursive(tree._rootNode, currentNode, key);
            return foundParentNode;
        }

        private static TreeNode SearchParentRecursive(TreeNode parentNode, TreeNode searchNode, int key)
        {
            TreeNode foundParentNode = null;
            //found parentNode
            if (parentNode._leftChild == searchNode || parentNode._rightChild == searchNode)
            {
                foundParentNode = parentNode;
            }
            else if (parentNode._key > key)
            {
                return SearchParentRecursive(parentNode._leftChild, searchNode, key);
            }
            else if (parentNode._key < key)
            {
                return SearchParentRecursive(parentNode._rightChild, searchNode, key);
            }

            return foundParentNode;
        }

        private static TreeNode SearchRecursive(TreeNode currentNode, int key)
        {
            TreeNode treeNodeToFind = null;
            
            if (currentNode == null)
            {
                return null;
            }
            else if (currentNode._key == key)
            {
                return currentNode;
            }
            else if (currentNode._key > key)
            {
                treeNodeToFind = SearchRecursive(currentNode._leftChild,key);
            }
            else if (currentNode._key < key )
            {
                treeNodeToFind = SearchRecursive(currentNode._rightChild, key);
            }
            return treeNodeToFind;
        }

        private static bool DeleteData(MyBinarySerachTree binarySerachTree, int key)
        {
            TreeNode nodeToDelete = Search(binarySerachTree, key);
            TreeNode parentNode = SearchParent(binarySerachTree, nodeToDelete, key);
            if (nodeToDelete != null)
            {
                //case 1. 자식 노드가 없는 경우
                if(nodeToDelete._leftChild == null && nodeToDelete._rightChild == null)
                {
                    //찾은노드가 부모노드의 왼쪽 자식이였을 경우
                    if (nodeToDelete._key == parentNode._leftChild._key)
                    {
                        parentNode._leftChild = null;
                    }
                    //찾은노드가 부모노드의 오른쪽 자식이였을 경우
                    else if (nodeToDelete._key == parentNode._rightChild._key)
                    {
                        parentNode._rightChild = null;
                    }
                }
                //case 2. 자식 노드가 한 개 인 경우
                else if (nodeToDelete._leftChild != null && nodeToDelete._rightChild == null
                || nodeToDelete._leftChild == null && nodeToDelete._rightChild != null)
                {
                    parentNode._leftChild = nodeToDelete._leftChild;
                    parentNode._rightChild = nodeToDelete._rightChild;
                }
                //case 3. 자식 노드가 두 개 인 경우
                else if (nodeToDelete._leftChild != null && nodeToDelete._rightChild != null)
                {
                    //왼쪽 자식 노드 중에서 가장 큰 값을 찾는다.
                    TreeNode maxValueNode = SearchLeftSubTreeMaxNode(nodeToDelete._leftChild);

                    TreeNode parentMaxNode = SearchParent(binarySerachTree, maxValueNode, maxValueNode._key);

                    int maxValueKey = maxValueNode._key;
                    int maxValue = maxValueNode._value;

                    //가장 큰 값을 지닌 노드가 오른쪽 자식 노드만 없을 때
                    if (maxValueNode._leftChild != null)
                    {
                        parentMaxNode._rightChild = maxValueNode._leftChild;
                    }
                    //가장 큰 값을 지닌 노드가 오른쪽 왼쪽 자식 모두 없을 때
                    else
                    {
                        DeleteData(binarySerachTree, maxValueNode._key);
                    }
                    
                    nodeToDelete._key = maxValueKey;
                    nodeToDelete._value= maxValue;
                }
                return true;
            }
            return false;
        }

        private static TreeNode SearchLeftSubTreeMaxNode(TreeNode currentNode)
        {
            if (currentNode._rightChild != null)
            {
                return SearchLeftSubTreeMaxNode(currentNode._rightChild);
            }
            else
            {
                return currentNode;
            }
        }

        private static bool InsertData(MyBinarySerachTree binarySerachTree, int key, int value)
        {
            if (binarySerachTree._rootNode == null)
            {
                binarySerachTree._rootNode = new TreeNode(key,value);
                return true;
            }
            else
            { 
                bool result = InsertNode(null, binarySerachTree._rootNode, key, value);
                return result;
            }
            
        }

        private static bool InsertNode(TreeNode parentNode, TreeNode currentNode, int key, int value)
        {
            if (currentNode == null)
            {
                TreeNode  newNode = new TreeNode(key, value);
                if(parentNode._key > newNode._key)
                {
                    parentNode._leftChild = newNode;
                    return true;
                }
                else if (parentNode._key < newNode._key)
                {
                    parentNode._rightChild = newNode;
                    return true;
                }
            }
            if (currentNode._key > key)
            {
                bool isSuccess = InsertNode(currentNode, currentNode._leftChild,key,value);
                return isSuccess;
            }
            else if (currentNode._key < key)
            {
                bool isSuccess = InsertNode(currentNode, currentNode._rightChild, key, value);
                return isSuccess;
            }
            else
            {
                return false;
            }

        }

        static MyBinarySerachTree CreateBinarySearchTree()
        {
            MyBinarySerachTree binarySerachTree = new MyBinarySerachTree();
            return binarySerachTree;
        }
        
    }
}
