# Department & Employee CRUD Console App (.NET - ADO.NET)

This is a simple console-based C# application that demonstrates **CRUD operations** on two tables:  
**Department** (connected mode) and **Employee** (disconnected mode using `DataTable` and `SqlDataAdapter`).


### Department (Connected Mode)
- `CreateDepartment(string name, int capacity)`
- `GetDepartment()` – List all departments
- `UpdateDepartment(int id, string name, int capacity)`
- `DeleteDepartment(int id)`

### Employee (Disconnected Mode)
- `AddEmployee(string name, int age)`
- `GetEmployee()` – List all employees
- `UpdateEmployee(int id, string name, int age)`
- `DeleteEmployee(int id)`


## ✅ Technologies Used

- .NET 6 / C#
- ADO.NET
- SQL Server
- Console App
