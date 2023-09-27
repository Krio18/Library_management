namespace ClassAuthor
{
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public Author(string firstName, string lastName, DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Age = now.Year - dateOfBirth.Year;
        }
    }
}
