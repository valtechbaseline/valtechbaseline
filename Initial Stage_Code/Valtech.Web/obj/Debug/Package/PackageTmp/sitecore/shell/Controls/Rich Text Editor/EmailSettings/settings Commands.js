var scEditor = null;  
var scTool = null; 

//Set the Id of your button into the RadEditorCommandList[]  
//RadEditorCommandList["TestBtn"] = function (commandName, editor, args) 
Telerik.Web.UI.Editor.CommandList["TestBtn"]  = function (commandName, editor, args) 
{  

     var d = Telerik.Web.UI.Editor.CommandList._getLinkArgument(editor);  
    Telerik.Web.UI.Editor.CommandList._getDialogArguments(d, "A", editor, "DocumentManager");  
  
 //Retrieve the html selected in the editor  
    var html = editor.getSelectionHtml();  
  
    scEditor = editor;  
  
 //Call your custom dialog box  
    editor.showExternalDialog(  
  "/sitecore/shell/default.aspx?xmlcontrol=RichText.TestBtn&la=" + scLanguage,  
  null, //argument  
  600, //Height  
  600, //Width  
  scTestBtnCallback, //callback  
  null, // callback args  
  "TestBtn",  
  true, //modal  
  Telerik.Web.UI.WindowBehaviors.Close, // behaviors  
  false, //showStatusBar  
  false //showTitleBar  
   );  
};  

//The function called when the user close the dialog  
function scTestBtnCallback(sender, returnValue) {  
    if (!returnValue) {  
        return;  
    }  
   
 //You may retreive some code from your returnValue  
   
 //For the example I add Hello and my return value in the Rich Text  
    scEditor.pasteHtml(" " + returnValue.text, "DocumentManager");  
} 