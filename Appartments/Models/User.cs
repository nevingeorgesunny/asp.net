using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Appartments.Models
{
    public class User
    {

        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        
        
        [Required]
        public string Email { get; set; }


       
        public string Password { get; set; }


        public string logUser(string email,string password)
        {
            string response = "false";

            password = Encode(password);

            using (var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AppartmentsDB;Integrated Security=True"))
            {
               

                string sql2 = "SELECT * FROM [dbo].[userDatabase] WHERE email = '"+email+"' AND password = '"+password+"'; ";




                SqlCommand cmd;

              

                try
                {
                    cn.Open();

                    cmd = new SqlCommand(sql2, cn);

                    var reader = cmd.ExecuteReader();

                   

                   

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            System.Diagnostics.Debug.WriteLine("\n\n*********LOGIN FOUND************\n\n\n");
                            System.Diagnostics.Debug.WriteLine(reader.GetString(0)+" "+reader.GetString(1));

                            response = reader.GetString(0) + " " + reader.GetString(1);
                        }


                       
                        cn.Close();
                        reader.Dispose();
                        cmd.Dispose();
                        return response;
                    }
                    else
                    {
                        cn.Close();
                        reader.Dispose();
                        cmd.Dispose();
                        return response;
                    }

                    cn.Close();


                }
                catch (Exception ex)
                {
                    cn.Close();


                    System.Diagnostics.Debug.WriteLine("\n\n******* CONNECTION FAILED (LOGIN )\n");





                    return response;


                }
            }



            return response;
        }

        public bool checkLimit(string email)
        {

            using (var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AppartmentsDB;Integrated Security=True"))
            {

                string userSqlQuery = "select enlistedProperty from userDatabase where email = '" + email + "'";

               

                SqlCommand cmd, cmd2;

                int userEntryCount = 0;

                

                try
                {
                    cn.Open();

                    cmd = new SqlCommand(userSqlQuery, cn);

                    var reader = cmd.ExecuteReader();



                    cmd.Dispose();


                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            userEntryCount = reader.GetInt32(0);

                            if (userEntryCount < 5)
                            {
                                cn.Close();
                                return true;
                            }
                        }
                    }




                }
                catch (Exception ex)
                {

                    
                    cn.Close();

                }
            }

            return false;
        }



        public bool addUser(string firstName, string LastName, string email,string password)
        {


           


            using (var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AppartmentsDB;Integrated Security=True"))
            {

                string _sql = "INSERT INTO [dbo].[userDatabase]" +
                      "([firstName], [secondName], [email], [password])" +
                      "VALUES" +
                      "('" + firstName + "','" + LastName + "','" + email + "','" + Encode(password) + "')";




                SqlCommand cmd;

                try
                {
                    cn.Open();

                    cmd = new SqlCommand(_sql, cn);

                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    cn.Close();

                    System.Diagnostics.Debug.WriteLine("DB update sucess");



                }
                catch (Exception ex)
                {
                    cn.Close();


                    System.Diagnostics.Debug.WriteLine("DB update failed");

                    return false;


                }


            }
            return true;

        }

          public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }


    }
}