using Huffman.Shared.Models;

namespace Huffman.Server.Services
{
    public class HuffmanService
    {
        Node[] PQ;
        public static List<string> Code = new List<string>();
        public static List<char> characters = new List<char>();
        public static List<int> countList = new List<int>();
        public string code = "";
        public HeapTree MinHeapTree = new HeapTree();
        public int InitialQueue(string sentence)
        {

            sentence = sentence.Trim();
            foreach (var ch in sentence)
            {
                if (characters.Any(c => c == ch))
                {
                    countList[characters.IndexOf(ch)]++;
                }
                else
                {
                    characters.Add(ch);
                    countList.Add(1);
                    Code.Add("");
                }
            }
            PQ = new Node[characters.Count + 1];

            foreach (var item in characters)
            {
                NodeType node = new NodeType { Symbol = item, Frequency = countList[characters.IndexOf(item)] };
                MinHeapTree.Insert(PQ, node);
            }


            return characters.Count;
        }

        public void Huffman(int count)
        {
            for (int i = 1; i < count; i++)
            {
                var p = MinHeapTree.Delete(PQ);
                p.code = 0;
                var q = MinHeapTree.Delete(PQ);
                q.code = 1;
                var r = new NodeType { Frequency = p.Frequency + q.Frequency, Left = p, Right = q };
                MinHeapTree.Insert(PQ, r);
            }
            var tree = MinHeapTree.Delete(PQ);
            inorder(tree);


        }
        void inorder(NodeType root)
        {
            if (root is not null)
            {
                code += root.code;
                if (root.Left is null && root.Right is null)
                    Code[characters.IndexOf(root.Symbol.Value)] = code;

                inorder(root.Left);
                inorder(root.Right);
                if (code != "")
                    code = code.Remove(code.Length - 1, 1);
            }

        }
        public string Encode(string sentence)
        {
            string EncodedString = "";
            sentence = sentence.Trim();
            foreach (var item in sentence)
            {
                EncodedString += Code[characters.IndexOf(item)];
            }
            return EncodedString;
        }

        public string Decode(string sentence)
        {
            sentence = sentence.Trim();
            string DecodedString = "";
            string code = "";
            foreach (var item in sentence)
            {
                code += item;
                if (Code.Any(x => x.Equals(code)))
                {
                    DecodedString += characters[Code.IndexOf(code)];
                    code = "";
                }
            }
            return DecodedString;
        }
    }
}
