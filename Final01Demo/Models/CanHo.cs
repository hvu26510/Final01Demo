using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final01Demo.Models
{
    public class CanHo
    {
        [Key]
        public string ID { get; set; }
        public string Ten { get; set; }
        public double DienTich { get; set; }
        public string SoNha { get; set; }
        public string IDToaNha { get; set; }

        [ForeignKey("IDToaNha")]
        public ToaNha toaNha { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; } = false;
    }
}
