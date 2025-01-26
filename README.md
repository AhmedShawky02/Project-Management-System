
### **Project Management System (Web API Project)**

#### **Overview**  
A comprehensive Web API designed for managing projects, tasks, and comments, with a focus on security, access control, and usability.

---

#### **Key Features**
- **Authentication & Authorization:**  
  Secure user authentication using ASP.NET Identity and JWT, with role-based access control.  

- **Project Management:**  
  Create, update, delete, and view projects with support for tasks linked to each project.  

- **Task Management:**  
  Full task management, including assignment to team members and status tracking.  

- **Commenting System:**  
  Users can add comments to tasks to enhance collaboration.  

- **API Documentation:**  
  Comprehensive documentation with Swagger for easy testing and usage of the API.  

---

#### **Technologies**
- **Backend:** ASP.NET Core Web API, Entity Framework Core  
- **Authentication:** ASP.NET Identity, JWT  
- **Database:** SQL Server  
- **Serialization:** System.Text.Json  
- **Documentation:** Swagger/OpenAPI  

---

#### **Endpoints Overview**

##### **Project Management**
- `GET: api/Project` – Retrieve all projects with filtering and pagination support. *(Admin/User)*  
- `GET: api/Project/{id}` – Fetch details of a specific project. *(Admin/User)*  
- `POST: api/Project` – Add a new project. *(Admin)*  
- `PUT: api/Project/{id}` – Update project details. *(Admin)*  
- `DELETE: api/Project/{id}` – Delete a project. *(Admin)*  

##### **Task Management**
- `GET: api/Task` – Retrieve all tasks with filtering and pagination. *(Admin/User)*  
- `GET: api/Task/{id}` – Fetch details of a specific task. *(Admin/User)*  
- `POST: api/Task` – Add a new task. *(Admin)*  
- `PUT: api/Task/{id}` – Update task details. *(Admin)*  
- `DELETE: api/Task/{id}` – Delete a task. *(Admin)*  

##### **Comment Management**
- `GET: api/Comment` – Retrieve all comments. *(Admin/User)*  
- `GET: api/Comment/{id}` – Fetch details of a specific comment. *(Admin/User)*  
- `POST: api/Comment/{TaskId}` – Add a comment to a task. *(Admin/User)*  
- `PUT: api/Comment/{id}` – Update a comment. *(Admin/User)*  
- `DELETE: api/Comment/{id}` – Delete a comment. *(Admin/User)*  

##### **Account Management**
- `POST: api/Account/login` – Authenticate user and issue a JWT token.  
- `POST: api/Account/register` – Register a new user with a default role.  

---

#### **Security & Validation**
- **Authentication:**  
  Secure JWT-based token authentication for all endpoints.  

- **Authorization:**  
  Role-based access control to ensure proper permissions for Admins and Users.  

- **Validation:**  
  Input validation to maintain data integrity and prevent invalid operations.  

- **Error Handling:**  
  Clear and descriptive error messages for failed operations.  

---

#### **Outcome**
Delivered a scalable and secure Web API for managing projects and tasks, prioritizing performance, maintainability, and collaboration.  
