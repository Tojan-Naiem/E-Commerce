# E-Commerce API

A full-featured e-commerce REST API built with ASP.NET Core 9.0, implementing a three-tier architecture with comprehensive authentication, payment processing via Stripe, and shopping cart functionality.

## 📖 Table of Contents

- [Architecture](#-architecture)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [API Endpoints](#-api-endpoints)
- [Project Structure](#-project-structure)
- [Security Features](#-security-features)
- [Testing](#-testing)
- [Localization](#-localization)
- [Payment Flow](#-payment-flow)
- [Database Seeding](#-database-seeding)
- [Email Notifications](#-email-notifications)

## 🏗️ Architecture

The project follows a clean three-tier architecture:

- **E-Commerce.API** - Presentation layer with controllers and endpoints
- **E-Commerce.BLL** - Business Logic Layer with services
- **E-Commerce.DAL** - Data Access Layer with repositories and models

## ✨ Features

### Authentication & Authorization
- User registration with email confirmation
- JWT-based authentication
- Role-based access control (Admin, SuperAdmin, Customer)
- Password reset with time-limited codes
- User blocking/unblocking functionality

### Product Management
- CRUD operations for products, categories, and brands
- Image upload support for products and brands
- Product status toggling (Active/Inactive)
- Inventory quantity tracking

### Shopping Cart
- Add items to cart
- View cart summary with totals
- Clear cart functionality
- Per-user cart isolation

### Payment Processing
- Stripe integration for card payments
- Cash on delivery option
- Order creation and tracking
- Payment success/cancel handling
- Order status management (Pending, Approved, Shipped, Delivered, Canceled)

### User Management
- Admin dashboard for user oversight
- Block users for specified days
- Promote users to admin role
- Remove admin privileges

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity + JWT
- **Caching**: Redis (StackExchange.Redis)
- **Payment**: Stripe.NET
- **Mapping**: Mapster
- **Email**: SMTP (Gmail)

## 📋 Prerequisites

- .NET 9.0 SDK
- SQL Server
- Redis server
- Stripe account (for payment testing)

## 🚀 Getting Started

### 1. Clone the Repository


### 2. Configure Application Settings

Update `appsettings.json` with your configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ecommerce;Trusted_Connection=True;TrustServerCertificate=True",
    "Redis": "localhost:6379"
  },
  "jwtOptions": {
    "SecretKey": "YOUR_SECRET_KEY"
  },
  "Stripe": {
    "SecretKey": "YOUR_STRIPE_SECRET_KEY"
  }
}
```

### 3. Update Email Configuration

Edit `E-Commerce.API/Util/EmailSender.cs` with your SMTP credentials:

```csharp
Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password")
```

### 4. Run Migrations

```bash
cd E-Commerce.API
dotnet ef database update
```

### 5. Start the Application

```bash
dotnet run
```

The API will be available at `https://localhost:7039` and `http://localhost:5162`.

## 📚 API Endpoints

### Authentication
- `POST /api/Account/register` - Register new user
- `POST /api/Account/login` - Login
- `GET /api/Account/ConfirmedEmail` - Confirm email
- `POST /api/Account/ForgotPassword` - Request password reset
- `PATCH /api/Account/ResetPassword` - Reset password

### Categories
- `GET /api/Categories` - Get all categories
- `GET /api/Categories/{id}` - Get category by ID
- `POST /api/Categories` - Create category (Admin)
- `PATCH /api/Categories/{id}` - Update category (Admin)
- `PATCH /api/Categories/{id}/toggle-status` - Toggle status (Admin)
- `DELETE /api/Categories/{id}` - Delete category (Admin)

### Brands
- `GET /api/Brands` - Get all brands
- `GET /api/Brands/{id}` - Get brand by ID
- `POST /api/Brands` - Create brand (Admin)
- `PATCH /api/Brands/{id}` - Update brand (Admin)
- `PATCH /api/Brands/{id}/toggle-status` - Toggle status (Admin)
- `DELETE /api/Brands/{id}` - Delete brand (Admin)

### Products
- `GET /api/Products` - Get all products
- `GET /api/Products/{id}` - Get product by ID
- `POST /api/Products` - Create product (Admin)
- `PATCH /api/Products/{id}` - Update product (Admin)
- `PATCH /api/Products/{id}/toggle-status` - Toggle status (Admin)
- `DELETE /api/Products/{id}` - Delete product (Admin)

### Cart
- `POST /api/Carts` - Add item to cart (Customer)
- `GET /api/Carts` - Get user cart (Customer)

### Checkout
- `POST /api/CheckOut/payment` - Process payment (Customer)
- `GET /api/CheckOut/success/{orderId}` - Payment success callback
- `GET /api/CheckOut/cancel` - Payment cancel callback

### Users
- `GET /api/Users` - Get all users (Admin)
- `GET /api/Users/{id}` - Get user by ID (Admin)
- `PATCH /api/Users/block/{id}/{days}` - Block user (Admin)
- `PATCH /api/Users/unblock/{id}` - Unblock user (Admin)
- `GET /api/Users/IsBlocked/{id}` - Check if blocked (Admin)
- `PATCH /api/Users/RoleToAdmin/{id}` - Promote to admin (Admin)
- `PATCH /api/Users/RemoveAdmin/{id}` - Remove admin role (Admin)

## 🗂️ Project Structure

```
E-Commerce/
├── E-Commerce.API/          # Controllers, configuration, startup
│   ├── Controllers/         # API endpoints
│   ├── Resources/          # Localization files
│   └── Util/               # Utilities (email sender)
├── E-Commerce.BLL/          # Business logic
│   └── Service/            # Service implementations
├── E-Commerce.DAL/          # Data access
│   ├── Model/              # Entity models
│   ├── Repository/         # Repository pattern
│   ├── DTO/                # Data transfer objects
│   └── Data/               # DbContext
```

## 🔒 Security Features

- JWT token-based authentication
- Password hashing with ASP.NET Core Identity
- Email confirmation required for new accounts
- Role-based authorization
- Account lockout after failed login attempts
- Time-limited password reset codes

## 🧪 Testing

The project includes Swagger UI for API testing. Navigate to `/swagger` when the application is running.



## 📝 Localization

The API supports multiple languages through resource files:
- English (default)
- Arabic

Pass `?lang=ar` query parameter for Arabic responses.

## 🎯 Payment Flow

1. Customer adds items to cart
2. Customer initiates checkout with payment method (Cash/Visa)
3. For Visa:
   - Stripe session created
   - Customer redirected to Stripe checkout
   - On success, order approved and cart cleared
4. For Cash:
   - Order created with pending status
   - Email confirmation sent

## 🗄️ Database Seeding

The application automatically seeds:
- Default roles (Admin, SuperAdmin, Customer)
- Sample categories and brands
- Admin users

## 📧 Email Notifications

Automated emails are sent for:
- Email confirmation
- Password reset
- Order confirmation
- Payment success


