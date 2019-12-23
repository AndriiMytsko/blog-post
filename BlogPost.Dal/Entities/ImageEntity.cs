using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Dal.Entities
{
    public class ImageEntity: Entity
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Type { get; set; }
    }
}
