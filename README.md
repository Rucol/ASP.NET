# 🚲 BikeRentalSystem

> A comprehensive **Bike Rental System** developed using **ASP.NET**.

## 📌 Project Overview

**BikeRentalSystemGr2B** is a web application designed to facilitate bike rentals. The system allows users to browse available bikes, make reservations, and manage rentals efficiently.

## 🛠 Technologies Used

- **Frontend:**
  - HTML5
  - CSS3
  - JavaScript

- **Backend:**
  - ASP.NET (C#)

- **Database:**
  - [Specify the database used, e.g., SQL Server, MySQL]

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [Specify any other prerequisites, e.g., a specific database server]

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Rucol/ASP.NET.git
   cd ASP.NET/BikeRentalSystemGr2B
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Update database connection string:**
   - Navigate to `appsettings.json`.
   - Modify the connection string to point to your database.

4. **Apply database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **Access the application:**
   - Open your browser and navigate to `http://localhost:5000` (or the specified port).

## 📂 Project Structure

- `Controllers/` : Contains the MVC controllers handling HTTP requests.
- `Models/` : Defines the data models and entities.
- `Views/` : Contains the Razor views for the frontend UI.
- `wwwroot/` : Static files like CSS, JavaScript, and images.
- `Data/` : Database context and migration files.

## 📝 Features

- **User Registration & Authentication:** Secure user sign-up and login functionalities.
- **Bike Browsing:** View available bikes with detailed information.
- **Reservation System:** Reserve bikes for specific time slots.
- **Rental Management:** Track and manage ongoing and past rentals.
- **Admin Panel:** Manage bike listings, user accounts, and reservations.

## 🛡 Security

- Implements ASP.NET Identity for authentication and authorization.
- Data validation and protection against common web vulnerabilities.

## 📜 License

This project is licensed under the [MIT License](LICENSE).

## 🤝 Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## 📞 Contact

For any inquiries or support, please contact [Your Name] at [your.email@example.com].

---

*Note: Ensure to replace placeholders like `[Specify the database used, e.g., SQL Server, MySQL]` and `[Your Name]` with actual information relevant to your project.*

