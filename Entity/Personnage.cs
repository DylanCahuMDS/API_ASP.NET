namespace APIMDS
{
    public class Personnage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int UniversId { get; set; } // Clé étrangère vers l'univers
        public Univers Univers { get; set; } // Propriété de navigation vers l'univers


        public Personnage()
        {

        }
    }
}
