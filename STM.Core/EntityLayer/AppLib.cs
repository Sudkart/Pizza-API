using System;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
/*using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;*/
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.ComponentModel;
using Npgsql;
using STM.Core;
using byMarc.Net2.Library.ListItems;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// Summary description for AppLib
/// </summary>
/// 

public delegate void dlgAfterMasterLoad();
public  class AppLib
{
    static string strQuery = "";
    public AppLib()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Bind DropDownList with stored procedure with parameters
    /*Sub Header***********************************************************
   Function Name: BindDropDownListWithDatabaseSP
   
    Functionality: Bind DropDownList With stored procedure
    Input: DropDownList refference,stored procedure name,initial text,data field,value field,Parameters
    Output: 
    Note: Any Special comment
    *********************************************************************/
   /* public static void BindDropDownListWithDatabaseSP(ref DropDownList ddlDropDownListNameToBind, string strProcedureName, string strInitialText, string strDataTextField, string strDataValueField, SqlParameter[] arrParam)
    {
        SqlDataReader reader = SqlHelper.ExecuteReader(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName, arrParam);
        ddlDropDownListNameToBind.DataTextField = strDataTextField;
        ddlDropDownListNameToBind.DataValueField = strDataValueField;
        ddlDropDownListNameToBind.DataSource = reader;
        ddlDropDownListNameToBind.DataBind();
        reader.Close();
        reader.Dispose();
        if (strInitialText.Trim() != "")
        {
            ListItem lstInitialItem = new ListItem(strInitialText, "-1");
            ddlDropDownListNameToBind.Items.Insert(0, lstInitialItem);
        }

    }



    public static void BindDropDownListWithDatabaseQuery(ref DropDownList ddlDropDownListNameToBind, string strProcedureName, string strInitialText, string strDataTextField, string strDataValueField)
    {
        SqlDataReader reader = SqlHelper.ExecuteReader(AppConfig.GetConnectionString(), CommandType.Text, strProcedureName);
        ddlDropDownListNameToBind.DataTextField = strDataTextField;
        ddlDropDownListNameToBind.DataValueField = strDataValueField;
        ddlDropDownListNameToBind.DataSource = reader;
        ddlDropDownListNameToBind.DataBind();
        reader.Close();
        reader.Dispose();
        if (strInitialText.Trim() != "")
        {
            ListItem lstInitialItem = new ListItem(strInitialText, "-1");
            ddlDropDownListNameToBind.Items.Insert(0, lstInitialItem);
        }

    }

    public static void BindDropDownListWithDatabaseQueryps(ref DropDownList ddlDropDownListNameToBind, string strProcedureName, string strInitialText, string strDataTextField, string strDataValueField)
    {
        NpgsqlDataReader reader = SqlHelper.ExecuteReaderps(AppConfig.GetConnectionStringps(), CommandType.Text, strProcedureName);
        ddlDropDownListNameToBind.DataTextField = strDataTextField;
        ddlDropDownListNameToBind.DataValueField = strDataValueField;
        ddlDropDownListNameToBind.DataSource = reader;
        ddlDropDownListNameToBind.DataBind();
        reader.Close();
        reader.Dispose();
        if (strInitialText.Trim() != "")
        {
            ListItem lstInitialItem = new ListItem(strInitialText, "-1");
            ddlDropDownListNameToBind.Items.Insert(0, lstInitialItem);
        }

    }
    


    public static void BindDropDownListWithDatabaseQueryOption(ref DropDownList ddlDropDownListNameToBind, string strProcedureName, string strInitialText, string strDataTextField, string strDataValueField)
    {
        SqlDataReader reader = SqlHelper.ExecuteReader(AppConfig.GetConnectionString(), CommandType.Text, strProcedureName);
        ddlDropDownListNameToBind.DataTextField = strDataTextField;
        ddlDropDownListNameToBind.DataValueField = strDataValueField;
        ddlDropDownListNameToBind.DataSource = reader;
        ddlDropDownListNameToBind.DataBind();
        reader.Close();
        reader.Dispose();
        if (strInitialText.Trim() != "")
        {
            ListItem lstInitialItem = new ListItem(strInitialText, "-1");
            ddlDropDownListNameToBind.Items.Insert(0, lstInitialItem);
        }

    }
    */



    #endregion

