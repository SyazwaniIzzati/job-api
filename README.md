# Job Marketplace API

A backend REST API for a simplified job marketplace built using C# and ASP.NET Core.

The API enables users to create and retrieve job postings, filter jobs by status, close job postings, submit job applications, and retrieve applications associated with specific jobs. It follows a layered architecture to separate request handling, business logic, and data access, making the application easier to maintain, test, and extend.

## Technology Stack

- C#
- ASP.NET Core
- .NET 10
- Swagger / OpenAPI
- xUnit
- In-memory storage

## Features

### Jobs Management

- Create new job postings.
- Retrieve all available job postings.
- Retrieve a specific job by its ID.
- Filter job postings by status.
- Close existing job postings.

### Applications Management

- Submit applications for existing job postings.
- Retrieve all applications associated with a specific job.
- Validate job availability before accepting applications.
- Prevent applications from being submitted to closed job postings.

## API Endpoints

### Jobs

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Jobs` | Create a new job posting |
| GET | `/api/Jobs` | Retrieve all jobs with an optional status filter |
| GET | `/api/Jobs/{id}` | Retrieve a specific job |
| POST | `/api/Jobs/{id}/close` | Close a job posting |

### Applications

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/jobs/{jobId}/applications` | Submit an application for a job |
| GET | `/api/jobs/{jobId}/applications` | Retrieve applications for a specific job |

### Filtering Jobs

Job postings can be filtered by their status using a query parameter.

```http
GET /api/Jobs?status=OPEN
```

To retrieve closed job postings:

```http
GET /api/Jobs?status=CLOSED
```

The status filter is case-insensitive.

## Data Models

### Job

Each job posting contains the following properties:

- `Id` — Unique identifier for the job.
- `Title` — Job title.
- `Description` — Details about the job.
- `Location` — Job location.
- `CreatedDateTime` — Date and time the job was created.
- `Status` — Current status of the job (`OPEN` or `CLOSED`).

### Application

Each application contains the following properties:

- `Id` — Unique identifier for the application.
- `JobId` — Identifier of the job being applied for.
- `CandidateName` — Name of the candidate.
- `CandidateEmail` — Email address of the candidate.
- `Summary` — Brief description provided by the candidate.
- `SubmittedDateTime` — Date and time the application was submitted.

## Job Status and Business Rules

The API enforces the following business rules:

1. Newly created job postings are automatically assigned the `OPEN` status.
2. Existing job postings can be closed using the close endpoint.
3. Closed job postings cannot receive new applications.
4. Applications must reference an existing job.
5. Jobs and applications are stored in memory while the application is running.

## Architecture

The application follows a layered architecture that separates responsibilities into distinct components.

### Architecture Layers

**API Client**

Swagger UI provides an interface for sending HTTP requests and viewing API responses.

**Controllers**

Controllers handle incoming HTTP requests, invoke the appropriate services, and return HTTP responses.

**Services**

Services contain the business logic and enforce application rules. For example, `ApplicationService` verifies that a job exists and remains open before accepting an application.

**Repositories**

Repositories handle data access through repository interfaces, allowing the service layer to work independently of the underlying storage implementation.

**In-Memory Storage**

The repository implementations use `List<Job>` and `List<Application>` to store job postings and applications during runtime. The data is not persisted and will be lost when the application restarts.

### Design Overview

```text
API Client (Swagger UI)
        |
        v
   Controllers
        |
        v
     Services
        |
        v
 Repository Interfaces
        |
        v
 In-Memory Repositories
        |
        v
  Jobs and Applications
```

Dependency Injection connects repository and service interfaces to their respective implementations. This reduces direct dependencies between components and makes the application easier to maintain and test.

For example, `IJobRepository` is implemented by `InMemoryJobRepository`, while `IJobService` is implemented by `JobService`.

This design also allows the in-memory storage implementation to be replaced with a database-backed repository in the future without requiring major changes to the controllers or business logic.

## Testing

The project uses xUnit to perform unit testing on the application's business logic.

The tests cover key scenarios, including:

- Creating a job with the default `OPEN` status.
- Closing an existing job posting.
- Handling requests to close a non-existent job.
- Submitting an application to an open job.
- Preventing applications from being submitted to closed job postings.

Testing the service layer independently helps verify business rules without relying on HTTP requests or the API interface.

## How to Run

### Prerequisites

- .NET 10 SDK
- Visual Studio Code or Visual Studio

### Run the API

1. Clone the repository.
2. Open the project folder in a terminal.
3. Restore the dependencies:

```bash
dotnet restore
```

4. Run the application:

```bash
dotnet run
```

5. Open Swagger UI using the URL displayed in the terminal, for example:

```text
http://localhost:5114/swagger/index.html
```

The port may vary depending on the local environment.

### Run Unit Tests

Execute the following command from the project root directory:

```bash
dotnet test .\Tests\JobApi.Tests\JobApi.Tests.csproj
```