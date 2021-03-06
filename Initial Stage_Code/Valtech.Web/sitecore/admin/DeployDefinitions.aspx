﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeployDefinitions.aspx.cs" Inherits="Sitecore.Modules.EmailCampaign.DeployDefinitions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Email Campaign deploy definitions tool</title>
  <link href="/sitecore/shell/themes/standard/default/WebFramework.css" rel="Stylesheet" />
  <link href="/sitecore/admin/Wizard/UpdateInstallationWizard.css" rel="Stylesheet" />

  <script type="text/javascript" src="/sitecore/shell/Controls/lib/jQuery/jquery.js"></script>
  <script type="text/javascript" src="/sitecore/shell/controls/webframework/webframework.js"></script>
</head>
<body>
  <form id="form1" class="wf-container" runat="server">
    <asp:ScriptManager runat="server" EnablePartialRendering="True"></asp:ScriptManager>
    <div id="divUpgradeTool">
      <div class="wf-content">
        <h1 runat="server" id="lblHeader">Email Campaign deploy definitions tool
        </h1>
        <asp:UpdatePanel runat="server">
          <contenttemplatecontainer>
                    <div class="wf-statebox wf-statebox-error" style="margin: 2em 0; display: none" runat="server" id="ErrorContainer">
                            <asp:Repeater runat="server" ID="ErrorMessages">
                                <ItemTemplate>
                                    <p class="Error"><%#Container.DataItem.ToString() %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>

                    <div class="wf-statebox wf-statebox-warning" style="margin: 2em 0; display: none" runat="server" id="WarningContainer">
                            <asp:Repeater runat="server" ID="WarningMessages">
                                <ItemTemplate>
                                    <p><%#Container.DataItem.ToString() %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>
                </contenttemplatecontainer>
        </asp:UpdatePanel>
      </div>
      <div class="wf-footer">
        <div id="ActionElementsContainer" runat="server">
          <asp:UpdatePanel ID="upLogArea" runat="server" UpdateMode="Conditional">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="PerformStep" EventName="Click" />
            </Triggers>
            <contenttemplatecontainer>
                        <div style="border: solid 1px #ccc;">
                            <iframe id="LogArea" runat="server" scrolling="auto" src="" class="wf-progress-details"></iframe>
                        </div>
                    </contenttemplatecontainer>
          </asp:UpdatePanel>
          <asp:UpdatePanel runat="server" UpdateMode="Always">
            <contenttemplatecontainer>
                        <asp:Button runat="server" ID="PerformStep" />
                        <asp:Button runat="server" ID="RefreshPanels" style="display: none" />
                        <div class="wf-statebox wf-statebox-success" id="SuccessMessage" runat="server" Visible="False">
                            <p style="margin: 0; font-weight:bold;">
                                The migrating data to ECM 2.2 was successful.
                            </p>
                            <p>
                                Please refer to the documentation to complete the post installation steps
                            </p>
                        </div>
                        <div class="wf-statebox wf-statebox-error" id="FailedMessage" runat="server" Visible="False">
                            <p style="margin: 0; font-weight:bold;">
                                The migrating data to ECM 2.2 failed.
                            </p>
                            <p>
                                Please look to logs to find out what has happened.
                            </p>
                        </div>
                    </contenttemplatecontainer>
          </asp:UpdatePanel>
        </div>
      </div>
    </div>
  </form>
</body>
</html>

