<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="PG.Web.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title></title>
	<!-- <link href="css/tab.css" rel="stylesheet" type="text/css" /> !-->
	<link href="css/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
	<link href="css/layout-default-latest.css" rel="stylesheet" type="text/css" />
  
    <!-- Font Awesome -->
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" type="text/css"/>
				 
	<script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
	<script src="javascript/jquery-migrate-latest.js" type="text/javascript"></script>
	<script src="javascript/jquery-ui-latest.min.js" type="text/javascript"></script>
	<script src="javascript/jquery.layout.min-latest.js" type="text/javascript"></script>

	<script src="javascript/jquery.bgiframe.js" type="text/javascript"></script>
	<script src="javascript/jquery.pngFix.js" type="text/javascript"></script>

	<script src="javascript/pg.enums.js?v=1.0.3" type="text/javascript"></script>
	<script src="javascript/pg.jsutility.js?v=4.2.0" type="text/javascript"></script>
	<script src="javascript/pg.jssecurity.js?v=1.2.0" type="text/javascript"></script>
	<script src="javascript/pg.tabclass.js?v=2.0.3" type="text/javascript"></script>
	<script src="javascript/pg.tabmenu.js?v=2.2.0" type="text/javascript"></script>

	
	
	<style type="text/css">
	 
	 *
	 {
		margin: 0px 0px 0px 0px;
		padding: 0px 0px 0px 0px;
		outline: none;
		border:  none;
	 }
   
	 html, body {
		width:		100%;
		height:		100%;					
		overflow:	auto; /* when page gets too small */
	}
	
	#containerOuter 
	{
		height:     100%;
		min-height: 100%;
		width:      100%;
		min-width:  100%;
		margin:     0;
		padding:    0;
	}
	

	#container {
		background:	777;
		/* HEIGHT */
		height:		100%;					
		min-height:	300px;
		_height:	100%; /* min-height for IE6 */
		/* WIDTH */
		width:		100%;
		_width:		100%; /* min-width for IE6 */
		min-width:	400px;
		/* HORIZONTAL CENTERING */
		margin:		0;
		padding:    0;
	}
	
	
	#container11 {
		background:	777;
		/* HEIGHT */
		height:		100%;					
		min-height:	300px;
		_height:	300px; /* min-height for IE6 */
		/* WIDTH */
		width:		100%;
		max-width:	900px;
		min-width:	700px;
		_width:		700px; /* min-width for IE6 */
		/* HORIZONTAL CENTERING */
		
	}
	#container22 {
		background:	777;
		/* HEIGHT */
		height:		100%;					
		min-height:	300px;
		_height:	300px; /* min-height for IE6 */
		/* WIDTH */
		width:		100%;
		_width:		400px; /* min-width for IE6 */
		min-width:	400px;
		/* HORIZONTAL CENTERING */
		margin:		0 auto;
	}

	
	#containerInner {
		padding: 0px;
		border:		1px solid #BBB;
	}
	
	.pane {
		display:	none; /* will appear when layout inits */
	}
	
	.ui-layout-pane { /* all 'panes' */
		background:	#FFF; 
		border:		1px solid #BBB; 
	/* DO NOT add scrolling (or padding) to 'panes' that have a content-div,
	   otherwise you may get double-scrollbars - on the pane AND on the content-div
	*/
	padding:	0px; 
	overflow:	auto;
	}
	/* (scrolling) content-div inside pane allows for fixed header(s) and/or footer(s) */
	.ui-layout-content {
		padding: 0px;
		position:	relative; /* contain floated or positioned elements */
		overflow:	auto; /* add scrolling to content-div */
	}
	
	
	
	/* inner divs inside Outer East/West panes  url('image/header03.png') 0 50% repeat-x*/
	.leftHeader { 
		/*background: #00B386;*/
        background: #494E53 ;
		font-weight: bold;
		text-align: center;
		padding: 2px 0 4px;
		position: relative;
		overflow: hidden;
		height:20px;
        color:white;
       
	}
	.leftContent {
		padding:	0px;
		border:     none;
		border-width: 0px;
		position:	relative;
		width: 100%;
		height: 300px;	    
	}
	
	#west-closer {
			position:	absolute;
			top:		1px;
			width: 		20px;
			height:		20px;
			z-index:	2;
			display:	block;
			cursor: 	pointer;
			background: url('image/go-lt-of.gif') no-repeat center;
			right:      1px;
			
	}
	#west-closer:hover
	{
			background: url('image/go-lt-on.gif') no-repeat center;
	}
	
	
	.ui-layout-resizer-sliding {
		opacity: 0.1;
		filter: alpha(opacity=10);
		background-color: #999;
	}
	.ui-layout-resizer-sliding:hover {
		opacity: 1;
		filter: alpha(opacity=100);
	}
	.ui-layout-resizer-west-dragging ,
	.ui-layout-resizer-west-open:hover	{ background: url('image/resizabl.gif') repeat-y center; }
	
	.ui-layout-resizer-west-closed:hover {
		background-color: #F6F6F6;
	} 
	
	.ui-layout-toggler-west-closed {
		background-color:	#EEE;
		border-top:			1px solid #FFF;
		border-left:		1px solid #FFF;
		border-bottom:		1px solid #677;
	}

	
	.ui-layout-toggler-west-closed:hover {
		background-color: #F6F6F6;
	} 
	
	.ui-layout-center
	{
		padding: 0px 0px 0px 0px;
		margin: 0px 0px 0px 0px;
		border: 0px none;
		
		background-color: #DCE4EF;	
		background-image  :url('image/bgblue.jpg');
	}
	
	
	  .version
	{
		font-family: Verdana;
		font-size: 8pt;
		color: #FFFFFF;
		padding-left:10px;
	}
	
	 
  .login
	{
		font-family: Verdana;
		font-size: 8pt;
		color: black;
	}
	.loginbold
	{
		font-weight:bold;
	}
	 
	.dvContent 
	{
		width:100%;
		height:100%;
	   
		padding: 0px 0px 0px 0px;
		margin: 0px 0px 0px 0px;
		border: 0px none;
	}

	.dvTabs 
	{
		height:100%;
		width:100%;
		
		padding: 0px 0px 0px 0px;
		margin: 0px 0px 0px 0px;
		border: 0px none;
	}  
	
	.dvIFrame 
	{
		height:100%;
		width:100%;
			
		padding: 0px 0px 0px 0px;
		margin: 0px 0px 0px 0px;
		border: 0px none;

		overflow:hidden;

	}
	
	.ifContent
	{
		padding: 0px 0px 0px 0px;
		margin: 0px 0px 0px 0px;
		border: 0px none;
		
		width : 100%;
		height: 100%; 
 
		
		/* border: 1px solid #C0C0C0; */
	}
	  
	#dialog label, #dialog input { display:block; }
	#dialog label { margin-top: 0
						.4em; }
	#dialog input, #dialog textarea { width: 95%; }
	
   .ui.helper-reset {line-height: 1;}
   /*.ui-tabs.nav {padding-top : 0.1em; }*/

	#dvTabs .ui-state-default { height:20px; font-size:8pt;}
	/*#dvTabs .ui-tabs .ui-tabs.nav LI A {
								padding-left : 3px;
								padding-right : 0.5em;
								padding-bottom : 0.0em;
								padding-top : 3px;
	}*/

	
	
	#dvTabs .ui-tabs .ui-tabs-nav li a {
								padding-left : 3px;
								padding-right : 0.5em;
								padding-bottom : 0.0em;
								padding-top : 3px;
	}
	.ui-tabs .ui-tabs-nav li a
	{
		padding-top : 3px;
	}
	

	#dvTabs .ui-tabs-panel {height:auto; width:100%; padding: 1px 0 0 0;}
	/*#dvTabs li .ui-icon-close { float: left; margin: 0.2em 0.2em 0 0; cursor: pointer; } */
	#dvTabs .ui-icon-close  
	{
		float: left; 
		margin: 0.2em 0.2em 0 0;
		cursor: pointer;
		/*
		border: 1px solid transparent;
		*/
	 }
	
	#dvTabs .ui-icon-close:hover
	{
		/*background-color: #D3D3D3;
		border: 1px solid #099;
		border: 1px solid grey; 
		*/
	}
	
	
	  
	.ui-widget-content { border: none;}
	#dvTabs .ui-widget-content { border:none; padding:0px; margin:0px; }
	#dvTabs .ui-corner-all { border-radius : 0px; }

	
	.ui-tabs 
	{
		border: 0px none;
		margin: 0px 0 0 0;
		padding: 0px 0 0 0;
	}     
	#dvTabs .ui-tabs-nav {
		  margin: 0px 0 0 0;
		  padding: 2px 20px 0 2px ;
	} 
	
	
	#add_tab { cursor: pointer; }	
	
	 
	.tabButtonOptions
	{
	   display: none; 
	   list-style-type: none; 
	   padding: 0px; 
	   margin: 0; 
	   border: 1px solid #DCDCDC; 
	   background-color: #fff; 
	   z-index: 999;
	   position: absolute;
	}
	
   
	.tabButtonOptions li
	{
		padding: 1px 5px 1px 5px; 
		margin: 0; 
		width: auto;
		border: 1px solid #fff;
		font-size:8pt;
		cursor: pointer;
	}
	/*
	.tabButtonOptions li:hover
	{
		background-color: #87A7E9;
	}
	*/
	.tabButtonOptionsLiHover
	{
		background-color: #DCDCDC;
	}
	
	
	
	.tabButtonOptions li a
	{
	  text-transform: none;
	}
 
	.treeNode
	{
		outline:none;
		cursor:pointer;
        padding:4px 2px 4px 2px;
        width:100%;
       

	}
	
	.treeNode:hover
	{
		background-color:#3CC1CF; 
		color:white;
        padding:4px 2px 4px 2px;
        border-radius:2px 2px;
        font-weight:bold;
        width:100%;
       

	}
	
	.treeNodeSelected
	{
		
		color:White;
		/*background-color:#494E53;*/
        /*background-color:blue;*/
		background-color:#3CC1CF;
        padding:4px 2px 4px 2px;
        border-radius:2px 2px;
        font-weight:bold;
        width:100%;
	}
	
	.nodeTextSelected
	{
		color:White !important;
		 /*background-color:#1F4AA5;*/ 
	}

   .dvDialogGlobal
   {
	  display: none;    
	  border: 1px solid black;
	  width: 200px;
	  height: 200px;
   }
   
   .dvWait11
   {
	  position:fixed;
	  top:2px;
	  left: 50%;

	  height:20px;
	  width:50px;
	  border: solid 1px red;
	  background-color:White;
	  color:Blue;
	  z-index:3000;
	  font-size: 8pt;
	  text-align:center;
	  vertical-align:middle;
   }
   
   .dvWait
   {
	  position:fixed;
	 /* top:5px;*/
      top:10px;
	  left: 50%;

	  height:20px;
	  width:50px;
	  /*border: solid 1px orange ;*/
	  /*background-color:White;*/
        /*background-image: url('image/spin.gif');*/
	  color:black;
	  z-index:3000;
	  font-size: 8pt;
	  text-align:center;
	  vertical-align:middle;
	  display:none;
   }
   
   .menuRootNode111111
   {
		background-color : green;
        color: white;
   }

    .menuRootNode
   {
		
   }
   
   .menuRootNode:hover
   {
		/*font-weight:bold;*/
        
   }
	 
   .menuRootNodeWide
   { /*
	  background-image : url('/image/header13.png'); */
		/*background-color : Aqua;*/
   }
   
   .spanMenu
   {
	 
   }
   
   .spanMenu:hover
   {
		/*font-weight:bold;*/
   }
   
   
   .fixPng{}



   .spinner {
  animation: rotate 2s linear infinite;
  z-index: 2;
  position: absolute;
  top: 50%;
  left: 50%;
  margin: -25px 0 0 -25px;
  width: 50px;
  height: 50px;
      stroke: #fff;
  }
  .path {
    stroke: hsl(210, 70, 75);
    stroke-linecap: round;
    animation: dash 1.5s ease-in-out infinite;
}

