## Movie Reviews System
In this project, I am developing a full-stack, Rotten Tomatoes-style movie review web application developed incrementally across 
4 Scrum sprints.
The platform features an administrative backend for content and critic management, a backend REST API, and a public facing frontend.

## System architecture
**The system consists of four main components:**

- **Admin Website (ASP.NET Core MVC)**: Internal read + write platform where staff manage movie data and critics manage their reviews
- **Database (SQL Server)**: The primary data store housing all movie, review, and user information.
- **API (ASP.NET Core Web API)**: A read-only service layer that fetches data from SQL Server and serves JSON endpoints.
- **Public Website (React)**: A responsive client application that consumes JSON data from the API to render the public interface.

## Tech Stack
| Component        | Technology           | Primary Function            | Access Level |
| :--------------- | :------------------- | :-------------------------- | :----------- |
| **Admin Portal** | ASP.NET Core MVC     | Content & review management | Read / Write |
| **Database**     | SQL Server           | Centralized data storage    | Read / Write |
| **Backend API**  | ASP.NET Core Web API | Serves JSON to client       | Read-Only    |
| **Frontend**     | React                | Public user interface       | Read-Only    |

## Data Flow
1. **Content Management**: Staff and critics input and update movie details or reviews via the ASP.NET Core MVC Admin Portal, 
   saving directly to the SQL Server database.
2. **API Layer**: The ASP.NET Core Web API queries the SQL Server database for published content.
3. **Client Rendering**: The React frontend fetches JSON data from the API to display movies and reviews to public users.