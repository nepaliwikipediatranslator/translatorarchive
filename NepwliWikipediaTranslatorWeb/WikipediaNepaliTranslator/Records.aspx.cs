using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WikipediaNepaliTranslator
{
    public partial class Records : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["Delete"]))
            {
                DeleteThisById(Request.QueryString["Delete"]);
            }
            int start = 0;
            int pageSize = 10;
            
            if(!string.IsNullOrEmpty(Request.QueryString["start"]))
            {
                int.TryParse(Request.QueryString["start"], out start);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["show"]))
            {
                int.TryParse(Request.QueryString["show"], out pageSize);
            }

            DBDataContext db = new DBDataContext();
            this.GridView1.DataSource = db.Log.OrderByDescending(q => q.id)
                .Skip(start * pageSize).Take(pageSize)
                .Select(q =>
                new
                    {
                   ID = q.id,
                   Hindi = q.input,
                   Nepali = q.output,
                   Date = String.Format("{0:ddd hh:mm dd-MM-yy}", q.date),
                   Android = q.webservice,
                   Refferrer = q.referrer,
                   IP = q.ip,
                   Delete = string.Format("<a href=\"?Delete={0}\">Delete</a>",q.id)
                    });
//            this.GridView1.DataSource = db.Log.OrderByDescending(q => q.id);
                
            GridView1.DataBind();

            HyperLink link = new HyperLink();
            link.Text = "Next > ";
            link.NavigateUrl = string.Format("?start={0}&show={1}", start +1,pageSize);

            GridView1.Parent.Controls.Add(link);

        }

        private void DeleteThisById(string id)
        {
            int logId;
            int.TryParse(id,out logId);
            if(logId>0)
            {
                DBDataContext db = new DBDataContext();
                IQueryable<Log>  logsForDeletion = db.Log.Where(q => q.id == logId);
                db.Log.DeleteAllOnSubmit(logsForDeletion);
                db.SubmitChanges();
            }
            
        }

        
    }
}