@keyframes rotate {
  100% {
    transform: rotate(360deg);
  }
}

@keyframes dash {
  0% {
    stroke-dasharray: 1, 150;
    stroke-dashoffset: 0;
  }
  50% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -35;
  }
  100% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -124;
  }
}


.button1 {
	
      
  font-family: 'Oswald', sans-serif;
  font-size: 1em;
  letter-spacing: 1px;
  position: relative;
  float: right;
  padding:5px;
  margin: 5px;
  color: white;
  text-decoration:none;
  background-color:transparent;
  border-radius:5px;
  border:1px solid white;
  transition: width .35s;
  -webkit-transition: width .35s;
  overflow: hidden;
            top: 0px;
            left: 0px;
        }
.button1:hover{background-color:none;color:red;}
		
	</style>
	
	
	<script language="javascript" type="text/javascript">
// <!CDATA[

	 //var isIE = navigator.userAgent.indexOf("MSIE") > -1;

 var self = this;
 var appID = '<%=this.AppID%>';
 var rootPath = '<%=this.RootPath%>';
		var getMenuLinks = '<%=this.GetMenuPageLinks%>';

		var getKeepLiveLinks = '<%=this.GetMenuKeepLive%>';

		var hdnKeepLive = '<%=hdnKeepLive.ClientID%>';
		var hdnKeepLiveInterval = '<%=hdnKeepLiveInterval.ClientID%>';

		var keepLiveTimer;

 var outerLayout, innerLayout;
 var $ctltabs;

 var timerTabOption;

 $(document).ready(function () {
	 $('.fixPng').pngFix();
	 //setTimeout(toggleFull(), 1000);
	 //goFullScreen();
	 //setTimeout(function () {
	 //    alert('before');
	 //    toggleFull();
	 //    alert('after');
	 //}
	 //    , 5000);
 });

 $(document).ready(function () {
	 JSSecurity.AppID = appID;
	 JSSecurity.RootPath = rootPath;
	 activateKeepLive();
 });

 function activateKeepLive() {

	 var isKeepLive = parseInt($('#' + hdnKeepLive).val());
	 var keepLiveInterval = parseInt($('#' + hdnKeepLiveInterval).val());

	 if (isKeepLive == 1) {
		 if (keepLiveInterval >= 10) {
			 keepLiveInterval = keepLiveInterval * 1000; //seconds to miliseconds
			 keepLiveTimer = setInterval(fncKeepLive, keepLiveInterval);
			 //keepLiveTimer = setInterval(fncKeepLive, 10000);
		 }
	 }
 }

 function fncKeepLive() {
	 var isError = false;
	 var isComplete = false;
	 var getKeepLiveUrl = this.getKeepLiveLinks;

	 //ajax call
	 var dummyVar = $.ajax({
		 type: "GET",
		 cache: false,
		 async: true,
		 dataType: "text",
		 url: getKeepLiveUrl,
		 success: function (rdata) {
			 //alert(rdata);
		 },
		 complete: function () {
			 if (!isError) {
				 //return;
			 }
			 isComplete = true;
		 },
		 error: function (XMLHttpRequest, textStatus, errorThrown) {
			 isError = true;
			 //alert('keep live error');
		 }
	 });

	 return true;
 }

 function btnFullScreenClick()
 {
	 toggleFull();
 }


 function cancelFullScreen(el) {
	 var requestMethod = el.cancelFullScreen || el.webkitCancelFullScreen || el.mozCancelFullScreen || el.msExitFullscreen;

	 if (requestMethod) { // cancel full screen.
		 requestMethod.call(el);
	 } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
		 try {
			 var wscript = new ActiveXObject("WScript.Shell");
			 if (wscript !== null) {
				 wscript.SendKeys("{F11}");
			 }
		 }
		 catch (err) {
			 alert('Change Browser Security Settings to activate this feature!');
		 }
	 }
	 else {
		 alert('Browser Not Supported!');
	 }
 }

 function requestFullScreen(el) {

	 //ie8 activate activex, Internet Options -> Security -> custom level -> acitivex controls and plugins -> Initialize and script activex controls.... = enable


	 // Supports most browsers and their versions.
	 var requestMethod = el.requestFullScreen || el.webkitRequestFullScreen || el.mozRequestFullScreen || el.msRequestFullscreen;

	 if (requestMethod) { // Native full screen.
		 requestMethod.call(el);
	 } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.

		 try
		 {
			 var wscript = new ActiveXObject("WScript.Shell");
			 if (wscript !== null) {
				 wscript.SendKeys("{F11}");
			 }
		 }
		 catch(err)
		 {
			 alert('Change Browser Security Settings to activate this feature!');
		 }
	 }
	 else {
		 alert('Browser Not Supported!');
	 }
	 return false
 }

 function toggleFull() {
	 var elem = document.body; // Make the body go full screen.

	 //var isInFullScreen = (document.fullScreenElement && document.fullScreenElement !== null) || (document.mozFullScreen || document.webkitIsFullScreen);
	 //var isInFullScreen = (document.fullScreenElement && document.fullScreenElement !== null) || (document.mozFullScreen || document.webkitIsFullScreen || document.msFullscreenEnabled);
	 var isInFullScreen = (document.fullScreenElement && document.fullScreenElement !== null) || (document.mozFullScreen || document.webkitIsFullScreen || document.msFullscreenElement);

	 if (isInFullScreen) {
		 cancelFullScreen(document);
	 } else {
		 requestFullScreen(elem);
	 }
	 return false;
 }


		//function goFullScreen()
		//{
		//    if ((document.fullScreenElement && document.fullScreenElement !== null) || (!document.mozFullScreen && !document.webkitIsFullScreen)) {
		//   	 if (document.documentElement.requestFullScreen) {
		//   		 document.documentElement.requestFullScreen();
		//   	 } else if (document.documentElement.mozRequestFullScreen) {
		//   		 document.documentElement.mozRequestFullScreen();
		//   	 } else if (document.documentElement.webkitRequestFullScreen) {
		//   		 document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
		//   	 }
		//    } else {
		//   	 if (document.cancelFullScreen) {
		//   		 document.cancelFullScreen();
		//   	 } else if (document.mozCancelFullScreen) {
		//   		 document.mozCancelFullScreen();
		//   	 } else if (document.webkitCancelFullScreen) {
		//   		 document.webkitCancelFullScreen();
		//   	 }
		//    }
		//}

	 
 var layoutSettings_Inner2 = {
	  name: "innerLayout2"
		, defaults: {
		
		}
		, north: {   //top pane
		size: 60
			, initClosed: false
			, spacing_open: 1
			, closable: false
			, resizable: false
		}
		, west: {   //left pane
			size: 180
			, initClosed: false
			, spacing_open: 3
			, spacing_closed: 20
			, togglerLength_open: 0
			, togglerLength_closed: 100
			, togglerAlign_closed: "top"
			, togglerContent_closed: "M<BR>e<BR>n<BR>u"
			, togglerTip_open: "Close Menu"
			, togglerTip_closed: "Open Menu"
			, onresize: "ResizeLeftPanels"
		}
		, east: {   //right pane
			size: 100
			, initClosed: true
		}
		, south: {   //bottom pane
			size: 25
			, initClosed: true
			, resizable: false

		}
		, center: {
			paneSelector: "#dvCenter" 			// sample: use an ID to select pane instead of a class
			, minWidth: 300
			, minHeight: 200
			, onresize_end: "OnCenterResize"
		}
	};

	function OnCenterResize() {
		TabMenu.PopupsHide();
		if (JSUtility.IsIE6()) {
			TabMenu.ResizeTabControlALLTimer(100);
		}
		else {
			TabMenu.ResizeTabControlALL();
		}
	}

	function ResizeLeftPanels() {
		//left panel
		//var barHeight = $('#dvLeft').height();
		var barHeight = $('#dvLeft').innerHeight();

		var headerHeight = $('#dvLeftHeader').outerHeight();
		
		//var leftContTop = $('#dvLeftContent').position().top;
		//var leftContHeight = barHeight - leftContTop;

		var leftContHeight = barHeight - headerHeight;
	   
		 
		$('#dvLeftContent').height(leftContHeight + 'px');
	}
	 
	 
