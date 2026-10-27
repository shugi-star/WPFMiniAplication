# Volunteer Call Management System

## Overview

A Windows desktop application built with C# and WPF for managing volunteers and service calls.

The application provides a graphical interface for users to sign in, manage volunteer information, view and filter calls, and create and manage service calls and their categories.

The application communicates with a separate REST API backend to retrieve and manage data.

## Features

- **User Authentication** — Sign-in interface for application users.
- **Volunteer Management** — Create and manage volunteer information.
- **Call Management** — View, add, and remove calls.
- **Call Filtering** — Filter the call list to find relevant records.
- **Call Creation** — Create new calls and manage their details.
- **Category Management** — Work with categories associated with calls.
- **REST API Integration** — Communicate with a backend service to access application data.
- **Data Transfer Objects (DTOs)** — Use structured data objects to transfer information between the desktop application and the API.

## Technologies

- **C#**
- **WPF (Windows Presentation Foundation)**
- **.NET 8**
- **XAML**
- **REST API**
- **JSON**
- **Newtonsoft.Json**

## Architecture

The application follows a client-server approach:

- **Desktop Client:** A WPF application responsible for the user interface and user interactions.
- **Backend API:** A separate service that handles API requests and provides access to application data.
- **DTO Layer:** Data Transfer Objects, including `VolunteerDTO`, `CallDTO`, and `CategoryDTO`, used to represent data exchanged with the backend.

The desktop client communicates with the backend through HTTP requests and processes JSON responses.

## Getting Started

### Prerequisites

Before running the application, make sure you have:

- Windows.
- The .NET 8 SDK.
- Visual Studio with the .NET desktop development workload, or another compatible .NET development environment.
- The backend API running locally.

### 1. Clone the Repository

```bash
git clone https://github.com/shugi-star/WPFMiniAplication.git
```

### 2. Open the Project

Open the solution file (`.sln`) in Visual Studio.

If you prefer using the terminal, navigate to the directory containing the project or solution file.

### 3. Restore Dependencies

From the project directory, run:

```bash
dotnet restore
```

### 4. Start the Backend API

The application is configured to communicate with the local API at:

```text
https://localhost:7227/api
```

Make sure the backend API is running and that its URL matches the application's configuration before launching the desktop client.

### 5. Run the Application

From the directory containing the appropriate project file, run:

```bash
dotnet run
```

Alternatively, start the application from Visual Studio using **Start** or **F5**.

## Project Structure

The solution includes the WPF desktop application, UI definitions written in XAML, C# application logic, and a separate DTO project for transferring data between the client and backend.

## Key Concepts Demonstrated

- Desktop application development with WPF and XAML.
- Object-oriented programming with C#.
- Separation of the user interface and data-transfer models.
- Integration with RESTful backend services.
- JSON serialization and deserialization.
- CRUD-oriented workflows for managing application records.
- Handling user input and displaying data in a graphical interface.

## Future Improvements

- Add automated tests for core application workflows.
- Improve input validation and error handling.
- Use asynchronous API calls to keep the user interface responsive.
- Improve API error reporting and connection handling.
- Add screenshots demonstrating the application's main screens.

## Author

Developed as a C# and WPF desktop application project.
