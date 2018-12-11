using System;
using System.Data;
using System.Data.SqlClient;

namespace Ado1
{
    class Program
    {
        static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mercury;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static void Main(string[] args)
        {
            //QueryExample();
            //StoredProcedureExample();
            //AverageSalaryMethod();
            ExecuteNonQueryMethod();
        }

        private static void ExecuteNonQueryMethod()
        {
            //Ett annat sätt att anropa Dispose på ett säkert 
            //sätt som är mera DRY

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "update persons set LastName = UPPER(lastname)";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Success!");
                    }
                    else
                    {
                        Console.WriteLine("Failure"); 
                    }
                }

                connection.Close();
            }

        }

        private static void AverageSalaryMethod()
        {
            //Ett annat sätt att anropa Dispose på ett säkert 
            //sätt som är mera DRY

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "AverageSalary";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter titel = new SqlParameter("@titel", "Lagerarbetare");
                    //titel.Direction = ParameterDirection.Input; //Default
                    //titel.ParameterName = "@titel";
                    //titel.Size = 32;
                    //titel.SqlDbType = SqlDbType.VarChar;
                    //titel.SqlValue = "Lagerarbetare";

                    SqlParameter sektion = new SqlParameter("@sektion", 1);

                    command.Parameters.AddRange(new []{sektion, titel});

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int antalAnstallda = (int) reader["Sektion"];
                        string title  = (string) reader["Titel"];
                        string medellön = (string) reader["Medellön"];

                        Console.WriteLine(antalAnstallda + ", " + title + ", " + medellön);

                    }
                    //command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }



        private static void StoredProcedureExample()
        {

            //Ett annat sätt att anropa Dispose på ett säkert 
            //sätt som är mera DRY

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "HighestWages";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var name = reader["Namn"];
                        Console.WriteLine(name);
                    }
                    //command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private static void QueryExample()
        {
            //Ett annat sätt att anropa Dispose på ett säkert 
            //sätt som är mera DRY

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "select * from Persons";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = reader["ID"];
                        Console.WriteLine(id);

                        var personNr = reader["Personnr"];
                        Console.WriteLine(personNr);

                        var firstName = reader["FirstName"];
                        Console.WriteLine(firstName);

                        int? yearOfBirth = reader["YearOfBirth"] as int?;
                        Console.WriteLine($"Ålder: {2018 - yearOfBirth}");
                    }
                    //command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