function GetMenu(id) {

	var menu = null;
	var isError = false;
	var isComplete = false;

	alert("kd");

	var getMenuUrl = this.getMenuLinks + "?id=" + id; 


	

	//ajax call
	var dummyVar = $.ajax({
	type: "GET",
	cache: false,
	async: false,
	dataType: "json",
	url: getMenuUrl,
	success: function(menudata) {
		if (menudata.menu[0].count > 0)
		{
			menu = menudata.menu[0];
		}     
	  },
	complete: function() {
		if (!isError)
		{
			//return;
			 //alert (menu.name);
		}
	   isComplete = true;
	},
	error: function(XMLHttpRequest, textStatus, errorThrown){
		isError = true;
		alert(textStatus);   
	  }
  });     

	return menu;
}

	 
function CreateLayout() {
	outerLayout = $('#container').layout();
	innerLayout = $('#containerInner').layout(layoutSettings_Inner2);

	var westSelector = "#containerInner > .ui-layout-west"; // outer-west pane
	$("<span></span>").attr("id", "west-closer").prependTo(westSelector);
	innerLayout.addCloseBtn("#west-closer", "west");

	//    innerLayout.addCloseBtn("#west-closer", "west");
	//    $("<span></span>").addClass("pin-button").prependTo(westSelector);
	//    innerLayout.addPinBtn(westSelector + " .pin-button", "west");

	ResizeLeftPanels();
}

