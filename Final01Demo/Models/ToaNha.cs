using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Final01Demo.Models
{
    public class ToaNha
    {
        [Key]
        public string ID { get; set; }
        public string DiaChi { get; set; }
        public ICollection<CanHo> canHos { get; set; }
    }
}
