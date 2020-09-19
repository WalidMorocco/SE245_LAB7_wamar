using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SE245_LAB5_wamar
{
    class PersonV2 : Person
    {
        private string instagram, cellPhone;

        public string Instagram
        {
            get
            {
                return instagram;
            }
            set
            {
                if (ValidationLibrary.IsValidInstagram(value))
                {
                    instagram = value;
                }
                else
                {
                    feedback += "\n ERROR : Invalid instagram nickname syntax";
                }
            }
        }

        public string CellPhone
        {
            get
            {
                return cellPhone;
            }
            set
            {
                if (ValidationLibrary.IsValidPhone(value))
                {
                    cellPhone = value;
                }
                else
                {
                    feedback += "\n ERROR : Invalid cell phone syntax";
                }
            }
        }



        public string AddARecord()
        {
            //Init string var
            string strResult = "";

            //Make a connection object
            SqlConnection Conn = new SqlConnection();

            //Initialize it's properties
            Conn.ConnectionString = @"Server=sql.neit.edu,4500;Database=SE245_WAmar;User Id=SE245_WAmar;Password=008009482;";     //Set the Who/What/Where of DB

            string strSQL = "INSERT INTO Persons (FName, MName, LName, Street1, Street2, City, State, Zip, Phone, Email, Instagram, CellPhone) VALUES (@FName, @MName, @LName, @Street1, @Street2, @City, @State, @Zip, @Phone, @Email, @Instagram, @CellPhone)";

            // Bark out our command
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSQL;  
            comm.Connection = Conn;     

            //Fill in the paramters (Has to be created in same sequence as they are used in SQL Statement)
            comm.Parameters.AddWithValue("@FName", FName);
            comm.Parameters.AddWithValue("@MName", MName);
            comm.Parameters.AddWithValue("@LName", LName);
            comm.Parameters.AddWithValue("@Street1", Street1);
            comm.Parameters.AddWithValue("@Street2", Street2);
            comm.Parameters.AddWithValue("@City", City);
            comm.Parameters.AddWithValue("@State", State);
            comm.Parameters.AddWithValue("@Zip", Zip);
            comm.Parameters.AddWithValue("@Phone", Phone);
            comm.Parameters.AddWithValue("@Email", Email);
            comm.Parameters.AddWithValue("@Instagram", Instagram);
            comm.Parameters.AddWithValue("@CellPhone", CellPhone);

            
            try
            {
                Conn.Open();                                        
                int intRecs = comm.ExecuteNonQuery();
                strResult = $"SUCCESS: Inserted {intRecs} records.";       
                Conn.Close();                                      
            }
            catch (Exception err)                                   
            {
                strResult = "ERROR: " + err.Message;                
            }
            finally
            {

            }



            return strResult;

        }


        public DataSet SearchPersons(String strFName, String strLName)
        {
            //Create a dataset to return filled
            DataSet ds = new DataSet();

            //Create a command for our SQL statement
            SqlCommand comm = new SqlCommand();

            //Write a Select Statement to perform Search
            String strSQL = "SELECT Phone, FName, MName, LName, City, State FROM Persons WHERE 0=0";

            //If the First/Last Name is filled in include it as search criteria
            if (strFName.Length > 0)
            {
                strSQL += " AND FName LIKE @FName";
                comm.Parameters.AddWithValue("@FName", "%" + strFName + "%");
            }
            if (strLName.Length > 0)
            {
                strSQL += " AND LName LIKE @LName";
                comm.Parameters.AddWithValue("@LName", "%" + strLName + "%");
            }

            //Create DB tools and Configure
            SqlConnection conn = new SqlConnection();

            //Create the who, what where of the DB
            string strConn = @GetConnected();
            conn.ConnectionString = strConn;

            //Fill in basic info to command object
            comm.Connection = conn;     
            comm.CommandText = strSQL;  

            //Create Data Adapter
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comm;   

            //Get Data
            conn.Open();                
            da.Fill(ds, "Persons_Temp");     
            conn.Close();               

            //Return the data
            return ds;
        }

        public SqlDataReader FindOnePerson(string strPersonV2_Phone)
        {
            //Create and Initialize the DB Tools we need
            SqlConnection conn = new SqlConnection();
            SqlCommand comm = new SqlCommand();

            //My Connection String
            string strConn = GetConnected();

            //My SQL command string to pull up one EBook's data
            string sqlString =
           "SELECT * FROM Persons WHERE Phone = @Phone;";

            //Tell the connection object the who, what, where, how
            conn.ConnectionString = strConn;

            //Give the command object info it needs
            comm.Connection = conn;
            comm.CommandText = sqlString;
            comm.Parameters.AddWithValue("@Phone", strPersonV2_Phone);

            //Open the DataBase Connection and Yell our SQL Command
            conn.Open();

            //Return some form of feedback
            return comm.ExecuteReader();   

        }

        private string GetConnected()
        {
            return "Server=sql.neit.edu,4500;Database=SE245_WAmar;User Id=SE245_WAmar;Password=008009482;";
        }


        public PersonV2() : base()
        {
            instagram = "";
            cellPhone = "";
        }


    }
}
