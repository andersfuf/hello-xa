using System;
using Npgsql;

class Sample
{
	static void Main(string[] args)
	{
		// Connect to PostgreSQL  database
		//NpgsqlConnection dbconn = new NpgsqlConnection("Server=127.0.0.1; User Id = knh487; " + " Password = rwetw; Database = uis; Port = 5433");
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


		//a price
		int opa_max_price = 0;
		string in_sa = string.Empty;
		do
		{
			Console.WriteLine("a-ks-v-int Enter highest price");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opa_max_price));


		//a speed
		float fop_min_speed = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("ks-v-float Enter min speed");
			in_sa = Console.ReadLine();
		} while (!float.TryParse(in_sa, out fop_min_speed));


		//a ram
		int opa_min_ram = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("a-ks-v-int Enter min ram");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opa_min_ram));

		//a hd
		int opa_min_hd = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("a-ks-v-int Enter min hd");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opa_min_hd));


		//a screen
		float fop_min_screen = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("ks-v-float Enter min screen");
			in_sa = Console.ReadLine();
		} while (!float.TryParse(in_sa, out fop_min_screen));

		Console.Write("price {0}\tspeed {1}\tram {2}\thd {3}\tscreen {4} \n", opa_max_price, fop_min_speed, opa_min_ram, opa_min_hd, fop_min_screen);

		NpgsqlCommand cmda = new NpgsqlCommand("SELECT l.model, speed, ram, hd, screen, price, maker" +
		                                       "\nFROM Laptop l, Product p" +
		                                       "\nWHERE l.model = p.model" +
		                                       "\nAND price <= :price " +
		                                       "\nAND speed >= :speed " +
		                                       "\nAND ram >= :ram" +
		                                       "\nAND hd >= :hd" +
		                                       "\nAND screen >= :screen\n;" 
			  , dbconn);
		cmda.Parameters.Add(new NpgsqlParameter("price", NpgsqlTypes.NpgsqlDbType.Integer));
		cmda.Parameters.Add(new NpgsqlParameter("speed", NpgsqlTypes.NpgsqlDbType.Double));
		cmda.Parameters.Add(new NpgsqlParameter("ram", NpgsqlTypes.NpgsqlDbType.Integer));
		cmda.Parameters.Add(new NpgsqlParameter("hd", NpgsqlTypes.NpgsqlDbType.Integer));
		cmda.Parameters.Add(new NpgsqlParameter("screen", NpgsqlTypes.NpgsqlDbType.Double));

		cmda.Parameters[0].Value = opa_max_price;
		//cmda.Parameters[0].Value = 4000;
		cmda.Parameters[1].Value = fop_min_speed;//1.2;
		cmda.Parameters[2].Value = opa_min_ram;//512;
		cmda.Parameters[3].Value = opa_min_hd;//80;
		cmda.Parameters[4].Value = fop_min_screen;//15.3;

		NpgsqlDataReader dra = cmda.ExecuteReader();
		Console.Write("depth {0}\t recordsaffected {1}\t fieldcount {2} \n", dra.Depth, dra.RecordsAffected, dra.FieldCount);
		Console.Write("depth {0}\t hasRows {1}\t{2} \n", dra.Depth, dra.HasRows, dra.Statements);

		while (dra.Read())
			Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6} \n", dra[0], dra[1], dra[2], dra[3], dra[4], dra[5], dra[6]);

		dra.Close();


		//b Manufacturer
		Console.WriteLine("b-m-n-string.  Enter manufacturer.");
		string opb_manufacturer = Console.ReadLine();

		//b model
		int opb_model = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("b-ks-v-int Enter model");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opb_model));

		//b speed
		float fopb_speed = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("b-ks-v-float Enter speed");
			in_sa = Console.ReadLine();
		} while (!float.TryParse(in_sa, out fopb_speed));


		//b ram
		int opb_ram = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("b-ks-v-int Enter ram");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opb_ram));

		//b hd
		int opb_hd = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("b-ks-v-int Enter hd");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opb_hd));

		//b price
		int opb_price = 0;
		in_sa = string.Empty;
		do
		{
			Console.WriteLine("b-ks-v-int Enter price");
			in_sa = Console.ReadLine();
		} while (!int.TryParse(in_sa, out opb_price));

		//transacton
		//Check
		//insert
		//commit
		Console.Write("Chosen model {0} \n", opb_model);

		NpgsqlCommand cmdb = new NpgsqlCommand("SELECT pc.model, speed, ram, hd, price, maker" +
		                                       "\nFROM pc pc, Product p" +
		                                       "\nWHERE pc.model = p.model" +
		                                       "\nAND p.model = :model\n;" 
			  , dbconn);

		cmdb.Parameters.Add(new NpgsqlParameter("model", NpgsqlTypes.NpgsqlDbType.Integer));
		cmdb.Parameters[0].Value = opb_model;
		//cmdb.Parameters[0].Value = 2003;

		NpgsqlDataReader drb = cmdb.ExecuteReader();
		Console.Write("b-depth {0}\t recordsaffected {1}\t fieldcount {2} \n", drb.Depth, drb.RecordsAffected, drb.FieldCount);
		Console.Write("b-depth {0}\t hasRows {1}\t statements {2} \n", drb.Depth, drb.HasRows, drb.Statements);

		while (drb.Read())
			Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5} \n", drb[0], drb[1], drb[2], drb[3], drb[4], drb[5]);
		drb.Close();

		drb = cmdb.ExecuteReader();
		if (drb.Read()) { 
			Console.Write("Model {0} exists \n", drb[0]);
  			Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5} \n", drb[0], drb[1], drb[2], drb[3], drb[4], drb[5]);
        	drb.Close();

		} else {
		    Console.Write("Inserting Model {0}  \n", opb_model);
			drb.Close();

			try
			{
				NpgsqlTransaction tranb = dbconn.BeginTransaction();

				NpgsqlCommand cmdb2 = new NpgsqlCommand("INSERT INTO product (maker, model, type) " +
				                                        "VALUES (:maker,:model,'pc');" 
				                                        //"VALUES ('G',2030,'pc');" +
					  , dbconn);
				cmdb2.Parameters.Add(new NpgsqlParameter("maker", NpgsqlTypes.NpgsqlDbType.Text));
				cmdb2.Parameters.Add(new NpgsqlParameter("model", NpgsqlTypes.NpgsqlDbType.Integer));
				cmdb2.Parameters[0].Value = opb_manufacturer;//'G';
				cmdb2.Parameters[1].Value = opb_model;//2030;

				cmdb2.ExecuteNonQuery();

				NpgsqlCommand cmdb3 = new NpgsqlCommand("INSERT INTO pc(model, speed, ram, hd, price) " +
														"VALUES (:model,:speed, :ram, :hd, :price);"
					  //"VALUES(2030, 2.12, 4096, 2048, 2522);" 
					  , dbconn);
				cmdb3.Parameters.Add(new NpgsqlParameter("model", NpgsqlTypes.NpgsqlDbType.Integer));
				cmdb3.Parameters.Add(new NpgsqlParameter("speed", NpgsqlTypes.NpgsqlDbType.Double));
				cmdb3.Parameters.Add(new NpgsqlParameter("ram", NpgsqlTypes.NpgsqlDbType.Integer));
				cmdb3.Parameters.Add(new NpgsqlParameter("hd", NpgsqlTypes.NpgsqlDbType.Integer));
				cmdb3.Parameters.Add(new NpgsqlParameter("price", NpgsqlTypes.NpgsqlDbType.Integer));
				cmdb3.Parameters[0].Value = opb_model;//2030;
				cmdb3.Parameters[1].Value = fopb_speed;//2.12;
				cmdb3.Parameters[2].Value = opb_ram;//096;
				cmdb3.Parameters[3].Value = opb_hd;//2048;
				cmdb3.Parameters[4].Value = opb_price;//2522;

				cmdb3.ExecuteNonQuery();

				cmdb.Parameters[0].Value = opb_model;//2030;
				drb = cmdb.ExecuteReader();
				while (drb.Read())
					Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5} \n", drb[0], drb[1], drb[2], drb[3], drb[4], drb[5]);
				drb.Close();

				tranb.Rollback();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}


		}

		//while (drb.Read())
		//	Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6} \n", dra[0], dra[1], dra[2], dra[3], dra[4], dra[5], dra[6]);

		//Wai Ha Lee
		Console.WriteLine("whl-n-1. Add account.");
		Console.WriteLine("Enter choice: ");
		Console.ReadLine(); // Needs to take in int rather than string or char.

		// Prasanth chinja
		Console.WriteLine("pc-n-int.");
		Console.WriteLine("Enter choice integer: ");
		int intTemp = Convert.ToInt32(Console.ReadLine());

		// Marco
		Console.WriteLine("m-n-1. Add account.");
		Console.WriteLine("Enter choice: ");
		string input = Console.ReadLine();
		int number;
		Int32.TryParse(input, out number);

		// dddd
		Console.WriteLine("ddd-n-1. Add account.");
		Console.WriteLine("Enter choice: ");
		int choice = int.Parse(Console.ReadLine());

		// Kajal Sinha
		int op = 0;
		string in_s = string.Empty;
		do
		{
			Console.WriteLine("ks-v-int-enter choice");
            in_s = Console.ReadLine();
		} while (!int.TryParse(in_s, out op));

		// Kajal Sinha
		float fop = 0;
		string in_sf = string.Empty;
		do
		{
			Console.WriteLine("ks-v-float-enter choice");
			in_sf = Console.ReadLine();
		} while (!float.TryParse(in_sf, out fop));


		Console.WriteLine("F-Press any key to exit.");
		Console.ReadKey(); //keep console window open in debug mode

		dbconn.Close();
	}
}
