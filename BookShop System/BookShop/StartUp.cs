namespace BookShop
{
    using BookShop.Initializer;
    using BookShop.Models.Enums;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();

            //Create/Reset database
            //DbInitializer.ResetDatabase(db);

            //"input" variable needed the following methods: GetBooksByAuthor, GetBookTitlesContainingm, GetAuthorNamesEndingIn, GetBooksByCategory
            //var input = int.Parse(Console.ReadLine());

            //Rename "Method" with the needed method
            var result = Method(db);
            Console.WriteLine(result);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context
                 .Categories
                 .Select(c => new
                 {
                     c.Name,
                     CategoriesSum = c.CategoryBooks
                     .Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                 })
                 .OrderByDescending(x=> x.CategoriesSum)
                 .ThenBy(x=> x.Name)
                 .ToList();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine(category.Name + " $" + category.CategoriesSum);
            }

            return sb.ToString().Trim();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorCopies = context
                .Authors
                .Select(a => new
                {
                    BookCopies = a.Books.Select(b => b.Copies).Sum(),
                    Author = a.FirstName + " " + a.LastName
                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();
                
            var sb = new StringBuilder();
            foreach (var pair in authorCopies)
            {
                sb.AppendLine($"{pair.Author} - {pair.BookCopies}");
            }
            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList().Count();

        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var authors = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(a => new
                {
                    Books = a.BookCategories.Select(b => b.Book.Title),
                    AuthorName = a.Author.FirstName + " " + a.Author.LastName,
                })
                .ToList();
            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                foreach (var Book in author.Books)
                {

                    sb.AppendLine($"{Book} ({author.AuthorName})");
                }
            }
            return sb.ToString().Trim();
        } 
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendJoin(Environment.NewLine, books);

            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .OrderBy(a => a.FirstName + a.LastName)
                .Select(a => a)
                .ToList();

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine(author.FirstName + " " + author.LastName);
            }
            return sb.ToString();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var books = context
                .Books
                .Where(d => d.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                })
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var gettingCategories = input
                .Split(" ", StringSplitOptions
                .RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToList();

            var sb = new StringBuilder();
            var allBooks = new List<string>();
            foreach (var category in gettingCategories)
            {
                var books = context
                .BooksCategories
                .Where(c => c.Category.Name.ToLower() == category)
                .Select(b => b.Book.Title)
                .ToList();
                allBooks.AddRange(books);
            }
            allBooks = allBooks.OrderBy(x => x).ToList();
            sb.AppendJoin(Environment.NewLine, allBooks);
            return sb.ToString().Trim();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.EditionType.ToString() == "Gold")
                .Where(b => b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            var books = context
                .Books
                .Where(x => x.AgeRestriction.ToString().ToLower() == command)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();
            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }
    }
}
