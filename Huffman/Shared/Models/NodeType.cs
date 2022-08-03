namespace Huffman.Shared.Models
{
    public class NodeType
    {
        public char? Symbol { get; set; }
        public int Frequency { get; set; }
        public int? code { get; set; }
        public NodeType? Left { get; set; } = null;
        public NodeType? Right { get; set; } = null;
    }
}
