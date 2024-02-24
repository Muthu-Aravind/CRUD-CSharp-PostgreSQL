
using Npgsql;
namespace vstudio
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // Your code here

            Console.WriteLine("Choose any option from below");
            Console.WriteLine("\n1 - CREATE\n2 - READ\n3 - UPDATE\n4 - DELETE");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(choice);

            if (choice == 1)
            {
                #pragma warning disable CS8600
                Console.WriteLine("Enter name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter email: ");
                string email = Console.ReadLine();
                //calling create
                CreateRecord(ref name, ref email);
            }
            else if (choice == 2)
            {
                #pragma warning disable CS8600
                Console.WriteLine("Enter the name of the table");
                string tbname = Console.ReadLine();
                ReadRecord(ref tbname);
            }
            else if (choice == 3)
            {
                #pragma warning disable CS8600
                Console.WriteLine("Enter the name of the record need to updated");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the old mail");
                string oldemail = Console.ReadLine();
                Console.WriteLine("Enter the new mail");
                string newemail = Console.ReadLine();
                UpdateRecord(ref name,ref oldemail,ref newemail);
            }
            else if(choice == 4)
            {
                #pragma warning disable CS8600
                Console.WriteLine("Enter the name of the record need to deleted");
                string name = Console.ReadLine();
                DeleteRecord(ref name);
            }
            else
            {
                Console.WriteLine("Enter the correct choice");
            }

            
           


           


        
        }
        static void CreateRecord(ref string name,ref string email)
        {

            //connecting DB
            var cs = "Host=localhost;Username=postgres;Password=strongpassword=1;Database=sample";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            Console.WriteLine("Connected to the database.");

            //create
            var create = "INSERT INTO \"login_user\" (name, email) VALUES (@name, @email)";
            using var cmd = new NpgsqlCommand(create, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("email", email);
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) inserted");

            con.Close();
            Console.WriteLine("Disconnected from the database.");
        }

        static void DeleteRecord(ref string name)
        {
            //connecting DB
            var cs = "Host=localhost;Username=postgres;Password=strongpassword=1;Database=sample";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            Console.WriteLine("Connected to the database.");

            //delete
            var delete = "DELETE FROM \"login_user\" WHERE name = @name";
            using var cmddelete = new NpgsqlCommand(delete, con);
            cmddelete.Parameters.AddWithValue("name", name);
            int rowsAffecteddelete = cmddelete.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffecteddelete} row(s) affected");
            con.Close();
            Console.WriteLine("Disconnected from the database.");
        }

        static void ReadRecord(ref string tbname)
        {
            //connecting DB
            var cs = "Host=localhost;Username=postgres;Password=strongpassword=1;Database=sample";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            Console.WriteLine("Connected to the database.");

            //read

            var read = $"SELECT name, email FROM \"{tbname}\"" ;
            using var cmdd = new NpgsqlCommand(read, con);
            using NpgsqlDataReader reader = cmdd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                string email = reader.GetString(1);
                Console.WriteLine($"Name: {name}, Email: {email}");
            }
            con.Close();
            Console.WriteLine("Disconnected from the database.");
        }

        static void UpdateRecord(ref string name,ref string oldemail, ref string newemail)
        {
            //connecting DB
            var cs = "Host=localhost;Username=postgres;Password=strongpassword=1;Database=sample";
            using var con = new NpgsqlConnection(cs);
            con.Open();
            Console.WriteLine("Connected to the database.");

            //update

            var update = "UPDATE \"login_user\" SET email = @newEmail WHERE name = @name";
            using var cmdupdate = new NpgsqlCommand(update, con);
            cmdupdate.Parameters.AddWithValue("name", name);
            cmdupdate.Parameters.AddWithValue("email", oldemail);
            cmdupdate.Parameters.AddWithValue("newEmail", newemail);
            int rowsAffectedupdate = cmdupdate.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffectedupdate} row(s) inserted");

            con.Close();
            Console.WriteLine("Disconnected from the database.");
        }


    }
}
