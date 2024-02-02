
var Edumin = function(){

	// Metis Menu
	var handleMetisMenu = function() {
		if(jQuery('#menu').length > 0 ){
			$("#menu").metisMenu();
		}
		jQuery('.metismenu > .mm-active ').each(function(){
			if(!jQuery(this).children('ul').length > 0)
			{
				jQuery(this).addClass('active-no-child');
			}
		});
	}
	
	// All Checked
	var handleAllChecked = function() {
		$("#checkAll").change(function() {
			$("td input:checkbox").prop('checked', $(this).prop("checked"));
		});
	}
	
	// Navigation
	var handleNavigation = function() {
		$(".nav-control").on('click', function() {

			$('#main-wrapper').toggleClass("menu-toggle");

			$(".hamburger").toggleClass("is-active");
		});
	}
	
	// Current Active
	var handleCurrentActive = function() {
		for (var nk = window.location,
			o = $("ul#menu a").filter(function() {
				
				return this.href == nk;
				
			})
			.addClass("mm-active")
			.parent()
			.addClass("mm-active");;) 
		{
			
			if (!o.is("li")) break;
			
			o = o.parent()
				.addClass("mm-show")
				.parent()
				.addClass("mm-active");
		}
	}
	
	// Mini Sidebar
	var handleMiniSidebar = function() {
		$("ul#menu>li").on('click', function() {
			const sidebarStyle = $('body').attr('data-sidebar-style');
			if (sidebarStyle === 'mini') {
				console.log($(this).find('ul'))
				$(this).find('ul').stop()
			}
		})
	}
	
	// Min Height
	var handleMinHeight = function() {
		var win_h = window.outerHeight;
		var win_h = window.outerHeight;
		if (win_h > 0 ? win_h : screen.height) {
			$(".content-body").css("min-height", (win_h + 60) + "px");
		};
	}
	
	// Data Action
	var handleDataAction = function() {
		$('a[data-action="collapse"]').on("click", function(i) {
			i.preventDefault(),
				$(this).closest(".card").find('[data-action="collapse"] i').toggleClass("mdi-arrow-down mdi-arrow-up"),
				$(this).closest(".card").children(".card-body").collapse("toggle");
		});

		$('a[data-action="expand"]').on("click", function(i) {
			i.preventDefault(),
				$(this).closest(".card").find('[data-action="expand"] i').toggleClass("icon-size-actual icon-size-fullscreen"),
				$(this).closest(".card").toggleClass("card-fullscreen");
		});

		$('[data-action="close"]').on("click", function() {
			$(this).closest(".card").removeClass().slideUp("fast");
		});

		$('[data-action="reload"]').on("click", function() {
			var e = $(this);
			e.parents(".card").addClass("card-load"),
				e.parents(".card").append('<div class="card-loader"><i class=" ti-reload rotate-refresh"></div>'),
				setTimeout(function() {
					e.parents(".card").children(".card-loader").remove(),
						e.parents(".card").removeClass("card-load")
				}, 2000)
		});
	}
	
	// Header Hight
	var handleHeaderHight = function() {
		const headerHight = $('.header').innerHeight();
		$(window).scroll(function() {
			if ($('body').attr('data-layout') === "horizontal" && $('body').attr('data-header-position') === "static" && $('body').attr('data-sidebar-position') === "fixed")
				$(this.window).scrollTop() >= headerHight ? $('.deznav').addClass('fixed') : $('.deznav').removeClass('fixed')
		});
	}
	
	// btn Number
	var handleBtnNumber = function() {
		$('.btn-number').on('click', function(e) {
			e.preventDefault();

			fieldName = $(this).attr('data-field');
			type = $(this).attr('data-type');
			var input = $("input[name='" + fieldName + "']");
			var currentVal = parseInt(input.val());
			if (!isNaN(currentVal)) {
				if (type == 'minus')
					input.val(currentVal - 1);
				else if (type == 'plus')
					input.val(currentVal + 1);
			} else {
				input.val(0);
			}
		});
	}
	
	// Show Pass
	var handleshowPass = function(){
		jQuery('.show-pass').on('click',function(){
			jQuery(this).toggleClass('active');
			if(jQuery('#dlabPassword').attr('type') == 'password'){	
				jQuery('#dlabPassword').attr('type','text');
			}else if(jQuery('#dlabPassword').attr('type') == 'text'){
				jQuery('#dlabPassword').attr('type','password');
			}
		});
		jQuery('.pass-handle').on('click',function(){
			jQuery(this).toggleClass('active');
			if(jQuery(this).parent().find('.pass-input').attr('type') == 'password'){
				jQuery(this).parent().find('.pass-input').attr('type','text');
			}else{
				jQuery(this).parent().find('.pass-input').attr('type','password');
			}
		})
	}
	
    // Lightgallery
    var handleLightgallery = function(){
		if(jQuery('#lightgallery').length > 0){
			lightGallery(document.getElementById('lightgallery'), {
				plugins: [lgThumbnail, lgZoom],
				selector: '.lg-item',
				thumbnail:true,
				exThumbImage: 'data-src'
            });
		}
	}
	
	// handleAllChecked
	var handleAllChecked = function() {
		$("#checkAll").on('change',function() {
			$("td input, .email-list .custom-checkbox input").prop('checked', $(this).prop("checked"));
		});
		$(".checkAllInput").on('click',function() {
			jQuery(this).closest('.ItemsCheckboxSec').find('input[type="checkbox"]').prop('checked', true);		
		});
		$(".unCheckAllInput").on('click',function() {
			jQuery(this).closest('.ItemsCheckboxSec').find('input[type="checkbox"]').prop('checked', false);		
		});
	}

	var handleDatetimepicker = function(){
		if(jQuery('.bt-datepicker').length > 0){
			$(".bt-datepicker").datepicker({ 
				autoclose: true, 
			}).datepicker('update', new Date());
		}
	}

	var handleCkEditor = function(){
		if(jQuery("#ckeditor").length>0) {
			ClassicEditor
			.create( document.querySelector( '#ckeditor' ), {
				simpleUpload: {
                    uploadUrl: 'ckeditor-upload.php', 
                }
			} )
			.then( editor => {
				window.editor = editor;
			} )
			.catch( err => {
				console.error( err.stack );
			} );
		}
	}

	var handleThemeMode = function() {
   
		if(jQuery(".dlab-theme-mode").length>0) {
            jQuery('.dlab-theme-mode').on('click',function(){
                jQuery(this).toggleClass('active');
                if(jQuery(this).hasClass('active')){
                    jQuery('body').attr('data-theme-version','dark');
                    setCookie('version', 'dark');
                    jQuery('#theme_version').val('dark');
                    
                }else{
                    jQuery('body').attr('data-theme-version','light');
                    setCookie('version', 'light');
                    jQuery('#theme_version').val('light');	
                    		
                }
                $('.default-select').selectpicker('refresh');
            });
            var version = getCookie('version');
            
            jQuery('body').attr('data-theme-version', version);
            jQuery('.dlab-theme-mode').removeClass('active');
            setTimeout(function(){
                if(jQuery('body').attr('data-theme-version') === "dark")
                {
                    jQuery('.dlab-theme-mode').addClass('active');
                }
            },1500)
        }
		
		
	}
	
	var handleChatbox = function() {
		jQuery('.bell-link').on('click',function(){
			jQuery('.chatbox').addClass('active');
		});
		jQuery('.chatbox-close').on('click',function(){
			jQuery('.chatbox').removeClass('active');
		});
	}

	var handleDzChatUser = function() {
		jQuery('.dlab-chat-user-box .dlab-chat-user').on('click',function(){
			jQuery('.dlab-chat-user-box').addClass('d-none');
			jQuery('.dlab-chat-history-box').removeClass('d-none');
			//$(".chatbox .msg_card_body").height(vHeightArea());
			//$(".chatbox .msg_card_body").css('height',vHeightArea());
		}); 
		
		jQuery('.dlab-chat-history-back').on('click',function(){
			jQuery('.dlab-chat-user-box').removeClass('d-none');
			jQuery('.dlab-chat-history-box').addClass('d-none');
		}); 
		
		jQuery('.dlab-fullscreen').on('click',function(){
			jQuery('.dlab-fullscreen').toggleClass('active');
		});
	}

	var vHeight = function(){
		var ch = $(window).height() - 190;
		$(".chatbox .msg_card_body").css('height',ch);
	}

	var heartBlast = function (){
		$(".heart").on("click", function() {
			$(this).toggleClass("heart-blast");
		});
	}

	var handleHorizontalDropDown = function(){
		// it will push all the submenu inside in the window 
		$('.metismenu li').hover(
			function() {
				var $submenu = $(this).children('ul');
				var $windowEdge = $('#main-wrapper').width();
				var $leftOffset = ($(window).width() - $('#main-wrapper').width())/2;
				
				if($('html').attr('dir')==='ltr'){
					var $menuRightEdge = ($(this).offset().left + $(this).outerWidth() + $submenu.outerWidth()) - $leftOffset;
					if($menuRightEdge > $windowEdge) {
						if($(this).parent('ul.metismenu').length > 0){
							$submenu.css({ left: 'auto', right: '0' });
						}else{
							$submenu.css({ left: 'auto', right: '100%' });
						}
					}
				}else{
					var $rightOffset = $windowEdge - ($(this).offset().left + $(this).outerWidth() - $leftOffset); 
					var $menuLeftEdge = $rightOffset + $(this).outerWidth() + $submenu.outerWidth();
					console.log($rightOffset);
					if($menuLeftEdge > $windowEdge) {
						if($(this).parent('ul.metismenu').length > 0){
							$submenu.css({ left: '0', right: 'auto' });
						}else{
							$submenu.css({ left: '100%', right: 'auto' });
						}
					}
				}
			},
			function() {
				$(this).children('ul').removeAttr('style');
			}
		);
	}

	var domoPanel = function(){
		$('.dlab-demo-trigger').on('click', function() {
				$('.dlab-demo-panel').addClass('show');
		  });
		  $('.dlab-demo-close, .bg-close').on('click', function() {
				$('.dlab-demo-panel').removeClass('show');
		  });
		  
		  $('.dlab-demo-bx').on('click', function() {
			  $('.dlab-demo-bx').removeClass('demo-active');
			  $(this).addClass('demo-active');
		  });
	} 

	var handleAddWishlist = function(){
		$('.add-wishlist-btn').on('click',function(){
			$(this).toggleClass('active');
			var num = $(this).find('span').text();
			if($(this).hasClass('active')){
				$(this).find('span').text(parseInt(num) + 1);
			}else{
				$(this).find('span').text(parseInt(num) - 1);
			}
		})
	}

	/* Function ============ */
	return {
		init:function(){
			handleMetisMenu();
			handleNavigation();
			handleCurrentActive();
			handleMiniSidebar();
			//handleMinHeight();
			handleDataAction();
			handleHeaderHight();
			handleBtnNumber();
			handleshowPass();
            handleLightgallery();
			handleAllChecked();
			handleCkEditor();
			handleDatetimepicker();
			handleThemeMode();
			handleChatbox();
			handleDzChatUser(); 
			vHeight();
			heartBlast();
			setTimeout(function(){
				handleHorizontalDropDown();
			},500);
			domoPanel();
			handleAddWishlist();
		},
		load:function(){
			
		},
		resize:function(){
			vHeight();
		},
	}
	
}();


/* Document.ready Start */	
jQuery(document).ready(function() {
	
	$('[data-bs-toggle="popover"]').popover();
	
	Edumin.init();
	
});
/* Document.ready END */

/* Window Load START */
jQuery(window).on('load',function () {
	'use strict'; 
	
	$('#preloader').fadeOut(500);
    $('#main-wrapper').addClass('show');
	$('select').selectpicker();
	
	Edumin.load();
	
});
/*  Window Load END */
/* Window Resize START */
jQuery(window).on('resize',function () {
	'use strict'; 
	Edumin.resize();
});
/*  Window Resize END */