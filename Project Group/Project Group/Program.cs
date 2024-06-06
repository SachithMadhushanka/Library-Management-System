class Program
{
    static void Main()
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Add Member");
            Console.WriteLine("4. Remove Member");
            Console.WriteLine("5. Search Book");
            Console.WriteLine("6. Search Member");
            Console.WriteLine("7. Display All Books");
            Console.WriteLine("8. Display All Members");
            Console.WriteLine("9. Lend Book");
            Console.WriteLine("10. Return Book");
            Console.WriteLine("11. View Lending Information");
            Console.WriteLine("12. Display Overdue Books");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter ISBN: ");
                    string isbn = Console.ReadLine();
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    library.AddBook(new Book(isbn, title, author));
                    break;
                case 2:
                    Console.Write("Enter ISBN of the book to remove: ");
                    isbn = Console.ReadLine();
                    library.RemoveBook(isbn);
                    break;
                case 3:
                    Console.Write("Enter Member ID: ");
                    int memberId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string memberName = Console.ReadLine();
                    Console.Write("Enter Contact Number: ");
                    string contactNumber = Console.ReadLine();
                    library.AddMember(new Member(memberId, memberName, contactNumber));
                    break;
                case 4:
                    Console.Write("Enter Member ID to remove: ");
                    memberId = int.Parse(Console.ReadLine());
                    library.RemoveMember(memberId);
                    break;
                case 5:
                    Console.Write("Enter Title of the book to search: ");
                    title = Console.ReadLine();
                    Book foundBook = library.SearchBook(title);
                    if (foundBook != null)
                        Console.WriteLine($"Book found: {foundBook.Title} by {foundBook.Author}");
                    else
                        Console.WriteLine("Book not found.");
                    break;
                case 6:
                    Console.Write("Enter Member Name to search: ");
                    memberName = Console.ReadLine();
                    Member foundMember = library.SearchMember(memberName);
                    if (foundMember != null)
                        Console.WriteLine($"Member found: {foundMember.Name}, Contact: {foundMember.ContactNumber}");
                    else
                        Console.WriteLine("Member not found.");
                    break;
                case 7:
                    library.DisplayAllBooks();
                    break;
                case 8:
                    library.DisplayAllMembers();
                    break;
                case 9:
                    Console.Write("Enter ISBN of the book to lend: ");
                    isbn = Console.ReadLine();
                    Console.Write("Enter Member ID: ");
                    memberId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Due Date (yyyy-MM-dd): ");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    library.LendBook(isbn, memberId, dueDate);
                    break;
                case 10:
                    Console.Write("Enter ISBN of the book to return: ");
                    isbn = Console.ReadLine();
                    library.ReturnBook(isbn);
                    break;
                case 11:
                    library.ViewLendingInformation();
                    break;
                case 12:
                    library.DisplayOverdueBooks();
                    break;
                case 0:
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Library
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();
    private List<Transaction> transactions = new List<Transaction>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine("Book added to the library.");
    }

    public void RemoveBook(string isbn)
    {
        Book bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine("Book removed from the library.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void AddMember(Member member)
    {
        members.Add(member);
        Console.WriteLine("Member registered.");
    }

    public void RemoveMember(int memberId)
    {
        Member memberToRemove = members.FirstOrDefault(m => m.MemberId == memberId);
        if (memberToRemove != null)
        {
            members.Remove(memberToRemove);
            Console.WriteLine("Member removed.");
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
    }

    public Book SearchBook(string title)
    {
        return books.FirstOrDefault(b => b.Title == title);
    }

    public Member SearchMember(string name)
    {
        return members.FirstOrDefault(m => m.Name == name);
    }

    public void DisplayAllBooks()
    {
        Console.WriteLine("All Books in the Library:");
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} by {book.Author}");
        }
    }

    public void DisplayAllMembers()
    {
        Console.WriteLine("All Members in the Library:");
        foreach (var member in members)
        {
            Console.WriteLine($"{member.Name}, Contact: {member.ContactNumber}");
        }
    }

    public void LendBook(string isbn, int memberId, DateTime dueDate)
    {
        Book bookToLend = books.FirstOrDefault(b => b.ISBN == isbn);
        Member member = members.FirstOrDefault(m => m.MemberId == memberId);

        if (bookToLend == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        if (bookToLend.CopiesAvailable > 0)
        {
            bookToLend.CopiesAvailable--;
            transactions.Add(new Transaction(bookToLend, member, dueDate));
            Console.WriteLine("Book lent successfully.");
        }
        else
        {
            Console.WriteLine("All copies of this book are currently lent out.");
        }
    }

    public void ReturnBook(string isbn)
    {
        Transaction transaction = transactions.FirstOrDefault(t => t.Book.ISBN == isbn && t.ReturnDate == DateTime.MinValue);

        if (transaction != null)
        {
            transaction.ReturnDate = DateTime.Now;
            decimal fine = CalculateFine(transaction);
            if (fine > 0)
            {
                Console.WriteLine($"Book returned. Fine: Rs. {fine}");
            }
            else
            {
                Console.WriteLine("Book returned. No fine.");
            }
        }
        else
        {
            Console.WriteLine("Transaction not found.");
        }
    }

    public void ViewLendingInformation()
    {
        Console.WriteLine("Lending Information:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Book: {transaction.Book.Title}, Member: {transaction.Member.Name}, Due Date: {transaction.DueDate}");
        }
    }

    public void DisplayOverdueBooks()
    {
        Console.WriteLine("Overdue Books:");
        DateTime currentDate = DateTime.Now;
        foreach (var transaction in transactions)
        {
            if (transaction.ReturnDate == DateTime.MinValue && currentDate > transaction.DueDate)
            {
                Console.WriteLine($"Book: {transaction.Book.Title}, Member: {transaction.Member.Name}, Due Date: {transaction.DueDate}");
            }
        }
    }

    private decimal CalculateFine(Transaction transaction)
    {
        DateTime currentDate = DateTime.Now;
        TimeSpan overduePeriod = currentDate - transaction.DueDate;

        if (overduePeriod.TotalDays <= 7)
        {
            return (decimal)overduePeriod.TotalDays * 50;
        }
        else
        {
            return 7 * 50 + (decimal)(overduePeriod.TotalDays - 7) * 100;
        }
    }
}

class Book
{
    public string ISBN { get; }
    public string Title { get; }
    public string Author { get; }
    public int CopiesAvailable { get; set; }

    public Book(string isbn, string title, string author)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        CopiesAvailable = 1; // Default to one copy available when added to the library.
    }
}

class Member
{
    public int MemberId { get; }
    public string Name { get; }
    public string ContactNumber { get; }

    public Member(int memberId, string name, string contactNumber)
    {
        MemberId = memberId;
        Name = name;
        ContactNumber = contactNumber;
    }
}

class Transaction
{
    public Book Book { get; }
    public Member Member { get; }
    public DateTime IssueDate { get; }
    public DateTime DueDate { get; }
    public DateTime ReturnDate { get; set; }

    public Transaction(Book book, Member member, DateTime dueDate)
    {
        Book = book;
        Member = member;
        IssueDate = DateTime.Now;
        DueDate = dueDate;
        ReturnDate = DateTime.MinValue;
    }
}