$(document).ready(function () {
	CreateLayout();
	TabMenu.RootPath = self.rootPath;
	TabMenu.GetMenuURL = self.getMenuLinks;
	$ctltabs = TabMenu.CreateTabControl('dvTabs');
	TabMenu.CreateTabOptionButton();
	TabMenu.WindowUnloadALL();

	TabMenu.ResizeTabControlALL();

	$("#dvDialogGlobal").dialog({
		autoOpen: false,
		resizable: false,
		modal: true,
		closeOnEscape: true,
		//show: "fade",
		//hide: "fadeout",
		//open: function(event, ui) { $(".ui-dialog-titlebar-close").hide(); }
		width: 350,
		height: 200
		//        buttons: {
		//            "Close": function() {
		//			  $( this ).dialog( "close" );
		//			},
		//            "Save": function() {
		//              alert("save");
		//              $( this ).dialog( "close" );
		//              window.location.reload();
		//            }
		//        }
	});


	//    $(window).hashchange(function () {
	//        alert('a');
	//    });

	$(window).bind('hashchange', function (e) {
		//alert('a');
		hash = location.hash;
		//alert(hash);
		//$(window).trigger('hashchange');
		//location.hash = hash;
		$("#dvTabs").tabs("select", window.location.hash);
		//$tabcontrol.tabs("option", "active", 1);
	});


	//    $("#myTabs").bind("tabsshow", function (event, ui) {
	//        window.location.hash = ui.panel.id;
	//    });

});

 
 //for tree menu
