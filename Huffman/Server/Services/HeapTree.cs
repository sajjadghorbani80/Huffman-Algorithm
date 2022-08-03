using Huffman.Shared.Models;

namespace Huffman.Server.Services
{
    public class HeapTree
    {
        public Node root;
        public Node current;
        public Node tmp_parent;
        int count = 1;

        public void Insert(Node[] PQ, NodeType node)
        {
            Node newNode = new Node() { data = node, Parent = PQ[count / 2] };
            current = newNode;
            tmp_parent = newNode.Parent;
            PQ[count] = newNode;
            count++;
            while (tmp_parent != null)//Heapify
            {
                if (current.data.Frequency < tmp_parent.data.Frequency)
                {
                    NodeType tmp_par = tmp_parent.data;
                    tmp_parent.data = current.data;
                    current.data = tmp_par;
                }

                current = current.Parent;
                tmp_parent = current.Parent;
            }
        }
        public NodeType Delete(Node[] PQ)
        {
            NodeType root = PQ[1].data;
            NodeType lastElement = null;
            for (int i = PQ.Length - 1; i > 0; i--)
            {
                if (PQ[i] != null)
                {
                    lastElement = PQ[i].data;
                    PQ[i] = null;
                    break;
                }
            }
            if (PQ[1] != null)
            {
                PQ[1].data = lastElement;
            }
            count--;

            HeapifyTopToBottom(PQ, 1);
            return root;
        }


        public void HeapifyTopToBottom(Node[] PQ, int index)
        {
            int left = index * 2;
            int right = index * 2 + 1;
            int smallestChild = 0;

            if (count - 1 < left)
            { //If there is no child of this node, then nothing to do. Just return.  
                return;
            }
            else if (count - 1 == left)
            { //If there is only left child of this node, then do a comparison and return.  
              //  index = (index - 1) / 2;
                index = 1;
                if (PQ[index].data.Frequency > PQ[left].data.Frequency)
                {
                    NodeType tmp = PQ[index].data;
                    PQ[index].data = PQ[left].data;
                    PQ[left].data = tmp;
                }
                return;
            }
            else
            { //If both children are there  
                if (PQ[left].data.Frequency < PQ[right].data.Frequency)
                { //Find out the smallest child  
                    smallestChild = left;
                }
                else
                {
                    smallestChild = right;
                }
                if (PQ[index].data.Frequency > PQ[smallestChild].data.Frequency)
                { //If Parent is greater than smallest child, then swap  
                    NodeType tmp = PQ[index].data;
                    PQ[index].data = PQ[smallestChild].data;
                    PQ[smallestChild].data = tmp;
                }
            }
            HeapifyTopToBottom(PQ, smallestChild);
        }//end of method  
    }
}
