namespace APIMDS
{
    public class Conversation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Personnage Personnage { get; set; }
        public Message[] Messages { get; set; }

        public Conversation()
        {
            
        }

    }
}
