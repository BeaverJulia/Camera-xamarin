using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFilesServer.Models;

namespace UploadFilesServer.Context
{
    public class ImageContext: DbContext
    {
        public ImageContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
    }
}
