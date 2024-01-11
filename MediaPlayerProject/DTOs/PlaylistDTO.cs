using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.DTOs
{   
    public class PlaylistDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }

    }
}
