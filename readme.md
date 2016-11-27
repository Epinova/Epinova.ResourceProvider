Epinova.ResourceProvider
======

A provider for serving embedded content in Episerver solutions

Use this if you have
  * views in your module and don't want to clutter up the Views-folder in your site
  * views in your module, which needs to be included in non-web projects
  * language xml-files in your module and don't want to clutter up the lang-folder in you site.
  * language xml-files in your module, which needs to be included in non-web projects

Setup
-------------

In Application_Start or initialization module
```

// Serve xml language files
Epinova.ResourceProvider.Localization.Register(typeof(MyType).Assembly)

// Serve embedded views
Epinova.ResourceProvider.Views.Register(typeof(MyType).Assembly)

```

Localization example
-------------

Razor
```
@Html.Translate("/some/key/in/embedded/file.xml")
```

C#
```
LocalizationService.Current.GetString.Translate("/some/key/in/embedded/xmlfile")
```


Embedding example
-------------

The path will be equal to the path of the embedded file in your solution-structure

Views
```
return View("~/Views/My/Wonderful/View.cshtml");
```


Debugging
-------------
Set level=debug for namespace "Epinova.ResourceProvider" to get details about the registration process 