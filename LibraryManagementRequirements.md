# Library Management System Requirements Specification

## Project Goal

Build an ASP.NET Core Web API project for learning CRUD operations and
core ASP.NET concepts using 3-Layer Architecture without Authentication
initially, while keeping the project ready for JWT integration later.

## Architecture

-   Library.API (Presentation Layer)
-   Library.BLL (Business Logic Layer)
-   Library.DAL (Data Access Layer)
-   Optional: Library.Domain for shared entities

## Entities

### Author

-   Id
-   FullName
-   Biography
-   DateOfBirth
-   Nationality

### Category

-   Id
-   Name
-   Description

### Publisher

-   Id
-   Name
-   Address
-   Phone

### Book

-   Id
-   Title
-   ISBN
-   PublicationYear
-   TotalCopies
-   AvailableCopies
-   Price
-   AuthorId
-   CategoryId
-   PublisherId

### Member

-   Id
-   FullName
-   Email
-   Phone
-   Address
-   RegistrationDate

### Borrowing

-   Id
-   BookId
-   MemberId
-   BorrowDate
-   DueDate
-   ReturnDate
-   Status

## Relationships

-   Author 1..\* Books
-   Category 1..\* Books
-   Publisher 1..\* Books
-   Member 1..\* Borrowings
-   Book 1..\* Borrowings

## Required Endpoints

### Authors

GET, GET by Id, POST, PUT, DELETE

### Categories

GET, GET by Id, POST, PUT, DELETE

### Publishers

GET, GET by Id, POST, PUT, DELETE

### Books

GET, GET by Id, POST, PUT, DELETE Search, Filter, Pagination, Sorting

### Members

GET, GET by Id, POST, PUT, DELETE

### Borrowings

Borrow book, Return book, Get overdue books, Get member borrowings.

## Business Rules

-   A book cannot be borrowed if AvailableCopies = 0.
-   Borrow duration is 14 days.
-   Author deletion is not allowed when books exist.
-   AvailableCopies decreases on borrow and increases on return.

## DTO Usage

-   Create DTO
-   Update DTO
-   Response DTO
-   Details DTO

## ASP.NET Concepts Covered

-   Controllers
-   Routing
-   Dependency Injection
-   Entity Framework Core
-   DTOs
-   Repository Pattern
-   Service Layer
-   Validation
-   Middleware
-   Exception Handling
-   Swagger
-   Logging
-   Pagination
-   Filtering
-   Sorting
-   Searching

## Future Enhancements

-   JWT Authentication
-   Authorization
-   Roles
-   Refresh Tokens
-   Soft Delete
-   Caching
-   API Versioning
