using System;
using System.Linq;

// dotnet add package MySql.Data - sākumā jānorāda

using MySql.Data;
using MySql.Data.MySqlClient;

namespace SGT_15April_Databases_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "server=localhost;port=3306;database=WebShop;user=newuser;password=Signe12345*";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            MySqlCommand cmd;
            int rowsAffected;

            cmd = new MySqlCommand("UPDATE Orders SET `status` = 'in processing' WHERE id = 2", conn);
            //int rowsAffected = cmd.ExecuteNonQuery(); <-- jāraksta šādi, ja grib redzēt rezultātu
            cmd.ExecuteNonQuery(); // non-query for INSERT, UPDATE, DELETE and others that do not return dataset.

            cmd = new MySqlCommand("SELECT AVG(price) FROM Products", conn);
            decimal averagePrice = (decimal)cmd.ExecuteScalar();
            Console.WriteLine($"{averagePrice} is the average price.");



            cmd = new MySqlCommand("SELECT id, `name`, contact FROM Suppliers", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    uint id = (uint)reader[0]; //uint - unasigned intager
                    string name = (string)reader[1];
                    string contact = (string)reader[2];

                    Console.WriteLine($"{id} \t| {name} \t| {contact}");
                }
            }

            cmd = new MySqlCommand("INSERT INTO `Categories` (`name`) VALUES " + 
            "('Headphones'), ('Harddrive'), ('Software')", conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand(@"INSERT INTO Suppliers (`name`, `contact`, `phone`, `email`) VALUES 
            ('Big Distributor Inc.', 'John Doe', '+37122345345', 'john.doe@big-distributor.com'), 
            ('Pear', 'Jane Appleseed', '+37127288388', 'j.apple@pear.com'), 
            ('The Other Company', 'Adam J Peterson', '+37129001002', 'apet@other.com')", conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand (@"INSERT INTO Products (`name`, `description`, `price`, `warrantyPeriod`, `categoryID`, `supplierID`) VALUES 
            ('Icer', 'The perfect companion', 149, 36, 11, 1), '
            ('EarPods Pro', 'Redefine listening to music', 499, 1, 12, 2), 
            ('Yahama Floor', 'Sound quality great', 650, 1, 13, 3)", conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("INSERT INTO Orders (`customerName`, `customerSurname`, `customerEmail`, `customerPhone`, `status`) VALUES ('Eric', 'Rosen', 'e.rosen@gmail.com', '26263636', 'done'), ('Alyssa', 'Kane', 'alyssa@yahoo.com', '22009372', 'entered'), ('Bruno', 'Jewel', 'bruno@jewel.net', '27001001', 'entered')", conn);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("INSERT INTO OrderProducts (`orderId`, `productId`, `quantity`) VALUES ('7', '1', '1'), ('8', '2', '2'), ('9', '3', '1')", conn);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand("UPDATE Products SET price = 800 WHERE id=3", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Updated rows{rowsAffected}");

         




            conn.Close();

        }
    }
}
