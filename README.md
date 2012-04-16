Web.Optimization
================

Web.Optimization allows you to transform LESS and CoffeeScript files by making use of the upcoming bundling and minification features.

## Installation

* Get the packages via NuGet
  * [Install-Package Web.Optimization -Pre](https://nuget.org/packages/Web.Optimization)
  * [Install-Package Web.Optimization.Bundles.CoffeeScript -Pre](https://nuget.org/packages/Web.Optimization.Bundles.CoffeeScript)
  * [Install-Package Web.Optimization.Bundles.Less -Pre](https://nuget.org/packages/Web.Optimization.Bundles.Less)
  * [Install-Package Web.Optimization.Bundles.Sass -Pre](https://nuget.org/packages/Web.Optimization.Bundles.Sass)
  * [Install-Package Web.Optimization.Bundles.AjaxMin -Pre](https://nuget.org/packages/Web.Optimization.Bundles.AjaxMin)
  * [Install-Package Web.Optimization.Bundles.YUI -Pre](https://nuget.org/packages/Web.Optimization.Bundles.YUI)
* Clone or download the code from GitHub => Build the solution => Add references to your project.

## Usage - Bundles (CoffeeScript, LESS & SASS/SCSS)

	protected void Application_Start()
	{
		// ...
		
		// Register CoffeeScript files
		
		var scripts = new Bundle(
                "~/Content/coffee", 
                new CoffeeScriptTransform().Then(new JsMinify()));
	
		scripts.AddFile("~/Scripts/first.coffee", false);
		scripts.AddFile("~/Scripts/second.coffee", false);
		scripts.AddFile("~/Scripts/third.coffee", false);
	
		BundleTable.Bundles.Add(scripts);
		
		
		// Register LESS files
		
	    var styles = new Bundle(
                "~/Content/less",
                new LessTransform().Then(new CssMinify()));
	    
		styles.AddFile("~/Content/first.less", false);
	    styles.AddFile("~/Content/second.less", false);
	    styles.AddFile("~/Content/third.less", false);
	
	    BundleTable.Bundles.Add(styles);
	}

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

After the registration is done, you have to tell your application to use these bundles:

	protected void Application_Start()
	{
	  // ...
	
	  BundleTable.Bundles.RegisterConfigurationBundles();
	}

## Usage - Unbundled files for debugging purposes

	<head>
	  <meta charset="utf-8" />
	  <!-- ... -->
	  @Html.RenderBundle("~/Content/style")  
	</head>