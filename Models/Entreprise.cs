using System.ComponentModel.DataAnnotations;

namespace ZoneFranche.Models
{
    public class Entreprise
    {
        [Key]
        public int id { get; set; }
        public string nom { get; set; }
        public string heureDeTravail { get; set; }
        public string email { get; set; }
        public string numeroDirecteur { get; set; }
        public string description { get; set; }
        public string path { get; set; }
    }
}
