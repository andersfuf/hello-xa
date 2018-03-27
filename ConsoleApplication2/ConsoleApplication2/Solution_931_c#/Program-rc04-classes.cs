using System;
using Npgsql;

class Sample
{
	static void Main(string[] args)
	{
		// Connect to PostgreSQL  database
		//NpgsqlConnection dbconn = new NpgsqlConnection("Server=127.0.0.1; User Id = knh487; " + " Password = ergerg; Database = uis; Port = 5433");
		NpgsqlConnection dbconn = new NpgsqlConnection("Server=127.0.0.1; User Id = postgres; " + " Database = postgres; Port = 5432");

		dbconn.Open();
		Console.Write("Connection properties {0}\t{1}\t{2} \n", dbconn.ProcessID, dbconn.Port, dbconn.Host);
		Console.Write("Connection properties {0}\t{1}\t{2} \n", dbconn.PostgreSqlVersion, dbconn.UserName, dbconn.UseSslStream);

		// Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
		NpgsqlTransaction tran = dbconn.BeginTransaction();
		//tran.Rollback();


		// Define a query returning a single row result set
		NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM public.classes", dbconn);
		//NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM public.classes", dbconn);
		//NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM classes", dbconn);

		// Execute the query and obtain a result set
		NpgsqlDataReader dr = query.ExecuteReader();
		Console.Write("depth {0}\t recordsaffected {1}\t fieldcount {2} \n", dr.Depth, dr.RecordsAffected, dr.FieldCount);
		Console.Write("depth {0}\t hasRows {1}\t{2} \n", dr.Depth, dr.HasRows, dr.Statements);


		while (dr.Read())
			Console.Write("{0}\t {1}\t{2}\t numguns {3}\t{4} \n", dr[0], dr[1], dr[2], dr[3], dr[4]);

		dr.Close();
		Console.WriteLine("1-current-Press any key to continue.");
		Console.ReadKey(); //keep console window open in debug mode


		NpgsqlCommand cmdu = new NpgsqlCommand("UPDATE classes SET numguns = :NumGuns" +
			  " WHERE class = :Class ;", dbconn);
		//NpgsqlCommand cmdu = new NpgsqlCommand("UPDATE classes SET \"numguns\" = :NumGuns" +
		//	  " WHERE \"class\" = :Class ;", dbconn);
		cmdu.Parameters.Add(new NpgsqlParameter("NumGuns", NpgsqlTypes.NpgsqlDbType.Integer));
		cmdu.Parameters.Add(new NpgsqlParameter("Class", NpgsqlTypes.NpgsqlDbType.Text));
		cmdu.Parameters[0].Value = 6;
		cmdu.Parameters[1].Value = "Kongo";
        cmdu.ExecuteNonQuery();



		NpgsqlCommand query2 = new NpgsqlCommand("SELECT * FROM public.classes", dbconn);
		NpgsqlDataReader dr2 = query2.ExecuteReader();
		while (dr2.Read())
		    Console.Write("{0}\t{1}\t{2}\t{3}\t{4} \n", dr2[0], dr2[1], dr2[2], dr2[3], dr2[4]);

		dr2.Close();
		Console.WriteLine("2-updated-kongo-Press any key to continue (and rollback).");
		Console.ReadKey(); //keep console window open in debug mode
		tran.Rollback();


	}
}
