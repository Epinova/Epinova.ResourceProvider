Epinova.ResourceProvider
======

A VPP provider for serving embedded content

Use this if you 

  * have views in your module and don't want to clutter up the Views-folder in you site.
  * have language xml-files in your module and don't want to clutter up the lang-folder in you site.

Example:

`
<epinova.resourceprovider xdt:Transform="InsertIfMissing">  
    <providers>  
      <!-- Views -->  
	  <add assembly="Epinova.Foo" filetypes="cshtml" />  
      <!-- JS/CSS -->  
	  <add assembly="Epinova.Bar" filetypes="js,css" />  
      <!-- Xml lang files-->  
      <add assembly="Epinova.Baz" provideLocalization="true" />  
    </providers>  
  </epinova.resourceprovider>  
`