    #region Bind DropDownList with stored procedure
    /*Sub Header***********************************************************
   Function Name: BindDropDownListWithDatabaseSPNoParam
   
    Functionality: Bind DropDownList With stored procedure
    Input: DropDownList refference,stored procedure name,initial text,data field,value field
    Output: 
    Note: Any Special comment
    *********************************************************************/
    /*public static void BindDropDownListWithDatabaseSPNoParam(ref DropDownList ddlDropDownListNameToBind, string strProcedureName, string strInitialText, string strDataTextField, string strDataValueField)
    {
        SqlDataReader reader = SqlHelper.ExecuteReader(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName);
        ddlDropDownListNameToBind.DataTextField = strDataTextField;
        ddlDropDownListNameToBind.DataValueField = strDataValueField;
        ddlDropDownListNameToBind.DataSource = reader;
        ddlDropDownListNameToBind.DataBind();
        reader.Close();
        reader.Dispose();
        if (strInitialText.Trim() != "")
        {
            ListItem lstInitialItem = new ListItem(strInitialText, "-1");
            ddlDropDownListNameToBind.Items.Insert(0, lstInitialItem);
        }
    }
    */
    #endregion

    #region Get Date Table By Query
    /*Sub Header***********************************************************
   Function Name: GetDataTableByQuery
   
    Functionality: Get DataTable By Query
    Input: string query
    Output: Datatable
    Note: Any Special comment
    *********************************************************************/
    public static DataTable GetDataTableByQuery(string sQuery)
    {
        DataSet ds = new DataSet();

        ds = SqlHelper.ExecuteDatasetps(AppConfig.GetConnectionString(), CommandType.Text, sQuery);
        return ds.Tables[0];
    }

    #endregion


