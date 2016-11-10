Epinova.ResourceProvider
======

A VPP provider for serving embedded content

Use this if you 
  * have views in your module and don't want to clutter up the Views-folder in your site
  * have views in your module, which needs to be included in non-web projects
  * have language xml-files in your module and don't want to clutter up the lang-folder in you site.
  * have language xml-files in your module, which needs to be included in non-web projects

Usage:

In Application_Start or initialization module
```

// Xml files
Epinova.ResourceProvider.Localization.Register(AssemblyAssembly.GetAssembly(typeof(MyType)))

// Embedded resources
Epinova.ResourceProvider.Embedding.Register(Assembly.GetAssembly(typeof(MyType)), "cshtml", "txt")

```

Legacy mode. Don't use if not upgrading from older version
```
<configuration>
	<configSections>
		<section name="epinova.resourceprovider" type="Epinova.ResourceProvider.Configuration.ModuleSection, Epinova.ResourceProvider" restartOnExternalChanges="true" xdt:Transform="Insert" />
	</configSections>
</configuration>

<epinova.resourceprovider>   
    <providers>   
      <!-- Views -->   
	  <add assembly="MyAssembly.MyType" filetypes="cshtml" />   
      <!-- JS/CSS -->   
	  <add assembly="MyAssembly.MyType" filetypes="js,css" />   
      <!-- Xml lang files-->   
      <add assembly="MyAssembly.MyType" provideLocalization="true" />   
    </providers>   
  </epinova.resourceprovider>   
```

Debugging:

Set level=debug for namespace "Epinova.ResourceProvider" to get details about the registration process 