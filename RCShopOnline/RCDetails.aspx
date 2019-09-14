<%@ Page Title="RC Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RCDetails.aspx.cs" Inherits="RCShopOnline.RCDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:FormView ID="rcDetail" runat="server" ItemType="RCShopOnline.Models.RC" SelectMethod ="GetDetails"                     
         RenderOuterTable="false"> 
 <ItemTemplate> 
       <div>                 
           <h1><%#:Item.RCName %></h1> 
            </div>
       <br />             
     <table>
          <tr>                     
              <td>                         
                  <img src="/Images/<%#:Item.ImagePath %>"                             
                      style="border:solid; height:225px" alt="<%#:Item.RCName %>"/>                     
              </td>                     
              <td>&nbsp;</td>                     
              <td style="vertical-align: top; text-align:left;">                         
                  <b>Description:</b><br /><%#:Item.Description %>                         
                  <br/>                         
                  <span><b>Price:</b>&nbsp;<%#: String.Format("{0:c}",Item.UnitPrice) %></span>                         
                  <br />                         
                  <span><b>RC Number:</b>&nbsp;<%#:Item.RCID %></span>                         
                  <br/>                     
              </td>                 
          </tr>   
         </table> 
      </ItemTemplate> 
 </asp:FormView> 
</asp:Content>
