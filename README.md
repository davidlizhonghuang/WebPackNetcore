# WebPackNetcore

Webpack is the module bundler, AMD and requirejs is its competitor. Webpack becomes more and more popular due to its eay to use. I try to implement this simply techniques in asp.net core.

###The procedures are:
<pre>
1,create a new asp.net core web application
2, open project folder in window explorer, run cmd to install webpack globally npm install -g webpack
3, npm install -g requirejs
4, open js folder in wwwroot, add js1.js, js2.js, style.css, and one.js four files (need css-loader, style-loader)
5, point cmd to js folder ro run webpack command webpack ./one.js site.js
6, put site.js in html page as < script src=" ./js/site.js">< /script>
7, run index.cshtml to check the result as below
<img src="webp1.png">
8, here, webpack merge js together into site.js and attach css file to html page at runtime.
9, no matter how many js file, we can use webpack to consolidate them into on js file for html page. this is siimple and easy to implement.
</pre>
Webpack better is running in cmd format, this enables us to simplify visual studio tasks.
