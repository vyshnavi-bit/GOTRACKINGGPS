namespace Bustop.Hanlders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.SessionState;
    using System.Data;
    using System.IO;
    using System.Net.Mail;
    using System.Net;
    using System.Text.RegularExpressions;
    using MySql.Data.MySqlClient;
    using Demo.WindowsForms;
    using System.Globalization;

    /// <summary>
    /// Summary description for Bus
    /// </summary>
    public class Bus : IHttpHandler, IRequiresSessionState
    {
       
        public bool IsReusable
        {
            get { return true; }
        }

        public class vehiclesclass
        {
            public string vehicleno { get; set; }
            public string vehicletype { get; set; }
            public string vehicletag { get; set; }
        }
        public class SelectData
        {
            
        }
        public class vehiclesupdateclass
        {
            public string vehiclenum { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string direction { get; set; }
            public string Speed { get; set; }
            public string Datetime { get; set; }
            public string mainpower { get; set; }
            public string dieselvalue { get; set; }
            public string odometervalue { get; set; }
            public string Ignation { get; set; }
            public string ACStatus { get; set; }
            public string GPSSignal { get; set; }
            public string Geofence { get; set; }
            public string todaymileage { get; set; }
            public double fulltankval { get; set; }
            public string stoppedfor { get; set; }
        }

        public class Groupsclass
        {
            public string groupname { get; set; }
            public List<string> vehicleno { get; set; }
        }

        //public class odometervalues
        //{
        //    public string odometer { get; set; }
        //}

        public class BranchData
        {
            public string BranchName { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string Decription { get; set; }
            public string Image { get; set; }
        }

        public class logsclass
        {
            public string Sno { get; set; }
            public string datetime { get; set; }
            public string vehicleno { get; set; }
            public string vehicletype { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string direction { get; set; }
            public string speed { get; set; }
            public string Status { get; set; }
            public  DataTable Reportsdata { get; set; }
            public string odometer { get; set; }
            public string Altitude { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string operation = context.Request["op"];
                 if (operation == "login" || operation == "log_out")
                {
                }
                else
                {
                    context.Session["field1"] = HttpUtility.UrlDecode(context.Request.Cookies["field1"].Value);
                    context.Session["field2"] = HttpUtility.UrlDecode(context.Request.Cookies["field2"].Value);
                    context.Session["AccountStatus"] = HttpUtility.UrlDecode(context.Request.Cookies["AccountStatus"].Value);
                    context.Session["smsoffer"] = HttpUtility.UrlDecode(context.Request.Cookies["smsoffer"].Value);
                    context.Session["field3"] = HttpUtility.UrlDecode(context.Request.Cookies["field3"].Value);
                    context.Session["ReportFromTime"] = HttpUtility.UrlDecode(context.Request.Cookies["ReportFromTime"].Value);
                    context.Session["ReportToTime"] = HttpUtility.UrlDecode(context.Request.Cookies["ReportToTime"].Value);

                    //context.Session["UserType"] = HttpUtility.UrlDecode(context.Request.Cookies["UserType"].Value);
                    //context.Session["Data"] = HttpUtility.UrlDecode(context.Request.Cookies["Data"].Value);
                    //context.Session["reportdata"] = HttpUtility.UrlDecode(context.Request.Cookies["reportdata"].Value);
                    //context.Session["title"] = HttpUtility.UrlDecode(context.Request.Cookies["title"].Value);
                    //context.Session["vendorstable"] = HttpUtility.UrlDecode(context.Request.Cookies["vendorstable"].Value);
                    //context.Session["odometerValues"] = HttpUtility.UrlDecode(context.Request.Cookies["odometerValues"].Value);
                    //context.Session["xportdata"] = HttpUtility.UrlDecode(context.Request.Cookies["xportdata"].Value);
                    //context.Session["filteredtable"] = HttpUtility.UrlDecode(context.Request.Cookies["filteredtable"].Value);
                    //context.Session["vehiclesdata"] = HttpUtility.UrlDecode(context.Request.Cookies["vehiclesdata"].Value);
                    //context.Session["allvehicles"] = HttpUtility.UrlDecode(context.Request.Cookies["allvehicles"].Value);
                }
                switch (operation)
                {
                    case "checkstatus":
                        checkstatus(context);
                        break;
                    case "login":
                        login(context);
                        break;
                    case "log_out":
                        log_out(context);
                        break;
                    case "InitilizeVehicles":
                        InitilizeVehicles(context);
                        break;
                    case "LiveUpdate":
                        LiveUpdate(context);
                        break;
                    case "InitilizeGroups":
                        InitilizeGroups(context);
                        break;
                    case "ShowMyLocations":
                        ShowMyLocations(context);
                        break;
                    case "getdata":
                        getdata(context);
                        break;
                    case "getNearestVehicle":
                        getNearestVehicle(context);
                        break;
                    case "InitilizeVehiclesreports":
                        InitilizeVehiclesreports(context);
                        break;
                    case "updatetagname":
                        updatetagname(context);
                        break;
                    case "getvehiclesdatareport":
                        getvehiclesdatareport(context);
                        break;
                    case "OnclickDrawRoute":
                        OnclickDrawRoute(context);
                        break;

                    case "retrieve_alrtname":
                        retrieve_alrtname(context);
                        break;
                    case "rettrieve_alert_data":
                        rettrieve_alert_data(context);
                        break;
                    case "for_alert_delete":
                        for_alert_delete(context);
                        break;
                    case "for_alert_status":
                        for_alert_status(context);
                        break;
                    case "getvehicles":
                        getvehicles(context);
                        break;
                    case "get_assignalerts":
                        get_assignalerts(context);
                        break;
                    case "assignvehper_del":
                        assignvehper_del(context);
                        break;
                    case "getvehicles_assign":
                        getvehicles_assign(context);
                        break;
                    case "Person_details_save":
                        Person_details_save(context);
                        break;
                    case "person_details_delete":
                        person_details_delete(context);
                        break;
                    case "get_persondetails":
                        get_persondetails(context);
                        break;
                    case "get_Routes":
                        get_Routes(context);
                        break;
                    case "get_offer":
                        get_offer(context);
                        break;
                    case "ReportTiming_details_save":
                        ReportTiming_details_save(context);
                        break;
                    case "Get_ReportTiming_details":
                        Get_ReportTiming_details(context);
                        break; 
                    case "save_locations":
                        save_locations(context);
                        break;
                    case "get_locations":
                        get_locations(context);
                        break;
                    case "delete_location":
                        delete_location(context);
                        break; 
                    case "get_groups":
                        get_groups(context);
                        break;
                    case "delete_group":
                        delete_group(context);
                        break;
                    case "save_groups":
                        save_groups(context);
                        break;
                    case "changepassword":
                        changepassword(context);
                        break;
                    case "plantvehiclesdata_gen":
                        plantvehiclesdata_gen(context);
                        break;
                        
                    default:
                        var js = new JavaScriptSerializer();
                        var title1 = context.Request.Params[1];


                        vehicle_routes obj1 = js.Deserialize<vehicle_routes>(title1);
                        if (obj1.op == "Vehicle_alrts_save")
                        {
                            Vehicle_alrts_save(context);
                        }
                        assign_alrts obj2 = js.Deserialize<assign_alrts>(title1);
                        if (obj1.op == "assignalerts_save")
                        {
                            assignalerts_save(context);
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                string response = GetJson("Error login");
                context.Response.Write(response);
            }
        }
        private void plantvehiclesdata_gen(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string checkedvehicle = context.Request["checkedvehicle"];
                string startdate = context.Request["startdt"];
                string enddate = context.Request["enddt"];
                DateTime fromdate = DateTime.Now;
                DateTime todate = DateTime.Now;
                fromdate = DateTime.Parse(startdate);
                if (startdate != "" && startdate != null)
                {
                    fromdate = DateTime.ParseExact(startdate, "yyyy-MM-dd'T'HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (enddate != "" && enddate != null)
                {
                    todate = DateTime.ParseExact(enddate, "yyyy-MM-dd'T'HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch
            {
            }
        }
        private void changepassword(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string txt_oldpassword = context.Request["txt_oldpassword"].ToString();
                string txt_newpassword = context.Request["txt_newpassword"].ToString();
                cmd = new MySqlCommand("select * from UserAccounts where UserName=@UserName");
                cmd.Parameters.Add("@UserName", Username);
                DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                string msg = "";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["password"].ToString() == txt_oldpassword)
                    {
                        cmd = new MySqlCommand("Update UserAccounts set Password=@password where UserName=@UserName");
                        cmd.Parameters.Add("@UserName", Username);
                        cmd.Parameters.Add("@password", txt_newpassword);
                        vdm.Update(cmd);
                        msg = "success";
                    }
                    else
                    {
                        msg = "Old password not matched!";
                    }
                }
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        class locationscls
        {
            public string locacationname { set; get; }
            public string Description { set; get; }
            public string latitude { set; get; }
            public string longitude { set; get; }
            public string Radious { set; get; }
            public string PhoneNumber { set; get; }
            public string Image { set; get; }
            public string Sno { set; get; }
        }
        
        private void delete_group(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string group = context.Request["group"].ToString();
                cmd = new MySqlCommand("Delete from VehicleGroup where GroupName=@GroupName and UserName=@UserName");
                cmd.Parameters.Add("@GroupName", group);
                cmd.Parameters.Add("@UserName", Username);
                vdm.Delete(cmd);
                string response = GetJson("success");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void delete_location(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string locacationsno = context.Request["locacationsno"].ToString();
                cmd = new MySqlCommand("delete from BranchData where Sno=@Sno and UserName= @UserName");
                cmd.Parameters.Add("@UserName", Username);
                cmd.Parameters.Add("@Sno", locacationsno);
                vdm.Delete(cmd);
                string response = GetJson("success");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void save_locations(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string locacationsno = context.Request["locacationsno"].ToString();
                string txtlocacationname = context.Request["txtlocacationname"].ToString();
                string txtDescription = context.Request["txtDescription"].ToString();
                string txtlatitude = context.Request["txtlatitude"].ToString();
                string txtlongitude = context.Request["txtlongitude"].ToString();
                string txtMyLocationRadious = context.Request["txtMyLocationRadious"].ToString();
                string txt_PhoneNumber = context.Request["txt_PhoneNumber"].ToString();
                string txt_Image = context.Request["txt_Image"].ToString();
                string operationtype = context.Request["operationtype"].ToString();

                if (operationtype == "Save")
                {
                    cmd = new MySqlCommand("insert into BranchData (UserName,BranchID, Description, Latitude, Longitude, PhoneNumber,ImagePath,Radious ) values (@UserName,@BranchID, @Description, @Latitude, @Longitude, @PhoneNumber,@ImagePath,@Radious)");
                    cmd.Parameters.Add("@BranchID", txtlocacationname);
                    cmd.Parameters.Add("@Description", txtDescription);
                    cmd.Parameters.Add("@Latitude", txtlatitude);
                    cmd.Parameters.Add("@Longitude", txtlongitude);
                    cmd.Parameters.Add("@PhoneNumber", txt_PhoneNumber);
                    cmd.Parameters.Add("@UserName", Username);
                    cmd.Parameters.Add("@ImagePath", txt_Image);
                    cmd.Parameters.Add("@Radious", txtMyLocationRadious);
                    vdm.insert(cmd);
                }
                else
                {
                    cmd = new MySqlCommand("update BranchData set BranchID=@BranchID, Description=@Description, Latitude=@Latitude, Longitude=@Longitude, PhoneNumber=@PhoneNumber,ImagePath=@ImagePath,Radious=@Radious where  Sno=@Sno and UserName= @UserName ");
                    cmd.Parameters.Add("@BranchID", txtlocacationname);
                    cmd.Parameters.Add("@Description", txtDescription);
                    cmd.Parameters.Add("@Latitude", txtlatitude);
                    cmd.Parameters.Add("@Longitude", txtlongitude);
                    cmd.Parameters.Add("@PhoneNumber", txt_PhoneNumber);
                    cmd.Parameters.Add("@UserName", Username);
                    cmd.Parameters.Add("@ImagePath", txt_Image);
                    cmd.Parameters.Add("@Radious", txtMyLocationRadious);
                    cmd.Parameters.Add("@Sno", locacationsno);
                    vdm.Update(cmd);
                }
                string response = GetJson("success");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void get_locations(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                List<locationscls> locationsclslst = new List<locationscls>();
                cmd = new MySqlCommand("select BranchID,Description,Latitude,Longitude,PhoneNumber,ImagePath,Radious,Sno from BranchData where UserName=@UN");
                cmd.Parameters.Add("@UN", Username);
                DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    locationscls locationsclass = new locationscls();
                    locationsclass.locacationname = dr["BranchID"].ToString();
                    locationsclass.Description = dr["Description"].ToString();
                    locationsclass.latitude = dr["Latitude"].ToString();
                    locationsclass.longitude = dr["Longitude"].ToString();
                    locationsclass.Radious = dr["Radious"].ToString();
                    locationsclass.PhoneNumber = dr["PhoneNumber"].ToString();
                    locationsclass.Image = dr["ImagePath"].ToString();
                    locationsclass.Sno = dr["Sno"].ToString();
                    locationsclslst.Add(locationsclass);
                }
                string response = GetJson(locationsclslst);
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void save_groups(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string txt_groupname = context.Request["txt_groupname"].ToString();
                string vehicles = context.Request["vehicles"].ToString();
                string operationtype = context.Request["operationtype"].ToString();
                string[] grpvehs = vehicles.Split(',');
                if (operationtype == "Save")
                {
                    foreach (string veh in grpvehs)
                    {
                        cmd = new MySqlCommand("insert into VehicleGroup (GroupName,VehicleID,UserName) values (@GroupName,@VehicleID,@UserName) ");
                        cmd.Parameters.Add("@GroupName", txt_groupname);
                        cmd.Parameters.Add("@VehicleID", veh.Trim());
                        cmd.Parameters.Add("@UserName", Username);
                        vdm.insert(cmd);
                    }
                }
                else
                {
                    cmd = new MySqlCommand("Delete from VehicleGroup where GroupName=@GroupName and UserName=@UN");
                    cmd.Parameters.Add("@GroupName", txt_groupname);
                    cmd.Parameters.Add("@UN", Username);
                    vdm.Delete(cmd);

                    foreach (string veh in grpvehs)
                    {
                        cmd = new MySqlCommand("insert into VehicleGroup (GroupName,VehicleID,UserName) values (@GroupName,@VehicleID,@UserName) ");
                        cmd.Parameters.Add("@GroupName", txt_groupname);
                        cmd.Parameters.Add("@VehicleID", veh.Trim());
                        cmd.Parameters.Add("@UserName", Username);
                        vdm.insert(cmd);
                    }
                }
                string response = GetJson("success");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        class groupscls
        {
            public string GroupName { set; get; }
            public string VehicleID { set; get; }
        }

        private void get_groups(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                List<groupscls> groupsclslst = new List<groupscls>();
                cmd = new MySqlCommand("SELECT GroupName, VehicleID FROM vehiclegroup WHERE (UserName = @UserName)");
                cmd.Parameters.Add("@UserName", Username);
                DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    DataTable dstcttbl = dv.ToTable(true, "GroupName");
                    foreach (DataRow dr in dstcttbl.Rows)
                    {
                        groupscls groupsclass = new groupscls();
                        groupsclass.GroupName = dr["GroupName"].ToString();
                        DataRow[] rows = dt.Select("GroupName='" + dr["GroupName"].ToString() + "'");
                        var vehs = "";
                        foreach (DataRow drr in rows)
                        {
                            vehs += drr["VehicleID"].ToString().Trim() + ',';
                        }
                        if (vehs != "")
                            vehs = vehs.Substring(0, vehs.Length - 1);
                        groupsclass.VehicleID = vehs;
                        groupsclslst.Add(groupsclass);
                    }
                }
                string response = GetJson(groupsclslst);
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void get_offer(HttpContext context)
        {
            if (context.Session["smsoffer"] != null)
            {
                string smsoffer = context.Session["smsoffer"].ToString();
                string response = GetJson(smsoffer);
                context.Response.Write(response);
            }
            
        }

        class vehicle_routes
        {
            public string op { set; get; }
            public string alertname { set; get; }
            public List<alerttype1> alerttype = new List<alerttype1>();

            //public string alerttype { set; get; }
            public string alrtrept { set; get; }
            public string timegap { set; get; }
            public string maxspeed { set; get; }
            public string time1 { set; get; }
            public string time2 { set; get; }
            public List<string> inpoi { get; set; }
            public List<string> outpoi { get; set; }

            //public string inpoi { set; get; }
            //public string outpoi { set; get; }
            //public string tablelocs { set; get; }
            public List<tablelocs1> tablelocs = new List<tablelocs1>();

            public string stpmaxstptme { set; get; }
            public string btnval { set; get; }
            public string mail { set; get; }
            public string mobile { set; get; }
            public string sno { set; get; }
            public string subsno { set; get; }
            public List<string> noalert { get; set; }

        }
        class checkstatuscls
        {
            public string Username { set; get; }
            public string AccountStatus { set; get; }
            public string status { set; get; }
        }
        private void checkstatus(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string username = context.Request["field1"].ToString();
                cmd = new MySqlCommand("SELECT report_totime FROM useraccounts WHERE (UserName = @UserName)");
                cmd.Parameters.Add("@UserName", username);
                 DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                 string msg = "0";
                 if (dt.Rows.Count > 0)
                 {
                     //string strdate = dt.Rows[0]["report_totime"].ToString();
                     //if (strdate == "")
                     //{
                     //}
                     //else
                     //{
                     //    DateTime dtstrdate = Convert.ToDateTime(strdate);
                     //    DateTime Currentdate = DbManager.GetTime(vdm.conn);
                     //    if (Currentdate > dtstrdate)
                     //    {
                     //        msg = "1";
                     //    }
                     //    else
                     //    {
                     //        msg = "0";
                     //    }
                     //}

                 }
                string Username = context.Session["field1"].ToString();
                string AccountStatus = context.Session["AccountStatus"].ToString();
                checkstatuscls checkstatusclss = new checkstatuscls();
                checkstatusclss.Username = Username;
                checkstatusclss.AccountStatus = AccountStatus;
                //checkstatusclss.status = msg;
                string response = GetJson(checkstatusclss);
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void login(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string username = context.Request["username"].ToString();
                string password = context.Request["pwd"].ToString();
                DateTime Currentdate = DbManager.GetTime(vdm.conn);
                cmd = new MySqlCommand("select * from UserAccounts where UserName=@UN and Password=@Pwd");
                cmd.Parameters.Add("@UN", username);
                cmd.Parameters.Add("@Pwd", password);
                DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string date = dt.Rows[0]["report_totime"].ToString();
                    if (date != "")
                    {
                        DateTime dtstrdate = Convert.ToDateTime(date);
                        if (Currentdate > dtstrdate)
                        {
                            string response = GetJson("Expired");
                            context.Response.Write(response);
                        }
                        else
                        {
                            context.Session["smsoffer"] = dt.Rows[0]["msgoffer"].ToString();
                            context.Response.Cookies["smsoffer"].Value = HttpUtility.UrlEncode(dt.Rows[0]["msgoffer"].ToString());
                            context.Response.Cookies["smsoffer"].Path = "/";
                            context.Response.Cookies["smsoffer"].Expires = DateTime.Now.AddDays(1);

                            context.Session["field3"] = dt.Rows[0]["Sno"].ToString();
                            context.Response.Cookies["field3"].Value = HttpUtility.UrlEncode(dt.Rows[0]["Sno"].ToString());
                            context.Response.Cookies["field3"].Path = "/";
                            context.Response.Cookies["field3"].Expires = DateTime.Now.AddDays(1);

                            context.Session["ReportFromTime"] = dt.Rows[0]["report_fromtime"].ToString();
                            context.Response.Cookies["ReportFromTime"].Value = HttpUtility.UrlEncode(dt.Rows[0]["report_fromtime"].ToString());
                            context.Response.Cookies["ReportFromTime"].Path = "/";
                            context.Response.Cookies["ReportFromTime"].Expires = DateTime.Now.AddDays(1);

                            context.Session["ReportToTime"] = dt.Rows[0]["report_totime"].ToString();
                            context.Response.Cookies["ReportToTime"].Value = HttpUtility.UrlEncode(dt.Rows[0]["report_totime"].ToString());
                            context.Response.Cookies["ReportToTime"].Path = "/";
                            context.Response.Cookies["ReportToTime"].Expires = DateTime.Now.AddDays(1);

                            string UN = dt.Rows[0]["UserName"].ToString();
                            if (UN == "APURVA")
                            {
                                UN = "vyshnavi";
                                context.Session["field1"] = UN;
                            }
                            else
                            {
                                context.Session["field1"] = dt.Rows[0]["UserName"].ToString();
                            }
                            context.Response.Cookies["field1"].Value = HttpUtility.UrlEncode(dt.Rows[0]["UserName"].ToString());
                            context.Response.Cookies["field1"].Path = "/";
                            context.Response.Cookies["field1"].Expires = DateTime.Now.AddDays(1);

                            context.Session["field2"] = true;
                            context.Response.Cookies["field2"].Value = HttpUtility.UrlEncode("true");
                            context.Response.Cookies["field2"].Path = "/";
                            context.Response.Cookies["field2"].Expires = DateTime.Now.AddDays(1);

                            context.Session["AccountStatus"] = dt.Rows[0]["Status"].ToString();
                            context.Response.Cookies["AccountStatus"].Value = HttpUtility.UrlEncode(dt.Rows[0]["Status"].ToString());
                            context.Response.Cookies["AccountStatus"].Path = "/";
                            context.Response.Cookies["AccountStatus"].Expires = DateTime.Now.AddDays(1);

                            cmd = new MySqlCommand("insert into logininfo(username,doe) values(@username,@doe)");
                            cmd.Parameters.Add("@username", username);
                            cmd.Parameters.Add("@doe", Currentdate);
                            vdm.insert(cmd);
                            string response = GetJson("Valid");
                            context.Response.Write(response);
                        }
                    }
                   
                }
                else
                {
                    string response = GetJson("Not Valid");
                    context.Response.Write(response);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void log_out(HttpContext context)
        {
            try
            {
                context.Session["field1"] = null;
                context.Session["field2"] = null;
                context.Session["AccountStatus"] = null;
                context.Session["ReportFromTime"] = null;
                context.Session["ReportToTime"] = null;
                context.Session["field3"] = null;
                context.Session["smsoffer"] = null;
                context.Response.Cookies["field1"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["field2"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["AccountStatus"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["ReportFromTime"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["ReportToTime"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["field3"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["smsoffer"].Expires = DateTime.Now.AddDays(-1);

                context.Session["Dealer"] = null;
                context.Session["UserType"] = null;
                context.Session["Data"] = null;
                context.Session["reportdata"] = null;
                context.Session["title"] = null;
                context.Session["odometerValues"] = null;
                context.Session["xportdata"] = null;
                context.Session["filteredtable"] = null;
                context.Session["vendorstable"] = null;
                context.Session["vehiclesdata"] = null;
                context.Session["allvehicles"] = null;

                context.Response.Cookies["allvehicles"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["vehiclesdata"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["vendorstable"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["filteredtable"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["xportdata"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["odometerValues"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["title"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["reportdata"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["Data"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["Dealer"].Expires = DateTime.Now.AddDays(-1);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void ReportTiming_details_save(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string fromtime = context.Request["fromtime"].ToString();
                string totime = context.Request["totime"].ToString();
                cmd = new MySqlCommand("update useraccounts set report_fromtime=@report_fromtime, report_totime=@report_totime where UserName=@UserName");
                cmd.Parameters.Add("@report_fromtime", fromtime);
                cmd.Parameters.Add("@report_totime", totime);
                cmd.Parameters.Add("@UserName", Username);
                vdm.Update(cmd);
                string response = GetJson("Data Saved Successfully");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        class Get_ReportTiming_detailscls
        {
            public string fromtime { set; get; }
            public string totime { set; get; }
        }
        private void Get_ReportTiming_details(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                List<Get_ReportTiming_detailscls> Get_ReportTiming_detailsclslist = new List<Get_ReportTiming_detailscls>();
                string Username = context.Session["field1"].ToString();
                cmd = new MySqlCommand("select report_fromtime,report_totime from useraccounts where UserName=@UserName");
                cmd.Parameters.Add("@UserName", Username);
                DataTable dt = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    Get_ReportTiming_detailscls Get_ReportTiming_details = new Get_ReportTiming_detailscls();
                    Get_ReportTiming_details.fromtime = dr["report_fromtime"].ToString();
                    Get_ReportTiming_details.totime = dr["report_totime"].ToString();
                    Get_ReportTiming_detailsclslist.Add(Get_ReportTiming_details);
                }
                if (Get_ReportTiming_detailsclslist != null)
                {
                    string respnceString = GetJson(Get_ReportTiming_detailsclslist);
                    context.Response.Write(respnceString);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void Vehicle_alrts_save(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                var js = new JavaScriptSerializer();
                string Username = context.Session["field1"].ToString();
                var title1 = context.Request.Params[1];
                vehicle_routes obj2 = js.Deserialize<vehicle_routes>(title1);
                string Userid = context.Session["field3"].ToString();

                if (obj2.btnval == "SAVE")
                {
                    vehicel_alers_fun(context);
                }

                else
                {
                    string[] subsnoval = obj2.subsno.Split(',');
                    string collofsnos = "";
                    foreach (string subval in subsnoval)
                    {
                        collofsnos += "alert_mgr_sinfo_sno =" + subval + " or ";
                    }
                    if (collofsnos.Length > 0)
                        collofsnos = collofsnos.Substring(0, collofsnos.LastIndexOf("or"));

                    if (collofsnos != "")
                    {
                        cmd = new MySqlCommand("delete from alert_mgr_subinfo where alert_mgr_sno=@sno;delete from alert_mgr_loc_info where " + collofsnos + ";");
                    }
                    else
                    {
                        cmd = new MySqlCommand("delete from alert_mgr_subinfo where alert_mgr_sno=@sno;");
                    }
                    cmd.Parameters.Add("@sno", obj2.sno);
                    vdm.Delete(cmd);
                    //vehicel_alers_fun(context);


                    cmd = new MySqlCommand("update alert_manager set alert_name=@alert_name, status=@status, userid=@userid,timegap=@timegap, nooftimes=@nooftimes, email=@email, mobile=@mobile,time1=@time1,time2=@time2  where sno=@sno ");
                    cmd.Parameters.Add("@alert_name", obj2.alertname);
                    cmd.Parameters.Add("@userid", Userid);
                    cmd.Parameters.Add("@status", '1');
                    cmd.Parameters.Add("@timegap", obj2.timegap);
                    cmd.Parameters.Add("@nooftimes", obj2.alrtrept);
                    cmd.Parameters.Add("@email", obj2.mail);
                    cmd.Parameters.Add("@mobile", obj2.mobile);
                    cmd.Parameters.Add("@time1", obj2.time1);
                    cmd.Parameters.Add("@time2", obj2.time2);
                    cmd.Parameters.Add("@sno", obj2.sno);
                    vdm.Update(cmd);

                    long Sno = Convert.ToInt64(obj2.sno);

                    foreach (alerttype1 alrt in obj2.alerttype)
                    {

                        if (alrt.mainpowerchk == "mainpower")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.mainpowerchk);
                            cmd.Parameters.Add("@sub_type", "");

                            vdm.insert(cmd);
                        }
                        if (alrt.ACchk == "AC_ON")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type) values (@alert_mgr_sno, @alert_type, @sub_type)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.ACchk);
                            cmd.Parameters.Add("@sub_type", "");

                            vdm.insert(cmd);
                        }
                        if (alrt.UATchk == "UATchk")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type) values (@alert_mgr_sno, @alert_type, @sub_type)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.UATchk);
                            cmd.Parameters.Add("@sub_type", "");

                            vdm.insert(cmd);
                        }
                        if (alrt.speedchk == "speed")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.speedchk);
                            cmd.Parameters.Add("@sub_type", "");
                            if (obj2.maxspeed != "")
                            {
                                cmd.Parameters.Add("@value", obj2.maxspeed);
                            }
                            else
                            {
                                cmd.Parameters.Add("@value", 0);
                            }
                            vdm.insert(cmd);
                        }
                        if (alrt.inpoichk == "inpoi")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.inpoichk);
                            cmd.Parameters.Add("@sub_type", "");
                            cmd.Parameters.Add("@value", 0);
                            long inpoisno = vdm.insertScalar(cmd);
                            //string inpoilocsstr = obj2.inpoi;
                            //string[] delimiters = new string[] { "<br>" };
                            //string[] inpoilocs = inpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var locs in obj2.inpoi)
                            {
                                if (locs != "" && locs != null)
                                {
                                    cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                    cmd.Parameters.Add("@alert_mgr_sinfo_sno", inpoisno);
                                    cmd.Parameters.Add("@loc_id", locs);
                                    vdm.insert(cmd);
                                }
                            }
                        }
                        if (alrt.outpoichk == "outpoi")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.outpoichk);
                            cmd.Parameters.Add("@sub_type", "");
                            cmd.Parameters.Add("@value", 0);
                            long outpoisno = vdm.insertScalar(cmd);
                            //string inpoilocsstr = obj2.outpoi;
                            //string[] delimiters = new string[] { "<br>" };
                            //string[] outpoilocs = inpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var locs in obj2.outpoi)
                            {
                                if (locs != "" && locs != null)
                                {
                                    cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                    cmd.Parameters.Add("@alert_mgr_sinfo_sno", outpoisno);
                                    cmd.Parameters.Add("@loc_id", locs);
                                    vdm.insert(cmd);
                                }
                            }
                        }
                        if (alrt.stoppagechk == "stoppage")
                        {
                            if (alrt.stpinpoichk == "stopinpoi")
                            {
                                cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                                cmd.Parameters.Add("@alert_mgr_sno", Sno);
                                cmd.Parameters.Add("@alert_type", alrt.stoppagechk);
                                cmd.Parameters.Add("@sub_type", "inpoi");
                                cmd.Parameters.Add("@value", 0);
                                long stopinpoisno = vdm.insertScalar(cmd);


                                foreach (tablelocs1 tbl in obj2.tablelocs)
                                {
                                    //string stpinpoilocsstr = "";
                                    //stpinpoilocsstr = tbl.loc_logs;
                                    //string[] delimiters = new string[] { "<br>" };
                                    //string[] stpinpoilocsstrlocs = stpinpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var locs in tbl.loc_logs)
                                    {
                                        if (locs != "" && locs != null)
                                        {
                                            cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id, value) values (@alert_mgr_sinfo_sno, @loc_id,@value)");
                                            cmd.Parameters.Add("@alert_mgr_sinfo_sno", stopinpoisno);
                                            cmd.Parameters.Add("@loc_id", locs);
                                            cmd.Parameters.Add("@value", tbl.maxtime);
                                            vdm.insert(cmd);
                                        }
                                    }
                                }


                            }
                            if (alrt.stpoutpoichk == "stopoutpoi")
                            {
                                cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                                cmd.Parameters.Add("@alert_mgr_sno", Sno);
                                cmd.Parameters.Add("@alert_type", alrt.stoppagechk);
                                cmd.Parameters.Add("@sub_type", "outpoi");
                                if (obj2.stpmaxstptme != "")
                                {
                                    cmd.Parameters.Add("@value", obj2.stpmaxstptme);
                                }
                                else
                                {
                                    cmd.Parameters.Add("@value", 0);

                                }
                                long stopoutpoisno = vdm.insertScalar(cmd);


                                foreach (var locs in obj2.noalert)
                                {
                                    if (locs != "" && locs != null)
                                    {
                                        cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                        cmd.Parameters.Add("@alert_mgr_sinfo_sno", stopoutpoisno);
                                        cmd.Parameters.Add("@loc_id", locs);
                                        vdm.insert(cmd);
                                    }
                                }


                            }

                        }
                    }
                    string response = GetJson("Successfully Modified");
                    context.Response.Write(response);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        class tablelocs1
        {
            public string maxtime { set; get; }
            // public string loc_logs { set; get; }
            public List<string> loc_logs { get; set; }

        }

        class alerttype1
        {
            public string speedchk { set; get; }
            public string inpoichk { set; get; }
            public string outpoichk { set; get; }
            public string stoppagechk { set; get; }
            public string stpinpoichk { set; get; }
            public string stpoutpoichk { set; get; }
            public string mainpowerchk { set; get; }
            public string UATchk { set; get; }
            public string ACchk { set; get; }
        }
        public void vehicel_alers_fun(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                var js = new JavaScriptSerializer();
                string Username = context.Session["field1"].ToString();
                var title1 = context.Request.Params[1];
                vehicle_routes obj2 = js.Deserialize<vehicle_routes>(title1);
                string Userid = context.Session["field3"].ToString();

                cmd = new MySqlCommand("insert into  alert_manager (alert_name, status, main_user,timegap, nooftimes, email, mobile,time1,time2) values (@alert_name, @status, @userid,@timegap, @nooftimes, @email, @mobile,@time1,@time2)");
                cmd.Parameters.Add("@alert_name", obj2.alertname);
                cmd.Parameters.Add("@userid", Userid);
                cmd.Parameters.Add("@status", '1');
                cmd.Parameters.Add("@timegap", obj2.timegap);
                cmd.Parameters.Add("@nooftimes", obj2.alrtrept);
                cmd.Parameters.Add("@email", obj2.mail);
                cmd.Parameters.Add("@mobile", obj2.mobile);
                cmd.Parameters.Add("@time1", obj2.time1);
                cmd.Parameters.Add("@time2", obj2.time2);
                long Sno = vdm.insertScalar(cmd);
                foreach (alerttype1 alrt in obj2.alerttype)
                {

                    if (alrt.mainpowerchk == "mainpower")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type) values (@alert_mgr_sno, @alert_type, @sub_type)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.mainpowerchk);
                        cmd.Parameters.Add("@sub_type", "");

                        vdm.insert(cmd);
                    }
                    if (alrt.ACchk == "AC_ON")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type) values (@alert_mgr_sno, @alert_type, @sub_type)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.ACchk);
                        cmd.Parameters.Add("@sub_type", "");

                        vdm.insert(cmd);
                    }
                    if (alrt.UATchk == "UATchk")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type) values (@alert_mgr_sno, @alert_type, @sub_type)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.UATchk);
                        cmd.Parameters.Add("@sub_type", "");

                        vdm.insert(cmd);
                    }

                    if (alrt.speedchk == "speed")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.speedchk);
                        cmd.Parameters.Add("@sub_type", "");
                        if (obj2.maxspeed != "")
                        {
                            cmd.Parameters.Add("@value", obj2.maxspeed);
                        }
                        else
                        {
                            cmd.Parameters.Add("@value", 0);
                        }
                        vdm.insert(cmd);
                    }
                    if (alrt.inpoichk == "inpoi")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.inpoichk);
                        cmd.Parameters.Add("@sub_type", "");
                        cmd.Parameters.Add("@value", 0);
                        long inpoisno = vdm.insertScalar(cmd);
                        //string inpoilocsstr = obj2.inpoi;
                        //string[] delimiters = new string[] { "<br>" };
                        //string[] inpoilocs = inpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var locs in obj2.inpoi)
                        {
                            if (locs != "" && locs != null)
                            {
                                cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                cmd.Parameters.Add("@alert_mgr_sinfo_sno", inpoisno);
                                cmd.Parameters.Add("@loc_id", locs);
                                vdm.insert(cmd);
                            }
                        }
                    }
                    if (alrt.outpoichk == "outpoi")
                    {
                        cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                        cmd.Parameters.Add("@alert_mgr_sno", Sno);
                        cmd.Parameters.Add("@alert_type", alrt.outpoichk);
                        cmd.Parameters.Add("@sub_type", "");
                        cmd.Parameters.Add("@value", 0);
                        long outpoisno = vdm.insertScalar(cmd);
                        //string inpoilocsstr = obj2.outpoi;
                        //string[] delimiters = new string[] { "<br>" };
                        //string[] outpoilocs = inpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var locs in obj2.outpoi)
                        {
                            if (locs != "" && locs != null)
                            {
                                cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                cmd.Parameters.Add("@alert_mgr_sinfo_sno", outpoisno);
                                cmd.Parameters.Add("@loc_id", locs);
                                vdm.insert(cmd);
                            }
                        }
                    }
                    if (alrt.stoppagechk == "stoppage")
                    {
                        if (alrt.stpinpoichk == "stopinpoi")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.stoppagechk);
                            cmd.Parameters.Add("@sub_type", "inpoi");
                            cmd.Parameters.Add("@value", 0);
                            long stopinpoisno = vdm.insertScalar(cmd);


                            foreach (tablelocs1 tbl in obj2.tablelocs)
                            {
                                //string stpinpoilocsstr = "";
                                //stpinpoilocsstr = tbl.loc_logs;
                                //string[] delimiters = new string[] { "<br>" };
                                //string[] stpinpoilocsstrlocs = stpinpoilocsstr.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var locs in tbl.loc_logs)
                                {
                                    if (locs != "" && locs != null)
                                    {
                                        cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id, value) values (@alert_mgr_sinfo_sno, @loc_id,@value)");
                                        cmd.Parameters.Add("@alert_mgr_sinfo_sno", stopinpoisno);
                                        cmd.Parameters.Add("@loc_id", locs);
                                        cmd.Parameters.Add("@value", tbl.maxtime);
                                        vdm.insert(cmd);
                                    }
                                }
                            }


                        }
                        if (alrt.stpoutpoichk == "stopoutpoi")
                        {
                            cmd = new MySqlCommand("Insert into alert_mgr_subinfo (alert_mgr_sno, alert_type, sub_type, value) values (@alert_mgr_sno, @alert_type, @sub_type, @value)");
                            cmd.Parameters.Add("@alert_mgr_sno", Sno);
                            cmd.Parameters.Add("@alert_type", alrt.stoppagechk);
                            cmd.Parameters.Add("@sub_type", "outpoi");
                            if (obj2.stpmaxstptme != "")
                            {
                                cmd.Parameters.Add("@value", obj2.stpmaxstptme);
                            }
                            else
                            {
                                cmd.Parameters.Add("@value", 0);

                            }
                            long stopoutpoisno = vdm.insertScalar(cmd);


                            foreach (var locs in obj2.noalert)
                            {
                                if (locs != "" && locs != null)
                                {
                                    cmd = new MySqlCommand("insert into alert_mgr_loc_info (alert_mgr_sinfo_sno, loc_id) values (@alert_mgr_sinfo_sno, @loc_id)");
                                    cmd.Parameters.Add("@alert_mgr_sinfo_sno", stopoutpoisno);
                                    cmd.Parameters.Add("@loc_id", locs);
                                    vdm.insert(cmd);
                                }
                            }


                        }

                    }

                }


                string response = GetJson("Successfully Saved");
                context.Response.Write(response);


            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        class Branches
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string PlantName { get; set; }
            public string PlantSno { get; set; }
        }
        private void get_Routes(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();

                cmd = new MySqlCommand("SELECT        branchdata.Sno, branchdata.BranchID, branchdata.Latitude, branchdata.Longitude, branchdata_1.BranchID AS PlantName, branchdata_1.Sno AS PlantSno FROM            branchdata LEFT OUTER JOIN branchdata branchdata_1 ON branchdata.PlantName = branchdata_1.Sno AND branchdata.UserName = branchdata_1.UserName WHERE        (branchdata.UserName = @UserName)");
                //cmd = new MySqlCommand("SELECT branchdata.Sno, branchdata.BranchID, branchdata.Latitude, branchdata.Longitude, branchdata_1.BranchID AS PlantName, branchdata_1.Sno AS PlantSno FROM branchdata INNER JOIN branchdata branchdata_1 ON branchdata.PlantName = branchdata_1.Sno AND branchdata.UserName = branchdata_1.UserName WHERE (branchdata.UserName = @UserName)");
                // cmd = new MySqlCommand("SELECT branchdata.BranchID, branchdata.Description, branchdata.Latitude, branchdata.Longitude, branchdata.PhoneNumber, branchdata.ImagePath, branchdata.ImageType, branchdata.Radious, branchdata.UserName FROM loginstable INNER JOIN branchdata ON loginstable.main_user = branchdata.UserName WHERE (loginstable.loginid = @UserName)");
                cmd.Parameters.Add("@UserName", Username);
                DataTable Branchdata = vdm.SelectQuery(cmd).Tables[0];
                List<Branches> getBranchList = new List<Branches>();
                foreach (DataRow dr in Branchdata.Rows)
                {
                    Branches getBranches = new Branches();
                    getBranches.id = dr["Sno"].ToString();
                    getBranches.Name = dr["BranchID"].ToString();
                    getBranches.latitude = dr["Latitude"].ToString();
                    getBranches.longitude = dr["Longitude"].ToString();
                    getBranches.PlantName = dr["PlantName"].ToString();
                    getBranches.PlantSno = dr["PlantSno"].ToString();
                    getBranchList.Add(getBranches);
                }
                string respnceString = GetJson(getBranchList);
                context.Response.Write(respnceString);
            }
            catch
            {
            }
        }


        public class retve_alrtnme
        {
            public string alert_name { get; set; }
            public string alert_sno { get; set; }
        }
        private void retrieve_alrtname(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Userid = context.Session["field3"].ToString();
                cmd = new MySqlCommand("SELECT alert_name, sno FROM alert_manager where main_user=@userid");
                cmd.Parameters.Add("@userid", Userid);
                List<retve_alrtnme> alrtnme = new List<retve_alrtnme>();
                DataTable alrttble = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in alrttble.Rows)
                {
                    retve_alrtnme getretve_alrtnme = new retve_alrtnme();
                    getretve_alrtnme.alert_name = dr["alert_name"].ToString();
                    getretve_alrtnme.alert_sno = dr["sno"].ToString();
                    alrtnme.Add(getretve_alrtnme);
                }
                if (alrtnme != null)
                {
                    string response = GetJson(alrtnme);
                    context.Response.Write(response);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }

        }



        public class get_persons
        {
            public string sno { get; set; }
            public string pname { get; set; }
            public string emailid { get; set; }
            public string phonenumber { get; set; }
            public string designation { get; set; }
            public string userid { get; set; }
        }
        private void get_persondetails(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Userid = context.Session["field3"].ToString();
                cmd = new MySqlCommand("SELECT sno, pname, emailid, phonenumber, designation, userid FROM person_details WHERE (main_user = @userid)");
                cmd.Parameters.Add("@userid", Userid);
                List<get_persons> alrtnme = new List<get_persons>();
                DataTable alrttble = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in alrttble.Rows)
                {
                    get_persons getretve_alrtnme = new get_persons();
                    getretve_alrtnme.sno = dr["sno"].ToString();
                    getretve_alrtnme.pname = dr["pname"].ToString();
                    getretve_alrtnme.emailid = dr["emailid"].ToString();
                    getretve_alrtnme.phonenumber = dr["phonenumber"].ToString();
                    getretve_alrtnme.designation = dr["designation"].ToString();
                    getretve_alrtnme.userid = dr["userid"].ToString();
                    alrtnme.Add(getretve_alrtnme);
                }
                if (alrtnme != null)
                {
                    string response = GetJson(alrtnme);
                    context.Response.Write(response);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }
        }

        private void person_details_delete(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string sno = context.Request["sno"].ToString();
                cmd = new MySqlCommand("delete from person_details where sno=@sno");
                cmd.Parameters.Add("@sno", sno);
                vdm.Delete(cmd);
                string response = GetJson("Successfully deleted");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }
        }

        private void Person_details_save(HttpContext context)
        {

            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string name = context.Request["name"].ToString();
                string mobile = context.Request["mobile"].ToString();
                string mail = context.Request["mail"].ToString();
                string designation = context.Request["designation"].ToString();
                string btnval = context.Request["btnval"].ToString();
                string sno = context.Request["sno"].ToString();
                string Userid = context.Session["field3"].ToString();

                if (btnval == "SAVE")
                {
                    cmd = new MySqlCommand("insert into person_details (pname, emailid, phonenumber, designation, main_user) values ( @pname, @emailid, @phonenumber, @designation, @userid)");
                    cmd.Parameters.Add("@pname", name);
                    cmd.Parameters.Add("@emailid", mail);
                    cmd.Parameters.Add("@phonenumber", mobile);
                    cmd.Parameters.Add("@designation", designation);
                    cmd.Parameters.Add("@userid", Userid);
                    vdm.insert(cmd);
                    string response = GetJson("Successfully Inserted");
                    context.Response.Write(response);
                }
                else
                {
                    cmd = new MySqlCommand("update person_details set pname=@pname, emailid=@emailid, phonenumber=@phonenumber, designation=@designation, main_user=@userid where sno=@sno");
                    cmd.Parameters.Add("@pname", name);
                    cmd.Parameters.Add("@emailid", mail);
                    cmd.Parameters.Add("@phonenumber", mobile);
                    cmd.Parameters.Add("@designation", designation);
                    cmd.Parameters.Add("@userid", Userid);
                    cmd.Parameters.Add("@sno", sno);
                    vdm.Update(cmd);
                    string response = GetJson("Successfully Edited");
                    context.Response.Write(response);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }

        }
        private void getvehicles_assign(HttpContext context)
        {
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            try
            {
                List<vehicles> Vehlist = new List<vehicles>();
                string Username = context.Session["field3"].ToString();
                cmd = new MySqlCommand("SELECT paireddata.UserID, paireddata.VehicleNumber, paireddata.Sno FROM paireddata INNER JOIN loginsconfigtable ON paireddata.VehicleNumber = loginsconfigtable.VehicleID WHERE (loginsconfigtable.Refno = @UserName) AND (paireddata.Sno NOT IN (SELECT vehicle_Sno FROM alert_assignment_vehicles alert_assignment_vehicles))");
                cmd.Parameters.Add("@UserName", Username);
                DataTable vehicles = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in vehicles.Rows)
                {
                    vehicles Branch = new vehicles();
                    Branch.VehicleNumber = dr["VehicleNumber"].ToString();
                    Branch.Sno = dr["Sno"].ToString();
                    Vehlist.Add(Branch);
                }
                if (Vehlist != null)
                {
                    string respnceString = GetJson(Vehlist);
                    context.Response.Write(respnceString);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        private void assignvehper_del(HttpContext context)
        {

            string sno = context.Request["sno"].ToString();
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                cmd = new MySqlCommand("delete from alertassignment where sno=@sno;delete from alert_assignment_vehicles where alert_ass_sno=@sno;delete from alert_assignment_persons where alert_ass_sno=@sno;");
                cmd.Parameters.Add("@sno", sno);
                vdm.Delete(cmd);
                string response = GetJson("Assigned Alert Deleted Successfully");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }
        }
        public class ret_assining
        {
            public string alert_name { get; set; }
            public string alertid { get; set; }
            public string sno { get; set; }
            public string alertassignmentName { get; set; }
            //public List<ass_veh> vehicle = new List<ass_veh>();
            //public List<ass_per> person = new List<ass_per>();
            public string vehicle { get; set; }
            public string person { get; set; }
            public string vehicle_sno { get; set; }
            public string person_sno { get; set; }
        }
        public class ass_veh
        {
            public string vehicle_Sno { get; set; }
            public string VehicleNumber { get; set; }
        }
        public class ass_per
        {
            public string persons_sno { get; set; }
            public string pname { get; set; }
        }

        private void get_assignalerts(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Userid = context.Session["field3"].ToString();
                cmd = new MySqlCommand("SELECT alert_manager.alert_name, alert_assignment_vehicles.vehicle_Sno, alert_assignment_persons.persons_sno, paireddata.VehicleNumber, person_details.pname, alertassignment.sno,alertassignment.alertid,alertassignment.alertassignmentName FROM alertassignment INNER JOIN alert_manager ON alertassignment.alertid = alert_manager.sno INNER JOIN alert_assignment_persons ON alertassignment.sno = alert_assignment_persons.alert_ass_sno INNER JOIN alert_assignment_vehicles ON alertassignment.sno = alert_assignment_vehicles.alert_ass_sno INNER JOIN paireddata ON alert_assignment_vehicles.vehicle_Sno = paireddata.Sno INNER JOIN person_details ON alert_assignment_persons.persons_sno = person_details.sno WHERE (alert_manager.main_user = @userid)");
                cmd.Parameters.Add("@userid", Userid);
                // cmd = new MySqlCommand("SELECT paireddata.UserID, paireddata.VehicleNumber, paireddata.Sno FROM paireddata INNER JOIN loginsconfigtable ON paireddata.VehicleNumber = loginsconfigtable.VehicleID WHERE (loginsconfigtable.Refno = @UserName)");
                DataTable asstable = vdm.SelectQuery(cmd).Tables[0];
                DataTable defaults = asstable.DefaultView.ToTable(true, "alert_name", "alertid", "sno", "alertassignmentName");
                DataTable vehicles = asstable.DefaultView.ToTable(true, "vehicle_Sno", "VehicleNumber", "sno");
                DataTable persons = asstable.DefaultView.ToTable(true, "persons_sno", "pname", "sno");
                List<ret_assining> assdata = new List<ret_assining>();
                DataRow[] data;
                DataRow[] data1;

                foreach (DataRow dr in defaults.Rows)
                {
                    string vehsno = "";
                    string vehcls = "";
                    string persno = "";
                    string persond = "";
                    ret_assining getassdata = new ret_assining();
                    getassdata.alert_name = dr["alert_name"].ToString();
                    getassdata.alertid = dr["alertid"].ToString();
                    getassdata.sno = dr["sno"].ToString();
                    getassdata.alertassignmentName = dr["alertassignmentName"].ToString();

                    data = vehicles.Select("sno='" + dr["sno"].ToString() + "'");
                    data1 = persons.Select("sno='" + dr["sno"].ToString() + "'");

                    foreach (DataRow dr1 in data)
                    {
                        vehsno += dr1["vehicle_Sno"].ToString() + ",";
                        vehcls += dr1["VehicleNumber"].ToString() + "->";
                    }
                    foreach (DataRow dr2 in data1)
                    {
                        persno += dr2["persons_sno"].ToString() + ",";
                        persond += dr2["pname"].ToString() + "->";
                    }
                    vehcls = vehcls.Substring(0, vehcls.Length - 2);
                    persond = persond.Substring(0, persond.Length - 2);
                    vehsno = vehsno.Substring(0, vehsno.Length - 1);
                    persno = persno.Substring(0, persno.Length - 1);
                    getassdata.vehicle = vehcls;
                    getassdata.person = persond;
                    getassdata.vehicle_sno = vehsno;
                    getassdata.person_sno = persno;
                    assdata.Add(getassdata);
                }
                string response = GetJson(assdata);
                context.Response.Write(response);
            }

            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }

        }
        public class assign_alrts
        {
            public string assgnnme { get; set; }
            public string alrtgrp { get; set; }
            public List<string> vehicleids { get; set; }
            public List<string> personids { get; set; }
            public string btnval { get; set; }
            public string sno { get; set; }
            public string persno { get; set; }
            public string vehsno { get; set; }
        }
        private void assignalerts_save(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                var js = new JavaScriptSerializer();
                string Username = context.Session["field1"].ToString();
                var title1 = context.Request.Params[1];
                assign_alrts obj2 = js.Deserialize<assign_alrts>(title1);
                string Userid = context.Session["field3"].ToString();
                if (obj2.btnval == "SAVE")
                {
                    cmd = new MySqlCommand("insert into alertassignment (alertid, alertassignmentName) values (@alertid, @alertassignmentName)");
                    cmd.Parameters.Add("@alertid", obj2.alrtgrp);
                    cmd.Parameters.Add("@alertassignmentName", obj2.assgnnme);
                    long asssno = vdm.insertScalar(cmd);
                    foreach (var pers in obj2.personids)
                    {
                        if (pers != "" && pers != null)
                        {
                            cmd = new MySqlCommand("insert into alert_assignment_persons (alert_ass_sno, persons_sno) values (@alert_ass_sno, @persons_sno)");
                            cmd.Parameters.Add("@alert_ass_sno", asssno);
                            cmd.Parameters.Add("@persons_sno", pers);
                            vdm.insert(cmd);
                        }
                    }
                    foreach (var vehi in obj2.vehicleids)
                    {
                        if (vehi != "" && vehi != null)
                        {
                            cmd = new MySqlCommand("insert into alert_assignment_vehicles (alert_ass_sno, vehicle_Sno) values (@alert_ass_sno, @vehicle_Sno)");
                            cmd.Parameters.Add("@alert_ass_sno", asssno);
                            cmd.Parameters.Add("@vehicle_Sno", vehi);
                            vdm.insert(cmd);
                        }
                    }
                    string response = GetJson("Alert Assigned Successfully");
                    context.Response.Write(response);
                }

                else
                {
                    string persno = obj2.persno;
                    string vehsno = obj2.vehsno;
                    string sno = obj2.sno;


                    cmd = new MySqlCommand("delete from alert_assignment_vehicles where alert_ass_sno=@sno;delete from alert_assignment_persons where alert_ass_sno=@sno;");
                    cmd.Parameters.Add("@sno", sno);
                    vdm.Delete(cmd);
                    cmd = new MySqlCommand("update alertassignment set alertid=@alertid, alertassignmentName=@alertassignmentName where sno=@sno");
                    cmd.Parameters.Add("@alertid", obj2.alrtgrp);
                    cmd.Parameters.Add("@alertassignmentName", obj2.assgnnme);
                    cmd.Parameters.Add("@sno", obj2.sno);
                    vdm.Update(cmd);
                    long asssno = Convert.ToInt64(obj2.sno);
                    foreach (var pers in obj2.personids)
                    {
                        if (pers != "" && pers != null)
                        {
                            cmd = new MySqlCommand("insert into alert_assignment_persons (alert_ass_sno, persons_sno) values (@alert_ass_sno, @persons_sno)");
                            cmd.Parameters.Add("@alert_ass_sno", asssno);
                            cmd.Parameters.Add("@persons_sno", pers);
                            vdm.insert(cmd);
                        }
                    }
                    foreach (var vehi in obj2.vehicleids)
                    {
                        if (vehi != "" && vehi != null)
                        {
                            cmd = new MySqlCommand("insert into alert_assignment_vehicles (alert_ass_sno, vehicle_Sno) values (@alert_ass_sno, @vehicle_Sno)");
                            cmd.Parameters.Add("@alert_ass_sno", asssno);
                            cmd.Parameters.Add("@vehicle_Sno", vehi);
                            vdm.insert(cmd);
                        }
                    }
                    string response = GetJson("Alert Assign Edited Successfully");
                    context.Response.Write(response);
                }

            }
            catch (Exception ex)
            {
                string response = GetJson(ex.ToString());
                context.Response.Write(response);
            }
        }

        public class vehicles
        {
            public string VehicleNumber { get; set; }
            public string Sno { get; set; }
        }
        private void getvehicles(HttpContext context)
        {
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            try
            {
                List<vehicles> Vehlist = new List<vehicles>();
                string Username = context.Session["field1"].ToString();
                cmd = new MySqlCommand("SELECT        UserID, VehicleNumber, Sno FROM            paireddata WHERE        (UserID = @userid)");
                cmd.Parameters.Add("@userid", Username);
                DataTable vehicles = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in vehicles.Rows)
                {
                    vehicles Branch = new vehicles();
                    Branch.VehicleNumber = dr["VehicleNumber"].ToString();
                    Branch.Sno = dr["Sno"].ToString();
                    Vehlist.Add(Branch);
                }
                if (Vehlist != null)
                {
                    string respnceString = GetJson(Vehlist);
                    context.Response.Write(respnceString);
                }
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void for_alert_status(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string status = context.Request["status"].ToString();
                string sno = context.Request["sno"].ToString();
                if (status == "Enable")
                {
                    cmd = new MySqlCommand("update alert_manager set status='0' where sno=@sno ");
                    cmd.Parameters.Add("@sno", sno);
                    vdm.Update(cmd);
                }
                else
                {
                    cmd = new MySqlCommand("update alert_manager set status='1' where sno=@sno");
                    cmd.Parameters.Add("@sno", sno);
                    vdm.Update(cmd);
                }
                string response = GetJson("Status Updated Successfully");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }

        private void for_alert_delete(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string subsno = context.Request["subs"].ToString();
                string sno = context.Request["sno"].ToString();
                string[] subsnoval = subsno.Split(',');
                string collofsnos = "";
                foreach (string subval in subsnoval)
                {
                    collofsnos += "alert_mgr_sinfo_sno =" + subval + " or ";
                }
                if (collofsnos.Length > 0)
                    collofsnos = collofsnos.Substring(0, collofsnos.LastIndexOf("or"));

                if (collofsnos != "")
                {
                    cmd = new MySqlCommand("delete from alert_manager where sno=@sno;delete from alert_mgr_subinfo where alert_mgr_sno=@sno;delete from alert_mgr_loc_info where " + collofsnos + ";");
                }
                else
                {
                    cmd = new MySqlCommand("delete from alert_manager where sno=@sno;delete from alert_mgr_subinfo where alert_mgr_sno=@sno;");
                }
                cmd.Parameters.Add("@sno", sno);
                vdm.Delete(cmd);
                string response = GetJson("Successfully Deleted");
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
        public class retrieve_alrtnme
        {
            public string alert_nam { get; set; }
            public string status { get; set; }
            public string timegap { get; set; }
            public string nooftimes { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string time1 { get; set; }
            public string time2 { get; set; }
            //public string alert_type { get; set; }
            // public string sub_type { get; set; }
            //public List<string> sub_type { get; set; }
            //public List<string> outpoi { get; set; }
            public List<alrttype> alert_type = new List<alrttype>();


            public string sno { get; set; }
            public List<sub_sno> subsno = new List<sub_sno>();

        }
        public class alrttype
        {
            public string alert_type { get; set; }
            public string sub_type { get; set; }
            public string value { get; set; }
            public List<locations> loc_id = new List<locations>();
            //public string timegap { get; set; }
        }
        public class locations
        {
            public string loc_id { get; set; }
            public string loc_name { get; set; }
            public string locval { get; set; }
        }
        public class sub_sno
        {
            public string subsno { get; set; }

        }

        private void rettrieve_alert_data(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                List<retrieve_alrtnme> getalerts = new List<retrieve_alrtnme>();
                string alertname = context.Request["alertname"].ToString();
                string alertsno = context.Request["alertsno"].ToString();
                cmd = new MySqlCommand("SELECT alert_manager.alert_name, alert_manager.sno, alert_manager.status, alert_manager.timegap, alert_manager.nooftimes, alert_manager.email, alert_manager.mobile,   alert_manager.time1, alert_manager.time2,alert_mgr_subinfo.alert_type, alert_mgr_subinfo.sub_type, alert_mgr_subinfo.value, alert_mgr_loc_info.loc_id, alert_mgr_loc_info.value AS locval, branchdata.BranchID, alert_mgr_subinfo.sno AS subsno FROM branchdata INNER JOIN alert_mgr_loc_info ON branchdata.Sno = alert_mgr_loc_info.loc_id RIGHT OUTER JOIN alert_manager INNER JOIN alert_mgr_subinfo ON alert_manager.sno = alert_mgr_subinfo.alert_mgr_sno ON alert_mgr_loc_info.alert_mgr_sinfo_sno = alert_mgr_subinfo.sno WHERE (alert_manager.sno = @alertsno)");
                cmd.Parameters.Add("@alertsno", alertsno);
                DataTable griddata = vdm.SelectQuery(cmd).Tables[0];
                DataTable alert_val = griddata.DefaultView.ToTable(true, "alert_name", "status", "timegap", "nooftimes", "email", "mobile", "sno", "time1", "time2");
                DataTable alert_type = griddata.DefaultView.ToTable(true, "alert_type", "sub_type", "value");
                DataTable subsno = griddata.DefaultView.ToTable(true, "subsno");
                retrieve_alrtnme retalrt = new retrieve_alrtnme();
                foreach (DataRow dr in alert_val.Rows)
                {
                    retalrt.alert_nam = dr["alert_name"].ToString();
                    retalrt.status = dr["status"].ToString();
                    retalrt.timegap = dr["timegap"].ToString();
                    retalrt.nooftimes = dr["nooftimes"].ToString();
                    retalrt.email = dr["email"].ToString();
                    retalrt.mobile = dr["mobile"].ToString();
                    retalrt.time1 = dr["time1"].ToString();
                    retalrt.time2 = dr["time2"].ToString();
                    retalrt.sno = dr["sno"].ToString();
                    getalerts.Add(retalrt);
                }

                foreach (DataRow dr in alert_type.Rows)
                {
                    alrttype alrttpe = new alrttype();
                    alrttpe.alert_type = dr["alert_type"].ToString();
                    alrttpe.sub_type = dr["sub_type"].ToString();
                    alrttpe.value = dr["value"].ToString();
                    DataRow[] data;
                    if (dr["sub_type"].ToString() != "" && dr["alert_type"].ToString() == "stoppage")
                    {
                        data = griddata.Select("sub_type='" + dr["sub_type"].ToString() + "'");
                    }
                    else
                    {
                        data = griddata.Select("alert_type='" + dr["alert_type"].ToString() + "'");
                    }
                    foreach (DataRow dr1 in data)
                    {
                        if (dr1["loc_id"].ToString() != "")
                        {
                            locations loc = new locations();
                            loc.loc_id = dr1["loc_id"].ToString();
                            loc.loc_name = dr1["BranchID"].ToString();
                            loc.locval = dr1["locval"].ToString();
                            alrttpe.loc_id.Add(loc);
                        }
                    }
                    retalrt.alert_type.Add(alrttpe);
                }
                foreach (DataRow dr3 in subsno.Rows)
                {
                    sub_sno alrttpe2 = new sub_sno();
                    alrttpe2.subsno = dr3["subsno"].ToString();
                    retalrt.subsno.Add(alrttpe2);
                }

                string response = GetJson(getalerts);
                context.Response.Write(response);

            }
            catch (Exception ex)
            {
                string response = GetJson("Error");
                context.Response.Write(response);
            }
        }
      

        public class NearestVehicle
        {
            public string Vehicleno { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string Distance { get; set; }
            public string ExpectedTime { get; set; }
        }

        public class GeneralReportCLS
        {
            public string VehicleID { get; set; }
            public string TotalDistanceTravelled { get; set; }
            public string WorkingHours { get; set; }
            public string MotionHours { get; set; }
            public string StationaryHours { get; set; }
            public string IdleTime { get; set; }
            public string MaxSpeed { get; set; }
            public string AvgSpeed { get; set; }
            public string ACONTime { get; set; }
            public string NoOfStops { get; set; }
            public string Remarks { get; set; }
            public string MainPowerOFFTime { get; set; }
        }

        public class StoppageReportCLS
        {
            public string VehicleID { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string DateTime { get; set; }
            public string StoppedHours { get; set; }
            public string Speed { get; set; }
        }
        public class DailyReportCLS
        {
            public string VehicleID { get; set; }
            public string StartDate { get; set; }
            public string StartTime { get; set; }
            public string StopDate { get; set; }
            public string StopTime { get; set; }
            public string TotalDistanceTravelled { get; set; }
            public string MotionHours { get; set; }
            public string StationaryHours { get; set; }
            public string MaxSpeed { get; set; }
            public string AvgSpeed { get; set; }
            public string IdleTime { get; set; }
        }
        public class LocationHaltingHoursReportCLS
        {
            public string VehicleID { get; set; }
            public string LocationName { get; set; }
            public string VehicleEnteredDate { get; set; }
            public string VehicleEnteredTime { get; set; }
            public string VehicleLeftDate { get; set; }
            public string VehicleLeftTime { get; set; }
            public string StoppedHours { get; set; }
        }
        public class LocationtoLocationReportCLS
        {
            public string VehicleID { get; set; }
            public string FromLocation { get; set; }
            public string StartingDate { get; set; }
            public string StartingTime { get; set; }
            public string StopTime { get; set; }
            public string ToLocation { get; set; }
            public string ReachingDate { get; set; }
            public string ReachingTime { get; set; }
            public string Distance { get; set; }
            public string JourneyHours { get; set; }
        }
        private void getvehiclesdatareport(HttpContext context)
        {
            try
            {
                double Maxspeed = 0;
                double totalSpeed = 0;
                string UserName = "";
                int zoomlevel = 14;
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                DataTable table = new DataTable();
                string requestfrom = context.Request["requestfrom"];
                string startdate = context.Request["startdate"];
                string enddate = context.Request["enddate"];
                DateTime fromdate = DateTime.Now;
                DateTime todate = DateTime.Now;
                fromdate = DateTime.Parse(startdate);
                if (startdate != "" && startdate != null)
                {
                    if (requestfrom == "App")
                        fromdate = DateTime.Parse(startdate);
                        //fromdate = DateTime.ParseExact(startdate, "MM-dd-yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    else
                        fromdate = DateTime.ParseExact(startdate, "yyyy-MM-dd'T'HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (enddate != "" && enddate != null)
                {
                    if (requestfrom == "App")
                        todate = DateTime.Parse(enddate);

                        //todate = DateTime.ParseExact(enddate, "MM-dd-yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    else
                        todate = DateTime.ParseExact(enddate, "yyyy-MM-dd'T'HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                string checkedvehicles = context.Request["checkedvehicles"];
                Array checkedvhcles = checkedvehicles.Split(',');
                string reportname = context.Request["reporttype"];
                string txtvalue = context.Request["txtvalue"];
                string Username = context.Session["field1"].ToString();

                try
                {
                    DataDownloader ddwnldr;
                    DataTable sampletable = new DataTable();
                    Dictionary<string, DataTable> reportData = new Dictionary<string, DataTable>();
                    #region code
                    List<string> logstbls = new List<string>();
                    logstbls.Add("GpsTrackVehicleLogs");
                    logstbls.Add("GpsTrackVehicleLogs1");
                    logstbls.Add("GpsTrackVehicleLogs2");
                    logstbls.Add("GpsTrackVehicleLogs3");
                    int dcnrycnt = 1;
                    if (reportname == "General Report")
                    {
                        #region GeneralReports
                        DataTable rpttable = new DataTable();
                        DataColumn col = new DataColumn("VehicleID");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("TotalDistanceTravelled(Kms)");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("WorkingHours");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("MotionHours");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("StationaryHours");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("IdleTime");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("MaxSpeed");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("AvgSpeed");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("A/C ON Time");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("No Of Stops");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("MainPower OFFTime");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("Remarks");
                        rpttable.Columns.Add(col);
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            #region codefor selected vehicles
                            Maxspeed = 0;
                            double SpeedLimit = 0.0;
                            double MaxIdleLimit = 0.0;
                            double MaxStopLimit = 0.0;

                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            foreach (string tbname in logstbls)
                            {
                                cmd = new MySqlCommand("select * from " + tbname + " where DateTime>= @starttime and DateTime<=@endtime and VehicleID='" + vehiclestr + "' and UserID='" + Username + "' order by DateTime");
                                cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                                cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                                logs = vdm.SelectQuery(cmd).Tables[0];
                                if (tottable.Rows.Count == 0)
                                {
                                    tottable = logs.Clone();
                                }
                                foreach (DataRow dr in logs.Rows)
                                {
                                    tottable.ImportRow(dr);
                                }
                            }
                            DataView dv = tottable.DefaultView;
                            dv.Sort = "DateTime ASC";
                            table = dv.ToTable();
                            reportData.Add(vehiclestr, table);

                            double lat = 0.0;
                            double longi = 0.0;
                            double prvlat = 0.0;
                            double prevLongi = 0.0;
                            double TotalDistance = 0.0;
                            double IdleTime = 0.0;
                            double TotalTimeSpent = 0.0;
                            double StopTime = 0.0;
                            double totalStops = 0.0;
                            double RunningTime = 0.0;
                            double TotalACTime = 0.0;
                            double TotalEPOFFTime = 0.0;

                            bool onceMet = false;
                            bool IdleStarted = false;
                            bool runningStarted = false;
                            bool StopStarted = false;
                            bool SpentStarttime = false;
                            bool IsDisplayed = false;
                            bool ACStatred = false;
                            bool EPOff = false;

                            DateTime PrvIdletime = DateTime.Now;
                            DateTime presIdletime = DateTime.Now;
                            DateTime PrvRunningtime = DateTime.Now;
                            DateTime PresRunningtime = DateTime.Now;
                            DateTime PrvStoptime = DateTime.Now;
                            DateTime PresStoptime = DateTime.Now;
                            DateTime PresSpenttime = DateTime.Now;
                            DateTime PrvSpenttime = DateTime.Now;
                            DateTime PresACOnTime = DateTime.Now;
                            DateTime PrvACOnTime = DateTime.Now;
                            DateTime PresEPOffTime = DateTime.Now;
                            DateTime PrvEPOffTime = DateTime.Now;
                            DateTime PrevGenTime = DateTime.Now;
                            string vehicleEnteredDate = "";
                            string vehicleLeftDate = "";
                            string Remarks = "No";
                            string PrvBranch = "";
                            string PresBranchName = "";


                            DataRow firstrow = null;
                            DataRow lastrow = null;
                            if (table.Rows.Count > 1)
                            {
                                firstrow = table.Rows[0];
                                lastrow = table.Rows[table.Rows.Count - 1];

                                DateTime inputfirstdate = (DateTime)table.Rows[0]["DateTime"];
                                TimeSpan tsfirst = new TimeSpan(inputfirstdate.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                TimeSpan tsfirst1 = new TimeSpan(fromdate.Ticks);
                                StopTime += Math.Abs(tsfirst.Subtract(tsfirst1).TotalSeconds);
                            }

                            foreach (DataRow dr1 in table.Rows)
                            {
                                int AC = 0;
                                string EP="ON";
                                int.TryParse(dr1["inp3"].ToString(), out AC);
                                EP = dr1["EP"].ToString();
                                
                                if (lat == 0.0 && longi == 0.0)
                                {
                                    lat = (double)dr1["Latitiude"];
                                    longi = (double)dr1["Longitude"];
                                    prvlat = lat;
                                    prevLongi = longi;
                                    TotalDistance = 0.0;
                                    PrevGenTime = (DateTime)dr1["DateTime"];

                                    if (AC == 1)
                                    {
                                        PrvACOnTime = (DateTime)dr1["DateTime"];
                                        PresACOnTime = (DateTime)dr1["DateTime"];
                                        ACStatred = true;
                                    }
                                    else
                                    {
                                        ACStatred = false;
                                    }
                                    if (EP == "OFF")
                                    {
                                        PrvEPOffTime = (DateTime)dr1["DateTime"];
                                        PresEPOffTime = (DateTime)dr1["DateTime"];
                                        EPOff = true;
                                    }
                                    else
                                    {
                                        EPOff = false;
                                    }
                                }
                                else
                                {

                                    #region Calculations
                                    lat = (double)dr1["Latitiude"];
                                    longi = (double)dr1["Longitude"];
                                    TotalDistance += GeoCodeCalc.CalcDistance(lat, longi, prvlat, prevLongi);
                                    prvlat = lat;
                                    prevLongi = longi;

                                    double speed = (double)dr1["Speed"];
                                    int Ignition = 0;
                                    int.TryParse(dr1["inp2"].ToString(), out Ignition);
                                    if (speed == 0 && Ignition != 0)
                                    {
                                        runningStarted = false;
                                        if (IdleStarted)
                                        {
                                            presIdletime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(presIdletime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                            IdleTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            PrvIdletime = presIdletime;
                                            PrevGenTime = presIdletime;
                                            if (IdleTime > MaxIdleLimit)
                                                Remarks = "YES";
                                        }
                                        else
                                        {
                                            IdleStarted = true;
                                            PrvIdletime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(PrevGenTime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                            IdleTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            PrevGenTime = PrvIdletime;
                                        }
                                        //if (StopStarted)
                                        //{
                                        //    PresStoptime = (DateTime)dr1["DateTime"];
                                        //    TimeSpan t = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                        //    TimeSpan t1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                        //    StopTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                        //    PrvStoptime = PresStoptime;
                                        //    PrevGenTime = PresStoptime;
                                        //}
                                        //else
                                        //{
                                        //    StopStarted = true;
                                        //    PrvStoptime = (DateTime)dr1["DateTime"];
                                        //    PrevGenTime = PrvStoptime;
                                        //    totalStops += 1;
                                        //}
                                        if (runningStarted)
                                        {
                                            PresRunningtime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                            RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            if (speed > Maxspeed)
                                                Maxspeed = speed;
                                            totalSpeed += speed;
                                            PrvRunningtime = PresRunningtime;
                                            PrevGenTime = PresRunningtime;
                                            runningStarted = false;
                                        }
                                    }
                                    else if (speed == 0)
                                    {
                                        IdleStarted = false;
                                        runningStarted = false;
                                        if (StopStarted)
                                        {
                                            PresStoptime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                            StopTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            PrvStoptime = PresStoptime;
                                            PrevGenTime = PresStoptime;
                                        }
                                        else
                                        {
                                            StopStarted = true;
                                            PrvStoptime = (DateTime)dr1["DateTime"];
                                            PrevGenTime = PrvStoptime;
                                            totalStops += 1;
                                        }
                                        if (runningStarted)
                                        {
                                            PresRunningtime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                            RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            if (speed > Maxspeed)
                                                Maxspeed = speed;
                                            totalSpeed += speed;
                                            PrvRunningtime = PresRunningtime;
                                            PrevGenTime = PresRunningtime;
                                            runningStarted = false;
                                        }
                                    }
                                    else if (speed > 0)
                                    {
                                        if (runningStarted)
                                        {
                                            PresRunningtime = (DateTime)dr1["DateTime"];
                                            TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                            TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                            if (StopStarted)
                                            {
                                                PresStoptime = (DateTime)dr1["DateTime"];
                                                TimeSpan stpt = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                TimeSpan stpt1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                StopTime += Math.Abs(stpt.Subtract(stpt1).TotalSeconds);
                                                StopStarted = false;
                                            }
                                            if (IdleStarted)
                                            {
                                                presIdletime = (DateTime)dr1["DateTime"];
                                                TimeSpan idlet = new TimeSpan(presIdletime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                                TimeSpan idlet1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                                IdleTime += Math.Abs(idlet.Subtract(idlet1).TotalSeconds);
                                                IdleStarted = false;
                                            }
                                            RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                            if (speed > Maxspeed)
                                                Maxspeed = speed;

                                            totalSpeed += speed;
                                            PrvRunningtime = PresRunningtime;
                                            PrevGenTime = PresRunningtime;
                                        }
                                        else
                                        {
                                            runningStarted = true;
                                            PrvRunningtime = (DateTime)dr1["DateTime"];
                                            if (StopStarted)
                                            {
                                                PresStoptime = (DateTime)dr1["DateTime"];
                                                TimeSpan stpt = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                TimeSpan stpt1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                StopTime += Math.Abs(stpt.Subtract(stpt1).TotalSeconds);
                                                StopStarted = false;
                                            }
                                            if (IdleStarted)
                                            {
                                                presIdletime = (DateTime)dr1["DateTime"];
                                                TimeSpan idlet = new TimeSpan(presIdletime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                                TimeSpan idlet1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                                IdleTime += Math.Abs(idlet.Subtract(idlet1).TotalSeconds);
                                                IdleStarted = false;
                                            }
                                            totalSpeed += speed;
                                            if (speed > Maxspeed)
                                                Maxspeed = speed;
                                            PrevGenTime = PrvRunningtime;
                                        }
                                    }
                                    
                                    #endregion


                                    if (AC == 1)
                                    {
                                        if (ACStatred == false)
                                            PrvACOnTime = (DateTime)dr1["DateTime"];
                                        PresACOnTime = (DateTime)dr1["DateTime"];
                                        ACStatred = true;
                                    }
                                    else
                                    {
                                        ACStatred = false;
                                    }

                                    if (ACStatred)
                                    {
                                        PresACOnTime = (DateTime)dr1["DateTime"];
                                        TimeSpan t = new TimeSpan(PresACOnTime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                        TimeSpan t1 = new TimeSpan(PrvACOnTime.Ticks);
                                        TotalACTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                        PrvACOnTime = PresACOnTime;
                                    }
                                    
                                    
                                    if (EP == "OFF")
                                    {
                                        if (EPOff == false)
                                            PrvEPOffTime = (DateTime)dr1["DateTime"];
                                        PresEPOffTime = (DateTime)dr1["DateTime"];
                                        EPOff = true;
                                    }
                                    else
                                    {
                                        EPOff = false;
                                    }

                                    if (EPOff)
                                    {
                                        PresEPOffTime = (DateTime)dr1["DateTime"];
                                        TimeSpan t = new TimeSpan(PresEPOffTime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                        TimeSpan t1 = new TimeSpan(PrvEPOffTime.Ticks);
                                        TotalEPOFFTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                        PrvEPOffTime = PresEPOffTime;
                                    }
                                }
                            }


                            if (firstrow != null && lastrow != null)
                            {
                                double firstval = 0;
                                double.TryParse(firstrow["Odometer"].ToString(), out firstval);
                                double lastval = 0;
                                double.TryParse(lastrow["Odometer"].ToString(), out lastval);
                                if (lastval > 0 && firstval > 0)
                                    TotalDistance = lastval - firstval;
                            }


                            double workinghours = RunningTime + IdleTime;
                            double Stopedtime = StopTime + IdleTime;
                            var workinghoursSpan = TimeSpan.FromSeconds(workinghours);
                            var motionhoursSpan = TimeSpan.FromSeconds(RunningTime);
                            var idletimeSpan = TimeSpan.FromSeconds(IdleTime);
                            var stoppedtimeSpan = TimeSpan.FromSeconds(Stopedtime);
                            var TotalACTimeSpan = TimeSpan.FromSeconds(TotalACTime);
                            var TotalEPOFFTimeSpan = TimeSpan.FromSeconds(TotalEPOFFTime);
                            
                            double avgspeeddiv = (RunningTime / 3600);
                            double avgspeed = 0;
                            if (avgspeeddiv > 0)
                                avgspeed = TotalDistance / avgspeeddiv;
                            DataRow tablerow = rpttable.NewRow();
                            tablerow["VehicleID"] = vehiclestr;
                            tablerow["TotalDistanceTravelled(Kms)"] = (Math.Abs(Math.Round(TotalDistance, 3))).ToString();
                            if (workinghoursSpan.Days > 0)
                                tablerow["WorkingHours"] = workinghoursSpan.Days + "Days " + workinghoursSpan.Hours + "H " + workinghoursSpan.Minutes + "Min";
                            else
                                tablerow["WorkingHours"] = workinghoursSpan.Hours + "H " + workinghoursSpan.Minutes + "Min";
                            if (motionhoursSpan.Days > 0)
                                tablerow["MotionHours"] = motionhoursSpan.Days + "Days " + motionhoursSpan.Hours + "H " + motionhoursSpan.Minutes + "Min";
                            else
                                tablerow["MotionHours"] = motionhoursSpan.Hours + "H " + motionhoursSpan.Minutes + "Min";
                            if (stoppedtimeSpan.Days > 0)
                                tablerow["StationaryHours"] = stoppedtimeSpan.Days + "Days " + stoppedtimeSpan.Hours + "H " + stoppedtimeSpan.Minutes + "Min";
                            else
                                tablerow["StationaryHours"] = stoppedtimeSpan.Hours + "H " + stoppedtimeSpan.Minutes + "Min";
                            if (idletimeSpan.Days > 0)
                                tablerow["IdleTime"] = idletimeSpan.Days + "Days " + idletimeSpan.Hours + "H " + idletimeSpan.Minutes + "Min";
                            else
                                tablerow["IdleTime"] = idletimeSpan.Hours + "H " + idletimeSpan.Minutes + "Min";
                            tablerow["MaxSpeed"] = (int)Maxspeed + "KMPH";
                            tablerow["AvgSpeed"] = String.Format("{0:0.00}", avgspeed) + "KMPH";
                            tablerow["No Of Stops"] = totalStops.ToString();
                            if (TotalACTimeSpan.Days > 0)
                                tablerow["A/C ON Time"] = TotalACTimeSpan.Days + "Days " + TotalACTimeSpan.Hours + "H " + TotalACTimeSpan.Minutes + "Min";
                            else
                                tablerow["A/C ON Time"] = TotalACTimeSpan.Hours + "H " + TotalACTimeSpan.Minutes + "Min";

                            if (TotalEPOFFTimeSpan.Days > 0)
                                tablerow["MainPower OFFTime"] = TotalEPOFFTimeSpan.Days + "Days " + TotalEPOFFTimeSpan.Hours + "H " + TotalEPOFFTimeSpan.Minutes + "Min";
                            else
                                tablerow["MainPower OFFTime"] = TotalEPOFFTimeSpan.Hours + "H " + TotalEPOFFTimeSpan.Minutes + "Min";
                            rpttable.Rows.Add(tablerow);

                            #endregion
                        }
                        HttpContext.Current.Session["xportdata"] = rpttable;
                        HttpContext.Current.Session["reportdata"] = reportData;
                        List<GeneralReportCLS> GeneralReportCLSlst = new List<GeneralReportCLS>();
                        foreach (DataRow dr in rpttable.Rows)
                        {
                            GeneralReportCLS GeneralReportcls = new GeneralReportCLS();
                            GeneralReportcls.VehicleID = dr["VehicleID"].ToString();
                            GeneralReportcls.TotalDistanceTravelled = dr["TotalDistanceTravelled(Kms)"].ToString();
                            GeneralReportcls.WorkingHours = dr["WorkingHours"].ToString();
                            GeneralReportcls.MotionHours = dr["MotionHours"].ToString();
                            GeneralReportcls.StationaryHours = dr["StationaryHours"].ToString();
                            GeneralReportcls.MaxSpeed = dr["MaxSpeed"].ToString();
                            GeneralReportcls.AvgSpeed = dr["AvgSpeed"].ToString();
                            GeneralReportcls.IdleTime = dr["IdleTime"].ToString();
                            GeneralReportcls.NoOfStops = dr["No Of Stops"].ToString();
                            GeneralReportcls.ACONTime = dr["A/C ON Time"].ToString();
                            GeneralReportcls.MainPowerOFFTime = dr["MainPower OFFTime"].ToString();
                            GeneralReportCLSlst.Add(GeneralReportcls);
                        }
                        string respnceString = GetJson(GeneralReportCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }
                    else if (reportname == "Stopage Report")
                    {
                        #region Stoppage Report
                        string vehicls = "";
                        DataTable Stoppagereport = new DataTable();
                        DataTable dtble = new DataTable();
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            foreach (string tbname in logstbls)
                            {
                                cmd = new MySqlCommand("select * from " + tbname + " where DateTime>= @starttime and DateTime<=@endtime and VehicleID='" + vehiclestr + "' and UserID='" + Username + "' order by DateTime");
                                cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                                cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                                logs = vdm.SelectQuery(cmd).Tables[0];
                                if (tottable.Rows.Count == 0)
                                {
                                    tottable = logs.Clone();
                                }
                                foreach (DataRow dr in logs.Rows)
                                {
                                    tottable.ImportRow(dr);
                                }
                            }
                            DataView dv = tottable.DefaultView;
                            dv.Sort = "DateTime ASC";
                            dtble = dv.ToTable();
                            Stoppagereport = new DataTable();
                            Stoppagereport.Columns.Add("SNo");
                            Stoppagereport.Columns.Add("DateTime");
                            Stoppagereport.Columns.Add("Latitude");
                            Stoppagereport.Columns.Add("Longitude");
                            Stoppagereport.Columns.Add("Stopped Hours");
                            Stoppagereport.Columns.Add("VehicleID");

                            DateTime pdt = new DateTime();
                            bool first = true;
                            bool getstoppedtime = false;
                            int minutes = 0;
                            int.TryParse(txtvalue, out minutes);
                            int a = 1;
                            for (int i = 0; i < dtble.Rows.Count; i++)
                            {
                                DataRow row = dtble.Rows[i];
                                DateTime latdat = new DateTime();
                                if (row["Speed"].ToString() == "0")
                                {
                                    if (getstoppedtime == false)
                                    {
                                        latdat = (DateTime)row["DateTime"]; //new DateTime(int.Parse(datevalues[2]), int.Parse(datevalues[1]), int.Parse(datevalues[0]), int.Parse(timevalues[0]), int.Parse(timevalues[1]), int.Parse(timevalues[2]));
                                        pdt = latdat;
                                        getstoppedtime = true;
                                    }
                                }
                                else if (getstoppedtime == true)
                                {
                                    latdat = (DateTime)row["DateTime"]; //new DateTime(int.Parse(datevalues[2]), int.Parse(datevalues[1]), int.Parse(datevalues[0]), int.Parse(timevalues[0]), int.Parse(timevalues[1]), int.Parse(timevalues[2]));
                                    TimeSpan ts1 = new TimeSpan(latdat.Ticks);
                                    TimeSpan ts2 = new TimeSpan(pdt.Ticks);
                                    TimeSpan ts3 = ts1.Subtract(ts2);

                                    if (ts3.TotalMinutes >= minutes)
                                    {
                                        DataRow newrow = Stoppagereport.NewRow();
                                        newrow["SNo"] = a.ToString();
                                        newrow["DateTime"] = latdat;
                                        GooglePoint GP1 = new GooglePoint();
                                        GP1.Latitude = (double)row["Latitiude"];
                                        GP1.Longitude = (double)row["Longitude"];
                                        GooglePoint g1 = new GooglePoint();
                                        g1.Latitude = (double)row["Latitiude"];
                                        g1.Longitude = (double)row["Longitude"];
                                        g1.Address = g1.Latitude + "," + g1.Longitude;
                                        newrow["Latitude"] = row["Latitiude"];
                                        newrow["Longitude"] = row["Longitude"];
                                        newrow["VehicleID"] = row["VehicleID"];
                                        newrow["Stopped Hours"] = (int)(ts3.TotalHours % 24) + "Hours " + (int)(ts3.TotalMinutes % 60) + "Min ";
                                        Stoppagereport.Rows.Add(newrow);
                                        a++;
                                    }
                                    getstoppedtime = false;
                                }
                            }
                        }
                       
                        HttpContext.Current.Session["reportdata"] = Stoppagereport;
                        List<StoppageReportCLS> StoppageReportCLSlst = new List<StoppageReportCLS>();
                        foreach (DataRow dr in Stoppagereport.Rows)
                        {
                            StoppageReportCLS StoppageReportCLS = new StoppageReportCLS();
                            StoppageReportCLS.VehicleID = dr["VehicleID"].ToString();
                            StoppageReportCLS.Latitude = dr["Latitude"].ToString();
                            StoppageReportCLS.Longitude = dr["Longitude"].ToString();
                            StoppageReportCLS.DateTime = dr["DateTime"].ToString();
                            StoppageReportCLS.StoppedHours = dr["Stopped Hours"].ToString();
                            StoppageReportCLSlst.Add(StoppageReportCLS);
                        }
                        string respnceString = GetJson(StoppageReportCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }
                    else if (reportname == "OverSpeed Report")
                    {
                        #region Overspeed Report

                        float spd = 0;
                        int cot = 1;
                        float.TryParse(txtvalue, out spd);
                        if (spd < 1)
                        {
                            string respnceString = GetJson("Speed Limit must be Specified");
                            context.Response.Write(respnceString);
                            return;
                        }
                        else
                        {
                            DataTable SpeedReport = new DataTable();
                            foreach (string vehiclestr in checkedvhcles)
                            {
                                DataTable logs = new DataTable();
                                DataTable tottable = new DataTable();
                                foreach (string tbname in logstbls)
                                {
                                    cmd = new MySqlCommand("select '' as SNo,VehicleID,DateTime,Speed,Latitiude ,Longitude,Direction,Diesel from " + tbname + " where DateTime>= @starttime and DateTime<=@endtime and speed>@Speed and VehicleID in ('" + vehiclestr + "') and UserID='" + Username + "' order by DateTime");
                                    cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                                    cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                                    cmd.Parameters.Add(new MySqlParameter("@Speed", spd));
                                    logs = vdm.SelectQuery(cmd).Tables[0];
                                    if (tottable.Rows.Count == 0)
                                    {
                                        tottable = logs.Clone();
                                    }
                                    foreach (DataRow dr in logs.Rows)
                                    {
                                        tottable.ImportRow(dr);
                                    }
                                }
                                DataView dv = tottable.DefaultView;
                                dv.Sort = "DateTime ASC";
                                SpeedReport = dv.ToTable();

                                for (int i = 0; i < SpeedReport.Rows.Count; i++)
                                {
                                    SpeedReport.Rows[i]["SNo"] = (cot);
                                    cot++;
                                }
                            }
                            HttpContext.Current.Session["xportdata"] = SpeedReport;
                            HttpContext.Current.Session["reportdata"] = SpeedReport;
                            List<StoppageReportCLS> StoppageReportCLSlst = new List<StoppageReportCLS>();
                            foreach (DataRow dr in SpeedReport.Rows)
                            {
                                StoppageReportCLS StoppageReportCLS = new StoppageReportCLS();
                                StoppageReportCLS.VehicleID = dr["VehicleID"].ToString();
                                StoppageReportCLS.Latitude = dr["Latitiude"].ToString();
                                StoppageReportCLS.Longitude = dr["Longitude"].ToString();
                                StoppageReportCLS.DateTime = dr["DateTime"].ToString();
                                StoppageReportCLS.Speed = dr["Speed"].ToString();
                                StoppageReportCLSlst.Add(StoppageReportCLS);
                            }
                            string respnceString = GetJson(StoppageReportCLSlst);
                            context.Response.Write(respnceString);
                        }
                        #endregion
                    }
                    else if (reportname == "Daily Report")
                    {
                        #region "Daily Report"
                        string Duration = "";
                        string StDuration = "";
                        DateTime Stopdt = DateTime.Now;
                        DateTime Startdt = DateTime.Now;
                        reportData = new Dictionary<string, DataTable>();
                        DataTable dailydatatable = new DataTable();
                        dailydatatable.Columns.Add("VehicleID");
                        dailydatatable.Columns.Add("StartDate");
                        dailydatatable.Columns.Add("StartTime");
                        dailydatatable.Columns.Add("StopDate");
                        dailydatatable.Columns.Add("StopTime");
                        dailydatatable.Columns.Add("TotalDistanceTravelled(Kms)");
                        dailydatatable.Columns.Add("MotionHours");
                        dailydatatable.Columns.Add("StationaryHours");
                        dailydatatable.Columns.Add("MaxSpeed");
                        dailydatatable.Columns.Add("AvgSpeed");
                        dailydatatable.Columns.Add("IdleTime");
                        dailydatatable.Columns.Add("ACONTime");

                        #region multyvehicles
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            foreach (string tbname in logstbls)
                            {
                                cmd = new MySqlCommand("select * from " + tbname + " where DateTime>= @starttime and DateTime<=@endtime and VehicleID='" + vehiclestr + "' and UserID='" + Username + "' order by DateTime");
                                cmd.Parameters.Add(new MySqlParameter("@starttime", GetLowDate(fromdate)));
                                cmd.Parameters.Add(new MySqlParameter("@endtime", GetLowDate(todate)));
                                logs = vdm.SelectQuery(cmd).Tables[0];
                                if (tottable.Rows.Count == 0)
                                {
                                    tottable = logs.Clone();
                                }
                                foreach (DataRow dr in logs.Rows)
                                {
                                    tottable.ImportRow(dr);
                                }
                            }
                            DataView dv = tottable.DefaultView;
                            dv.Sort = "DateTime ASC";
                            table = dv.ToTable();
                            for (DateTime date = fromdate; GetHighDate(todate).CompareTo(GetLowDate(date)) > 0; date = date.AddDays(1.0))
                            {
                                double SpeedLimit = 0.0;
                                double MaxIdleLimit = 0.0;
                                double MaxStopLimit = 0.0;
                                Maxspeed = 0;
                                double lat = 0.0;
                                double longi = 0.0;
                                double prvlat = 0.0;
                                double prevLongi = 0.0;
                                double TotalDistance = 0.0;
                                double IdleTime = 0.0;
                                double TotalTimeSpent = 0.0;
                                double totalStops = 0.0;
                                double StopTime = 0.0;
                                double RunningTime = 0.0;
                                bool onceMet = false;
                                bool IdleStarted = false;
                                bool runningStarted = false;
                                bool StopStarted = false;
                                bool SpentStarttime = false;
                                bool IsDisplayed = false;
                                bool ACStatred = false;
                                double TotalACTime = 0.0;

                                DateTime PrvIdletime = DateTime.Now;
                                DateTime presIdletime = DateTime.Now;
                                DateTime PrvRunningtime = DateTime.Now;
                                DateTime PresRunningtime = DateTime.Now;
                                DateTime PrvStoptime = DateTime.Now;
                                DateTime PresStoptime = DateTime.Now;
                                DateTime PresSpenttime = DateTime.Now;
                                DateTime PrvSpenttime = DateTime.Now;
                                DateTime PresACOnTime = DateTime.Now;
                                DateTime PrvACOnTime = DateTime.Now;

                                DateTime PrevGenTime = DateTime.Now;
                                string vehicleEnteredDate = "";
                                string vehicleLeftDate = "";
                                string Remarks = "No";
                                string PrvBranch = "";
                                string PresBranchName = "";
                                DataTable daydatatable = table.Clone();
                                DataRow[] dailyreport = table.Select("DateTime>='" + GetLowDate(date) + "' and DateTime<='" + GetHighDate(date) + "'");
                                foreach (DataRow row in dailyreport)
                                {
                                    daydatatable.ImportRow(row);
                                }
                                reportData.Add(dcnrycnt.ToString(), daydatatable);
                                dcnrycnt++;

                                DataRow firstrow = null;
                                DataRow lastrow = null;
                                if (dailyreport.Length > 1)
                                {
                                    firstrow = dailyreport[0];
                                    lastrow = dailyreport[dailyreport.Length - 1];

                                    DateTime starttime = GetLowDate(date);
                                    DateTime stoptime = GetHighDate(date);
                                    bool startflag = false;
                                    bool stopflag = false;
                                    foreach (DataRow dr1 in dailyreport)
                                    {
                                        if (double.Parse(dr1["Speed"].ToString()) > 10)
                                        {
                                            if (!startflag)
                                            {
                                                starttime = (DateTime)dr1["DateTime"];
                                                startflag = true;
                                            }
                                        }

                                        if (double.Parse(dr1["Speed"].ToString()) == 0)
                                        {
                                            stoptime = (DateTime)dr1["DateTime"];
                                            stopflag = true;
                                        }
                                        else
                                        {
                                            stopflag = false;
                                        }

                                        int AC = 0;
                                        int.TryParse(dr1["inp3"].ToString(), out AC);
                                        if (lat == 0.0 && longi == 0.0)
                                        {
                                            lat = (double)dr1["Latitiude"];
                                            longi = (double)dr1["Longitude"];
                                            prvlat = lat;
                                            prevLongi = longi;
                                            TotalDistance = 0.0;
                                            PrevGenTime = (DateTime)dr1["DateTime"];



                                            if (AC == 1)
                                            {
                                                PrvACOnTime = (DateTime)dr1["DateTime"];
                                                PresACOnTime = (DateTime)dr1["DateTime"];
                                                ACStatred = true;
                                            }
                                            else
                                            {
                                                ACStatred = false;
                                            }
                                        }
                                        else
                                        {
                                            #region Calculations
                                            lat = (double)dr1["Latitiude"];
                                            longi = (double)dr1["Longitude"];
                                            TotalDistance += GeoCodeCalc.CalcDistance(lat, longi, prvlat, prevLongi);
                                            prvlat = lat;
                                            prevLongi = longi;

                                            double speed = (double)dr1["Speed"];
                                            int Ignition = 0;
                                            int.TryParse(dr1["inp2"].ToString(), out Ignition);
                                            if (speed == 0 && Ignition != 0)
                                            {
                                                runningStarted = false;
                                                if (IdleStarted)
                                                {
                                                    presIdletime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(presIdletime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                                    IdleTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    PrvIdletime = presIdletime;
                                                    PrevGenTime = presIdletime;
                                                    if (IdleTime > MaxIdleLimit)
                                                        Remarks = "YES";
                                                }
                                                else
                                                {
                                                    IdleStarted = true;
                                                    PrvIdletime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PrevGenTime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvIdletime.Ticks);//PrvIdletime.Hour, PrvIdletime.Minute, PrvIdletime.Second);
                                                    IdleTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    PrevGenTime = PrvIdletime;
                                                }
                                                if (StopStarted)
                                                {
                                                    PresStoptime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                    StopTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    PrvStoptime = PresStoptime;
                                                    PrevGenTime = PresStoptime;
                                                }
                                                else
                                                {
                                                    StopStarted = true;
                                                    PrvStoptime = (DateTime)dr1["DateTime"];
                                                    PrevGenTime = PrvStoptime;
                                                    totalStops += 1;
                                                }
                                                if (runningStarted)
                                                {
                                                    PresRunningtime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                                    RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    if (speed > Maxspeed)
                                                        Maxspeed = speed;
                                                    totalSpeed += speed;
                                                    PrvRunningtime = PresRunningtime;
                                                    PrevGenTime = PresRunningtime;
                                                    runningStarted = false;
                                                }
                                            }
                                            else if (speed == 0 && Ignition == 0)
                                            {
                                                IdleStarted = false;
                                                runningStarted = false;
                                                if (StopStarted)
                                                {
                                                    PresStoptime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                    StopTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    PrvStoptime = PresStoptime;
                                                    PrevGenTime = PresStoptime;
                                                }
                                                else
                                                {
                                                    StopStarted = true;
                                                    PrvStoptime = (DateTime)dr1["DateTime"];
                                                    PrevGenTime = PrvStoptime;
                                                    totalStops += 1;
                                                }
                                                if (runningStarted)
                                                {
                                                    PresRunningtime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                                    RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    if (speed > Maxspeed)
                                                        Maxspeed = speed;
                                                    totalSpeed += speed;
                                                    PrvRunningtime = PresRunningtime;
                                                    PrevGenTime = PresRunningtime;
                                                    runningStarted = false;
                                                }
                                            }
                                            else if (speed > 0)
                                            {
                                                IdleStarted = false;
                                                if (runningStarted)
                                                {
                                                    PresRunningtime = (DateTime)dr1["DateTime"];
                                                    TimeSpan t = new TimeSpan(PresRunningtime.Ticks);//PresRunningtime.Hour, PresRunningtime.Minute, PresRunningtime.Second);
                                                    TimeSpan t1 = new TimeSpan(PrvRunningtime.Ticks);//PrvRunningtime.Hour, PrvRunningtime.Minute, PrvRunningtime.Second);
                                                    if (StopStarted)
                                                    {
                                                        PresStoptime = (DateTime)dr1["DateTime"];
                                                        TimeSpan stpt = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                        TimeSpan stpt1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                        StopTime += Math.Abs(stpt.Subtract(stpt1).TotalSeconds);
                                                        StopStarted = false;
                                                    }
                                                    RunningTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                    if (speed > Maxspeed)
                                                        Maxspeed = speed;

                                                    totalSpeed += speed;
                                                    PrvRunningtime = PresRunningtime;
                                                    PrevGenTime = PresRunningtime;
                                                }
                                                else
                                                {
                                                    runningStarted = true;
                                                    PrvRunningtime = (DateTime)dr1["DateTime"];
                                                    if (StopStarted)
                                                    {
                                                        PresStoptime = (DateTime)dr1["DateTime"];
                                                        TimeSpan stpt = new TimeSpan(PresStoptime.Ticks);//PresStoptime.Hour, PresStoptime.Minute, PresStoptime.Second);
                                                        TimeSpan stpt1 = new TimeSpan(PrvStoptime.Ticks);//PrvStoptime.Hour, PrvStoptime.Minute, PrvStoptime.Second);
                                                        StopTime += Math.Abs(stpt.Subtract(stpt1).TotalSeconds);
                                                        StopStarted = false;
                                                    }
                                                    totalSpeed += speed;
                                                    if (speed > Maxspeed)
                                                        Maxspeed = speed;
                                                    PrevGenTime = PrvRunningtime;
                                                }
                                            }

                                            #endregion

                                            if (AC == 1)
                                            {
                                                if (ACStatred == false)

                                                    PrvACOnTime = (DateTime)dr1["DateTime"];
                                                PresACOnTime = (DateTime)dr1["DateTime"];

                                                ACStatred = true;
                                            }
                                            else
                                            {
                                                ACStatred = false;
                                            }

                                            if (ACStatred)
                                            {
                                                PresACOnTime = (DateTime)dr1["DateTime"];
                                                TimeSpan t = new TimeSpan(PresACOnTime.Ticks);//presIdletime.Hour, presIdletime.Minute, presIdletime.Second);
                                                TimeSpan t1 = new TimeSpan(PrvACOnTime.Ticks);
                                                TotalACTime += Math.Abs(t.Subtract(t1).TotalSeconds);
                                                PrvACOnTime = PresACOnTime;
                                            }
                                        }
                                    }
                                    //datareport = new DataReport();

                                    if (firstrow != null && lastrow != null)
                                    {
                                        double firstval = 0;
                                        double.TryParse(firstrow["Odometer"].ToString(), out firstval);
                                        double lastval = 0;
                                        double.TryParse(lastrow["Odometer"].ToString(), out lastval);
                                        if (lastval > 0 && firstval > 0)
                                            TotalDistance = lastval - firstval;
                                    }


                                    double avgspeeddiv = (RunningTime / 3600);
                                    double avgspeed = 0;
                                    if (avgspeeddiv > 0)
                                        avgspeed = TotalDistance / avgspeeddiv;

                                    TimeSpan ts1 = new TimeSpan(starttime.Ticks);
                                    TimeSpan ts2 = new TimeSpan(GetLowDate(date).Ticks);
                                    TimeSpan ts3 = new TimeSpan(GetHighDate(date).Ticks);
                                    TimeSpan ts4 = new TimeSpan(DateTime.Parse(lastrow["DateTime"].ToString()).Ticks);
                                    double ts5 = ts1.Subtract(ts2).TotalSeconds;
                                    double ts6 = ts3.Subtract(ts4).TotalSeconds;
                                    double stationaryhours = ts5 + ts6;

                                    double totalstationaryhours = stationaryhours + StopTime;
                                    DataRow insntrow = dailydatatable.NewRow();
                                    //insntrow["SNo"] = dailydatatable.Rows.Count + 1;
                                    //insntrow["VehicleNo"] = VehicleID;
                                    Startdt = starttime;
                                    string Startdate = Startdt.ToString("M/dd/yyyy");
                                    string StartTime = Startdt.ToString("hh:mm:ss tt");
                                    insntrow["VehicleID"] = vehiclestr;
                                    insntrow["StartDate"] = Startdate;

                                    string[] Reachsplt = StartTime.ToString().Split(' ');
                                    if (Reachsplt.Length > 1)
                                    {
                                        int departuretimemin = 0;
                                        int dephours = 0;
                                        int depmin = 0;
                                        int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                        int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                        //departuretimemin = 720 - ((dephours * 60) + depmin);

                                        if (Reachsplt[1] == "PM")
                                        {
                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                departuretimemin = ((dephours * 60) + depmin);
                                            else
                                                departuretimemin = 720 + ((dephours * 60) + depmin);
                                        }
                                        else
                                        {
                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                departuretimemin = ((dephours * 60) + depmin) - 720;
                                            else
                                                departuretimemin = ((dephours * 60) + depmin);
                                        }

                                        //ddlTravels.Items.Add(dr["traveler_agent"].ToString());

                                        int time = departuretimemin;
                                        if ((time % 60) == 0 && (time / 60) < 10)
                                        {
                                            Duration = "0" + time / 60 + " : " + "0" + time % 60;
                                        }
                                        else if ((time % 60) >= 10 && (time / 60) < 10)
                                        {
                                            Duration = "0" + time / 60 + " : " + time % 60;
                                        }
                                        else if ((time % 60) < 10 && (time / 60) >= 10)
                                        {
                                            Duration = time / 60 + " : " + "0" + time % 60;
                                        }
                                        else if ((time % 60) >= 10 && (time / 60) >= 10)
                                        {
                                            Duration = time / 60 + " : " + time % 60;
                                        }

                                    }


                                    insntrow["StartTime"] = Duration;
                                    //if(!stopflag)
                                    //    insntrow["StopTime"] = DateConverter.GetHighDate(date);
                                    //else
                                    //    insntrow["StopTime"] = stoptime;
                                    Stopdt = (DateTime)lastrow["DateTime"];
                                    string Stopdate = Stopdt.ToString("M/dd/yyyy");
                                    string StoopTime = Stopdt.ToString("hh:mm:ss tt");
                                    insntrow["StopDate"] = Stopdate;
                                    string[] Stopsplt = StoopTime.ToString().Split(' ');
                                    if (Stopsplt.Length > 1)
                                    {
                                        int departuretimemin = 0;
                                        int dephours = 0;
                                        int depmin = 0;
                                        int.TryParse(Stopsplt[0].Split(':')[0], out dephours);
                                        int.TryParse(Stopsplt[0].Split(':')[1], out depmin);
                                        //departuretimemin = 720 - ((dephours * 60) + depmin);

                                        if (Stopsplt[1] == "PM")
                                        {
                                            if (Stopsplt[0].Split(':')[0] == "12")
                                                departuretimemin = ((dephours * 60) + depmin);
                                            else
                                                departuretimemin = 720 + ((dephours * 60) + depmin);
                                        }
                                        else
                                        {
                                            if (Stopsplt[0].Split(':')[0] == "12")
                                                departuretimemin = ((dephours * 60) + depmin) - 720;
                                            else
                                                departuretimemin = ((dephours * 60) + depmin);
                                        }


                                        //ddlTravels.Items.Add(dr["traveler_agent"].ToString());

                                        int time = departuretimemin;
                                        if ((time % 60) < 10 && (time / 60) < 10)
                                        {
                                            StDuration = "0" + time / 60 + " : " + "0" + time % 60;
                                        }
                                        else if ((time % 60) >= 10 && (time / 60) < 10)
                                        {
                                            StDuration = "0" + time / 60 + " : " + time % 60;
                                        }
                                        else if ((time % 60) < 10 && (time / 60) >= 10)
                                        {
                                            StDuration = time / 60 + " : " + "0" + time % 60;
                                        }
                                        else if ((time % 60) >= 10 && (time / 60) >= 10)
                                        {
                                            StDuration = time / 60 + " : " + time % 60;
                                        }

                                    }


                                    insntrow["StopTime"] = StDuration;
                                    insntrow["TotalDistanceTravelled(Kms)"] = Math.Round(TotalDistance, 3);
                                    insntrow["MotionHours"] = (int)RunningTime / 3600 + "H " + (int)RunningTime % (60) + " Min";
                                    insntrow["StationaryHours"] = (int)totalstationaryhours / 3600 + " H " + (int)totalstationaryhours % 60 + " Min";
                                    insntrow["MaxSpeed"] = (int)Maxspeed;
                                    insntrow["AvgSpeed"] = String.Format("{0:0.00}", avgspeed);
                                    insntrow["IdleTime"] = (int)IdleTime / 3600 + "H " + (int)IdleTime % 60 + " Min";
                                    insntrow["ACONTime"] = (int)TotalACTime / 3600 + "H " + (int)TotalACTime % 60 + " Min";
                                    dailydatatable.Rows.Add(insntrow);
                                }
                                else
                                {
                                    DataRow insntrow = dailydatatable.NewRow();
                                    //insntrow["SNo"] = dailydatatable.Rows.Count + 1;
                                    insntrow["VehicleID"] = vehiclestr;
                                    insntrow["StartDate"] = "MOVEMENT NOT FOUND";
                                    insntrow["StartTime"] = "0 H";
                                    //if(!stopflag)
                                    //    insntrow["StopTime"] = DateConverter.GetHighDate(date);
                                    //else
                                    //    insntrow["StopTime"] = stoptime;
                                    insntrow["StopDate"] = "MOVEMENT NOT FOUND";
                                    insntrow["StopTime"] = "0 H";
                                    insntrow["TotalDistanceTravelled(Kms)"] = 0;
                                    insntrow["MotionHours"] = "0 H 0 Min";
                                    insntrow["StationaryHours"] = "0 H 0 Min";
                                    insntrow["MaxSpeed"] = 0;
                                    insntrow["AvgSpeed"] = 0;
                                    insntrow["IdleTime"] = "0 H 0 Min";
                                    insntrow["ACONTime"] = "0 H 0 Min";
                                    dailydatatable.Rows.Add(insntrow);

                                }
                            }
                            if (dailydatatable.Rows[dailydatatable.Rows.Count - 1]["StartDate"].ToString() == "MOVEMENT NOT FOUND")
                            {
                                dailydatatable.Rows.RemoveAt(dailydatatable.Rows.Count - 1);
                            }
                        }
                        #endregion

                        HttpContext.Current.Session["reportdata"] = reportData;
                        HttpContext.Current.Session["xportdata"] = dailydatatable;
                        List<DailyReportCLS> DailyReportCLSlst = new List<DailyReportCLS>();
                        foreach (DataRow dr in dailydatatable.Rows)
                        {
                            DailyReportCLS DailyReportCLS = new DailyReportCLS();
                            DailyReportCLS.VehicleID = dr["VehicleID"].ToString();
                            DailyReportCLS.StartDate = dr["StartDate"].ToString();
                            DailyReportCLS.StartTime = dr["StartTime"].ToString();
                            DailyReportCLS.StopDate = dr["StopDate"].ToString();
                            DailyReportCLS.StopTime = dr["StopTime"].ToString();
                            DailyReportCLS.TotalDistanceTravelled = dr["TotalDistanceTravelled(Kms)"].ToString();
                            DailyReportCLS.MotionHours = dr["MotionHours"].ToString();
                            DailyReportCLS.StationaryHours = dr["StationaryHours"].ToString();
                            DailyReportCLS.MaxSpeed = dr["MaxSpeed"].ToString();
                            DailyReportCLS.AvgSpeed = dr["AvgSpeed"].ToString();
                            DailyReportCLS.IdleTime = dr["IdleTime"].ToString();
                            DailyReportCLSlst.Add(DailyReportCLS);
                        }
                        string respnceString = GetJson(DailyReportCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }

                    else if (reportname == "Location HaltingHours Report")
                    {
                        #region Branch wise Reports
                        //DataTable BranchDetails = new DataTable();
                        //if (HttpContext.Current.Session["BranchDetails"] == null)
                        //{
                        //    cmd = new MySqlCommand("select * from BranchData where UserName=@un");
                        //    cmd.Parameters.Add("@un", Username);
                        //    vdm.InitializeDB();
                        //    BranchDetails = vdm.SelectQuery(cmd).Tables[0];
                        //    HttpContext.Current.Session["BranchDetails"] = BranchDetails;
                        //}
                        //else
                        //{
                        //    BranchDetails = (DataTable)HttpContext.Current.Session["BranchDetails"];
                        //}
                        ddwnldr = new DataDownloader();
                        ddwnldr.UpdateBranchDetails(Username);
                        int sno = 1;
                        string Duration = "";
                        string StDuration = "";
                        DateTime Enteringdt = DateTime.Now;
                        DateTime Leftingingdt = DateTime.Now;
                        DataTable summeryTable = new DataTable();
                        DataColumn summeryColumn = new DataColumn("SNo");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleNo");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Location Name");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleEnteredDate");
                        //summeryColumn.DataType = System.Type.GetType("System.DateTime");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleEnteredTime");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleLeftDate");
                        //summeryColumn.DataType = System.Type.GetType("System.DateTime");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleLeftTime");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Stopped Hours");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Remarks");
                        summeryTable.Columns.Add(summeryColumn);
                        DataRow summeryRow = null;
                      
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            foreach (string tbname in logstbls)
                            {
                                cmd = new MySqlCommand("select '' as SNo,VehicleID,DateTime,Speed,Latitiude ,Longitude,Direction,Diesel,Odometer,Altitude from " + tbname + " where DateTime>= @starttime and DateTime<=@endtime and VehicleID='" + vehiclestr + "' and UserID='" + Username + "' order by DateTime");
                                cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                                cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                                logs = vdm.SelectQuery(cmd).Tables[0];
                                if (tottable.Rows.Count == 0)
                                {
                                    tottable = logs.Clone();
                                }
                                foreach (DataRow dr in logs.Rows)
                                {
                                    tottable.ImportRow(dr);
                                }
                            }
                            DataView dv = tottable.DefaultView;
                            dv.Sort = "DateTime ASC";
                            DataTable TripData = dv.ToTable();

                            DataRow Prevrow = null;
                            summeryRow = null;
                            Dictionary<string, string> statusobserver = new Dictionary<string, string>();
                            foreach (DataRow dr in ddwnldr.BranchDetails.Rows)
                            {
                                statusobserver.Add(dr["BranchID"].ToString(), "");
                            }
                            //if (DDL_locations.Text == "ALL")
                            //{
                            foreach (DataRow tripdatarow in TripData.Rows)
                            {
                                foreach (DataRow lstLocation in ddwnldr.BranchDetails.Rows)
                                {
                                    DataRow[] branch = ddwnldr.BranchDetails.Select("BranchID='" + lstLocation["BranchID"].ToString() + "'");

                                    double presLat = (double)tripdatarow["Latitiude"];
                                    double PresLng = (double)tripdatarow["Longitude"];

                                    foreach (DataRow Brncs in branch)
                                    {
                                        double ag_Lat = 0;
                                        double.TryParse(Brncs["Latitude"].ToString(), out ag_Lat);
                                        double ag_lng = 0;
                                        double.TryParse(Brncs["Longitude"].ToString(), out ag_lng);
                                        double ag_radious = 100;
                                        double.TryParse(Brncs["Radious"].ToString(), out ag_radious);
                                        string statusvalue = ddwnldr.getGeofenceStatus(presLat, PresLng, ag_Lat, ag_lng, ag_radious);
                                        if (statusobserver[Brncs["BranchID"].ToString()] != statusvalue)
                                        {
                                            statusobserver[Brncs["BranchID"].ToString()] = statusvalue;
                                            if (statusobserver[Brncs["BranchID"].ToString()] == "In Side")
                                            {
                                                summeryRow = summeryTable.NewRow();
                                                summeryRow["SNo"] = sno;
                                                summeryRow["VehicleNo"] = tripdatarow["VehicleID"];
                                                summeryRow["Location Name"] = Brncs["BranchID"];
                                                Enteringdt = (DateTime)tripdatarow["DateTime"];
                                                string Enterdate = Enteringdt.ToString("M/dd/yyyy");
                                                string EnterTime = Enteringdt.ToString("hh:mm:ss tt");
                                                summeryRow["VehicleEnteredDate"] = Enterdate;
                                                string[] Reachsplt = EnterTime.ToString().Split(' ');
                                                if (Reachsplt.Length > 1)
                                                {
                                                    int departuretimemin = 0;
                                                    int dephours = 0;
                                                    int depmin = 0;
                                                    int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                                    int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                                    //departuretimemin = 720 - ((dephours * 60) + depmin);

                                                    if (Reachsplt[1] == "PM")
                                                    {
                                                        if (Reachsplt[0].Split(':')[0] == "12")
                                                            departuretimemin = ((dephours * 60) + depmin);
                                                        else
                                                            departuretimemin = 720 + ((dephours * 60) + depmin);
                                                    }
                                                    else
                                                    {
                                                        if (Reachsplt[0].Split(':')[0] == "12")
                                                            departuretimemin = ((dephours * 60) + depmin) - 720;
                                                        else
                                                            departuretimemin = ((dephours * 60) + depmin);
                                                    }

                                                    //ddlTravels.Items.Add(dr["traveler_agent"].ToString());

                                                    int time = departuretimemin;
                                                    if ((time % 60) == 0 && (time / 60) < 10)
                                                    {
                                                        Duration = "0" + time / 60 + " : " + "0" + time % 60;
                                                    }
                                                    else if ((time % 60) >= 10 && (time / 60) < 10)
                                                    {
                                                        Duration = "0" + time / 60 + " : " + time % 60;
                                                    }
                                                    else if ((time % 60) < 10 && (time / 60) >= 10)
                                                    {
                                                        Duration = time / 60 + " : " + "0" + time % 60;
                                                    }
                                                    else if ((time % 60) >= 10 && (time / 60) >= 10)
                                                    {
                                                        Duration = time / 60 + " : " + time % 60;
                                                    }

                                                }
                                                summeryRow["VehicleEnteredTime"] = Duration;
                                                sno++;
                                                summeryTable.Rows.Add(summeryRow);
                                            }
                                            if (statusobserver[Brncs["BranchID"].ToString()] == "Out Side")
                                            {
                                                if (summeryRow != null && Prevrow != null)
                                                {
                                                    Leftingingdt = (DateTime)tripdatarow["DateTime"];
                                                    string Leftdate = Leftingingdt.ToString("M/dd/yyyy");
                                                    string LeftTime = Leftingingdt.ToString("hh:mm:ss tt");
                                                    summeryRow["VehicleLeftDate"] = Leftdate;

                                                    string[] Reachsplt = LeftTime.ToString().Split(' ');
                                                    if (Reachsplt.Length > 1)
                                                    {
                                                        int departuretimemin = 0;
                                                        int dephours = 0;
                                                        int depmin = 0;
                                                        int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                                        int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                                        //departuretimemin = 720 - ((dephours * 60) + depmin);

                                                        if (Reachsplt[1] == "PM")
                                                        {
                                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                                departuretimemin = ((dephours * 60) + depmin);
                                                            else
                                                                departuretimemin = 720 + ((dephours * 60) + depmin);
                                                        }
                                                        else
                                                        {
                                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                                departuretimemin = ((dephours * 60) + depmin) - 720;
                                                            else
                                                                departuretimemin = ((dephours * 60) + depmin);
                                                        }

                                                        //ddlTravels.Items.Add(dr["traveler_agent"].ToString());

                                                        int time = departuretimemin;
                                                        if ((time % 60) < 10 && (time / 60) < 10)
                                                        {
                                                            StDuration = "0" + time / 60 + " : " + "0" + time % 60;
                                                        }
                                                        else if ((time % 60) >= 10 && (time / 60) < 10)
                                                        {
                                                            StDuration = "0" + time / 60 + " : " + time % 60;
                                                        }
                                                        else if ((time % 60) < 10 && (time / 60) >= 10)
                                                        {
                                                            StDuration = time / 60 + " : " + "0" + time % 60;
                                                        }
                                                        else if ((time % 60) >= 10 && (time / 60) >= 10)
                                                        {
                                                            StDuration = time / 60 + " : " + time % 60;
                                                        }

                                                    }
                                                    summeryRow["VehicleLeftTime"] = StDuration;



                                                    DateTime sd = Enteringdt;
                                                    DateTime fd = Leftingingdt;

                                                    TimeSpan ts1 = new TimeSpan(fd.Ticks);
                                                    TimeSpan ts2 = new TimeSpan(sd.Ticks);
                                                    TimeSpan ts3 = ts1.Subtract(ts2);

                                                    if (fd.Ticks != sd.Ticks)
                                                    {
                                                        //summeryRow["Location1EnteredTime"] = Prevrow["DateTime"];
                                                    }
                                                    else
                                                    {
                                                        summeryTable.Rows.Remove(summeryRow);
                                                        sno--;
                                                        summeryRow = null;
                                                        break;
                                                    }
                                                    summeryRow["Stopped Hours"] = (int)(ts3.TotalHours % 24) + "Hours " + (int)(ts3.TotalMinutes % 60) + "Min ";
                                                }
                                            }
                                        }
                                        Prevrow = tripdatarow;
                                    }
                                }
                            }
                        }
                    
                        HttpContext.Current.Session["xportdata"] = summeryTable;
                        List<LocationHaltingHoursReportCLS> LocationHaltingHoursReportCLSlst = new List<LocationHaltingHoursReportCLS>();
                        foreach (DataRow dr in summeryTable.Rows)
                        {
                            LocationHaltingHoursReportCLS LocationHaltingHoursReportCLS = new LocationHaltingHoursReportCLS();
                            LocationHaltingHoursReportCLS.VehicleID = dr["VehicleNo"].ToString();
                            LocationHaltingHoursReportCLS.LocationName = dr["Location Name"].ToString();
                            LocationHaltingHoursReportCLS.VehicleEnteredDate = dr["VehicleEnteredDate"].ToString();
                            LocationHaltingHoursReportCLS.VehicleEnteredTime = dr["VehicleEnteredTime"].ToString();
                            LocationHaltingHoursReportCLS.VehicleLeftDate = dr["VehicleLeftDate"].ToString();
                            LocationHaltingHoursReportCLS.VehicleLeftTime = dr["VehicleLeftTime"].ToString();
                            LocationHaltingHoursReportCLS.StoppedHours = dr["Stopped Hours"].ToString();
                            LocationHaltingHoursReportCLSlst.Add(LocationHaltingHoursReportCLS);
                        }
                        string respnceString = GetJson(LocationHaltingHoursReportCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }
                    else if (reportname == "Location to Location Report")
                    {
                        #region location wise Reports
                        DateTime Startingdt = DateTime.Now;
                        string Duration = "";
                        string StDuration = "";
                        ddwnldr = new DataDownloader();
                        ddwnldr.UpdateBranchDetails(Username);
                        string vehicls = "";
                        ////string Status = "";
                        int sno = 1;
                        DataTable summeryTable = new DataTable();
                        DataColumn summeryColumn = new DataColumn("SNo");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("VehicleNo");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("From Location");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Starting Date");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Starting Time");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("To Location");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Reaching Date");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Reaching Time");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Distance(Kms)");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Journey Hours");
                        summeryTable.Columns.Add(summeryColumn);
                        summeryColumn = new DataColumn("Remarks");
                        summeryTable.Columns.Add(summeryColumn);
                        DataRow summeryRow = null;
                        double prevodometer = 0;
                        double presodometer = 0;
                        
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            DataRow gaprow = summeryTable.NewRow();
                            gaprow["To Location"] = vehiclestr;
                            summeryTable.Rows.Add(gaprow);

                            bool isfirlstlog = true;
                            bool islocation1 = true;

                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            foreach (string tbname in logstbls)
                            {
                                cmd = new MySqlCommand("SELECT '' AS SNo, " + tbname + ".VehicleID, " + tbname + ".DateTime, " + tbname + ".Speed, " + tbname + ".Latitiude, " + tbname + ".Longitude, " + tbname + ".Direction, " + tbname + ".Diesel, " + tbname + ".Odometer," + tbname + ".Altitude, vehiclemaster.MaintenancePlantName, vehiclemaster.VendorName, vehiclemaster.VendorNo,vehiclemaster.VehicleTypeName FROM " + tbname + " LEFT OUTER JOIN vehiclemaster ON " + tbname + ".VehicleID = vehiclemaster.VehicleID WHERE (" + tbname + ".DateTime >= @starttime) AND (" + tbname + ".DateTime <= @endtime) AND (" + tbname + ".VehicleID = '" + vehiclestr + "') AND (" + tbname + ".UserID = '" + Username + "') ORDER BY " + tbname + ".DateTime");
                                cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                                cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                                logs = vdm.SelectQuery(cmd).Tables[0];
                                if (tottable.Rows.Count == 0)
                                {
                                    tottable = logs.Clone();
                                }
                                foreach (DataRow dr in logs.Rows)
                                {
                                    tottable.ImportRow(dr);
                                }
                            }
                            DataView dv = tottable.DefaultView;
                            dv.Sort = "DateTime ASC";
                            DataTable TripData = dv.ToTable();

                            DataRow Prevrow = null;
                            summeryRow = null;

                            DataTable vehbarnches = ddwnldr.BranchDetails;
                            Dictionary<string, string> statusobserver = new Dictionary<string, string>();
                            Dictionary<int, List<obj>> dictionary = new Dictionary<int, List<obj>>();
                            foreach (DataRow dr in vehbarnches.Rows)
                            {
                                statusobserver.Add(dr["BranchID"].ToString(), "");
                                List<obj> objlist = new List<obj>();
                                foreach (DataRow dr1 in vehbarnches.Rows)
                                {
                                    obj objct = new obj();
                                    objct.latitude = double.Parse(dr1["Latitude"].ToString());
                                    objct.longitude = double.Parse(dr1["Longitude"].ToString());
                                    objct.lid = int.Parse(dr1["Sno"].ToString());
                                    objct.BranchID = dr1["BranchID"].ToString();
                                    objct.radius = int.Parse(dr1["Radious"].ToString());
                                    double lon1 = Double.Parse(dr["Longitude"].ToString());
                                    double lat1 = Double.Parse(dr["Latitude"].ToString());
                                    double lon22 = Double.Parse(dr1["Longitude"].ToString());
                                    double lat22 = Double.Parse(dr1["Latitude"].ToString());
                                    objct.distance = DistanceAlgorithm.DistanceBetweenPlaces(lon1, lat1, lon22, lat22) * 1000;
                                    objlist.Add(objct);
                                }
                                dictionary.Add(int.Parse(dr["Sno"].ToString()), objlist);
                            }
                            #region newcode
                            double lon, lat, lon2, lat2, distance, mindist;
                            int i, j, k, flag, index, flag2;
                            string date;
                            string bid;
                            int dog = 0;
                            int pig = 0;
                            index = 0;
                            flag = 0;
                            flag2 = 0;
                            obj refpoint = new obj();
                            mindist = double.PositiveInfinity;
                            DataTable foundrowbutton2 = TripData;
                            DataTable foundrow = vehbarnches;
                            for (j = 0; j < foundrowbutton2.Rows.Count; j++)
                            {
                                lon = Double.Parse(foundrowbutton2.Rows[j]["Longitude"].ToString());
                                lat = Double.Parse(foundrowbutton2.Rows[j]["Latitiude"].ToString());
                                date = foundrowbutton2.Rows[j]["DateTime"].ToString();
                                for (i = 0; i < foundrow.Rows.Count; i++)
                                {
                                    pig++;
                                    lon2 = Double.Parse(foundrow.Rows[i]["Longitude"].ToString());
                                    lat2 = Double.Parse(foundrow.Rows[i]["Latitude"].ToString());
                                    bid = foundrow.Rows[i]["BranchID"].ToString();
                                    distance = DistanceAlgorithm.DistanceBetweenPlaces(lon, lat, lon2, lat2) * 1000;
                                    if (distance <= int.Parse(foundrow.Rows[i]["Radious"].ToString()))
                                    {

                                        statusobserver[bid] = "In Side";
                                        refpoint.lid = int.Parse(foundrow.Rows[i]["Sno"].ToString());
                                        refpoint.longitude = lon2;
                                        refpoint.latitude = lat2;
                                        refpoint.BranchID = bid;
                                        flag = 1;

                                        summeryRow = summeryTable.NewRow();
                                        summeryRow["SNo"] = sno;
                                        summeryRow["VehicleNo"] = foundrowbutton2.Rows[j]["VehicleID"];
                                        summeryRow["From Location"] = bid;
                                        sno++;
                                        summeryTable.Rows.Add(summeryRow);
                                        isfirlstlog = false;
                                        islocation1 = true;

                                        break;
                                    }
                                    else
                                    {
                                        statusobserver[bid] = "Out Side";
                                    }
                                }
                                if (flag == 1)
                                {
                                    index = j;
                                    break;
                                }
                            }

                            //Console.WriteLine("hello");
                            if (flag == 1 && index < foundrowbutton2.Rows.Count)
                            {
                                int sno1 = refpoint.lid;
                                lon = refpoint.longitude;
                                lat = refpoint.latitude;
                                bid = refpoint.BranchID;
                                List<obj> temp = new List<obj>();
                                temp = dictionary[sno1];
                                for (k = index + 1; k < foundrowbutton2.Rows.Count; k++)
                                {
                                    lon2 = Double.Parse(foundrowbutton2.Rows[k]["Longitude"].ToString());
                                    lat2 = Double.Parse(foundrowbutton2.Rows[k]["Latitiude"].ToString());
                                    date = foundrowbutton2.Rows[k]["DateTime"].ToString();
                                    distance = DistanceAlgorithm.DistanceBetweenPlaces(lon, lat, lon2, lat2) * 1000;
                                    foreach (obj d in temp)
                                    {
                                        pig++;
                                        string statusvalue = "";
                                        if (Math.Abs(d.distance - distance) <= d.radius)
                                        {
                                            double gn = DistanceAlgorithm.DistanceBetweenPlaces(d.longitude, d.latitude, lon2, lat2) * 1000;
                                            dog++;
                                            if (gn <= d.radius)
                                            {
                                                statusvalue = "In Side";
                                                if (statusobserver[d.BranchID] != statusvalue)
                                                {
                                                    statusobserver[d.BranchID] = statusvalue;

                                                    if (!isfirlstlog)
                                                    {
                                                        summeryRow["To Location"] = d.BranchID;
                                                        DateTime Reachingdt = (DateTime)foundrowbutton2.Rows[k]["DateTime"];
                                                        string Reachdate = Reachingdt.ToString("M/dd/yyyy");
                                                        string ReachTime = Reachingdt.ToString("hh:mm:ss tt");
                                                        summeryRow["Reaching Date"] = Reachdate;


                                                        string[] Reachsplt = ReachTime.ToString().Split(' ');
                                                        if (Reachsplt.Length > 1)
                                                        {
                                                            int departuretimemin = 0;
                                                            int dephours = 0;
                                                            int depmin = 0;
                                                            int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                                            int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                                            departuretimemin = 720 - ((dephours * 60) + depmin);

                                                            if (Reachsplt[1] == "PM")
                                                            {
                                                                if (Reachsplt[0].Split(':')[0] == "12")
                                                                    departuretimemin = ((dephours * 60) + depmin);
                                                                else
                                                                    departuretimemin = 720 + ((dephours * 60) + depmin);
                                                            }
                                                            else
                                                            {
                                                                if (Reachsplt[0].Split(':')[0] == "12")
                                                                    departuretimemin = ((dephours * 60) + depmin) - 720;
                                                                else
                                                                    departuretimemin = ((dephours * 60) + depmin);
                                                            }

                                                            //ddlTravels.Items.Add(dr["traveler_agent"].ToString());

                                                            int time = departuretimemin;
                                                            int aaa = time % 60;
                                                            int sss = time / 60;
                                                            if ((time % 60) < 10 && (time / 60) < 10)
                                                            {
                                                                Duration = "0" + time / 60 + ":" + "0" + time % 60;
                                                            }
                                                            else if ((time % 60) >= 10 && (time / 60) < 10)
                                                            {
                                                                Duration = "0" + time / 60 + ":" + time % 60;
                                                            }
                                                            else if ((time % 60) < 10 && (time / 60) >= 10)
                                                            {
                                                                Duration = time / 60 + ":" + "0" + time % 60;
                                                            }
                                                            else if ((time % 60) >= 10 && (time / 60) >= 10)
                                                            {
                                                                Duration = time / 60 + ":" + time % 60;
                                                            }

                                                        }
                                                        summeryRow["Reaching Time"] = Duration;
                                                        presodometer = double.Parse(foundrowbutton2.Rows[k]["Odometer"].ToString());
                                                        if (presodometer < prevodometer)
                                                        {
                                                            summeryTable.Rows.Remove(summeryRow);
                                                            sno--;
                                                            summeryRow = null;
                                                            isfirlstlog = true;
                                                            break;
                                                        }
                                                        double totaldistance = presodometer - prevodometer;
                                                        totaldistance = Math.Abs(totaldistance);
                                                        summeryRow["Distance(Kms)"] = totaldistance.ToString("00.00");
                                                        //double Cost = 0;
                                                        //double totcost = 0;
                                                        //totcost = totaldistance * Cost;
                                                        //summeryRow["Total Cost"] = totcost.ToString("00.00");

                                                        DateTime l1et = Reachingdt;
                                                        DateTime l1lt = Startingdt;

                                                        TimeSpan l1ets = new TimeSpan(l1et.Ticks);
                                                        TimeSpan l1lts = new TimeSpan(l1lt.Ticks);
                                                        TimeSpan difftime = l1ets.Subtract(l1lts);

                                                        if ((int)(difftime.TotalDays) > 0)
                                                        {
                                                            summeryRow["Journey Hours"] = (int)(difftime.TotalDays) + "Days " + (int)(difftime.TotalHours % 24) + "Hours " + (int)(difftime.TotalMinutes % 60) + "Min ";
                                                        }
                                                        else
                                                        {
                                                            summeryRow["Journey Hours"] = (int)(difftime.TotalHours % 24) + "Hours " + (int)(difftime.TotalMinutes % 60) + "Min ";
                                                        }

                                                        summeryRow["Journey Hours"] = (int)(difftime.TotalHours % 24) + "Hours " + (int)(difftime.TotalMinutes % 60) + "Min ";

                                                        islocation1 = false;
                                                    }
                                                    else
                                                    {
                                                        summeryRow = summeryTable.NewRow();
                                                        summeryRow["SNo"] = sno;
                                                        summeryRow["VehicleNo"] = foundrowbutton2.Rows[k]["VehicleID"];
                                                        summeryRow["From Location"] = d.BranchID;
                                                        sno++;
                                                        summeryTable.Rows.Add(summeryRow);
                                                        isfirlstlog = false;
                                                        islocation1 = true;
                                                    }


                                                    refpoint.lid = d.lid;
                                                    refpoint.longitude = d.longitude;
                                                    refpoint.latitude = d.latitude;
                                                    refpoint.BranchID = d.BranchID;
                                                    sno1 = refpoint.lid;
                                                    lon = refpoint.longitude;
                                                    lat = refpoint.latitude;
                                                    temp = dictionary[sno1];
                                                    bid = refpoint.BranchID;

                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                statusvalue = "Out Side";
                                                if (statusobserver[d.BranchID] != statusvalue)
                                                {
                                                    statusobserver[d.BranchID] = statusvalue;
                                                    if (summeryRow != null && Prevrow != null)
                                                    {
                                                        Startingdt = (DateTime)foundrowbutton2.Rows[k]["DateTime"];
                                                        string Startdate = Startingdt.ToString("M/dd/yyyy");
                                                        string startTime = Startingdt.ToString("hh:mm:ss tt");
                                                        string[] Reachsplt = startTime.ToString().Split(' ');
                                                        if (Reachsplt.Length > 1)
                                                        {
                                                            int departuretimemin = 0;
                                                            int dephours = 0;
                                                            int depmin = 0;
                                                            int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                                            int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                                            departuretimemin = 720 - ((dephours * 60) + depmin);

                                                            if (Reachsplt[1] == "PM")
                                                            {
                                                                if (Reachsplt[0].Split(':')[0] == "12")
                                                                    departuretimemin = ((dephours * 60) + depmin);
                                                                else
                                                                    departuretimemin = 720 + ((dephours * 60) + depmin);
                                                            }
                                                            else
                                                            {
                                                                if (Reachsplt[0].Split(':')[0] == "12")
                                                                    departuretimemin = ((dephours * 60) + depmin) - 720;
                                                                else
                                                                    departuretimemin = ((dephours * 60) + depmin);
                                                            }


                                                            int time = departuretimemin;
                                                            if ((time % 60) < 10 && (time / 60) < 10)
                                                            {
                                                                StDuration = "0" + time / 60 + ":" + "0" + time % 60;
                                                            }
                                                            else if ((time % 60) >= 10 && (time / 60) < 10)
                                                            {
                                                                StDuration = "0" + time / 60 + ":" + time % 60;
                                                            }
                                                            else if ((time % 60) < 10 && (time / 60) >= 10)
                                                            {
                                                                StDuration = time / 60 + ":" + "0" + time % 60;
                                                            }
                                                            else if ((time % 60) >= 10 && (time / 60) >= 10)
                                                            {
                                                                StDuration = time / 60 + ":" + time % 60;
                                                            }

                                                        }
                                                        if (islocation1)
                                                        {

                                                            summeryRow["Starting Date"] = Startdate;
                                                            summeryRow["Starting Time"] = StDuration;


                                                            prevodometer = double.Parse(foundrowbutton2.Rows[k]["Odometer"].ToString());
                                                        }
                                                        else
                                                        {
                                                            summeryRow = null;
                                                            isfirlstlog = false;

                                                            summeryRow = summeryTable.NewRow();
                                                            summeryRow["SNo"] = sno;
                                                            summeryRow["VehicleNo"] = foundrowbutton2.Rows[k]["VehicleID"];
                                                            summeryRow["From Location"] = d.BranchID;
                                                            summeryRow["Starting Date"] = Startdate;
                                                            summeryRow["Starting Time"] = StDuration;

                                                            sno++;
                                                            summeryTable.Rows.Add(summeryRow);
                                                            prevodometer = double.Parse(foundrowbutton2.Rows[k]["Odometer"].ToString());

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            statusvalue = "Out Side";
                                            if (statusobserver[d.BranchID] != statusvalue)
                                            {
                                                statusobserver[d.BranchID] = statusvalue;
                                                if (summeryRow != null && Prevrow != null)
                                                {
                                                    Startingdt = (DateTime)foundrowbutton2.Rows[k]["DateTime"];
                                                    string Startdate = Startingdt.ToString("M/dd/yyyy");
                                                    string startTime = Startingdt.ToString("hh:mm:ss tt");
                                                    string[] Reachsplt = startTime.ToString().Split(' ');
                                                    if (Reachsplt.Length > 1)
                                                    {
                                                        int departuretimemin = 0;
                                                        int dephours = 0;
                                                        int depmin = 0;
                                                        int.TryParse(Reachsplt[0].Split(':')[0], out dephours);
                                                        int.TryParse(Reachsplt[0].Split(':')[1], out depmin);
                                                        departuretimemin = 720 - ((dephours * 60) + depmin);

                                                        if (Reachsplt[1] == "PM")
                                                        {
                                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                                departuretimemin = ((dephours * 60) + depmin);
                                                            else
                                                                departuretimemin = 720 + ((dephours * 60) + depmin);
                                                        }
                                                        else
                                                        {
                                                            if (Reachsplt[0].Split(':')[0] == "12")
                                                                departuretimemin = ((dephours * 60) + depmin) - 720;
                                                            else
                                                                departuretimemin = ((dephours * 60) + depmin);
                                                        }


                                                        int time = departuretimemin;
                                                        if ((time % 60) < 10 && (time / 60) < 10)
                                                        {
                                                            StDuration = "0" + time / 60 + ":" + "0" + time % 60;
                                                        }
                                                        else if ((time % 60) >= 10 && (time / 60) < 10)
                                                        {
                                                            StDuration = "0" + time / 60 + ":" + time % 60;
                                                        }
                                                        else if ((time % 60) < 10 && (time / 60) >= 10)
                                                        {
                                                            StDuration = time / 60 + ":" + "0" + time % 60;
                                                        }
                                                        else if ((time % 60) >= 10 && (time / 60) >= 10)
                                                        {
                                                            StDuration = time / 60 + ":" + time % 60;
                                                        }

                                                    }
                                                    if (islocation1)
                                                    {

                                                        summeryRow["Starting Date"] = Startdate;
                                                        summeryRow["Starting Time"] = StDuration;


                                                        prevodometer = double.Parse(foundrowbutton2.Rows[k]["Odometer"].ToString());
                                                    }
                                                    else
                                                    {
                                                        summeryRow = null;
                                                        isfirlstlog = false;

                                                        summeryRow = summeryTable.NewRow();
                                                        summeryRow["SNo"] = sno;
                                                        summeryRow["VehicleNo"] = foundrowbutton2.Rows[k]["VehicleID"];
                                                        summeryRow["From Location"] = d.BranchID;
                                                        summeryRow["Starting Date"] = Startdate;
                                                        summeryRow["Starting Time"] = StDuration;

                                                        sno++;
                                                        summeryTable.Rows.Add(summeryRow);
                                                        prevodometer = double.Parse(foundrowbutton2.Rows[k]["Odometer"].ToString());

                                                    }
                                                }
                                            }
                                        }
                                        Prevrow = foundrowbutton2.Rows[k];
                                    }
                                }
                            }
                            #endregion
                            //foreach (DataRow tripdatarow in TripData.Rows)
                            //{
                            //    foreach (DataRow lstLocation in ddwnldr.BranchDetails.Rows)
                            //    {
                            //        DataRow[] branch = ddwnldr.BranchDetails.Select("BranchID='" + lstLocation["BranchID"].ToString() + "'");

                            //        double presLat = (double)tripdatarow["Latitiude"];
                            //        double PresLng = (double)tripdatarow["Longitude"];

                            //        foreach (DataRow Brncs in branch)
                            //        {
                            //            double ag_Lat = 0;
                            //            double.TryParse(Brncs["Latitude"].ToString(), out ag_Lat);
                            //            double ag_lng = 0;
                            //            double.TryParse(Brncs["Longitude"].ToString(), out ag_lng);
                            //            double ag_radious = 100;
                            //            double.TryParse(Brncs["Radious"].ToString(), out ag_radious);
                            //            string statusvalue = ddwnldr.getGeofenceStatus(presLat, PresLng, ag_Lat, ag_lng, ag_radious);

                            //            if (statusobserver[Brncs["BranchID"].ToString()] != statusvalue)
                            //            {
                            //                statusobserver[Brncs["BranchID"].ToString()] = statusvalue;
                            //                if (statusobserver[Brncs["BranchID"].ToString()] == "In Side")
                            //                {
                            //                    if (!isfirlstlog)
                            //                    {
                            //                        summeryRow["To Location"] = Brncs["BranchID"];
                            //                        DateTime Reachingdt = (DateTime)tripdatarow["DateTime"];
                            //                        string Reachdate = Reachingdt.ToString("M/dd/yyyy");
                            //                        string ReachTime = Reachingdt.ToString("hh:mm:ss tt");
                            //                        summeryRow["Reaching Date"] = Reachdate;
                            //                        Duration = Reachingdt.ToString("HH:mm");
                            //                        summeryRow["Reaching Time"] = Duration;
                            //                        presodometer = double.Parse(tripdatarow["Odometer"].ToString());
                            //                        if (presodometer < prevodometer)
                            //                        {
                            //                            summeryTable.Rows.Remove(summeryRow);
                            //                            sno--;
                            //                            summeryRow = null;
                            //                            isfirlstlog = true;
                            //                            break;
                            //                        }
                            //                        double totaldistance = presodometer - prevodometer;
                            //                        totaldistance = Math.Abs(totaldistance);
                            //                        summeryRow["Distance(Kms)"] = totaldistance.ToString("00.00");

                            //                        DateTime l1et = Reachingdt;
                            //                        DateTime l1lt = Startingdt;

                            //                        TimeSpan l1ets = new TimeSpan(l1et.Ticks);
                            //                        TimeSpan l1lts = new TimeSpan(l1lt.Ticks);
                            //                        TimeSpan difftime = l1ets.Subtract(l1lts);

                            //                        if (l1et.Ticks != l1lt.Ticks)
                            //                        {
                            //                            //summeryRow["Location1LeftTime"] = Prevrow["DateTime"];
                            //                        }
                            //                        else
                            //                        {
                            //                            summeryTable.Rows.Remove(summeryRow);
                            //                            sno--;
                            //                            summeryRow = null;
                            //                            isfirlstlog = true;
                            //                            break;
                            //                        }
                            //                        summeryRow["Journey Hours"] = (int)(difftime.TotalHours % 24) + "Hours " + (int)(difftime.TotalMinutes % 60) + "Min ";

                            //                        islocation1 = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        summeryRow = summeryTable.NewRow();
                            //                        summeryRow["SNo"] = sno;
                            //                        summeryRow["VehicleNo"] = tripdatarow["VehicleID"];
                            //                        summeryRow["From Location"] = Brncs["BranchID"];
                            //                        //summeryRow["Location1EnteredTime"] = tripdatarow["DateTime"];
                            //                        sno++;
                            //                        summeryTable.Rows.Add(summeryRow);
                            //                        isfirlstlog = false;
                            //                        islocation1 = true;
                            //                    }
                            //                }
                            //                if (statusobserver[Brncs["BranchID"].ToString()] == "Out Side")
                            //                {
                            //                    if (summeryRow != null && Prevrow != null)
                            //                    {
                            //                        Startingdt = (DateTime)tripdatarow["DateTime"];
                            //                        string Startdate = Startingdt.ToString("M/dd/yyyy");
                            //                        string startTime = Startingdt.ToString("hh:mm:ss tt");
                            //                        string[] Reachsplt = startTime.ToString().Split(' ');
                            //                        if (islocation1)
                            //                        {
                            //                            StDuration = Startingdt.ToString("HH:mm");
                            //                            summeryRow["Starting Date"] = Startdate;
                            //                            summeryRow["Starting Time"] = StDuration;


                            //                            prevodometer = double.Parse(tripdatarow["Odometer"].ToString());
                            //                        }
                            //                        else
                            //                        {
                            //                            summeryRow = null;
                            //                            //isfirlstlog = true;

                            //                            summeryRow = summeryTable.NewRow();
                            //                            summeryRow["SNo"] = sno;
                            //                            summeryRow["VehicleNo"] = tripdatarow["VehicleID"];
                            //                            summeryRow["From Location"] = Brncs["BranchID"];
                            //                            //summeryRow["Location1EnteredTime"] = prevtime;
                            //                            summeryRow["Starting Date"] = Startdate;
                            //                            StDuration = Startingdt.ToString("HH:mm");
                            //                            summeryRow["Starting Time"] = StDuration;

                            //                            sno++;
                            //                            summeryTable.Rows.Add(summeryRow);
                            //                            prevodometer = double.Parse(tripdatarow["Odometer"].ToString());

                            //                        }

                            //                    }
                            //                }
                            //            }
                            //            Prevrow = tripdatarow;
                            //        }
                            //    }
                            //}
                        }
                        if (summeryTable.Rows.Count > 0)
                        {
                            int snocnt = 1;
                            for (int cnt = 0; cnt < summeryTable.Rows.Count; cnt++)
                            {
                                double odo = 0;
                                double.TryParse(summeryTable.Rows[cnt]["Distance(Kms)"].ToString(), out odo);
                                if (summeryTable.Rows[cnt]["To Location"].ToString() == "" || summeryTable.Rows[cnt]["Starting Date"].ToString() == "")
                                {
                                    summeryTable.Rows.RemoveAt(cnt);
                                    cnt--;
                                }
                                else if (summeryTable.Rows[cnt]["To Location"].ToString() == "")
                                {
                                    summeryTable.Rows.RemoveAt(cnt);
                                    cnt--;
                                }
                                else if (summeryTable.Rows[cnt]["From Location"].ToString() == summeryTable.Rows[cnt]["To Location"].ToString() && odo <= 1)
                                {
                                    summeryTable.Rows.RemoveAt(cnt);
                                    cnt--;
                                }
                                else
                                {
                                    if (summeryTable.Rows[cnt]["VehicleNo"].ToString() != "" && summeryTable.Rows[cnt]["VehicleNo"].ToString() != "&nbsp;")
                                    {
                                        summeryTable.Rows[cnt]["SNo"] = snocnt;
                                        snocnt++;
                                    }
                                }
                            }
                        }
                          HttpContext.Current.Session["xportdata"] = summeryTable;
                          List<LocationtoLocationReportCLS> LocationtoLocationReportCLSlst = new List<LocationtoLocationReportCLS>();
                        foreach (DataRow dr in summeryTable.Rows)
                        {
                            LocationtoLocationReportCLS LocationtoLocationReportCLS = new LocationtoLocationReportCLS();
                            LocationtoLocationReportCLS.VehicleID = dr["VehicleNo"].ToString();
                            LocationtoLocationReportCLS.FromLocation = dr["From Location"].ToString();
                            LocationtoLocationReportCLS.StartingDate = dr["Starting Date"].ToString();
                            LocationtoLocationReportCLS.StartingTime = dr["Starting Time"].ToString();
                            LocationtoLocationReportCLS.ToLocation = dr["To Location"].ToString();
                            LocationtoLocationReportCLS.ReachingDate = dr["Reaching Date"].ToString();
                            LocationtoLocationReportCLS.ReachingTime = dr["Reaching Time"].ToString();
                            LocationtoLocationReportCLS.Distance = dr["Distance(Kms)"].ToString();
                            LocationtoLocationReportCLS.JourneyHours = dr["Journey Hours"].ToString();
                            LocationtoLocationReportCLSlst.Add(LocationtoLocationReportCLS);
                        }
                        string respnceString = GetJson(LocationtoLocationReportCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }
                    else if (reportname == "Ignition Report")
                    {
                        #region ignition report
                        DataTable rpttable = new DataTable();
                        DataColumn col = new DataColumn("VehicleID");
                        rpttable.Columns.Add(col);
                        col = new DataColumn("IgnitionOnTime");
                        rpttable.Columns.Add(col);
                        foreach (string vehiclestr in checkedvhcles)
                        {
                            DataRow gaprow = rpttable.NewRow();
                            gaprow["VehicleID"] = vehiclestr;

                            Maxspeed = 0;
                            double tot_idletime_in_sec = 0.0;
                            DataTable logs = new DataTable();
                            DataTable tottable = new DataTable();
                            //cmd = new MySqlCommand("SELECT paireddata.UserID, paireddata.VehicleNumber, alarmlogs.AlertType, alarmlogs.Status, alarmlogs.DOH, alarmlogs.vehicleStatus, alarmlogs.Lat, alarmlogs.Lon, alarmlogs.Speed FROM paireddata INNER JOIN alarmlogs ON paireddata.Sno = alarmlogs.user_veh_refno WHERE (paireddata.UserID = @UserID) AND (paireddata.VehicleNumber = 'pc200murali') AND (alarmlogs.AlertType = 15 OR alarmlogs.AlertType = 16) AND (alarmlogs.DOH BETWEEN @d1 AND @d2) ORDER BY alarmlogs.DOH");
                            cmd = new MySqlCommand("SELECT paireddata.UserID, paireddata.VehicleNumber, alarmlogs.DOH, alarmlogs.Speed, alarmlogs.AlertType, alarmlogs.Status FROM alarmlogs INNER JOIN paireddata ON alarmlogs.user_veh_refno = paireddata.Sno WHERE (alarmlogs.DOH BETWEEN @d1 AND @d2) AND (paireddata.UserID ='" + Username + "') AND (paireddata.VehicleNumber  ='" + vehiclestr + "') AND (alarmlogs.AlertType = 15 OR alarmlogs.AlertType = 16)  ORDER BY alarmlogs.DOH");
                            cmd.Parameters.Add(new MySqlParameter("@d1", fromdate));
                            cmd.Parameters.Add(new MySqlParameter("@d2", todate));
                            logs = vdm.SelectQuery(cmd).Tables[0];

                            reportData.Add(vehiclestr, logs);
                            DateTime PrvIdletime = DateTime.Now;
                            DateTime presIdletime = DateTime.Now;
                            bool isfirst = true;
                            string prev_status = "";
                            foreach (DataRow dr1 in logs.Rows)
                            {
                                switch (dr1["AlertType"].ToString())
                                {
                                    case "15":
                                        if (prev_status != "15")
                                        {
                                            //ACC from 0 to 1- ON
                                            PrvIdletime = (DateTime)dr1["DOH"];
                                            prev_status = "15";
                                        }
                                        break;
                                    case "16":// ACC from 1 to 0 -OFF
                                        if (prev_status != "16")
                                        {
                                            if (isfirst == true)
                                            {
                                                isfirst = false;
                                                prev_status = "16";
                                            }
                                            else
                                            {
                                                presIdletime = (DateTime)dr1["DOH"];
                                                if (presIdletime != PrvIdletime)
                                                {
                                                    tot_idletime_in_sec += (presIdletime - PrvIdletime).TotalSeconds;
                                                }
                                                prev_status = "16";
                                            }
                                        }
                                        break;

                                }
                            }
                            TimeSpan result = TimeSpan.FromSeconds(tot_idletime_in_sec);
                            if (result.Days > 0)
                                gaprow["IgnitionOnTime"] = result.Days + "Days " + (int)tot_idletime_in_sec / 3600 + "Hours " + (int)tot_idletime_in_sec % (60) + "Min";
                            else
                                gaprow["IgnitionOnTime"] = (int)tot_idletime_in_sec / 3600 + "Hours " + (int)tot_idletime_in_sec % (60) + "Min";
                            rpttable.Rows.Add(gaprow);
                        }
                        List<Inginitioncls> InginitionCLSlst = new List<Inginitioncls>();
                        foreach (DataRow dr in rpttable.Rows)
                        {
                            Inginitioncls LocationtoLocationReportCLS = new Inginitioncls();
                            LocationtoLocationReportCLS.Vehicleno = dr["VehicleID"].ToString();
                            LocationtoLocationReportCLS.idletime = dr["IgnitionOnTime"].ToString();
                            InginitionCLSlst.Add(LocationtoLocationReportCLS);
                        }
                        string respnceString = GetJson(InginitionCLSlst);
                        context.Response.Write(respnceString);
                        #endregion
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    string respnceString = GetJson("Error");
                    context.Response.Write(respnceString);
                }
            }
            catch
            {

            }
        }
        public class obj
        {
            public int lid;
            public string BranchID;
            public double distance;
            public int radius;
            public double longitude;
            public double latitude;

        }
        public class DistanceAlgorithm
        {
            const double PIx = 3.141592653589793;
            const double RADIUS = 6378.16;

            /// <summary>
            /// This class cannot be instantiated.
            /// </summary>
            private DistanceAlgorithm() { }

            /// <summary>
            /// Convert degrees to Radians
            /// </summary>
            /// <param name="x">Degrees</param>
            /// <returns>The equivalent in radians</returns>
            public static double Radians(double x)
            {
                return x * PIx / 180;
            }

            /// <summary>
            /// Calculate the distance between two places.
            /// </summary>
            /// <param name="lon1"></param>
            /// <param name="lat1"></param>
            /// <param name="lon2"></param>
            /// <param name="lat2"></param>
            /// <returns></returns>
            public static double DistanceBetweenPlaces(
                double lon1,
                double lat1,
                double lon2,
                double lat2)
            {
                double dlon = Radians(lon2 - lon1);
                double dlat = Radians(lat2 - lat1);

                double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
                double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return angle * RADIUS;
            }
        }
        public class Inginitioncls
        {
            public string Vehicleno { get; set; }
            public string idletime { get; set; }
        }
        private void OnclickDrawRoute(HttpContext context)
        {
            try
            {
                vdm = new VehicleDBMgr();
                vdm.InitializeDB();
                string Username = context.Session["field1"].ToString();
                string vehicleno = context.Request["vehicleno"];
                string reportname = context.Request["reporttype"];
                List<string> logstbls = new List<string>();
                logstbls.Add("GpsTrackVehicleLogs");
                logstbls.Add("GpsTrackVehicleLogs1");
                logstbls.Add("GpsTrackVehicleLogs2");
                logstbls.Add("GpsTrackVehicleLogs3");
                Dictionary<string, DataTable> reportData = new Dictionary<string, DataTable>();
                if (reportname == "General Report" || reportname == "Daily Report")
                {
                    if (HttpContext.Current.Session["reportdata"] == null)
                    {
                    }
                    else
                    {
                        HttpContext.Current.Session["Data"] = null;
                        reportData = (Dictionary<string, DataTable>)HttpContext.Current.Session["reportdata"];
                        DataTable selecteddata = reportData[vehicleno];
                        HttpContext.Current.Session["Data"] = selecteddata;
                        HttpContext.Current.Session["xportdata"] = selecteddata;
                    }
                }
                else if (reportname == "Location to Location Report")
                {
                    if (HttpContext.Current.Session["xportdata"] == null)
                    {
                    }
                    else
                    {
                        DataTable vehdata = (DataTable)HttpContext.Current.Session["xportdata"];
                        string fromtime = context.Request["startdatetime"];
                        string totime = context.Request["enddatetime"];
                        //DateTime fromdate = new DateTime("M/dd/yyyy hh:mm:ss tt");
                        DateTime fromdate = DateTime.ParseExact(fromtime, "M/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime todate = DateTime.ParseExact(totime, "M/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        //DateTime fromdate = DateTime.Parse(fromtime);
                        //DateTime todate = DateTime.Parse(totime);
                        DataTable logs = new DataTable();
                        DataTable tottable = new DataTable();
                        foreach (string tbname in logstbls)
                        {
                            cmd = new MySqlCommand("SELECT UserID, VehicleID, Speed, DateTime, Distance, Diesel, TripFlag, Latitiude, Longitude, TimeInterval, Status, Direction, Remarks, Odometer, inp1, inp2, inp3, inp4, inp5, inp6, inp7, inp8, out1, out2, out3, out4, out5, out6, out7, out8, ADC1, ADC2, GSMSignal, GPSSignal, SatilitesAvail, EP, BP, Altitude, sno FROM " + tbname + " WHERE (DateTime >= @starttime) AND (DateTime <= @endtime) AND (VehicleID = '" + vehicleno + "') and UserID='" + Username + "' ORDER BY DateTime");
                            cmd.Parameters.Add(new MySqlParameter("@starttime", fromdate));
                            cmd.Parameters.Add(new MySqlParameter("@endtime", todate));
                            logs = vdm.SelectQuery(cmd).Tables[0];
                            if (tottable.Rows.Count == 0)
                            {
                                tottable = logs.Clone();
                            }
                            foreach (DataRow dr in logs.Rows)
                            {
                                tottable.ImportRow(dr);
                            }
                        }
                        HttpContext.Current.Session["Data"] = null;
                        DataView dv = tottable.DefaultView;
                        dv.Sort = "DateTime ASC";
                        tottable = dv.ToTable();
                        HttpContext.Current.Session["Data"] = tottable;
                    }
                }
                string respnceString = GetJson("OK");
                context.Response.Write(respnceString);
            }
            catch
            {
                string respnceString = GetJson("Error");
                context.Response.Write(respnceString);
            }
        }

        double vehdistance = 0;
        public double GetDistanceBetweenPoints(double Lat1, double Long1, double Lat2, double Long2)
        {
            vehdistance = 0;

            double dLat1InRad = Lat1 * (Math.PI / 180.0);
            double dLong1InRad = Long1 * (Math.PI / 180.0);
            double dLat2InRad = Lat2 * (Math.PI / 180.0);
            double dLong2InRad = Long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
            Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
            Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            vehdistance = kEarthRadiusKms * c;
            return (vehdistance);
        }
        DataTable vehdata = new DataTable();

        private void updatetagname(HttpContext context)
        {
            try
            {
                vdm.InitializeDB();
                string tagvehno = context.Request["tagvehno"];
                string tagname = context.Request["tagname"];
                string Username = context.Session["field1"].ToString();
                cmd = new MySqlCommand("UPDATE  paireddata SET vehicle_tag = @vehicle_tag WHERE (VehicleNumber = @VehicleNumber) AND (UserID = @UserID)");
                cmd.Parameters.Add("@vehicle_tag", tagname);
                cmd.Parameters.Add("@VehicleNumber", tagvehno);
                cmd.Parameters.Add("@UserID", Username);
                vdm.Update(cmd);
                string respnceString = GetJson("OK");
                context.Response.Write(respnceString);
            }
            catch
            {
                string respnceString = GetJson("Error");
                context.Response.Write(respnceString);
            }
        }
        private void getNearestVehicle(HttpContext context)
        {
            vehdata = new DataTable();
            List<NearestVehicle> NearestVehiclelist = new List<NearestVehicle>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            string Username = context.Session["field1"].ToString();
            string Lattitude = context.Request["latt"];
            string Longitude = context.Request["long"];
            string Nokm = context.Request["Nokm"];
            vehdata.Columns.Add("SNo");
            vehdata.Columns.Add("VehicleID");
            vehdata.Columns.Add("Distance");
            vehdata.Columns.Add("ExpectedTime");
            double lat = 0;
            double lng = 0;
            double.TryParse(Lattitude, out lat);
            double.TryParse(Longitude, out lng);
            ////objects.Markers.Clear();
            cmd = new MySqlCommand("select UserName,VehicleID,Lat,Longi,Speed, Timestamp,Direction,Diesel,Odometer,Ignation from OnlineTable Where UserName=@UserName");
            cmd.Parameters.Add("@UserName", Username);
            DataTable dt = vdm.SelectQuery(cmd).Tables[0];
            double distance = 0;

            double.TryParse(Nokm, out distance);
            foreach (DataRow dr in dt.Rows)
            {
                NearestVehicle Nearestdistance = new NearestVehicle();
                double lat1 = 0;
                double.TryParse(dr["Lat"].ToString(), out lat1);

                double lng1 = 0;
                double.TryParse(dr["Longi"].ToString(), out lng1);

                GetDistanceBetweenPoints(lat, lng, lat1, lng1);
                if (vehdistance <= distance)
                {
                    double hours = vehdistance / 40;
                    double seconds = hours * 3600;
                    DataRow newrow = vehdata.NewRow();
                    newrow["SNo"] = vehdata.Rows.Count + 1;
                    newrow["VehicleID"] = dr["VehicleID"].ToString();
                    newrow["Distance"] = vehdistance.ToString("0.00");
                    newrow["ExpectedTime"] = (int)(seconds / 3600) + "Hrs" + (int)(seconds / 60) + "Min" + (int)(seconds % 60) + "Sec with Speed " + 40 + " KMPH";
                    Nearestdistance.Vehicleno = dr["VehicleID"].ToString();
                    Nearestdistance.Distance = vehdistance.ToString("0.00");
                    Nearestdistance.ExpectedTime = (int)(seconds / 3600) + "Hrs" + (int)(seconds / 60) + "Min" + (int)(seconds % 60) + "Sec with Speed " + 40 + " KMPH";
                    Nearestdistance.latitude = lat1.ToString();
                    Nearestdistance.longitude = lng1.ToString();
                    NearestVehiclelist.Add(Nearestdistance);
                }
            }
            if (NearestVehiclelist != null)
            {
                string respnceString = GetJson(NearestVehiclelist);
                context.Response.Write(respnceString);
            }
        }

        private DateTime GetLowDateForLive(DateTime dt)
        {
            string fromtime = HttpContext.Current.Session["ReportFromTime"].ToString();
            string totime = HttpContext.Current.Session["ReportToTime"].ToString();
            double Hour, Min, Sec;
            double AddHour, AddMin, AddSec;
            DateTime DT = DateTime.Now;
            DT = dt;
            Hour = -dt.Hour;
            Min = -dt.Minute;
            Sec = -dt.Second;
            if (fromtime != "")
            {
                double.TryParse(fromtime.Split(':')[0].ToString(), out AddHour);
                double.TryParse(fromtime.Split(':')[1].ToString(), out AddMin);
                double.TryParse(fromtime.Split(':')[2].ToString(), out AddSec);
                
                DT = DT.AddHours(Hour + AddHour);
                DT = DT.AddMinutes(Min + AddMin);
                DT = DT.AddSeconds(Sec + AddSec);
                if (DateTime.Now < DT)
                {
                    DT = DT.AddDays(-1);
                }
            }
            else
            {
                DT = DT.AddHours(Hour);
                DT = DT.AddMinutes(Min);
                DT = DT.AddSeconds(Sec);
            }
            return DT;
        }

        private DateTime GetLowDate(DateTime dt)
        {
            string fromtime = HttpContext.Current.Session["ReportFromTime"].ToString();
            string totime = HttpContext.Current.Session["ReportToTime"].ToString();
            double Hour, Min, Sec;
            double AddHour, AddMin, AddSec;
            DateTime DT = DateTime.Now;
            DT = dt;
            if (fromtime != "")
            {
                Hour = -dt.Hour;
                Min = -dt.Minute;
                Sec = -dt.Second;

                double.TryParse(fromtime.Split(':')[0].ToString(), out AddHour);
                double.TryParse(fromtime.Split(':')[1].ToString(), out AddMin);
                double.TryParse(fromtime.Split(':')[2].ToString(), out AddSec);
                DT = DT.AddHours(Hour + AddHour);
                DT = DT.AddMinutes(Min + AddMin);
                DT = DT.AddSeconds(Sec + AddSec);
            }
            else
            {
                DT = dt;
                Hour = -dt.Hour;
                Min = -dt.Minute;
                Sec = -dt.Second;
                DT = DT.AddHours(Hour);
                DT = DT.AddMinutes(Min);
                DT = DT.AddSeconds(Sec);
            }
            return DT;
        }

        private DateTime GetHighDate(DateTime dt)
        {
            string fromtime = HttpContext.Current.Session["ReportFromTime"].ToString();
            string totime = HttpContext.Current.Session["ReportToTime"].ToString();
            double Hour, Min, Sec;
            double AddHour, AddMin, AddSec;
            DateTime DT = DateTime.Now;
            DT = dt;
            if (fromtime != "")
            {
                Hour = -dt.Hour;
                Min = -dt.Minute;
                Sec = -dt.Second;

                double.TryParse(fromtime.Split(':')[0].ToString(), out AddHour);
                double.TryParse(fromtime.Split(':')[1].ToString(), out AddMin);
                double.TryParse(fromtime.Split(':')[2].ToString(), out AddSec);
                DT = DT.AddHours(Hour + AddHour + 23);
                DT = DT.AddMinutes(Min + AddMin + 59);
                DT = DT.AddSeconds(Sec + AddSec);
            }
            else
            {
                Hour = 23 - dt.Hour;
                Min = 59 - dt.Minute;
                Sec = 59 - dt.Second;
                DT = dt;
                DT = DT.AddHours(Hour);
                DT = DT.AddMinutes(Min);
                DT = DT.AddSeconds(Sec);
            }
            return DT;
        }

        private void getdata(HttpContext context)
        {
            List<logsclass> vehiclelogslist = new List<logsclass>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            DataTable dt =(DataTable) context.Session["Data"];
            DataTable selecteddata = dt;
            DataTable allvehicles = new DataTable();
            if (context.Session["allvehicles"] != null)
            {
                allvehicles = (DataTable)context.Session["allvehicles"];
            }
            int rowcount = 1;
            foreach (DataRow dr in dt.Rows)
            {
                logsclass vehlogs = new logsclass();
                vehlogs.Sno = rowcount.ToString();
                vehlogs.vehicleno = dr["VehicleID"].ToString();
                if (allvehicles.Rows.Count > 0)
                {
                    DataRow[] vehtypearray = allvehicles.Select("VehicleNumber='" + dr["VehicleID"].ToString() + "'");
                    vehlogs.vehicletype = vehtypearray[0]["VehicleType"].ToString();
                }
                else
                {
                    vehlogs.vehicletype = "Car";
                }
                vehlogs.latitude = dr["Latitiude"].ToString();
                vehlogs.longitude = dr["Longitude"].ToString();
                vehlogs.direction = dr["Direction"].ToString();
                vehlogs.speed = dr["Speed"].ToString();
                vehlogs.datetime = dr["DateTime"].ToString();
                vehlogs.odometer = dr["Odometer"].ToString();
                double altitude = 0;
                double.TryParse(dr["Altitude"].ToString(), out altitude);
                altitude = Math.Pow(altitude, 2);
                vehlogs.Altitude = altitude.ToString();
                if (dr["Status"].ToString() == "0" || dr["Status"].ToString() == "Running")
                    vehlogs.Status = "Running";
                else if (dr["Status"].ToString() == "1" || dr["Status"].ToString() == "Stopped")
                    vehlogs.Status = "Stopped";
                //  vehlogs.Reportsdata=
                vehiclelogslist.Add(vehlogs);
                rowcount++;
            }

            if (vehiclelogslist != null)
            {
                string respnceString = GetJson(vehiclelogslist);
                context.Response.Write(respnceString);
            }
        }
        private void InitilizeVehiclesreports(HttpContext context)
        {
            List<vehiclesclass> vehicleslist = new List<vehiclesclass>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            string Username = context.Session["field1"].ToString();
            DataTable Vehiclesdata = new DataTable();
            if (context.Session["allvehicles"] == null)
            {
                cmd = new MySqlCommand("SELECT paireddata.VehicleNumber, paireddata.FullTankVal, paireddata.EmptyTankVal, paireddata.VehicleType, paireddata.FullTankLtrs, paireddata.emptyTankLrs, loginstable.main_user FROM loginsconfigtable INNER JOIN paireddata ON loginsconfigtable.VehicleID = paireddata.VehicleNumber INNER JOIN loginstable ON paireddata.UserID = loginstable.main_user AND loginsconfigtable.Refno = loginstable.refno WHERE (loginstable.loginid = @UserName)");
                //cmd = new MySqlCommand("select * from PairedData where UserID=@UserName");
                cmd.Parameters.Add("@UserName", Username);
                Vehiclesdata = vdm.SelectQuery(cmd).Tables[0];
                context.Session["allvehicles"] = Vehiclesdata;
                vdm = null;
            }
            else
            {
                Vehiclesdata = (DataTable)context.Session["allvehicles"];
            }
            foreach (DataRow dr in Vehiclesdata.Rows)
            {
                vehiclesclass veh = new vehiclesclass();
                string vehicleno = dr["VehicleNumber"].ToString();
                veh.vehicletype = dr["VehicleType"].ToString();
                //if (!vehicleno.Contains('/'))
                //{
                    //char[] charsToTrim = { ' ' };
                    //vehicleno = vehicleno.Trim(charsToTrim);
                //}
                //else
                //{
                //    char[] charsToTrim = { '/' };
                //    vehicleno = vehicleno.Remove(7, 1);
                //}
                //vehicleno = vehicleno.Replace(" ", "");
                veh.vehicleno = vehicleno;
                vehicleslist.Add(veh);
            }

            if (vehicleslist != null)
            {
                string respnceString = GetJson(vehicleslist);
                context.Response.Write(respnceString);
            }
        }
        VehicleDBMgr vdm;
        MySqlCommand cmd;
        private void InitilizeVehicles(HttpContext context)
        {
            List<vehiclesclass> vehicleslist = new List<vehiclesclass>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            string Username="";
            if (context.Session["field1"] != null)
            {
                Username = context.Session["field1"].ToString();
            }
            else
            {
                string responsestring = "Login.aspx";
                string sendresponse = GetJson(new redirecturl() { responseurl = responsestring });
                context.Response.Write(sendresponse);
                return;
            }
            //string Username = context.Request["Username"];
            DataTable Vehiclesdata = new DataTable();
            if (context.Session["allvehicles"] == null)
            {
               // cmd = new MySqlCommand("SELECT paireddata.VehicleNumber, paireddata.FullTankVal, paireddata.EmptyTankVal, paireddata.VehicleType, paireddata.FullTankLtrs, paireddata.emptyTankLrs, loginstable.main_user FROM loginsconfigtable INNER JOIN paireddata ON loginsconfigtable.VehicleID = paireddata.VehicleNumber INNER JOIN loginstable ON paireddata.UserID = loginstable.main_user AND loginsconfigtable.Refno = loginstable.refno WHERE (loginstable.loginid = @UserName)");
                cmd = new MySqlCommand("select * from PairedData where UserID=@UserName");
                cmd.Parameters.Add("@UserName", Username);
                Vehiclesdata = vdm.SelectQuery(cmd).Tables[0];
                context.Session["allvehicles"] = Vehiclesdata;
                vdm = null;
            }
            else
            {
                Vehiclesdata = (DataTable)context.Session["allvehicles"];
            }
            foreach (DataRow dr in Vehiclesdata.Rows)
            {
                vehiclesclass veh = new vehiclesclass();
                string vehicleno = dr["VehicleNumber"].ToString();
                veh.vehicletype = dr["VehicleType"].ToString();
                veh.vehicletag = dr["vehicle_tag"].ToString();
                if (!vehicleno.Contains('/'))
                {
                    char[] charsToTrim = { ' ' };
                    vehicleno = vehicleno.Trim(charsToTrim);
                }
                else
                {
                    char[] charsToTrim = { '/' };
                    vehicleno = vehicleno.Remove(7, 1);
                }
                vehicleno = vehicleno.Replace(" ", "");
                veh.vehicleno = vehicleno;
                vehicleslist.Add(veh);
            }

            if (vehicleslist != null)
            {
                string respnceString = GetJson(vehicleslist);
                context.Response.Write(respnceString);
            }
            Dictionary<string, double> odometerValues = new Dictionary<string, double>();
            if (context.Session["odometerValues"] == null)
            {
                odometerValues = new Dictionary<string, double>();
                foreach (DataRow dr in Vehiclesdata.Rows)
                {
                    vdm = new VehicleDBMgr();
                    vdm.InitializeDB();
                 
                    cmd = new MySqlCommand("SELECT Odometer, VehicleID AS VehicleNumber, UserID FROM  gpstrackvehiclelogs WHERE (DateTime > @dt) AND (Speed > 0) AND (UserID = @UserID) and (VehicleID=@VehicleID) ORDER BY DateTime");
                    cmd.Parameters.Add("@UserID", Username);
                    cmd.Parameters.Add("@VehicleID", dr["VehicleNumber"].ToString());
                    cmd.Parameters.Add("@Dt", GetLowDateForLive(DateTime.Now));
                    DataTable result = vdm.SelectQuery(cmd).Tables[0];
                    vdm = null;
                    //DataRow[] drr = result.Select("VehicleNumber='" + dr["VehicleNumber"].ToString() + "'");
                    if (result.Rows.Count > 0)
                    {
                        if (!odometerValues.Keys.Contains(dr["VehicleNumber"].ToString()))
                            odometerValues.Add(result.Rows[0]["VehicleNumber"].ToString(), (double)result.Rows[0]["Odometer"]);
                    }
                    else
                    {
                        if (!odometerValues.Keys.Contains(dr["VehicleNumber"].ToString()))
                            odometerValues.Add(dr["VehicleNumber"].ToString(), 0);
                    }
                }
                context.Session["odometerValues"] = odometerValues;
            }
        }
        public double dieseldivistion = 0;

        public void calculatedieseldivision(double fulltankadc, double emptytankadc, double fulltankval, double emptytankval)
        {
            try
            {
                dieseldivistion = (fulltankadc - emptytankadc) / (fulltankval - emptytankval);
            }
            catch (Exception ex)
            {
            }
        }
        public class redirecturl
        {
            public string responseurl { set; get; }
        }

        public class vehiclesupdateclasslist
        {
            public string ServerDt { get; set; }
            public List<vehiclesupdateclass> vehiclesupdatelist { get; set; }
        }
        private void LiveUpdate(HttpContext context)
        {
            List<vehiclesupdateclass> vehiclesupdatelist = new List<vehiclesupdateclass>();
            vdm = new VehicleDBMgr();

            vdm.InitializeDB();
            DateTime Currentdate = DbManager.GetTime(vdm.conn);
            double fulltankadc;
            double emptytankadc;
            double fulltankval = 0;
            double emptytankval;
            string Username = "";
            if (context.Session["field1"] != null)
            {
                Username = context.Session["field1"].ToString();
                if (Username == "APURVA")
                {
                    Username = "vyshnavi";
                }
            }
            else
            {
                string responsestring = "Login.aspx";
                string sendresponse = GetJson(new redirecturl() { responseurl = responsestring });
                context.Response.Write(sendresponse);
                return;
            }
            //  cmd = new MySqlCommand("SELECT onlinetable.VehicleID, onlinetable.Lat, onlinetable.Longi, onlinetable.Speed, onlinetable.Timestamp, onlinetable.Direction, onlinetable.Diesel, onlinetable.Odometer, onlinetable.Ignation, onlinetable.AC, onlinetable.Status, onlinetable.Geofense, onlinetable.In1, onlinetable.In2, onlinetable.In3, onlinetable.In4, onlinetable.In5, onlinetable.Op1, onlinetable.Op2, onlinetable.Op3, onlinetable.Op4, onlinetable.Op5, onlinetable.GSMSignal, onlinetable.GPSSignal, onlinetable.SatilitesAvail, onlinetable.EP, onlinetable.BP, onlinetable.Altitude, onlinetable.UserName FROM onlinetable INNER JOIN loginsconfigtable ON onlinetable.VehicleID = loginsconfigtable.VehicleID INNER JOIN loginstable ON loginsconfigtable.Refno = loginstable.refno AND onlinetable.UserName = loginstable.main_user WHERE (loginstable.loginid = @UserName)");
            cmd = new MySqlCommand("select * from OnlineTable where UserName=@UserName");
            cmd.Parameters.Add("@UserName", Username);
            DataTable vehupdate = vdm.SelectQuery(cmd).Tables[0];
            //cmd = new MySqlCommand("select * from PairedData where UserID=@UserName");
            DataTable Vehiclesdata = new DataTable();
            if (context.Session["vehiclesdata"] != null)
            {
                Vehiclesdata = (DataTable)context.Session["vehiclesdata"];
            }
            else
            {
                cmd = new MySqlCommand("SELECT loginstable.refno, loginstable.main_user, loginstable.loginid, loginstable.pwd, loginstable.usertype, loginsconfigtable.VehicleID  as VehicleNumber, paireddata.FullTankVal, paireddata.EmptyTankVal, paireddata.VehicleType, paireddata.FullTankLtrs, paireddata.emptyTankLrs FROM loginsconfigtable INNER JOIN loginstable ON loginsconfigtable.Refno = loginstable.refno INNER JOIN paireddata ON loginsconfigtable.VehicleID = paireddata.VehicleNumber WHERE (loginstable.loginid = @UserName)");
                cmd.Parameters.Add("@UserName", Username);
                Vehiclesdata = vdm.SelectQuery(cmd).Tables[0];
                vdm = null;
                context.Session["vehiclesdata"] = Vehiclesdata;
            }

            Dictionary<string, double> odometerValues = new Dictionary<string, double>();
            odometerValues = (Dictionary<string, double>)context.Session["odometerValues"];
            string OdometerRound = "";

            foreach (DataRow dr in vehupdate.Rows)
            {
                vehiclesupdateclass vehiclesupdate = new vehiclesupdateclass();
                string vehicleno = dr["VehicleID"].ToString();
                if (!vehicleno.Contains('/'))
                {
                    char[] charsToTrim = { ' ' };
                    vehicleno = vehicleno.Trim(charsToTrim);
                }
                else
                {
                    char[] charsToTrim = { '/' };
                    vehicleno = vehicleno.Remove(7, 1);
                }
                vehicleno = vehicleno.Replace(" ", "");
                vehiclesupdate.vehiclenum = vehicleno;
                vehiclesupdate.latitude = dr["Lat"].ToString();
                vehiclesupdate.longitude = dr["Longi"].ToString();
                vehiclesupdate.Speed = dr["Speed"].ToString();
                vehiclesupdate.Datetime = dr["Timestamp"].ToString();
                vehiclesupdate.dieselvalue = dr["Diesel"].ToString();
                if (odometerValues != null && odometerValues.Keys.Contains(dr["VehicleID"].ToString()))
                {
                    double prevodmrdng = odometerValues[dr["VehicleID"].ToString()];
                    if (prevodmrdng > 0)
                    {
                        double odometervalue = 0;
                        double.TryParse(dr["Odometer"].ToString(), out odometervalue);
                        double TotalDistancea = (odometervalue - prevodmrdng);
                        //////////OdometerRound = Math.Round(TotalDistancea, 2).ToString() + " KMs ";
                        OdometerRound = Math.Round(TotalDistancea, 2).ToString();
                    }
                    else
                    {
                        OdometerRound = 0 + " KMs ";
                    }
                }
                foreach (DataRow drR in Vehiclesdata.Rows)
                {
                    if (dr["VehicleID"].ToString() == drR["VehicleNumber"].ToString())
                    {
                        double.TryParse(drR["FullTankVal"].ToString(), out fulltankadc);
                        double.TryParse(drR["EmptyTankVal"].ToString(), out emptytankadc);
                        double.TryParse(drR["FullTankLtrs"].ToString(), out fulltankval);
                        double.TryParse(drR["emptyTankLrs"].ToString(), out emptytankval);
                        calculatedieseldivision(fulltankadc, emptytankadc, fulltankval, emptytankval);
                    }
                }
                double dieselvalue = 0;
                double.TryParse(dr["Diesel"].ToString(), out dieselvalue);
                double dieselstring = 0;
                try
                {
                    if (dieseldivistion != 0)
                    {
                        dieselstring = Math.Round((dieselvalue) / dieseldivistion, 2);

                        dieselstring = Math.Abs(dieselstring);
                    }
                }
                catch
                {
                }
                vehiclesupdate.dieselvalue = dieselstring.ToString();
                vehiclesupdate.fulltankval = fulltankval;
                vehiclesupdate.odometervalue = dr["Odometer"].ToString();
                vehiclesupdate.todaymileage = OdometerRound;
                vehiclesupdate.Ignation = dr["Ignation"].ToString();
                vehiclesupdate.ACStatus = dr["AC"].ToString();
                vehiclesupdate.Geofence = dr["Geofense"].ToString();
                vehiclesupdate.GPSSignal = dr["GPSSignal"].ToString();
                vehiclesupdate.mainpower = dr["EP"].ToString();
                vehiclesupdate.direction = dr["Direction"].ToString();
                vehiclesupdate.stoppedfor = dr["StoppedTime"].ToString();
                vehiclesupdatelist.Add(vehiclesupdate);

            }

            if (vehiclesupdatelist != null)
            {
                vehiclesupdateclasslist VDMlist = new vehiclesupdateclasslist();
                VDMlist.vehiclesupdatelist = vehiclesupdatelist;
                VDMlist.ServerDt = Currentdate.ToString("dd/MM/yyyy HH:mm:ss");
                string respnceString = GetJson(VDMlist);
                context.Response.Write(respnceString);
            }
        }

        private void InitilizeGroups(HttpContext context)
        {
            List<Groupsclass> Groupslist = new List<Groupsclass>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            string Username = context.Session["field1"].ToString();
            //string Username = context.Request["Username"];
            cmd = new MySqlCommand("SELECT GroupName, VehicleID, UserName FROM vehiclegroup WHERE (UserName = @UserName)");
            cmd.Parameters.Add("@UserName", Username);
            DataTable groups = vdm.SelectQuery(cmd).Tables[0];
            DataTable groupnames = groups.DefaultView.ToTable(true, "GroupName");
            vdm = null;
            foreach (DataRow group in groupnames.Rows)
            {
                Groupsclass groupsdict = new Groupsclass();
                List<string> vehicleslist = new List<string>();
                groupsdict.groupname = group["GroupName"].ToString();
                foreach (DataRow vehicles in groups.Rows)
                {
                    if (group["GroupName"].ToString() == vehicles["GroupName"].ToString())
                    {
                        string vehicleno = vehicles["VehicleID"].ToString();
                        char[] charsToTrim = { ' ' };
                        vehicleno = vehicleno.Trim(charsToTrim);
                        vehicleno = vehicleno.Replace(" ", "");
                        vehicleslist.Add(vehicleno);
                    }
                }
                groupsdict.vehicleno = vehicleslist;
                Groupslist.Add(groupsdict);
            }
            if (Groupslist != null)
            {
                string respnceString = GetJson(Groupslist);
                context.Response.Write(respnceString);
            }
        }

        private void ShowMyLocations(HttpContext context)
        {
            List<BranchData> Branchlist = new List<BranchData>();
            vdm = new VehicleDBMgr();
            vdm.InitializeDB();
            string Username = context.Request["field1"];
            cmd = new MySqlCommand("select * from BranchData where UserName=@UserName");
            //cmd = new MySqlCommand("select VehicleID from ManageData where UserName=@UserName");
            cmd.Parameters.Add("@UserName", Username);
            DataTable Branchdata = vdm.SelectQuery(cmd).Tables[0];
            vdm = null;
            foreach (DataRow dr in Branchdata.Rows)
            {
                BranchData Branch = new BranchData();
                Branch.BranchName = dr["BranchID"].ToString();
                Branch.latitude = dr["Latitude"].ToString();
                Branch.longitude = dr["Longitude"].ToString();
                Branch.Decription = dr["Description"].ToString();
                Branch.Image = dr["ImagePath"].ToString();
                Branchlist.Add(Branch);
            }
            if (Branchlist != null)
            {
                string respnceString = GetJson(Branchlist);
                context.Response.Write(respnceString);
            }
        }

        private static string GetJson(object obj)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(obj);
        }
    }
}