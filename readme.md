Epinova.ResourceProvider
======

A provider for serving embedded content in Episerver solutions

Use this if you have
  * views in your module and don't want to clutter up the Views-folder in your site
  * views in your module, which needs to be included in non-web projects
  * language xml-files in your module and don't want to clutter up the lang-folder in you site.
  * language xml-files in your module, which needs to be included in non-web projects

Usage
-------------

In Application_Start or initialization module
```

// Xml files
Epinova.ResourceProvider.Localization.Register(AssemblyAssembly.GetAssembly(typeof(MyType)))

// Embedded resources
Epinova.ResourceProvider.Embedding.Register(Assembly.GetAssembly(typeof(MyType)), "cshtml", "txt")

```

Debugging
-------------

Set level=debug for namespace "Epinova.ResourceProvider" to get details about the registration process 