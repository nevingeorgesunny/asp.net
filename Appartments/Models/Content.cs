using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Appartments.Models
{
    public class Content
    {
        public string LandLordFirstName { get; set; }
        public string LandLordSecondName { get; set; }
        
        public bool maleRestriction { get; set; }
        public bool femaleRestriction { get; set; }
        public bool maratialStatusRestrictionMaried { get; set; }
        public bool maratialStatusRestrictionunMaried { get; set; }

        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string addressline3 { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public bool washingMcCheck { get; set; }
        public bool ovenCheck { get; set; }
        public bool AcCheck { get; set; }
        public string numberOfRooms { get; set; }
        public string numberOfAttachedBaths { get; set; }
        public string MinproposedRent { get; set; }
        public string MaxproposedRent { get; set; }


        public int addContent(  string email,
                                string LandLordFirstName,
                                string LandLordSecondName,
                                bool maleRestriction,
                                bool femaleRestriction,
                                bool maratialStatusRestrictionMaried,
                                bool maratialStatusRestrictionunMaried,
                                string addressline1,
                                string addressline2,
                                string addressline3,
                                string city,
                                string pinCode,
                                bool washingMcCheck,
                                bool ovenCheck,
                                bool AcCheck,
                                string numberOfRooms,
                                string numberOfAttachedBaths,
                                string MinproposedRent,
                                string MaxproposedRent)
        {



            using (var cn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AppartmentsDB;Integrated Security=True"))
            {

                string userSqlQuery = "select enlistedProperty from userDatabase where email = '"+email+"'";

                string userEnlistCount = null;
               
                SqlCommand cmd ,cmd2;

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

                                userEntryCount++;
                                reader.Close();

                                userSqlQuery = "INSERT INTO [dbo].[contentDB]" +
                                      "([email], [LandLordFirstName], [LandLordSecondName], [maleRestriction]," +
                                       "[femaleRestriction], [maratialStatusRestrictionMaried], [maratialStatusRestrictionunMaried], [addressline1]," +
                                       "[addressline2], [addressline3], [city], [pinCode]," +
                                       "[washingMcCheck], [ovenCheck], [AcCheck], [numberOfRooms],[numberOfAttachedBaths]," +
                                        "[MinproposedRent], [MaxproposedRent], [accountCountId])" +
                                      "VALUES" +
                                      "('" + email + "','" + LandLordFirstName + "','" + LandLordSecondName + "','" + maleRestriction + "','"+
                                      femaleRestriction + "','" + maratialStatusRestrictionMaried + "','" + maratialStatusRestrictionunMaried + "','" + addressline1 + "','"+
                                        addressline2 + "','" + addressline3 + "','" + city + "','" + pinCode + "','"+
                                         washingMcCheck + "','" + ovenCheck + "','" + AcCheck + "','" + numberOfRooms + "','" + numberOfAttachedBaths + "','" + MinproposedRent + "','"+
                                         MaxproposedRent + "'," + userEntryCount +")" ;


                                try
                                {
                                    cmd2 = new SqlCommand(userSqlQuery, cn);

                                    cmd2.ExecuteNonQuery();

                                    cmd2.Dispose();

                                    userEnlistCount = "UPDATE  [dbo].[userDatabase] set enlistedProperty =" + userEntryCount +
                                                        "where email='" + email + "'";

                                    try
                                    {

                                        cmd = new SqlCommand(userEnlistCount, cn);

                                        cmd.ExecuteNonQuery();

                                        cmd.Dispose();

                                    }
                                    catch (Exception ex)
                                    {
                                        return 4;
                                    }

                                }
                                catch (Exception ex)
                                {

                                    System.Diagnostics.Debug.WriteLine("\n\n---------------" + ex);

                                    System.Diagnostics.Debug.WriteLine("\n\n---------------" + userSqlQuery);
                                    return 3;
                                }
                            }
                            else
                                return 2;
                         
                         
                            
                        }
                    }

                   

                    cn.Close();

                 



                }
                catch (Exception ex)
                {

                  
                    cn.Close();


                 

                    return 0;


                }


            }
            return 1;
        }
       
    }
}