$(document).ready(function() {
	$('#TreeView1').find('.treeNode').click(function(e) {
		$('#TreeView1').find('.treeNode').removeClass('treeNodeSelected');
		$(this).addClass('treeNodeSelected');

		$('#TreeView1').find('.treeNode').find('a').removeClass('nodeTextSelected');
		$(this).find('a').addClass('nodeTextSelected');
	});

	$('#TreeView1').find('td.menuRootNode').parentsUntil('tr').addClass('menuRootNodeWide');
	//$('#TreeView1').find('td.menuRootNode').parent().addClass('menuRootNodeWide');


	//alert($('#TreeView1').find('img').length);

	//$('#TreeView1').find('img').css('outline', 'none');
	$('#TreeView1').find('a').css('outline', 'none');


	//    $('#TreeView1').find('td.menuRootNode').parent().click(function(e) {
	//        //alert('dd');
	//        TabMenu.CheckWindowUnload = false;
	//    });

	$('#TreeView1').find('td.menuRootNode').parent().hover(function(e) {
		//alert('dd');
			TabMenu.CheckWindowUnload = false;
		},
		function(e) {
			TabMenu.CheckWindowUnload = true;
		}
	);


});


 function OpenPopupGlobal() {
	 $("#dvPopupGlobal").dialog("open");
 }






	// ]]>
