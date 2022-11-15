using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace crud
{
    class dbclient
    {
      
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AktivitetDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

           


               
        

     
        private int Getaktivitet(SqlConnection connection)
        {
            Console.WriteLine("Calling -> Aktivitet");


            string queryStringaktivitet = "SELECT  MAX(aktivitet)  FROM Aktivitet";
            Console.WriteLine($"SQL applied: {queryStringaktivitet}");


            SqlCommand command = new SqlCommand(queryStringaktivitet, connection);
            SqlDataReader reader = command.ExecuteReader();


            int aktivitet = 0;


            if (reader.Read())
            {

                aktivitet = reader.GetInt32(0);
            }


            reader.Close();

            Console.WriteLine($"aktivitet: {aktivitet}");
            Console.WriteLine();


            return aktivitet;
        }
        private int Deleteactivity(SqlConnection connection, int aktivitet)
        {
            Console.WriteLine("Calling -> Deleteaktivitet");

            string deleteCommandString = $"DELETE FROM Aktivitet  WHERE aktivitet = {aktivitet}";
            Console.WriteLine($"SQL applied: {deleteCommandString}");

            
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"sletter aktivitet{aktivitet}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

          
            return numberOfRowsAffected;
        }
        private int Updateactivity(SqlConnection connection, activity aktivitet)
        {
            Console.WriteLine("aktiviteter");

            
            string updateCommandString = $"UPDATE Aktivitet SET Type ='{aktivitet.type}', Hotel_Nr = '{aktivitet.hotel_nr}' WHERE aktivitet = {aktivitet.aktivitet}";
            Console.WriteLine($"{updateCommandString}");

            
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Updating aktivitet #{aktivitet.hotel_nr}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            
            return numberOfRowsAffected;
        }

        private int Insertactivity(SqlConnection connection, activity aktivitet)
        {
            Console.WriteLine("Calling -> Insertaktivitet");

            
            string insertCommandString = $"INSERT INTO aktivitet VALUES({aktivitet.aktivitet}, '{aktivitet.type}', '{aktivitet.hotel_nr}')";
            Console.WriteLine($"SQL applied: {insertCommandString}");

          
            SqlCommand command = new SqlCommand(insertCommandString, connection);

            Console.WriteLine($"Creating hotel #{aktivitet.aktivitet}");
            int numberOfRowsAffected = command.ExecuteNonQuery();

            Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
            Console.WriteLine();

            
            return numberOfRowsAffected;
        }
        private List<activity> ListAllactivities(SqlConnection connection)
        {
            Console.WriteLine("Calling -> ListAllactivities");


            string queryStringAllaktivitet = "SELECT * FROM aktivitet";
            Console.WriteLine($"SQL applied: {queryStringAllaktivitet}");

            
            SqlCommand command = new SqlCommand(queryStringAllaktivitet, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("hvis alle aktiviteter:");

            
            if (!reader.HasRows)
            {
                
                Console.WriteLine("ingen aktiviteter");
                reader.Close();

                
                return null;
            }

            
            List<activity> aktivitet = new List<activity>();
            while (reader.Read())
            {
                activity nextactivity = new activity()
                {
                    aktivitet = reader.GetInt32(0), 
                    type = reader.GetString(1),    
                    hotel_nr = reader.GetString(2)  
                };

                aktivitet.Add(nextactivity);
                
               
                Console.WriteLine(nextactivity);
            }

            
            reader.Close();
            Console.WriteLine();

            
            return aktivitet;
        }
        private activity Getaktivitet(SqlConnection connection, int aktivitet)
        {
            Console.WriteLine("aktiviteter ");

           
            string queryStringaktiv = $"SELECT * FROM aktivitet WHERE aktivitet = {aktivitet}";
            Console.WriteLine($"SQL applied: {queryStringaktiv}");

            
            SqlCommand command = new SqlCommand(queryStringaktiv, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"Finding aktivitet#: {aktivitet}");

           
            if (!reader.HasRows)
            {
                
                Console.WriteLine("aktiviteter");
                reader.Close();

                
                return null;
            }

            
            activity aktiv = null;
            if (reader.Read())
            {
                aktiv = new activity()
                {
                    aktivitet = reader.GetInt32(0),
                    type = reader.GetString(1), 
                    hotel_nr = reader.GetString(2) 
                };

                Console.WriteLine(aktivitet);
            }

           
            reader.Close();
            Console.WriteLine();

           
            return aktiv;
        }

        public void Start()
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                ListAllactivities(connection);

               
                activity Newactivity = new activity()
                {
                    aktivitet = Getaktivitet(connection) + 1,
                    type = "biograf",
                    hotel_nr = "2"
                };

               
                Insertactivity(connection, Newactivity);

                
                ListAllactivities(connection);

               
                activity aktivitetToBeUpdated = Getaktivitet(connection, Newactivity.aktivitet);

                
                aktivitetToBeUpdated.type += " updated";
                aktivitetToBeUpdated.hotel_nr += " updated";

               
                Updateactivity(connection, aktivitetToBeUpdated);

                
                ListAllactivities(connection);

                
                activity aktivToBeDeleted = Getaktivitet(connection, aktivitetToBeUpdated.aktivitet);

               
                Deleteactivity(connection, aktivToBeDeleted.aktivitet);

                
                ListAllactivities(connection);
            }
        }
    }
}
    
