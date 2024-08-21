
# StudentApp [Functional Programming]

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![C# Version](https://img.shields.io/badge/C%23-7.0-blue)
![.NET Version](https://img.shields.io/badge/.NET-4.7.2-blue)
![License](https://img.shields.io/badge/license-MIT-blue)

### Overview
`StudentApp` is an experimental project developed to explore functional programming techniques in C#. The goal of this application is to experiment with functional patterns such as immutability, pure functions, and language constructs like `Either` and `Option` monads from the `LanguageExt` library. The application performs various database operations related to students, subjects, and marks, and it incorporates cryptography utilities as well as various extension methods to facilitate functional programming approaches.

### Features
- **Functional Programming:** The app uses the `LanguageExt` library to implement functional patterns like monads (`Either`, `Option`), immutability, and higher-order functions.
- **Cryptography:** AES encryption and decryption utilities implemented in a functional programming style.
- **SQLite Database Integration:** The app manages student, subject, and mark records, as well as user credentials using SQLite. The connection and queries are handled in a functional way with error handling via monads.
- **NUnit Testing:** The project includes unit tests that cover the cryptography utilities, file system interactions, and database operations.
- **User Management:** Basic user authentication with encrypted passwords.
  
### Technologies Used
- **C#**
- **.NET Framework 4.7.2**
- **SQLite**
- **LanguageExt Library** for functional programming.
- **NUnit** for unit testing.

### Project Structure
```
StudentApp/
│
├── Config.cs
├── Cryptography.cs
├── Prelude.cs
├── Program.cs
├── Sql.cs
├── Query.cs
├── Script.cs
├── Model/
│   ├── Gender.cs
│   ├── Mark.cs
│   ├── Student.cs
│   └── User.cs
├── Extensions/
│   ├── Attributes.cs
│   ├── StyleExtensions.cs
├── Tests/
│   ├── DateTimeExtensions_Test.cs
│   ├── StringExtensions_Test.cs
├── App.config
├── StudentApp.sln
└── README.md
```

### Getting Started

1. **Prerequisites:**
   - Install .NET Framework 4.7.2 or higher.
   - Install the SQLite provider.
   - Install the `LanguageExt` NuGet package.

2. **Setting Up:**
   - Clone the repository.
   - Restore NuGet packages.
   - Ensure the SQLite database file (`StudentApp2.db`) is created in the designated path defined in `Config.cs`.

3. **Running the App:**
   - The main entry point is `Program.cs`, which contains test calls for various database queries and cryptographic functions.
   - Open the solution in Visual Studio or another C# IDE, build the project, and run it.

4. **Testing:**
   - The project includes unit tests written with NUnit, located in the `Tests` folder.
   - Run the tests using an NUnit-compatible test runner.

### Example Usage
The following are some examples of what the app does:
- **Add a User:**
  ```csharp
  Query.AddUser("username", "password");
  ```
- **Get All Students:**
  ```csharp
  var students = Query.GetAllStudents();
  ```

### Future Enhancements
- Expand the database schema to handle more complex relationships.
- Implement more functional patterns, such as applicatives or higher-kinded types.
- Improve user interface and add a graphical UI.

- ## Author

This project was created by [Gitrep77](https://github.com/Gitrep77). If you find it helpful, feel free to give a star on the repository!

## Support

For issues or questions, open an issue on the [GitHub repository](https://github.com/Gitrep77/FunctionalStudentApp/issues).

## Contributing

We welcome contributions! Please read our [contributing guidelines](CONTRIBUTING.md) for more details.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
