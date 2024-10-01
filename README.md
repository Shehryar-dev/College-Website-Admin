
# College Website Project

## Overview
This project is a **College Website** built using **ASP.NET Core** and **SQL Server**. It provides students, faculty, and administration with various features like course selection, admissions, and feedback. The platform supports smooth interaction between users and the institution, focusing on ease of use and effective management.

## Features
- **Student Registration**: Students can register, log in, and manage their profiles.
- **Course Selection**: Students can select courses and view available faculty.
- **Admission Process**: Online admission forms submission and tracking.
- **Feedback System**: A feedback mechanism for students to share their experiences.
- **Admin Panel**: Faculty and administrators can manage courses, students, and admissions.

## Technologies Used
- **Backend**: ASP.NET Core MVC
- **Database**: SQL Server
- **Frontend**: HTML, CSS, Bootstrap, JavaScript
- **Authentication**: ASP.NET Core Identity

## Database
The database uses an ER diagram, focusing on relationships between students, courses, admissions, and user roles.

Key Entities:
1. **Students**
2. **Courses**
3. **Admissions**
4. **Feedback**
5. **User Roles**

![Database ER Diagram](database/Er%20Diagram.png)

### Database Schema Overview:
- **Courses**: Contains course-related details like course name, credits, and department.
- **Students**: Stores student information such as name, contact, and education history.
- **Admissions**: Manages the admission forms submitted by students.
- **User Roles**: Defines access control for different types of users.
  
## Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/YourUsername/CollegeWebsite.git
    ```
2. Navigate to the project folder:
    ```bash
    cd CollegeWebsite
    ```
3. Update the **appsettings.json** with your SQL Server connection string.

4. Run the database migrations:
    ```bash
    dotnet ef database update
    ```

5. Launch the application:
    ```bash
    dotnet run
    ```

## Screenshots
- **Home Page**
    ![Home Page](admin%20screenshots/index-1.png)
  
- **Course Selection**
    ![Course Selection](admin%20screenshots/12%20%20student%20course%20details.png)
  
- **Student Registration**
    ![Student Registration](admin%20screenshots/11%20student%20registered.png)

- **Admin User**
    ![Admin and User](admin%20screenshots/17%20users.png)
  
- **Admin Login**
    ![Admin Panel Login](admin%20screenshots/Admin%20login.png)

---

Feel free to explore the project and suggest any improvements or features!
