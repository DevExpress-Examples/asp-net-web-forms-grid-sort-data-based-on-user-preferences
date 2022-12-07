Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports DevExpress.Web

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        If Session("dataSource") Is Nothing Then
            Session("dataSource") = GetDataSource()
        End If
        ASPxGridView1.DataSource = Session("dataSource")
        ASPxGridView1.DataBind()
    End Sub
    Protected Sub txtLB_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim lowerBoundStorage As Dictionary(Of Object, Integer) = TryCast(Session("lowerBoundStorage"), Dictionary(Of Object, Integer))
        If lowerBoundStorage IsNot Nothing Then
            Dim editor As ASPxSpinEdit = DirectCast(sender, ASPxSpinEdit)
            Dim templateContainer As GridViewDataItemTemplateContainer = CType(editor.NamingContainer, GridViewDataItemTemplateContainer)
            Dim key As Object = templateContainer.KeyValue
            If lowerBoundStorage.ContainsKey(key) Then
                editor.Value = lowerBoundStorage(key)
            End If
        End If
    End Sub
    Protected Sub ASPxGridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "LowerBound" Then
            Dim lowerBoundStorage As Dictionary(Of Object, Integer) = TryCast(Session("lowerBoundStorage"), Dictionary(Of Object, Integer))
            If lowerBoundStorage Is Nothing Then
                lowerBoundStorage = New Dictionary(Of Object, Integer)()
                Session("lowerBoundStorage") = lowerBoundStorage
            End If
            Dim key As Object = e.GetListSourceFieldValue(e.ListSourceRowIndex, "CategoryID")
            If lowerBoundStorage.ContainsKey(key) Then
                e.Value = lowerBoundStorage(key)
            Else
                e.Value = 0
            End If
        End If
    End Sub
    Protected Sub ASPxButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lowerBoundStorage As Dictionary(Of Object, Integer) = TryCast(Session("lowerBoundStorage"), Dictionary(Of Object, Integer))
        If lowerBoundStorage Is Nothing Then
            lowerBoundStorage = New Dictionary(Of Object, Integer)()
        End If

        Dim startIndex As Integer = ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(ASPxGridView1.VisibleRowCount, startIndex + ASPxGridView1.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            Dim txtLowerBound As ASPxTextBox = CType(ASPxGridView1.FindRowCellTemplateControl(i, CType(ASPxGridView1.Columns("LowerBound"), GridViewDataColumn), "txtLB"), ASPxTextBox)
            Dim lowerBound As Integer = Integer.Parse(txtLowerBound.Text.Trim())
            Dim key As Object = ASPxGridView1.GetRowValues(i, "CategoryID")
            If Not lowerBoundStorage.ContainsKey(key) Then
                lowerBoundStorage.Add(key, lowerBound)
            Else
                lowerBoundStorage(key) = lowerBound
            End If
        Next i
        Session("lowerBoundStorage") = lowerBoundStorage
    End Sub
    Private Function GetDataSource() As DataTable
        Dim dataTable As DataTable

        Using connection As New OleDbConnection()
            connection.ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", MapPath("~/App_Data/nwind.mdb"))

            dataTable = New DataTable()


            Dim adapter_Renamed As New OleDbDataAdapter(String.Empty, connection)
            adapter_Renamed.SelectCommand.CommandText = "SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]"
            adapter_Renamed.Fill(dataTable)
        End Using

        Return dataTable
    End Function
End Class