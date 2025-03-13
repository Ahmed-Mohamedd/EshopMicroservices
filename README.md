eShop Microservices
A cloud-native eCommerce application built on .NET Core API using a microservices architecture.
Overview
This project is a modern, container-based eCommerce platform that demonstrates best practices for building scalable, resilient microservices using .NET Core. The application is designed to be cloud-agnostic and can be deployed to any cloud provider or on-premises environment.
Architecture
The eShop application is composed of several independent microservices:

Catalog Service: Manages product catalog information
Basket Service: Handles the shopping cart functionality
Ordering Service: Processes and manages orders
Discount Service: Tracks product inventory levels

Each microservice:

Has its own database
Can be developed, deployed, and scaled independently
Communicates with other services through well-defined APIs

Technology Stack

Framework: .NET Core 8.0
API: RESTful APIs with ASP.NET Core
Communication: HTTP/gRPC for synchronous, RabbitMQ for asynchronous
Data Storage:

SQL Server/Postgres for relational data
MongoDB for catalog and product information
Redis for distributed caching and basket storage



Containerization: Docker
Orchestration: Docker compass
CI/CD: GitHub Actions
API Gateway: Yarp

Getting Started
Prerequisites

.NET 8 SDK
Docker Desktop
Visual Studio 2022 or Visual Studio Code

Running Locally

Clone the repository

bashCopygit clone https://github.com/aahmed-mohamedd/eshop-microservices.git
cd eshop-microservices

Build and run using Docker Compose

bashCopydocker-compose build
docker-compose up

Access the application



API Gateway: https://localhost:5054

Development Workflow

Each microservice can be developed independently
Use the provided solution file to open all projects in Visual Studio
Run the microservice directly from Visual Studio or using the command line:

bashCopycd src/Services/Catalog/Catalog.API
dotnet run
Project Structure
Copy├── src
│   ├── ApiGateways            # API Gateway configurations
│   ├── BuildingBlocks         # Common libraries and utilities
│   │   ├── EventBus           # Event bus abstraction
│   │   ├── Infrastructure     # Shared infrastructure components
│   │   └── WebHostCustomization
│   ├── Services               # Microservices
│   │   ├── Basket            
│   │   ├── Catalog                       
│   │   ├── Ordering          
│   │   └── Discount           
│   └── Web                    # Web applications
│       ├── AdminApp           # Admin portal
│       └── ShopApp            # Customer-facing SPA
├── tests                      # Test projects
│   ├── Functional
│   ├── Integration
│   └── Unit
└── deploy                     # Deployment scripts and configurations
    ├── docker
Configuration
Each microservice has its own configuration file (appsettings.json) for service-specific settings.
Global settings and secrets are managed using:

Environment variables
User Secrets (for local development)

Testing
The solution includes different test types:

Unit tests for business logic
Integration tests for API endpoints
Functional tests for end-to-end scenarios

To run tests:
bashCopydotnet test
Deployment
Docker
Build and deploy individual services:
bashCopydocker build -t eshop/catalog-api ./src/Services/Catalog/Catalog.API
docker run -p 6002:80 eshop/catalog-api


Building and testing code
Creating and pushing Docker images
Deploying to development, staging, and production environments

Contributing

Fork the repository
Create a feature branch (git checkout -b feature/amazing-feature)
Commit your changes (git commit -m 'Add some amazing feature')
Push to the branch (git push origin feature/amazing-feature)
Open a Pull Request

