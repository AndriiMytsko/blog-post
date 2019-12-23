using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Bll.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Type { get; set; }
    }
}
