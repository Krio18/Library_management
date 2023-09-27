using ClassAuthor;
using ClassInterface;
using ClassLibrary;

namespace ClassBook
{
    public class Book : IInterface
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }

        public bool IsAvaiable(string title, Library library)
        {
            if (library.SearchBooks(title) == true)
                return true;
            return false;
        }

        public void AvailabilityResult(string title, Library library)
        {
            if (IsAvaiable(title, library) == true)
                Console.WriteLine("Vous pouvez le prendre !");
            else
                Console.WriteLine("Il n'est pas ici.");
            Console.WriteLine("");
        }
    }
}
