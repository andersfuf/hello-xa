using System;
using Npgsql;

class Sample
{
    static void Main(string[] args)
    {
        // Connect to PostgreSQL  database
        NpgsqlConnection dbconn = new NpgsqlConnection("Server=127.0.0.1; User Id = postgres; " + " Password = postgres; Database = postgres");
        dbconn.Open();

        // Define a query returning a single row result set
        NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM information_schema.tables", dbconn);

        // Execute the query and obtain a result set
        NpgsqlDataReader dr = query.ExecuteReader();


        while (dr.Read())
            Console.Write("{0}\t{1}\t{2} \n", dr[0], dr[1], dr[2]);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey(); //keep console window open in debug mode

        dbconn.Close();
    }
}

