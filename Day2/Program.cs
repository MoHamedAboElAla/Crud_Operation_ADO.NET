using Microsoft.Data.SqlClient;
using System.Data;

namespace Day2
{
    internal class Program
    {
        static string connectionString = "";

        //DigitalCurrency

        static void Main(string[] args)
        {
        

            while (true)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Create Department");
                Console.WriteLine("2. Get Departments");
                Console.WriteLine("3. Update Department");
                Console.WriteLine("4. Delete Department");
                Console.WriteLine("5. Add Employee");
                Console.WriteLine("6. Get Employees");
                Console.WriteLine("7. Update Employee");
                Console.WriteLine("8. Delete Employee");
                Console.WriteLine("9. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Department Name: ");
                        string deptName = Console.ReadLine();
                        Console.Write("Enter Capacity: ");
                        int capacity = Convert.ToInt32(Console.ReadLine());
                        CreateDepartment(deptName, capacity);
                        break;
                    case 2:
                        GetDeptartment();
                        break;
                    case 3:
                        Console.Write("Enter Department Id to update: ");
                        int idToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter New Department Name: ");
                        string newDeptName = Console.ReadLine();
                        Console.Write("Enter New Capacity: ");
                        int newCapacity = Convert.ToInt32(Console.ReadLine());
                        UpdateDepartment(idToUpdate, newDeptName, newCapacity);
                        break;
                    case 4:
                        Console.Write("Enter Department Id to delete: ");
                        int idToDelete = Convert.ToInt32(Console.ReadLine());
                        DeleteDepartment(idToDelete);
                        break;
                    case 5:
                        Console.Write("Enter Employee Name: ");
                        string empName = Console.ReadLine();
                        Console.Write("Enter Employee Age: ");
                        int empAge = Convert.ToInt32(Console.ReadLine());
                        AddEmployee(empName, empAge);
                        break;
                    case 6:
                        GetEmployee();
                        break;
                    case 7:
                        Console.Write("Enter Employee Id to update: ");
                        int empIdToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter New Employee Name: ");
                        string newEmpName = Console.ReadLine();
                        Console.Write("Enter New Employee Age: ");
                        int newEmpAge = Convert.ToInt32(Console.ReadLine());
                        UpdateEmployee(empIdToUpdate, newEmpName, newEmpAge);
                        break;
                    case 8:
                        Console.Write("Enter Employee Id to delete: ");
                        int empIdToDelete = Convert.ToInt32(Console.ReadLine());
                        DeleteEmployee(empIdToDelete);
                        break;
                    case 9:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                static void CreateDepartment(string deptName, int Capacity)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Department (DeptName, Capacity) VALUES (@DeptName, @Capacity)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@DeptName", deptName);
                        cmd.Parameters.AddWithValue("@Capacity", Capacity);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Department created successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error creating department.");
                        }


                    }


                }
                static void GetDeptartment()
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM Department";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows) //Check if there are any rows returned
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Id: {reader["Id"]}, DeptName: {reader["DeptName"]}, Capacity: {reader["Capacity"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No departments found.");
                        }

                    }

                }
                static void UpdateDepartment(int id, string deptName, int Capacity)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Department SET DeptName = @DeptName, Capacity = @Capacity WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@DeptName", deptName);
                        cmd.Parameters.AddWithValue("@Capacity", Capacity);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Department updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("no department found with the given Id.");
                        }




                    }
                }
                static void DeleteDepartment(int id)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "delete from Department where Id = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Department deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error deleting department.");
                        }
                    }
                }

                // Disconnected Mode    
                static void AddEmployee(string Name, int Age)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter("Select * from Employee", conn);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataRow newRow = dataTable.NewRow();
                        newRow["EmpName"] = Name;
                        newRow["Age"] = Age;
                        dataTable.Rows.Add(newRow);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                        int rowsAffected = adapter.Update(dataTable);
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Employee added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error adding employee.");
                        }
                    }
                }
                static void GetEmployee()
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", conn);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employees found.");
                        }

                    }

                }

                static void UpdateEmployee(int id, string Name, int Age)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", conn);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };

                        DataRow rowToUpdate = dataTable.Rows.Find(id);
                        if (rowToUpdate != null)
                        {
                            rowToUpdate["EmpName"] = Name;
                            rowToUpdate["Age"] = Age;
                            int rowsAffected = adapter.Update(dataTable);
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Employee updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Error updating employee.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employee found with the given Id.");
                        }

                    }
                }

                static void DeleteEmployee(int id)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", conn);
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };

                        DataRow rowToDelete = dataTable.Rows.Find(id);
                        if (rowToDelete != null)
                        {
                            rowToDelete.Delete();
                            int rowsAffected = adapter.Update(dataTable);
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Employee deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Error deleting employee.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employee found with the given Id.");
                        }
                    }
                }
            }
        }
    }
} 
    
