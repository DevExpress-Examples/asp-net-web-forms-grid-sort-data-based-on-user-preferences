<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="CategoryID" OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData">
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="LowerBound" VisibleIndex="3" UnboundType="Integer">
                    <DataItemTemplate>
                         <dxe:ASPxSpinEdit ID="txtLB" runat="server" Width="60px" OnInit="txtLB_Init">
                            <SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false">
                            </SpinButtons>
                        </dxe:ASPxSpinEdit>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Save" OnClick="ASPxButton1_Click">
    </dxe:ASPxButton>
    </form>
</body>
</html>