    #region Get Date Table By Query in PostGres
    /*Sub Header***********************************************************
   Function Name: GetDataTableByQuery
   
    Functionality: Get DataTable By Query
    Input: string query
    Output: Datatable
    Note: Any Special comment
    *********************************************************************/
    public static DataTable GetDataTableByQueryps(string sQuery)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.Text, sQuery);
        ds = SqlHelper.ExecuteDatasetps(AppConfig.GetConnectionStringps(), CommandType.Text, sQuery);

        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }

        else
            return dt;
    }


    public static List<T> DataReaderMapToList<T>(string connctionStr, string sQuery)
    {
        // Connect to a PostgreSQL database
        NpgsqlConnection conn = new NpgsqlConnection(connctionStr);
        conn.Open();

        // Define a query
        NpgsqlCommand command = new NpgsqlCommand(sQuery, conn);

        // Execute the query and obtain a result set
        NpgsqlDataReader dr = command.ExecuteReader();

        List<T> list = new List<T>();
        T obj = default(T);
        while (dr.Read())
        {
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (!object.Equals(dr[prop.Name], DBNull.Value))
                {
                    prop.SetValue(obj, dr[prop.Name], null);
                }
            }
            list.Add(obj);
        }
        return list;
    }

    #endregion
    #region Get Data Table from stored procedure
    /*Sub Header***********************************************************
   Function Name: GetDataTableBySP
   
    Functionality: Get DataTable By stored procedur
    Input: stored procedure name
    Output: Datatable
    Note: Any Special comment
    *********************************************************************/
    public static DataTable GetDataTableBySP(string strProcedureName)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName);
        return ds.Tables[0];
    }

    public static DataTable GetDataTableBySPPS(string strProcedureName)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDatasetps(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName);
        return ds.Tables[0];
    }
    #endregion


    #region Get String Query String Value
    /*Sub Header***********************************************************
   Function Name: GetQueryStringParameterValue_String
   
    Functionality: Get QueryStringValue
    Input: Parameter Name
    Output: 
    Note: Any Special comment
    *********************************************************************/
 /*   public static string GetQueryStringParameterValue_String(string strParameterName)
    {

        return Convert.ToString((HttpContext.Current.Request.Params[strParameterName] + "") == "" ? "" : HttpContext.Current.Request.Params[strParameterName].ToString().Trim());
    }
    */
    #endregion

    #region Get Integer Query String value
    /*Sub Header***********************************************************
   Function Name: GetQueryStringParameterValue_Int
   
    Functionality: Get QueryString Value
    Input: Parameter Name
    Output: 
    Note: Any Special comment
    *********************************************************************/
    /*public static int GetQueryStringParameterValue_Int(string strParameterName)
    {
        return Convert.ToInt32((HttpContext.Current.Request.Params[strParameterName] + "") == "" ? "0" : HttpContext.Current.Request.Params[strParameterName].ToString().Trim());
    }*/
    #endregion

    #region Get Integer Session Value
    /*Sub Header***********************************************************
   Function Name: GetSessionValue_Int
   
    Functionality: Get Session Value
    Input: Parameter Name
    Output: 
    Note: Any Special comment
    *********************************************************************/
    public static int GetSessionValue_Int(string strParameterName)
    {
        int intRetVal = 0;

       /* if (HttpContext.Current.Session[strParameterName] != null)
        {
            intRetVal = Convert.ToInt32(HttpContext.Current.Session[strParameterName]);
        }*/
        return intRetVal;
    }
    #endregion

    #region Get String Session value
    /*Sub Header***********************************************************
   Function Name: GetSessionValue_String
   
    Functionality: Get Session Value
    Input: Parameter Name
    Output: 
    Note: Any Special comment
    *********************************************************************/
    public static string GetSessionValue_String(string strParameterName)
    {
        /*if (HttpContext.Current.Session[strParameterName] != null)
        {
            return Convert.ToString(HttpContext.Current.Session[strParameterName]);
        }
        else
        {
            return "";
        }*/
        return "";
    }
    #endregion
    public static string GetUniqueKey()
    {
        int maxSize = 8;
        int minSize = 5;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length)]); }
        return result.ToString();
    }


    #region Get DataTable By Stored Procedure With Parameter
    /*Sub Header***********************************************************
   Function Name: GetDataTableBySPWithParam
   
    Functionality: Get DataTable By Stored Procedure With Parameter
    Input: Stored Procedure Name ,Array of SQL Parameter
    Output: 
    Note: Any Special comment
    *********************************************************************/
    public static DataTable GetDataTableBySPWithParam(string strProcedureName, SqlParameter[] arrParam)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName, arrParam);
        return ds.Tables[0];

    }
    public static DataSet GetDataSetBySPWithParam(string strProcedureName, SqlParameter[] arrParam)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName, arrParam);
        return ds;

    }
    public static DataSet GetDataSetBySP(string strProcedureName)
    {
        DataSet ds = new DataSet();
        ds = SqlHelper.ExecuteDataset(AppConfig.GetConnectionString(), CommandType.StoredProcedure, strProcedureName);
        return ds;

    }
    #endregion


    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
   
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    #region Send Mail
    /*Sub Header***********************************************************
   Function Name: SendMailToUser
   
    Functionality: Send Mail To User
    Input: MailTo,MailFrom,Email Body,Email subject
    Output: 
    Note: Any Special comment
    *********************************************************************/
   
    public static string GetExtension(string FileName)
    {
        string[] split = FileName.Split('.');
        string Extension = split[split.Length - 1];
        return Extension;
    }
   
    public static void SendMail(string mailTo, string emailSubject, string emailBody, string mailFrom)
    {
        try
        {
            //emailBody = emailBody.Replace("[SitePath]", AppConfig.GetBaseSiteUrl());
            SmtpClient SmtpMail = new SmtpClient();
            MailMessage myMail = new MailMessage();
            System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            myMail.SubjectEncoding = myEncoding;
            myMail.BodyEncoding = myEncoding;
            myMail.From = (new MailAddress(mailFrom.ToString()));
            myMail.To.Add(mailTo.ToString());
            myMail.Subject = emailSubject;
            myMail.Priority = MailPriority.Normal;
            myMail.IsBodyHtml = true;
            myMail.Body = emailBody;
            SmtpMail.Host = "smtp.office365.com"; //smtpout.secureserver.net
            SmtpMail.Port = 587;
            //authenticate
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(mailFrom, "");
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = SMTPUserInfo;
            SmtpMail.EnableSsl = true;
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
           // SmtpMail.Send(myMail);
            Thread bgThread = new Thread(new ParameterizedThreadStart(SendEmail));
            bgThread.IsBackground = true;
            bgThread.Start(myMail);
            
        }
        catch (Exception exc)
        {
            string msg;
            //msg = Resources.Resource.strErrorMSGTryAgain;
            //throw new RentAlpsException(msg, exc);
        }
    }
    public static void SendEmail(Object mailMsg)
    {
        MailMessage mailMessage = (MailMessage)mailMsg;
        try
        {
            /* Setting should be kept somewhere so no need to 
               pass as a parameter (might be in web.config)       */
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            NetworkCredential networkCredential = new NetworkCredential();
            networkCredential.UserName = mailMessage.From.ToString();
            networkCredential.Password = "";
            smtpClient.Credentials = networkCredential;
            smtpClient.Port = 587;

            //If you are using gmail account then
            smtpClient.EnableSsl = true;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
        }
        catch (SmtpException ex)
        {
            // Code to Log error
        }
    }
    public void SendEmailInBackgroundThread(MailMessage mailMessage)
    {
        Thread bgThread = new Thread(new ParameterizedThreadStart(SendEmail));
        bgThread.IsBackground = true;
        bgThread.Start(mailMessage);
    }
    public static void SendMailByGrid(string mailTo, string emailSubject, string emailBody, string mailFrom)
    {
        try
        {
            //emailBody = emailBody.Replace("[SitePath]", AppConfig.GetBaseSiteUrl());
           

            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(mailTo));

            // From
            mailMsg.From = new MailAddress(mailFrom);

            // Subject and multipart/alternative Body
            mailMsg.Subject =emailSubject;
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = emailBody;
            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailFrom, "");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }
        catch (Exception exc)
        {
            string msg;
            //msg = Resources.Resource.strErrorMSGTryAgain;
            //throw new RentAlpsException(msg, exc);
        }
    }

    #endregion

    
}
