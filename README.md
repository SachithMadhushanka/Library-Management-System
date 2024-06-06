**Library Management System**

**Description:**

The Library Management System is a console-based application developed in C#. It provides functionalities for managing books, members, lending and returning books, and viewing transaction information in a library setting. The system helps librarians efficiently handle day-to-day operations, including book transactions and member management.

**Features:**

1. **Add Book**: Librarians can add new books to the library by providing details such as ISBN, title, and author.

2. **Remove Book**: Books can be removed from the library by specifying the ISBN of the book to be removed.

3. **Add Member**: New library members can be registered by entering their details, including member ID, name, and contact number.

4. **Remove Member**: Existing library members can be removed from the system by providing their member ID.

5. **Search Book**: Librarians can search for books by title to retrieve details such as the author and availability.

6. **Search Member**: Members can be searched by name to view their contact information.

7. **Display All Books**: Displays a list of all books available in the library.

8. **Display All Members**: Displays a list of all registered members in the library.

9. **Lend Book**: Allows librarians to lend books to members by specifying the book's ISBN, member ID, and due date.

10. **Return Book**: Enables members to return books to the library, calculating fines for overdue books if necessary.

11. **View Lending Information**: Displays information about all book transactions, including the book, member, and due date.

12. **Display Overdue Books**: Shows a list of books that are overdue for return, along with the member and due date.

**UML Diagram:**

<img width="678" alt="The UML Class Diagram for the Library management System" src="https://github.com/SachithMadhushanka/Library-Management-System/assets/131949127/b2988e5c-20ba-40e9-a705-1c98b58dfb17">

**Classes:**

- **Program**: Contains the main method to start the application and handle user interactions through a menu-based interface.
  
- **Library**: Manages books, members, and transactions. Provides methods for various operations such as adding/removing books and members, searching, and displaying information.
  
- **Book**: Represents a book entity with properties such as ISBN, title, author, and availability.
  
- **Member**: Represents a library member with properties like member ID, name, and contact number.
  
- **Transaction**: Represents a book lending transaction with details such as the book, member, issue date, due date, and return date.

**Usage:**

1. **Clone the Repository**: Clone the repository to your local machine using the `git clone` command.

2. **Open in IDE**: Open the project in your preferred IDE (Integrated Development Environment) such as Visual Studio.

3. **Build and Run**: Build the solution and run the application. Follow the on-screen menu prompts to perform various operations.

**Contributing:**

Contributions are welcome! If you have any suggestions, enhancements, or bug fixes, please feel free to open an issue or create a pull request.

**License:**

This project is licensed under the MIT License. See the LICENSE file for more details.
