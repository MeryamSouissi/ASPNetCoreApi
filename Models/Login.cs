using System.ComponentModel.DataAnnotations;

namespace ZoneFranche.Models
{
    public class Login
    {
        [Key]
        public int id {  get; set; }
        public string email { get; set; }
        public string motDePasse { get; set; }
        public string type { get; set; }
    }
}
