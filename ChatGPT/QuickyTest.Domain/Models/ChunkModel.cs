using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickyTest.Domain.Models
{
    public class ChunkModel
    {
        public string Chunk { get; set; } = String.Empty;
        public Prova Prove { get; set; } = new();
        public string Status { get; set; } = "LOADING";
    }
}
