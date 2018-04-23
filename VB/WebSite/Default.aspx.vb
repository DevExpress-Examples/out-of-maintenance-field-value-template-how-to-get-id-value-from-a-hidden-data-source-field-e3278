Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxPivotGrid
Imports System.Drawing
Imports DevExpress.XtraPivotGrid.Data
Imports DevExpress.XtraPivotGrid


Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ASPxPivotGrid1.FieldValueTemplate = New FieldValueTemplate()
        ASPxPivotGrid1.CellTemplate = New CellTemplate()
    End Sub
    Private Class FieldValueTemplate
        Implements ITemplate

        Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
            Dim c As PivotGridFieldValueTemplateContainer = CType(container, PivotGridFieldValueTemplateContainer)
            Dim cell As PivotGridFieldValueHtmlCell = c.CreateFieldValue()
            Dim valueItem As PivotFieldValueItem = c.ValueItem
            Dim ds As PivotDrillDownDataSource = valueItem.CreateDrillDownDataSource()
            Dim id As Integer = Convert.ToInt32(ds(0)("ProductID"))
            cell.Controls.AddAt(cell.Controls.IndexOf(cell.TextControl), New MyLink(c.Text, id))
            cell.Controls.Remove(cell.TextControl)
            c.Controls.Add(cell)
        End Sub
    End Class

    Private Class CellTemplate
        Implements ITemplate

        Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
            Dim c As PivotGridCellTemplateContainer = TryCast(container, PivotGridCellTemplateContainer)
            Dim ds As PivotDrillDownDataSource = c.Item.CreateDrillDownDataSource()
    If ds.RowCount > 0 Then
            Dim id As Integer = Convert.ToInt32(ds(0)("ProductID"))
            c.Controls.Add(New MyLink(c.Text, id))
    End If
        End Sub
    End Class


    Public Class MyLink
        Inherits HyperLink

        Public Sub New(ByVal text As String, ByVal id As Integer)
            MyBase.New()
            Me.Text = text
            ToolTip = id.ToString()
            NavigateUrl = "#"
            'NavigateUrl = "http://myserver.com/somepage.aspx?id=" + id;
            Attributes("onclick") = "alert(' " & id & " ')"
        End Sub
    End Class

End Class
