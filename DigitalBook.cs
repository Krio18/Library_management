using ClassBook;

namespace ClassDigitalBook
{
	public class DigitalBook : Book
    {
        public string DownloadLink { get; }
        public DigitalBook()
        {
            DownloadLink = _LinkGenerator();
        }

        private string _LinkGenerator()
        {
            string link = "https.DownloadBook.Com/";
            Random random = new Random();

            link += Convert.ToString(random.Next(1000, 9999));

            return link;
        }
    }
}
