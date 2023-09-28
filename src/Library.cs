using ClassAuthor;
using ClassBook;
using ClassDigitalBook;

namespace ClassLibrary
{
    public class Library
	{
        public Library()
        {
            Books = new List<Book>();
            _CollectData(Books);
        }

        public List<Book> Books { get; set; }
        private void _RegisterData(string title, Author author, string isbn)
        {
            string newDataLine = $"#\nTitre:{title}\nAuteurFirstName:{author.FirstName}\nAuteurLastName:{author.LastName}\nAuteurBirthDay:{author.DateOfBirth}\nISBN:{isbn}";
            
            File.AppendAllText("../../../src/Data.txt", newDataLine + Environment.NewLine);

        }
        private DateTime _ConvertStringInDate(string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            return dateTime;
        }

        private void _CollectData(List<Book> books)
        {
            string[] lines = File.ReadAllLines("../../../src/Data.txt");
            int j;
            string title = "";
            string firstName = "";
            string lastName = "";
            DateTime dateOfBirth = new DateTime(1, 1, 1);
            string isbn = "";

            for (int i = 0; i < lines.Length; i++) 
            {
                if (lines[i][0] == '#')
                    i++;
                for (j = 0; lines[i][j] != ':'; j++);
                if (lines[i][..j] == "Titre")
                    title = lines[i].Substring(j + 1);
                if (lines[i][..j] == "AuteurFirstName")
                    firstName = lines[i].Substring(j + 1);
                if (lines[i][..j] == "AuteurLastName")
                    lastName = lines[i].Substring(j + 1);
                if (lines[i][..j] == "AuteurBirthDay")
                    dateOfBirth = _ConvertStringInDate(lines[i].Substring(j + 1));
                if (lines[i][..j] == "ISBN")
                    isbn = lines[i].Substring(j + 1);
                if (i % 5 == 0 && i > 0) //------------------------------------------------------------------
                {
                    Author author = new(firstName, lastName, dateOfBirth);
                    Book newBook = new()
                    {
                        Title = title,
                        Author = author, //         Fonction a moité.
                        ISBN = isbn,
                    };
                    books.Add(newBook);
                } //-----------------------------------------------------------------------------------------
            }
            File.WriteAllText("../../../src/Data.txt", "");
            foreach (var book in books)
            {
                _RegisterData(book.Title, book.Author, book.ISBN);
            }
        }

        public void DisplayBooks()
		{
			foreach (Book bookItem in Books)
			{
				Console.WriteLine($"Titre du livre : {bookItem.Title}");
                Console.WriteLine($"\tAuthor : {bookItem.Author.FirstName} {bookItem.Author.LastName}");
                Console.WriteLine($"\tISBN : {bookItem.ISBN}");
            }
            Console.WriteLine("");
		}

        public void SearchAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("L'auteur de la recherche ne peut pas être vide ou null.");
            var matchingAuthor = Books.Where(books => books.Author.FirstName.Equals(author));

            if (matchingAuthor.Count() == 0)
                Console.WriteLine($"L'auteur ({author}) n'est pas enregistré.");
            else
            {
                foreach (var bookAuthors in matchingAuthor)
			    {
                    Console.WriteLine($"Auteur trouver ! :");
                    Console.WriteLine($"\tFirstName : {bookAuthors.Author.FirstName},  LastName : {bookAuthors.Author.LastName}");
                    Console.WriteLine($"\tAge : {bookAuthors.Author.Age} years old");
                    Console.WriteLine($"\tBorn in : {bookAuthors.Author.DateOfBirth}");
                }
            }
            Console.WriteLine("");
        }

        public bool SearchBooks(string title)
		{
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Le titre de la recherche ne peut pas être vide ou null.");
			var matchingBooks = Books.Where(books => books.Title.Equals(title));

			if (matchingBooks.Count() == 0)
            {
                Console.WriteLine($"Le titre ({title}) n'existe pas.");
                return false;
            }
            else
            {
                foreach (var bookItem in matchingBooks)
			    {
                    Console.WriteLine($"Livre trouvé ! :");
                    Console.WriteLine($"Titre du livre : {bookItem.Title}");
                    Console.WriteLine($"\tAuthor : {bookItem.Author.FirstName} {bookItem.Author.LastName}");
                    Console.WriteLine($"\tISBN : {bookItem.ISBN}");
                }
                return true;
            }
        }

		public void RemoveBooks(string title)
		{
			int index;
            bool existing = false;

            for (index = 0; index < Books.Count; index++)
            {
				if (Books[index].Title.Equals(title))
				{
					existing = true;
					break;
				}
            }
            if (existing == true)
			{
				Books.Remove(Books[index]);
				Console.WriteLine($"{title} a été retirer.");
			} else
                Console.WriteLine($"Le titre ({title}) n'existe pas.");
            Console.WriteLine("");
        }

        public void AddBook(string title, Author author, string isbn)
		{
            if (Books.Any(books => books.ISBN.Equals(isbn)))
				throw new InvalidOperationException("Le code ISBN existe déjà.");
			Book newBook = new Book()
			{
				Title = title,
				Author = author,
				ISBN = isbn,
            };
            Books.Add(newBook);
            _RegisterData(title, author, isbn);
        }
	}
}
