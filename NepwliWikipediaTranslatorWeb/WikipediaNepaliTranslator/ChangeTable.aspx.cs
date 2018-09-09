using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WikipediaNepaliTranslator
{
    public partial class ChangeTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            SqlDataSource1.ConnectionString =
                    @"Data Source=.\SQLExpress;Persist Security Info=True;Integrated Security=SSPI;Initial Catalog=Nepaliwikipedia_nwtdb";

            using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLExpress;Persist Security Info=True;Integrated Security=SSPI;Initial Catalog=Nepaliwikipedia_nwtdb"))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(this.dbField.InnerText);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                
                connection.Close();
            }
        }
    }
}