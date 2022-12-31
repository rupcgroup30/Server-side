using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using AirBnb1.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }




    //--------------------------------------------------------------------------------------------------
    // This method read vacations 
    //--------------------------------------------------------------------------------------------------
    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        cmd = CreateReadCommandSP("spGetVacation", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Vacation> list = new List<Vacation>();

            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.Id = v.FlatId = Convert.ToInt32(dataReader["id"]);
                v.UserId = dataReader["Email"].ToString();
                v.FlatId = Convert.ToInt32(dataReader["idF"]);
                v.StartDate = dataReader["startDate"].ToString();
                v.EndDate = dataReader["endDate"].ToString();
                list.Add(v);
            }

            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method inserts a flat to the flats table 
    //--------------------------------------------------------------------------------------------------
    public int InsertVacation(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }
        if (DateTime.Parse(vacation.StartDate) > DateTime.Parse(vacation.EndDate))
            return 0;
        else
            cmd = CreateInsertVacationCommandWithStoredProcedure("spInsertV", con, vacation);// create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the insert SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertVacationCommandWithStoredProcedure(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Email", vacation.UserId);

        cmd.Parameters.AddWithValue("@idF", vacation.FlatId);

        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);

        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);

        return cmd;
    }






    //--------------------------------------------------------------------------------------------------
    // This method read flats 
    //--------------------------------------------------------------------------------------------------
    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        cmd = CreateReadCommandSP("spGetFlat", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Flat> list = new List<Flat>();

            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = Convert.ToInt32(dataReader["id"]);
                f.City = dataReader["city"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                f.Address = dataReader["address"].ToString();
                f.NumberOfRooms = Convert.ToInt32(dataReader["number of rooms"]);
                list.Add(f);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method inserts a flat to the flats table 
    //--------------------------------------------------------------------------------------------------
    public int InsertFlat(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }
        if (flat.NumberOfRooms < 0 || flat.Price<0)
            return 0;
        else
            cmd = CreateInsertFlatCommandWithStoredProcedure("spInsertFlat", con, flat);// create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    //---------------------------------------------------------------------------------
    // Create the insert SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertFlatCommandWithStoredProcedure(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@city", flat.City);

        cmd.Parameters.AddWithValue("@price", flat.Price);

        cmd.Parameters.AddWithValue("@address", flat.Address);

        cmd.Parameters.AddWithValue("@numberofrooms", flat.NumberOfRooms);

        return cmd;
    }





    //--------------------------------------------------------------------------------------------------
    // This method read Users 
    //--------------------------------------------------------------------------------------------------
    public List<User> ReadUser(string email, string password)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        cmd = CreateReadUserCommandSP("spGetUser", con,email,password);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<User> list = new List<User>();

            while (dataReader.Read())
            {
                User u = new User();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.Email = dataReader["Email"].ToString();
                list.Add(u);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    //---------------------------------------------------------------------------------
    // Create the Read SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadUserCommandSP(String spName, SqlConnection con, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@email", email);

        cmd.Parameters.AddWithValue("@Password", password);

        return cmd;
    }



    //---------------------------------------------------------------------------------
    // Create the Read SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadCommandSP(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a user to the users table 
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        cmd = CreateInsertUserCommandWithStoredProcedure("spInsertUser", con, user);// create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the insert SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertUserCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);

        cmd.Parameters.AddWithValue("@LastName", user.LastName);

        cmd.Parameters.AddWithValue("@email", user.Email);

        cmd.Parameters.AddWithValue("@Password", user.Password);


        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method updates a user password to the user table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        cmd = CreateUpdateUserCommandSP("spUpdateUser", con, user);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw new Exception(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure
        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@email", user.Email);

        return cmd;
    }


}
