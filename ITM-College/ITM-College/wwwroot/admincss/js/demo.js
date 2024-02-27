"use strict"

var themeOptionArr = {
			typography: '',
			version: '',
			layout: '',
			primary: '',
			headerBg: '',
			navheaderBg: '',
			sidebarBg: '',
			sidebarStyle: '',
			sidebarPosition: '',
			headerPosition: '',
			containerLayout: '',
			direction: '',
		};
 	


(function($) {
	
	"use strict"
	
	//var direction =  getUrlParams('dir');
	var direction =  getUrlParams('dir');
	var theme =  getUrlParams('theme');
	
	/* Dz Theme Demo Settings  */
	
	var dlabThemeSet0 = { /* Default Theme */
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_1",
		navheaderBg: "color_3",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dlabThemeSet1 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dlabThemeSet2 = {
		typography: "poppins",
		version: "dark",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	
	var dlabThemeSet3 = {
		typography: "roboto",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_14",
		navheaderBg: "color_14",
		sidebarBg: "color_13",
		sidebarStyle: "modern",
		sidebarPosition: "static",
		headerPosition: "static",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dlabThemeSet4 = {
		typography: "roboto",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_2",
		navheaderBg: "color_10",
		sidebarBg: "color_2",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
	var dlabThemeSet5 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_1",
		headerBg: "color_13",
		navheaderBg: "color_13",
		sidebarBg: "color_13",
		sidebarStyle: "compact",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	var dlabThemeSet6 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_11",
		headerBg: "color_1",
		navheaderBg: "color_1",
		sidebarBg: "color_1",
		sidebarStyle: "modern",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	var dlabThemeSet7 = {
		typography: "poppins",
		version: "light",
		layout: "horizontal",
		primary: "color_5",
		headerBg: "color_5",
		navheaderBg: "color_5",
		sidebarBg: "color_1",
		sidebarStyle: "full",
		sidebarPosition: "fixed",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	var dlabThemeSet8 = {
		typography: "poppins",
		version: "light",
		layout: "vertical",
		primary: "color_14",
		headerBg: "color_1",
		navheaderBg: "color_14",
		sidebarBg: "color_14",
		sidebarStyle: "modern",
		sidebarPosition: "static",
		headerPosition: "fixed",
		containerLayout: "full",
		direction: 'ltr',
	};
	
		
	function themeChange(theme, direction){
		var themeSettings = {};
		themeSettings = eval('dlabThemeSet'+theme);
		themeSettings.direction = direction;
		dlabSettingsOptions = themeSettings; /* For Screen Resize */
		new dlabSettings(themeSettings);
		
		setThemeInCookie(themeSettings);
	}
	
	function setThemeInCookie(themeSettings)
	{
		//console.log(themeSettings);
		jQuery.each(themeSettings, function(optionKey, optionValue) {
			setCookie(optionKey,optionValue);
		});
	}
	
	function setThemeLogo() {
		var logo = getCookie('logo_src');
		
		var logo2 = getCookie('logo_src2');
		
		if(logo != ''){
			jQuery('.nav-header .logo-abbr').attr("src", logo);
		}
		
		if(logo2 != ''){
			jQuery('.nav-header .logo-compact, .nav-header .brand-title').attr("src", logo2);
		}
	}
	
	function setThemeOptionOnPage()
	{
		if(getCookie('version') != '')
		{
			jQuery.each(themeOptionArr, function(optionKey, optionValue) {
				var optionData = getCookie(optionKey);
				themeOptionArr[optionKey] = (optionData != '')?optionData:dlabSettingsOptions[optionKey];
			});
			//console.log(themeOptionArr);
			dlabSettingsOptions = themeOptionArr;
			new dlabSettings(dlabSettingsOptions);
			
			setThemeLogo();
		}
	}



	/*  set switcher option start  */
	function getElementAttrs(el) {
		return [].slice.call(el.attributes).map((attr) => {
			return {
				name: attr.name,
				value: attr.value
			}
		});
	}
	
	function handleSetThemeOption(item, index, arr) {
		
		var attrName = item.name.replace('data-','').replace('-','_');
		
		if(attrName === "sidebarbg" || attrName === "primary" || attrName === "headerbg" || attrName === "nav_headerbg" ){
			if(item.value === "color_1"){
				return false;
			}
			var attrNameColor = attrName.replace("bg","")
			document.getElementById(attrNameColor+"_"+item.value).checked = true;
		}else if(attrName === "navigationbarimg"){
			document.getElementById("sidebar_img_"+item.value.split('sidebar-img/')[1].split('.')[0]).checked = true;
		}else if(attrName === "sidebartext"){
			document.getElementById("sidebar_text_"+item.value).checked = true;
		}else if(attrName === "direction" || attrName === "nav_headerbg" || attrName === "headerbg"){
			document.getElementById("theme_direction").value = item.value;	
		}else if(attrName === "sidebar_style" || attrName === "sidebar_position" || attrName === "header_position" || attrName === "typography" || attrName === "theme_version" ){
			if(item.value === "cairo" || item.value === "full" || item.value === "fixed"|| item.value === "light"){return false}
			document.getElementById(attrName).value = item.value;				
		}else if(attrName === "layout"){
			if(item.value === "vertical"){return false}
			document.getElementById("theme_layout").value = item.value;		
		}
		else if(attrName === "container"){
			if(item.value === "wide"){return false}
			document.getElementById("container_layout").value = item.value;
		}
		
	}
	/* / set switcher option end / */

	
	
	

	
	
	jQuery(window).on('load', function(){
		direction = (direction != undefined) ? direction : 'ltr';

		if(getCookie('direction') == 'rtl'){
			jQuery('.main-css').attr('href','../admincss/css/style-rtl.css');
		}

		if(theme != undefined){
			if(theme == 'rtl'){
				themeChange(0, 'rtl');
				jQuery('.main-css').attr('href','../admincss`/css/style-rtl.css');
			}else {
				themeChange(theme, direction);
			}
		}
		else if(direction != undefined){
			if(getCookie('version') == ''){	
				themeChange(0, direction);
			}
		}
		
		setTimeout(() => {
			var allAttrs = getElementAttrs(document.querySelector('body'));
			allAttrs.forEach(handleSetThemeOption);
			$('.default-select').selectpicker('refresh');
		},1500);

		/* Set Theme On Page From Cookie */
		setThemeOptionOnPage();
	});
	
	jQuery(window).on('resize', function(){
		setThemeOptionOnPage();
	});
	

})(jQuery);