</script>
	
	
</head>
<body>
   <div id="containerOuter">
	<form id="form1" runat="server" style="width:100%;height:100%; padding:0; margin:0;">
	 <div id="hiddenField">
		  <asp:HiddenField ID="hdnKeepLive" runat="server" Value="0" />
		  <asp:HiddenField ID="hdnKeepLiveInterval" runat="server" Value="0" />
	 </div>
	<div id="container">
		<div id="containerInner" class="pane ui-layout-center">
			<div id="dvTop" class="pane ui-layout-north fixPng"">
			   <div id="dvTopInner" 
					style="padding:0; width:100%;height:100%; Background-color:#3CC1CF;  background-repeat:no-repeat;"> <%--background-image: linear-gradient(to right, green, red,#0094ff, green); --%>
				 <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
				   <tr>
					 <td style="width:60%;">
					  <table border="0" cellpadding="0" cellspacing="0">
						<tr>
						<td style="width:10px;">
						 </td>
						 <td style="width:45px;text-align:center; vertical-align:middle;">
						  <asp:Image ID="Image1" runat="server" 
									style="top:2px;" 
									ImageAlign="AbsMiddle" ImageUrl="image/wr.png" Width="150px" Height="40px" 
									BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="0px"  />
						 </td>
						 <td style="width:10px;">
						 </td>
						   <td style="" class="lbluser"><asp:Label runat="server" ID="lblUserID" ForeColor="White"></asp:Label></td>
                          <td style="" class="lbluser"><asp:Label runat="server" ID="lblUserName" ForeColor="White"></asp:Label></td> 
						
						</tr> 
					  </table> 
					 </td>
					 <td style="width:40%; color:black;" align="right">
					   <table border="0" cellpadding="0" cellspacing="0">
					
						
						  <tr>
                             
							<td align="left">
							  <asp:LinkButton ID="btnLogout" runat="server" CssClass="button1" onclick="btnLogOut_Click" ><i class=" fa fa-power-off" style="color:red"></i>&nbsp;Logout</asp:LinkButton>
										
							</td>

							
						  </tr>
					   </table> 
					 </td>
				   </tr>
				 </table>
                    <%--loading...--%>

				 <div id="dvWait" class="dvWait">
                    <svg class="spinner" viewBox="0 0 50 50">
                    <circle class="path" cx="25" cy="25" r="20" fill="none" stroke-width="5"></circle>
                    </svg>
				</div>


			   </div>  
			</div>

			<div id="dvLeft" class="pane ui-layout-west">
				<div id="dvLeftHeader" class="leftHeader">Menu</div>
				<div id="dvLeftContent"  class="leftContent">
				  <div id="dvLeftContentInner" 
						style="width:100%;height:100%; overflow:auto; background-color: #343A40;"> 
					<div id="dvTreeMenu" style="padding: 4px 4px 0px 0px;" >
						<asp:TreeView ID="TreeView1" runat="server" BackColor="#343A40" Font-Size="11pt" 
							ForeColor="White" NodeIndent="11"
							EnableViewState="False" Font-Names="Verdana" ImageSet="Arrows" 
							NodeWrap="false" RootNodeStyle-Width="100%" RootNodeStyle-NodeSpacing="4px" 
							ontreenodedatabound="TreeView1_TreeNodeDataBound">
							<HoverNodeStyle Font-Underline="false"  ForeColor="white" BackColor="#494E53" />
							<SelectedNodeStyle Font-Bold="true" Font-Italic="False" Font-Underline="false" />
							<RootNodeStyle NodeSpacing="2px" Width="100%" CssClass="menuRootNode"></RootNodeStyle>

							<NodeStyle CssClass="treeNode" NodeSpacing="2px" />
						</asp:TreeView>
					</div>
				  </div>
			 </div>
			</div>
			
			<div id="dvRight" class="pane ui-layout-east">
				
			</div>
			
			
			<div id="dvBottom" class="pane ui-layout-south">
			   <div style="width:100%;height:100%">
				  <table>
					  <tr>
						<td>
						</td>
						<td>
							<asp:Label ID="lblCopyright" runat="server" Text="Copyright"></asp:Label>
						</td>
						<td>
						</td>
						<td>
							<asp:HyperLink ID="hlinkWeb" runat="server" Target="_blank">HyperLink</asp:HyperLink>
						</td>
					  </tr>
				  </table>
			   </div>
			   
			</div>
			
			
			<div id="dvCenter" class="pane ui-layout-center">
				 <div id="dvContent" class="dvContent">
					 <div id="dvTabs" class="dvTabs" >
							<div style="width:15px; height:15px; right:4px; top:4px; position:absolute;">
								<div style="width:100%; height:100%;">
									<div id="dvTabButton"  style="height:15px;width:15px;" title="Tab Options"> 
									</div>

								 </div>
							</div>
					 
							<ul>
								<li><a href="#tabs-1">Home</a></li>
							</ul>
							<div id="tabs-1">
								<div id="dvIFrame-1" class="dvIFrame">
								  <iframe id="iframe-1" src="Home.aspx?_t=1&_n=1" class="ifContent" 
										frameborder="0"> 

                                         
								  </iframe>  
                                    			
                                

								</div>
							</div>
							
					</div>
				</div>
			</div>
	</div>
	
	</div>
	
	
	
	
	 <div id="dvTabButtonOptionList">
	   <ul id="ulTabButtonOption" class="tabButtonOptions">
			<li id="liCloseALL" title="Close all open tabs">Close All</li>
	  </ul>
	 </div>
	 <div id="dvDialogGlobal" class="dvDialogGlobal">
		<div style="height:100%;width:100%;">
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
			<asp:Button ID="Button1"
				runat="server" Text="Button" />
				
		</div>
	 </div>
	 
	 <div id="garbage-collector" style="width:0px;height:0px;display:none;">
	 
	 </div>


	</form>
   </div>
   

</body>
</html>
