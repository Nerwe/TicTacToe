# TicTacToe

TicTacToe is a Windows Presentation Foundation (WPF) application designed to allow users to play the classic Tic-Tac-Toe game. The application implements the Model-View-ViewModel (MVVM) pattern to ensure a clean separation between the UI and business logic, making the application maintainable and scalable.

## Features

- **User Registration and Login**: Users can register and log in to the application.
- **Profile Management**: Users can view their profile information after logging in.
- **Gameplay**: Users can play Tic-Tac-Toe against a bot.
- **Data Persistence**: User information and game records are stored in a database.

## Technologies Used

- **C#**
- **WPF**
- **MVVM Pattern**
- **SQL Server**
- **Entity Framework**

## Getting Started

### Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/TicTacToe.git
    ```
2. Open the solution in Visual Studio:
    ```sh
    cd TicTacToe
    start TicTacToe.sln
    ```
3. Restore the NuGet packages:
    ```sh
    dotnet restore
    ```
4. Set up the database:
    - Update the connection string in `App.config` or `appsettings.json` file to point to your SQL Server instance.
    - Run the database migrations to create the necessary tables:
        ```sh
        Update-Database
        ```

### Running the Application

1. Build and run the application from Visual Studio.
2. The application will start with the Login window by default.

## Usage

- **Login**: Enter your username and password to log in.
- **Register**: Create a new account by entering a username and password, then click "Create account".
- **Profile**: After logging in, you will be directed to the profile view, where you can see your user details.
- **Play Game**: Navigate to the game section to play Tic-Tac-Toe against a bot.

## Project Structure

- `Model`: Contains the data models used in the application.
- `View`: Contains the XAML files for the UI.
- `ViewModel`: Contains the view models that bind the data to the views.
- `Repository`: Contains the data access layer for interacting with the database.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the Apache License. See the [LICENSE](LICENSE.txt) file for more information.

## Acknowledgments

- Special thanks to the contributors of the project.
