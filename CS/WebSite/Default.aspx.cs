using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPivotGrid;
using System.Drawing;
using DevExpress.XtraPivotGrid.Data;
using DevExpress.XtraPivotGrid;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxPivotGrid1.FieldValueTemplate = new FieldValueTemplate();
        ASPxPivotGrid1.CellTemplate = new CellTemplate();
    }
    class FieldValueTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            PivotGridFieldValueTemplateContainer c = (PivotGridFieldValueTemplateContainer)container;
            PivotGridFieldValueHtmlCell cell = c.CreateFieldValue();            
            PivotFieldValueItem valueItem = c.ValueItem;
            PivotDrillDownDataSource ds = valueItem.CreateDrillDownDataSource();
            int id = Convert.ToInt32(ds[0]["ProductID"]);            
            cell.Controls.AddAt(cell.Controls.IndexOf(cell.TextControl), new MyLink(c.Text, id));
            cell.Controls.Remove(cell.TextControl);
            c.Controls.Add(cell);
        }
    }

    class CellTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            PivotGridCellTemplateContainer c = container as PivotGridCellTemplateContainer;
            PivotDrillDownDataSource ds = c.Item.CreateDrillDownDataSource();
            int id = Convert.ToInt32(ds[0]["ProductID"]);
            c.Controls.Add(new MyLink(c.Text, id));
        }
    }
    
    
    public class MyLink : HyperLink
    {
        public MyLink(string text, int id) : base()
        {
            Text = text;
            ToolTip = id.ToString();
            NavigateUrl = "#";
            //NavigateUrl = "http://myserver.com/somepage.aspx?id=" + id;
            Attributes["onclick"] = "alert(' " + id + " ')";
        }
    }

}
