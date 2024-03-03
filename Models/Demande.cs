using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZoneFranche.Models
{
    public class Demande
    {
        [Key]
        public int id { get; set; }
        public string dateEntree { get; set; }
        public string raisonViste { get; set; }
        public string etat { get; set; }

        public int idVisiteur { get; set; }
        [ForeignKey("idVisiteur")]
        public Visiteur Visiteur { get; set; }

    }
}
