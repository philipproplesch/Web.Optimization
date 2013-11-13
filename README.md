Web.Optimization
================

Web.Optimization allows you to transform LESS and CoffeeScript files by making use of the upcoming bundling and minification features.

## Installation

* Get the packages via NuGet
  * [Install-Package Web.Optimization](https://nuget.org/packages/Web.Optimization)
  * [Install-Package Web.Optimization.Bundles.CoffeeScript](https://nuget.org/packages/Web.Optimization.Bundles.CoffeeScript)
  * [Install-Package Web.Optimization.Bundles.Less](https://nuget.org/packages/Web.Optimization.Bundles.Less)
  * [Install-Package Web.Optimization.Bundles.Sass](https://nuget.org/packages/Web.Optimization.Bundles.Sass)
  * [Install-Package Web.Optimization.Bundles.AjaxMin](https://nuget.org/packages/Web.Optimization.Bundles.AjaxMin)
  * [Install-Package Web.Optimization.Bundles.YUI](https://nuget.org/packages/Web.Optimization.Bundles.YUI)
* Clone or download the code from GitHub => Build the solution => Add references to your project.

## Usage - Bundles (CoffeeScript, LESS & SASS/SCSS)

	protected void Application_Start()
	{
		// ...
		
		// Register CoffeeScript files
		
		var scripts = new Bundle(
            "~/Content/coffee", 
            new CoffeeScriptTransform().Then(new JsMinify()));
	
		instance.Include(
            "~/Scripts/first.coffee",
	        "~/Scripts/second.coffee",
	        "~/Scripts/third.coffee");
	
		BundleTable.Bundles.Add(scripts);
		
		
		// Register LESS files
		
	    var styles = new Bundle(
            "~/Content/less",
            new LessTransform().Then(new CssMinify()));
	    
		styles.Include(
		    "~/Content/first.less", 
		    "~/Content/second.less", 
		    "~/Content/third.less");
	
		BundleTable.Bundles.Add(styles);
	}

## Usage in \App_Start\BundleConfig.cs
```c#
// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
public static void RegisterBundles(BundleCollection bundles)
{
    // Coffee
    bundles.Add(new CoffeeScriptBundle("~/bundles/coffee")
        .Include("~/Scripts/first.coffee")
        .Include("~/Scripts/second.coffee")
        .Include("~/Scripts/third.coffee")
    );
    
    // Less
    bundles.Add(new LessStyleBundle("~/Conetnet/less")
        .Include("~/Scripts/first.less")
        .Include("~/Scripts/second.less")
        .Include("~/Scripts/third.less")
    );
    
    // Scss
    bundles.Add(new SassStyleBundle("~/Conetnet/less")
        .Include("~/Scripts/first.Sass")
        .Include("~/Scripts/second.Sass")
        .Include("~/Scripts/third.Sass")
    );
    
    // Combined CoffeeScript and JS
    bundles.Add(new CombinedCoffeeScriptBundle("~/Conetnet/combined")
        .Include("~/Scripts/first.coffee")
        .Include("~/Scripts/second.js")
        .Include("~/Scripts/third.coffee")
    );
}
```

## Usage - Configuration

``Web.Optimization`` contains a ConfigurationSection, which allows you to register all your bundles in the web.config:
	
	<configSections>
	  <section name="web.optimization" type="Web.Optimization.Configuration.OptimizationSection" />
	</configSections>
	
	<web.optimization>
	  <bundles>
	    <bundle virtualPath="~/Content/js" transform="System.Web.Optimization.JsMinify, System.Web.Optimization">
	      <content>
			<!-- Add some single files -->
            <add path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js" />
	        <add path="~/Scripts/forms.js" />
	        <add path="~/Scripts/validation.js" />            
			<!-- Add a directory (and its subdirectories) -->
	        <add path="~/Content/Scripts/Plugins" searchPattern="*.js" searchSubdirectories="true" />
	      </content>
	    </bundle>
	  </bundles>
	</web.optimization>

You could also apply multiple transformations:

    <web.optimization>
      <bundles>
        <bundle virtualPath="~/Content/coffee">
          <content>
            <add path="~/Scripts/script1.coffee" />
          </content>
          <transformations>
            <add type="Web.Optimization.Bundles.CoffeeScript.CoffeeScriptTransform, Web.Optimization.Bundles.CoffeeScript" />
            <add type="Web.Optimization.Bundles.YUI.YuiJsMinify, Web.Optimization.Bundles.YUI" />
          </transformations>
        </bundle>
      </bundles>
    </web.optimization>

After the registration is done, you have to tell your application to use bundles from your configuration:

	protected void Application_Start()
	{
	  // ...
	
	  BundleTable.Bundles.EnableConfigurationBundles();
	}

## Usage - Unbundled files for debugging purposes

	<head>
	  <meta charset="utf-8" />
	  <!-- ... -->
	  @Html.RenderBundle("~/Content/style")  
	</head>
