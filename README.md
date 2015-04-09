# Master-Page-Demo
A VB.Net WebForms project showing one approach to using master pages with wet-boew.

This is using the 
[Government of Canada (GC) Intranet theme for the Web Experience Toolkit](https://github.com/wet-boew/theme-gc-intranet).


The start page of the project should be set to home-accueil-eng.aspx.  The <urlMappings> section of the web.config file will 
translate this to home.aspx.  One should be able to cycle between English and French by using the language toggle on the 
top-right.

Each new page should inherit from BasePage instead of System.Web.UI.Page.
