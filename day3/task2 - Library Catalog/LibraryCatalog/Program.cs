using System;
using System.Collections.Generic;

namespace LibraryCatalog
{
    public class Library
    {
        public string? Name { get; set; }
        public string? Address { get; set; }

        List<Book> Books = new List<Book>();
        List<MediaItem> MediaItems = new List<MediaItem>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public void AddMediaItem(MediaItem item)
        {
            MediaItems.Add(item);
        }

        public void RemoveMediaItem(MediaItem item)
        {
            MediaItems.Remove(item);
        }

        public void PrintCatalog()
        {
            Console.WriteLine("Welcome to the Library Catalogue");
            Console.WriteLine("");

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address))
            {
                Console.WriteLine("Library information is missing. Kindly update your library");
            }
            else
            {
                Console.WriteLine($"Library Name: {Name}, Address: {Address}");

                Console.WriteLine("Books:");
                if (Books.Count == 0)
                {
                    Console.WriteLine("No books in the library.");
                }
                else
                {
                    foreach (var book in Books)
                    {
                        Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Publication Year: {book.PublicationYear}");
                    }
                }

                Console.WriteLine("");

                Console.WriteLine("Media Items:");
                if (MediaItems.Count == 0)
                {
                    Console.WriteLine("No media items in the library.");
                }
                else
                {
                    foreach (var media in MediaItems)
                    {
                        Console.WriteLine($"Title: {media.Title}, Type: {media.MediaType}, Duration: {media.Duration} minutes");
                    }
                }
            }
        }

    }

    public class Book
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int PublicationYear { get; set; }

        public Book(string title, string author, string isbn, int publicationYear)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
        }
    }

    public class MediaItem
    {
        public string? Title { get; set; }
        public string? MediaType { get; set; }
        public int Duration { get; set; }

        public MediaItem(string title, string mediaType, int duration)
        {
            Title = title;
            MediaType = mediaType;
            Duration = duration;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library() { Name = "Lib", Address = "12345" };
            library.AddBook(new Book("Book1", "Author1", "ISBN1", 2020));
            library.AddBook(new Book("Book2", "Author2", "ISBN2", 2018));
            library.AddMediaItem(new MediaItem("Media1", "DVD", 120));
            library.AddMediaItem(new MediaItem("Media2", "CD", 60));
            library.PrintCatalog();

            //Console.WriteLine("");

            // Case where no library information is added (No library information)
            //Library library1 = new Library() { };
            //library1.PrintCatalog();
        }
    }
}
