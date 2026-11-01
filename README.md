# Volunteer Call Management System

## Overview

A desktop application developed in C# using WPF (.NET 8), designed to manage volunteer calls and support the coordination of volunteer activities.

The system provides an interface for managing calls, volunteers, and related information, while communicating with a REST API for data operations.

## Technologies

- **C#**
- **.NET 8**
- **WPF (Windows Presentation Foundation)**
- **REST API**
- **DTO (Data Transfer Objects)**
- **JSON**
- **HTTP Client**

## Project Structure

The solution is organized into separate components:

- **WPFMiniAplication** – The desktop application, including its user interface and application logic.
- **API** – The backend service responsible for handling requests and data operations.
- **DTO** – Contains data transfer objects used for communication between the application and the API.

## Main Features

- Managing volunteer calls.
- Adding new calls through the application interface.
- Displaying and managing call information.
- Working with call categories.
- Communicating with the backend through HTTP requests.
- Using DTOs to transfer structured data between application components.

## Technical Highlights

- Separation between the desktop client, backend API, and data transfer layer.
- XAML-based user interface development.
- Asynchronous HTTP operations for server communication.
- Structured data handling using JSON.
- Object-oriented development with C#.

## Minor Notes

A few small aspects may be refined in future development, including input validation, handling cases where no item is selected, and making the remaining API operations fully asynchronous.

The application also relies on backend-side authorization to enforce access permissions securely.

## Future Improvements

- Expanding input validation and user feedback.
- Improving error handling for API communication.
- Refining asynchronous data-loading operations.
- Further improving the user experience.

---

*Note: The project structure and source code were reviewed. Runtime behavior and successful execution have not yet been verified.*
