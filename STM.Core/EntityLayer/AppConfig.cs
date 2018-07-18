using System;
using System.Data;
using System.Configuration;
using System.Web;
/*using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;*/

/// <summary>
/// Summary description for AppConfig
/// </summary>
public class AppConfig
{
	public AppConfig()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Get Connection String Value

    /*Sub Header***********************************************************
   Function Name: GetConnectionString
   
    Functionality: Return the Connection string value from web.config file
    Input: 
    Output: Connection string value
    Note: Any Special comment
    *********************************************************************/
    public static string GetConnectionString()
    {
        //return ConfigurationManager.ConnectionStrings["constr"].ConnectionString.ToString();
        return "server = localhost; database = postgres; user id = postgres; password = 9618;";
    }
    public static string GetConnectionStringps()
    {
        //  return ConfigurationManager.ConnectionStrings["constr1"].ConnectionString.ToString();
        return "server = localhost; database = postgres; user id = postgres; password = 9618;";
    }


    #endregion


}
