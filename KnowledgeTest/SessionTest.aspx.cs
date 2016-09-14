using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KnowledgeTest
{
    public partial class SessionTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestClass1 tc = Session["Test1"] as TestClass1;
            if(tc==null)
            {
                Session["Test1"] = new TestClass1(){ Id=1,Name="wersdf"};
                tc = Session["Test1"] as TestClass1;
            }
        }
    }
    public class TestClass1
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}