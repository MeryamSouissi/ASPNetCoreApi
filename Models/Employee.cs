using System.ComponentModel.DataAnnotations;

namespace ZoneFranche.Models
{
    public class Employee
    {
        [Key]
        public string id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string numTel { get; set; }
        public string cin { get; set; }
        public string email { get; set; }
    }
}
