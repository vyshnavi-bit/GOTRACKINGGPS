using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

using System.Data;

namespace UserData
{
 public   enum logintype
    {
        Facebook,
        Google,
        Bustop
    }
    public class ManageData
    {
        public static MySqlCommand cmd;
        public static void datainsert(string source, string destination, string name, string emailid, string status, string mobileno, string ticketno, DateTime journeydate, string seatnumbers, float fare, float totalamount)
        {
            try
            {
                cmd = new MySqlCommand("insert into datatransactions (Source,Destination,Name,Emailid,Status,MobNo,TicketNo,JourneyDate,SeatNumbers,Fare,TotalAmount) values (@Source,@Destination,@Name,@Emailid,@Status,@MobNo,@TicketNo,@JourneyDate,@SeatNumbers,@Fare,@TotalAmount)");
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Emailid", emailid);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@MobNo", mobileno);
                cmd.Parameters.AddWithValue("@TicketNo", ticketno);
                cmd.Parameters.AddWithValue("@JourneyDate", journeydate);
                cmd.Parameters.AddWithValue("@SeatNumbers", seatnumbers);
                cmd.Parameters.AddWithValue("@Fare", fare);
                cmd.Parameters.AddWithValue("@TotalAmount", totalamount);
                DbManager dbmgr = new DbManager();
                dbmgr.Execute(cmd);
                dbmgr = null;
            }
            catch
            {
            }
        }

        public static void statusupdate(string status, int transactionid)
        {
            try
            {
                cmd = new MySqlCommand("update datatransactions set Status=@Status where TransationID=@TransationID");
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@TransationID", transactionid);
                DbManager dbmgr = new DbManager();
                dbmgr.Execute(cmd);// DbManager.Execute(cmd);
                dbmgr = null;
            }
            catch
            {
            }
        }

        public static void insertuserinfo(string username, string password, string logintype, string name, DateTime birthday, string gender, string mobilenumber, string address)
        {
            try
            {
                cmd = new MySqlCommand("insert into userinfo (UserName,Password,LoginType,Name,BirthDay,Gender,MobileNumber,Address) values (@UserName,@Password,@LoginType,@Name,@BirthDay,@Gender,@MobileNumber,@Address)");
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@LoginType", logintype);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@BirthDay", birthday);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@MobileNumber", mobilenumber);
                cmd.Parameters.AddWithValue("@Address", address);
                DbManager dbmgr = new DbManager();
                dbmgr.Execute(cmd);
                dbmgr = null;//DbManager.Execute(cmd);
            }
            catch
            {
            }
        }
        public static DataTable getuserinfo(string username)
        {
            try
            {
                DataTable userinfotable = new DataTable();
                cmd = new MySqlCommand("select UserName,Password,LoginType,Name,BirthDay,Gender,MobileNumber,Address from userinfo where UserName=@UserName");
                cmd.Parameters.AddWithValue("@UserName", username);
                DbManager dbmgr = new DbManager();
              userinfotable=  dbmgr.SelectQuery(cmd).Tables[0];
              dbmgr = null;
            //     = DbManager.SelectQuery(cmd).Tables[0];
                return userinfotable;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void updateuserinfo(string username, string password, string logintype, string name, DateTime birthday, string gender, string mobilenumber, string address)
        {
            try
            {
                cmd = new MySqlCommand("update userinfo set Password=@Password,LoginType=@LoginType,Name=@Name,BirthDay=@BirthDay,Gender=@Gender,MobileNumber=@MobileNumber,Address=@Address where UserName=@UserName");
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@LoginType", logintype);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@BirthDay", birthday);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@MobileNumber", mobilenumber);
                cmd.Parameters.AddWithValue("@Address", address);
                DbManager dbmgr = new DbManager();
                dbmgr.Execute(cmd);
                dbmgr = null;//DbManager.Execute(cmd);
            }
            catch
            {
            }
        }

        public static DataTable getalluserinfo()
        {
            try
            {
                DataTable alluserinfotable = new DataTable();
                cmd = new MySqlCommand("select UserName,Password,LoginType,Name,BirthDay,Gender,MobileNumber,Address from userinfo");
             //   alluserinfotable = DbManager.SelectQuery(cmd).Tables[0];
                DbManager dbmgr = new DbManager();
               alluserinfotable= dbmgr.SelectQuery(cmd).Tables[0];
                dbmgr = null;//
                return alluserinfotable;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
