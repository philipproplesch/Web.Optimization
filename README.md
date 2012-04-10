Web.Optimization
================

Web.Optimization allow you to transform LESS and CoffeeScript files by making use of the upcoming bundling and minification feature.

##Installation

* Get the packages via NuGet
  * [Install-Package Web.Optimization.Bundles.CoffeeScript -Pre](https://nuget.org/packages/Web.Optimization.Bundles.CoffeeScript)
  * [Install-Package Web.Optimization.Bundles.Less -Pre](https://nuget.org/packages/Web.Optimization.Bundles.Less)
* Clone or download the code from GitHub => Build the solution => Add references to your project.

##Usage

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