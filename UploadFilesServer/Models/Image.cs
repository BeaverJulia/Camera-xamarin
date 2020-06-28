using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFilesServer.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string ImgPath { get; set; }
    }
}
