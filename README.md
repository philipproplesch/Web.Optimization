Web.Optimization
================

Web.Optimization allows you to transform LESS and CoffeeScript files by making use of the upcoming bundling and minification features.

##Installation

* Get the packages via NuGet
  * [Install-Package Web.Optimization.Bundles.CoffeeScript -Pre](https://nuget.org/packages/Web.Optimization.Bundles.CoffeeScript)
  * [Install-Package Web.Optimization.Bundles.Less -Pre](https://nuget.org/packages/Web.Optimization.Bundles.Less)
* Clone or download the code from GitHub => Build the solution => Add references to your project.

##Usage - Bundles

	protected void Application_Start()
	{
		// ...
		
		// Register CoffeeScript files
		
		#if DEBUG
		  var scripts = new Bundle("~/Content/coffee", new CoffeeScriptTransform());
		#else
		  var scripts = new Bundle("~/Content/coffee", new CoffeeScriptMinify());
		#endif
	
		scripts.AddFile("~/Scripts/first.coffee", false);
		scripts.AddFile("~/Scripts/second.coffee", false);
		scripts.AddFile("~/Scripts/third.coffee", false);
	
		BundleTable.Bundles.Add(scripts);
		
		
		// Register LESS files
		
	    #if DEBUG
	      var styles = new Bundle("~/Content/less", new LessTransform());
	    #else
	      var styles = new Bundle("~/Content/less", new LessMinify());
	    #endif
	    
		styles.AddFile("~/Content/first.less", false);
	    styles.AddFile("~/Content/second.less", false);
	    styles.AddFile("~/Content/third.less", false);
	
	    BundleTable.Bundles.Add(styles);
	}

## Usage - Configuration

``Web.Optimization.Configuration`` allows you to register all your bundles in the web.config:
	
	<configSections>
	  <section name="web.optimization" type="Web.Optimization.Configuration.OptimizationSection" />
	</configSections>
	
	<web.optimization>
	  <bundles>
	    <bundle virtualPath="~/Content/css" transform="System.Web.Optimization.CssMinify, System.Web.Optimization">
	      <content>
			<!-- Add some single files -->
	        <add virtualPath="~/Content/Site.css" />
	        <add virtualPath="~/Content/Forms.css" />
			<!-- Add directories  -->
	        <add virtualPath="~/Content/Styles" searchPattern="*.css" searchSubdirectories="true" />
	      </content>
	    </bundle>
	  </bundles>
	</web.optimization>

After the registration is done, you have to tell your application to use these bundles:

	protected void Application_Start()
	{
	  // ...
	
	  BundleTable.Bundles.RegisterFromConfiguration();
	}