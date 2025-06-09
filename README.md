# Checkout Kata

A .NET implementation of the [Checkout Kata](http://codekata.com/kata/kata09-back-to-the-checkout/), featuring a point-of-sale system with support for standard and bulk pricing rules.

## Features

- Standard item pricing
- Bulk pricing rules (e.g., "3 for 130" or "2 for 45")
- Interactive console interface with sound effects
- Unit tests for all components
- Clean architecture with separation of concerns
- Web API for integration
- React-based web interface

## Project Structure

```
├── Application/           # Backend applications
│   ├── Api/               # Web API
│   └── Console/           # Console application
├── Models/                # Domain models
│   ├── Item.cs
│   ├── IPricingRule.cs
│   ├── StandardPricingRule.cs
│   └── BulkPricingRule.cs
├── Models.Tests/          # Model unit tests
├── Services/              # Business logic services
│   ├── ICheckoutService.cs
│   └── CheckoutService.cs
├── Services.Tests/        # Service unit tests
└── Web/                   # React UI application
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Node.js and npm (for Web UI)
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Running the Backend

1. Clone the repository
```bash
git clone https://github.com/qjnz/kata-checkout.git
cd kata-checkout
```

2. Build and run the console application
```bash
cd Application/Console
dotnet run
```

3. Run the Web API
```bash
cd Application/Api
dotnet run
```

### Running the Web UI

```bash
cd Web
npm install
npm start
```

### Running Tests

```bash
dotnet test
```

## Pricing Rules

The system supports two types of pricing rules:

1. **Standard Pricing**: Regular per-item pricing
   - Example: Item A costs $50 each

2. **Bulk Pricing**: Special pricing for multiple items
   - Example: 3 of Item A for $130
   - Example: 2 of Item B for $45

## Example Usage

```csharp
// Create a checkout with standard items
var checkout = CheckoutFactory.CreateStandardItems();

// Scan items
checkout.Scan("A");
checkout.Scan("B");
checkout.Scan("A");

// Get total price
decimal total = checkout.GetTotalPrice();
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Inspired by the Checkout Kata
- Built with .NET 8.0
- Uses xUnit for testing
- React for frontend
