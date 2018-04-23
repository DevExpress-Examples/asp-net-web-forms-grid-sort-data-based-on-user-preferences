using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Init(object sender, EventArgs e) {
        if (Session["dataSource"] == null)
            Session["dataSource"] = GetDataSource();
        ASPxGridView1.DataSource = Session["dataSource"];
        ASPxGridView1.DataBind();
    }
    protected void txtLB_Init(object sender, EventArgs e) {
        Dictionary<object, int> lowerBoundStorage = Session["lowerBoundStorage"] as Dictionary<object, int>;
        if (lowerBoundStorage != null) {
            ASPxTextBox textBox = (ASPxTextBox)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)textBox.NamingContainer;
            object key = templateContainer.KeyValue;
            if (lowerBoundStorage.ContainsKey(key))
                textBox.Value = lowerBoundStorage[key];
        }
    }
    protected void ASPxGridView1_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs e) {
        if (e.Column.FieldName == "LowerBound") {
            Dictionary<object, int> lowerBoundStorage = Session["lowerBoundStorage"] as Dictionary<object, int>;
            if (lowerBoundStorage == null) {
                lowerBoundStorage = new Dictionary<object, int>();
                Session["lowerBoundStorage"] = lowerBoundStorage;
            }
            object key = e.GetListSourceFieldValue(e.ListSourceRowIndex, "CategoryID");
            if (lowerBoundStorage.ContainsKey(key))
                e.Value = lowerBoundStorage[key];
            else
                e.Value = 0;
        }
    }
    protected void ASPxButton1_Click(object sender, EventArgs e) {
        Dictionary<object, int> lowerBoundStorage = Session["lowerBoundStorage"] as Dictionary<object, int>;
        if (lowerBoundStorage == null)
            lowerBoundStorage = new Dictionary<object, int>();

        int startIndex = ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int endIndex = Math.Min(ASPxGridView1.VisibleRowCount, startIndex + ASPxGridView1.SettingsPager.PageSize);

        for (int i = startIndex; i < endIndex; i++) {
            ASPxTextBox txtLowerBound = (ASPxTextBox)ASPxGridView1.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1.Columns["LowerBound"], "txtLB");
            int lowerBound = int.Parse(txtLowerBound.Text.Trim());
            object key = ASPxGridView1.GetRowValues(i, "CategoryID");
            if (!lowerBoundStorage.ContainsKey(key))
                lowerBoundStorage.Add(key, lowerBound);
            else
                lowerBoundStorage[key] = lowerBound;
        }
        Session["lowerBoundStorage"] = lowerBoundStorage;
    }
    private DataTable GetDataSource() {
        DataTable dataTable;

        using (OleDbConnection connection = new OleDbConnection()) {
            connection.ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", MapPath("~/App_Data/nwind.mdb"));

            dataTable = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
            adapter.SelectCommand.CommandText = "SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]";
            adapter.Fill(dataTable);
        }

        return dataTable;
    }
}