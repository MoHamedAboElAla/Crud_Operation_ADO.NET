using Microsoft.Data.SqlClient;
using System.Data;

namespace Day2
{
    internal class Program
    {
        static string connectionString = "Data Source=.\\Sqlexpress;Initial Catalog=CrudDept;Integrated Security=True; TrustServerCertificate = True; ";


        static void Main(string[] args)
        {
            //int id = Convert.ToInt32(Console.ReadLine());
            //string deptName = Console.ReadLine();
            //int capacity = Convert.ToInt32(Console.ReadLine());
            //CreateDepartment(deptName,capacity);







        }

       static void CreateDepartment(string deptName, int Capacity)
        {
            using(SqlConnection conn= new SqlConnection(connectionString))
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
       static void UpdateDepartment(int id , string deptName, int Capacity)
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




            }        }
       static void DeleteDepartment(int id)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
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
            
                SqlDataAdapter adapter =new SqlDataAdapter("Select * from Employee", conn);
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

        static void UpdateEmployee(int id , string Name, int Age)
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
