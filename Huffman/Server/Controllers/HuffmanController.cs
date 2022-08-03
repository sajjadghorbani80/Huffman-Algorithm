using Huffman.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Huffman.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuffmanController : ControllerBase
    {
        private HuffmanService _huffmanService = new HuffmanService();

        [HttpPost("encode")]
        public IActionResult Encode([FromBody] string sentence)
        {
            var count = _huffmanService.InitialQueue(sentence);
            _huffmanService.Huffman(count);

            return Ok(_huffmanService.Encode(sentence));
        }

        [HttpPost("decode")]
        public IActionResult DeCode([FromBody] string sentence)
        {
            return Ok(_huffmanService.Decode(sentence));
        }
    }
}
