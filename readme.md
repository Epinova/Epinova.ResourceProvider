# Epinova.ResourceProvider

A provider for serving embedded content in Episerver solutions

Supports Episerver 7.17+

Use this if you have
  * views in your module and don't want to clutter up the Views-folder in your site
  * views in your module, which needs to be included in non-web projects
  * language xml-files in your module and don't want to clutter up the lang-folder in you site.
  * language xml-files in your module, which needs to be included in non-web projects
  * handlers you want share amongst libraries (aspx, ashx, ascx)

### Breaking changes v8.0
  * All config-based setup removed
  * All registration moved to a common entry-point (`Register.Assembly()`)
  * Previously `[Obsolete]` code removed


### Setup

In Application_Start or initialization module
```
// Serve xml language files
Epinova.ResourceProvider.Register.Assembly<MyType>(ResourceType.Xml)

// Serve embedded views
Epinova.ResourceProvider.Register.Assembly<MyType>(ResourceType.View)

// Serve embedded handlers
Epinova.ResourceProvider.Register.Assembly<MyType>(ResourceType.Handler)

// Serve all of the above
Epinova.ResourceProvider.Register.Assembly<MyType>(ResourceType.All)
```


### Localization example

Razor
```
@Html.Translate("/some/key/in/embedded/file.xml")
```

C#
```
LocalizationService.Current.GetString.Translate("/some/key/in/embedded/xmlfile")
```


### Views example

The path will be equal to the path of the embedded file in your solution-structure

Views
```
return View("~/Views/My/Wonderful/View.cshtml");
```


### Debugging
Set level=debug for namespace "Epinova.ResourceProvider" to get details about the registration process 