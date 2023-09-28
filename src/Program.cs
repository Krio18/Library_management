using ClassAuthor;
using ClassBook;
using ClassDigitalBook;
using ClassLibrary;

namespace ClassLibrary_management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassPlantUML.PlantUML plantUMLGenerator = new();
            Library library = new();
            Author author1 = new("Lewis", "Carroll", new DateTime(1832, 1, 14));
            Author author2 = new("Joanne", "Rowling", new DateTime(1965, 7, 31));
            Book book = new();
            DigitalBook digitalBook = new()
            {
                Title = "Harry potter",
                Author = author2,
                ISBN = "2070541274"
            };

            try
            {
                library.DisplayBooks();

                library.AddBook("Alice au pays des merveilles", author1, "9780194229647");
                library.AddBook(digitalBook.Title, digitalBook.Author, digitalBook.ISBN);

                library.DisplayBooks();
                library.RemoveBooks("Alice au pays des merveilles");
                library.DisplayBooks();

                book.AvailabilityResult("565465", library);
                book.AvailabilityResult("L'appel de cthulhu", library);

                library.SearchAuthor("Howard Phillips");
                library.SearchAuthor("Jean");

                //library.SearchAuthor("");
                //library.AddBook("Harry potter", author2, "2070541274");
                //library.SearchBook("");
                plantUMLGenerator.GeneratePlantUml();

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Une ArgumentException a été capturée : " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Une ArgumentException a été capturée : " + ex.Message);
            }
        }
    }
}
