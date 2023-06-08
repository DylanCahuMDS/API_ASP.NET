namespace APIMDS
{
    public class Univers
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Personnage> Personnages { get; set; } // Propriété de navigation vers les personnages

        public Univers()
        {
            Personnages = new List<Personnage>();
        }
    }
}
