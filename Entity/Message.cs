namespace APIMDS

{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }

        public Message()
        {
           
        }
    }
}
