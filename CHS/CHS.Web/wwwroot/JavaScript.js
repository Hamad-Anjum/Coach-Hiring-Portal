

var screenWidth = $(window).width();

function getUrlParams(dParam) {
    var dPageURL = window.location.search.substring(1),
        dURLVariables = dPageURL.split('&'),
        dParameterName,
        i;

    for (i = 0; i < dURLVariables.length; i++) {
        dParameterName = dURLVariables[i].split('=');

        if (dParameterName[0] === dParam) {
            return dParameterName[1] === undefined ? true : decodeURIComponent(dParameterName[1]);
        }
    }
}

function EmojiPicker() {
    var val = null;
    document.querySelector('emoji-picker')
        .addEventListener('emoji-click', event => {
            console.log(event.detail);
            val = event;
        });
}

function TryAgain() {
    $('body').append('<h6 class="alert alert-custom animated bounceInDown msg" style="border-radius:10px;background-color:#198754;color:#eee;position:fixed;top:6.6rem;right:2rem;z-index:12312;letter-spacing:0.09rem;"> Try Again</h6>');

    setTimeout(function () {
        $('body h6.msg').addClass('bounceOutUp');
    }, 4000);

    setTimeout(function () {
        $('body h6.msg').remove();
    }, 4555);
}

function FolderRemoved(val) {
    alert(val)
}

function AddAcceptAttribute() {
    $('.mat-file-upload input').attr("accept", "image/*");
}

function ShowModal(val) {
    var myModal = new bootstrap.Modal(document.getElementById(val), {
        keyboard: false
    });
    myModal.show();
    //$('#' + val).addClass('show bs modal');
    return true;
}

function HideModal(val) {
    var myModal = new bootstrap.Modal(document.getElementById(val), {
        keyboard: false
    })
    myModal.hide();
    //$('#' + val).addClass('hidden bs modal');
    return false;
}

function imageUpload(val) {
    document.getElementById(val).click();
}

function ClickCancel(val) {
    document.getElementById(val).click();
    return false;
}

function AddBackground(e) {
    $('.btn.btn-primary.light.btn-rounded.mr-2.mb-2.' + e).toggleClass('bg-click-add');
    $('.btn.btn-primary.light.btn-rounded.mr-2.mb-2').not($('.' + e)).removeClass('bg-click-add');
}

function NavControl() {
    $('#main-wrapper').toggleClass("menu-toggle");
    //var el = document.getElementById("main-wrapper");
    ////el.classList.toggle('menu-toggle');
    //var colClass = el.className.toString();
    //alert(colClass);

    $(".hamburger").toggleClass("is-active");
}

function DeleteMdcRippleLine() {
    $('label.mdc-text-field--textarea span.mdc-line-ripple').remove();
}

function SavedSuccessFully() {
    $('body').append('<h6 class="alert alert-custom animated bounceInDown jello msg" style="border-radius:10px;background-color:#198754;color:#eee;position:fixed;top:6.6rem;right:2rem;z-index:12312;letter-spacing:0.09rem;"> Successfully <strong>Updated</strong>.</h6>');

    setTimeout(function () {
        $('body h6.msg').addClass('bounceOutUp');
    }, 4000);

    setTimeout(function () {
        $('body h6.msg').remove();
    }, 4555);
}

function NotSavedSuccessFully() {
    $('body').append('<h6 class="alert alert-custom animated bounceInDown jello msg" style="border-radius:10px;background-color:#DC3545;color:#eee;position:fixed;top:6.6rem;right:2rem;z-index:12312;letter-spacing:0.09rem;"> Failed to <strong>Update</strong>.</h6>');

    setTimeout(function () {
        $('body h6.msg').addClass('bounceOutUp');
    }, 3500);

    setTimeout(function () {
        $('body h6.msg').remove();
    }, 3555);
}

function AddTabClasses() {
    $('.mdc-tab-scroller__scroll-area').addClass('mdc-tab-scroller__scroll-area--scroll');
}

function removeCssClass(val) {

    $('#' + val).removeClass("mdc-table");
}

function divClick() {
    $('#div1').hide();
}

window.Custom2 = function () {
    //alert('');
    $('#preloader').fadeOut(500);
    $('#main-wrapper').addClass('show');

     //if($('body').attr('data-sidebar-position') === "fixed") {
     //    $('.deznav-scroll').slimscroll({
     //        position: "right",
     //        size: "5px",
     //        height: "100%",
     //        color: "transparent"
     //    });
     //}
    $('select').selectpicker();
}

window.Custom3 = function () {
    (function ($) {
        "use strict";

        $("#menu").metisMenu();

        // $(function() {
        //     AOS.init({
        //         duration: 1500,
        //         easing: 'ease-in-out',
        //     });
        // });

        $("#checkAll").change(function () {
            $("td input:checkbox").prop('checked', $(this).prop("checked"));
        });





        /* $('.sidebar-right-inner').slimscroll({
            position: "left",
            size: "5px",
            height: "100%",
            color: "#c6c8c9"
        }); */

        //$(".nav-control").on('click', function () {

        //    $('#main-wrapper').toggleClass("menu-toggle");

        //    $(".hamburger").toggleClass("is-active");
        //});

        //to keep the current page active

        for (var nk = window.location,
            o = $("ul#menu a").filter(function () {
                return this.href == nk;
            })
                .addClass("mm-active")
                .parent()
                .addClass("mm-active"); ;) {
            // console.log(o)
            if (!o.is("li")) break;
            o = o.parent()
                .addClass("mm-show")
                .parent()
                .addClass("mm-active");
        }



        $("ul#menu>li").on('click', function () {
            const sidebarStyle = $('body').attr('data-sidebar-style');
            if (sidebarStyle === 'mini') {
                console.log($(this).find('ul'))
                $(this).find('ul').stop()
            }
        })



        // var win_w = window.outerWidth;
        var win_h = window.outerHeight;
        var win_h = window.outerHeight;
        if (win_h > 0 ? win_h : screen.height) {
            $(".content-body").css("min-height", (win_h + 60) + "px");
        };



        $('a[data-action="collapse"]').on("click", function (i) {
            i.preventDefault(),
                $(this).closest(".card").find('[data-action="collapse"] i').toggleClass("mdi-arrow-down mdi-arrow-up"),
                $(this).closest(".card").children(".card-body").collapse("toggle");
        });

        $('a[data-action="expand"]').on("click", function (i) {
            i.preventDefault(),
                $(this).closest(".card").find('[data-action="expand"] i').toggleClass("icon-size-actual icon-size-fullscreen"),
                $(this).closest(".card").toggleClass("card-fullscreen");
        });



        $('[data-action="close"]').on("click", function () {
            $(this).closest(".card").removeClass().slideUp("fast");
        });

        $('[data-action="reload"]').on("click", function () {
            var e = $(this);
            e.parents(".card").addClass("card-load"),
                e.parents(".card").append('<div class="card-loader"><i class=" ti-reload rotate-refresh"></div>'),
                setTimeout(function () {
                    e.parents(".card").children(".card-loader").remove(),
                        e.parents(".card").removeClass("card-load")
                }, 2000)
        });

        const headerHight = $('.header').innerHeight();

        $(window).scroll(function () {
            if ($('body').attr('data-layout') === "horizontal" && $('body').attr('data-header-position') === "static" && $('body').attr('data-sidebar-position') === "fixed")
                $(this.window).scrollTop() >= headerHight ? $('.deznav').addClass('fixed') : $('.deznav').removeClass('fixed')
        });


        jQuery('.dz-scroll').each(function () {

            var scroolWidgetId = jQuery(this).attr('id');
            const ps = new PerfectScrollbar('#' + scroolWidgetId, {
                wheelSpeed: 2,
                wheelPropagation: true,
                minScrollbarLength: 20
            });
        })

        jQuery('.metismenu > .mm-active ').each(function () {
            if (!jQuery(this).children('ul').length > 0) {
                jQuery(this).addClass('active-no-child');
            }
        });
        if (screenWidth <= 991) {
            jQuery('.menu-tabs .nav-link').on('click', function () {
                if (jQuery(this).hasClass('open')) {
                    jQuery(this).removeClass('open');
                    jQuery('.fixed-content-box').removeClass('active');
                    jQuery('.hamburger').show();
                } else {
                    jQuery('.menu-tabs .nav-link').removeClass('open');
                    jQuery(this).addClass('open');
                    jQuery('.fixed-content-box').addClass('active');
                    jQuery('.hamburger').hide();
                }
                //jQuery('.fixed-content-box').toggleClass('active');
            });
            jQuery('.close-fixed-content').on('click', function () {
                jQuery('.fixed-content-box').removeClass('active');
                jQuery('.hamburger').removeClass('is-active');
                jQuery('#main-wrapper').removeClass('menu-toggle');
                jQuery('.hamburger').show();
            });
        }
        jQuery('.bell-link').on('click', function () {
            jQuery('.chatbox').addClass('active');
        });
        jQuery('.chatbox-close').on('click', function () {
            jQuery('.chatbox').removeClass('active');
        });

        const qs = new PerfectScrollbar('.deznav-scroll');
        // const sr = new PerfectScrollbar('.sidebar-right-inner');


        //plugin bootstrap minus and plus
        $('.btn-number').on('click', function (e) {
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

        jQuery('.dz-chat-user-box .dz-chat-user').on('click', function () {
            jQuery('.dz-chat-user-box').addClass('d-none');
            jQuery('.dz-chat-history-box').removeClass('d-none');
        });

        jQuery('.dz-chat-history-back').on('click', function () {
            jQuery('.dz-chat-user-box').removeClass('d-none');
            jQuery('.dz-chat-history-box').addClass('d-none');
        });

        jQuery('.dz-fullscreen').on('click', function () {
            jQuery('.dz-fullscreen').toggleClass('active');
        });





        jQuery('.dz-fullscreen').on('click', function (e) {
            if (document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement) {
                /* Enter fullscreen */
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen(); /* IE/Edge */
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen(); /* Firefox */
                } else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen(); /* Chrome, Safari & Opera */
                }
            }
            else { /* exit fullscreen */
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.msRequestFullscreen) {
                    document.documentElement.msRequestFullscreen();
                }
            }
        });

        // Chart All Pages

    })(jQuery);
}

window.DevNav = function () {
    (function ($) {

        var direction = getUrlParams('dir');
        if (direction != 'rtl') { direction = 'ltr'; }

        var dezSettingsOptions = {
            typography: "poppins",
            version: "light",
            layout: "Vertical",
            headerBg: "color_1",
            navheaderBg: "color_3",
            sidebarBg: "color_3",
            sidebarStyle: "full",
            sidebarPosition: "fixed",
            headerPosition: "fixed",
            containerLayout: "full",
            direction: direction
        };

        new dezSettings(dezSettingsOptions);

        jQuery(window).on('resize', function () {
            new dezSettings(dezSettingsOptions);
        });

    })(jQuery);
}

window.Dashboard1 = function () {
    (function ($) {
        /* "use strict" */

        var dzChartlist = function () {
            let draw = Chart.controllers.line.__super__.draw; //draw shadow
            var screenWidth = $(window).width();

            var donutChart = function () {
                $("span.donut").peity("donut", {
                    width: "75",
                    height: "75"
                });
            }
            var lineChart = function () {
                //dual line chart
                if (jQuery('#lineChart').length > 0) {
                    const lineChart = document.getElementById("lineChart").getContext('2d');
                    //generate gradient
                    const lineChart_3gradientStroke1 = lineChart.createLinearGradient(500, 0, 100, 0);
                    lineChart_3gradientStroke1.addColorStop(0, "rgba(100, 24, 195, 1)");
                    lineChart_3gradientStroke1.addColorStop(1, "rgba(100, 24, 195, 0.5)");

                    const lineChart_3gradientStroke2 = lineChart.createLinearGradient(500, 0, 100, 0);
                    lineChart_3gradientStroke2.addColorStop(0, "rgba(27, 208, 132, 1)");
                    lineChart_3gradientStroke2.addColorStop(1, "rgba(27, 208, 132, 1)");

                    Chart.controllers.line = Chart.controllers.line.extend({
                        draw: function () {
                            draw.apply(this, arguments);
                            let nk = this.chart.chart.ctx;
                            let _stroke = nk.stroke;
                            nk.stroke = function () {
                                nk.save();
                                nk.shadowColor = 'rgba(78, 54, 226, .5)';
                                nk.shadowBlur = 10;
                                nk.shadowOffsetX = 0;
                                nk.shadowOffsetY = 0;
                                _stroke.apply(this, arguments)
                                nk.restore();
                            }
                        }
                    });

                    lineChart.height = 20;

                    new Chart(lineChart, {
                        type: 'line',
                        data: {
                            defaultFontFamily: 'Poppins',
                            labels: ["Week 01", "Week 02", "Week 03", "Week 04", "Week 05", "Week 06", "Week 07", "Week 08", "Week 09", "Week 10"],
                            datasets: [
                                {
                                    label: "My First dataset",
                                    data: [78, 80, 20, 40, 75, 75, 25, 40, 10, 30],
                                    borderColor: 'rgba(78, 54, 226, 1)',
                                    borderWidth: "5",
                                    pointHoverRadius: 10,
                                    backgroundColor: 'transparent',
                                    pointBackgroundColor: 'rgba(78, 54, 226, 1)',
                                }, {
                                    label: "My First dataset",
                                    data: [30, 50, 30, 40, 30, 40, 20, 10, 10, 10],
                                    borderColor: lineChart_3gradientStroke2,
                                    borderWidth: "5",
                                    backgroundColor: 'transparent',
                                    pointHoverRadius: 10,
                                    pointBorderWidth: 5,
                                    pointBorderColor: 'rgba(255, 255, 255, 1)',
                                    pointBackgroundColor: 'rgba(27, 208, 132, 1)'
                                }
                            ]
                        },
                        options: {
                            legend: false,
                            tooltips: {
                                mode: 'index',
                                intersect: false,
                            },
                            hover: {
                                mode: 'nearest',
                                intersect: true
                            },
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: true,
                                        max: 100,
                                        min: 0,
                                        stepSize: 20,
                                        padding: 10
                                    }
                                }],
                                xAxes: [{
                                    ticks: {
                                        padding: 5
                                    }
                                }]
                            },
                            elements: {
                                point: {
                                    radius: 0
                                }
                            }
                        }
                    });
                }
            }
            /* Function ============ */
            return {
                init: function () {
                },


                load: function () {
                    donutChart();
                    lineChart();
                },

                resize: function () {

                }
            }

        }();

        jQuery(document).ready(function () {
        });

        jQuery(window).on('load', function () {
            setTimeout(function () {
                dzChartlist.load();
            }, 1000);

        });

        jQuery(window).on('resize', function () {


        });

    })(jQuery);
}


window.LoadChart = function () {
    myChartlist.load();
}

var myChartlist = function () {
    let draw = Chart.controllers.line.__super__.draw; //draw shadow
    var screenWidth = $(window).width();

    var donutChart = function () {
        $("span.donut").peity("donut", {
            width: "75",
            height: "75"
        });
    }
    var lineChart = function () {
        //dual line chart
        if (jQuery('#lineChart').length > 0) {
            const lineChart = document.getElementById("lineChart").getContext('2d');
            //generate gradient
            const lineChart_3gradientStroke1 = lineChart.createLinearGradient(500, 0, 100, 0);
            lineChart_3gradientStroke1.addColorStop(0, "rgba(100, 24, 195, 1)");
            lineChart_3gradientStroke1.addColorStop(1, "rgba(100, 24, 195, 0.5)");

            const lineChart_3gradientStroke2 = lineChart.createLinearGradient(500, 0, 100, 0);
            lineChart_3gradientStroke2.addColorStop(0, "rgba(27, 208, 132, 1)");
            lineChart_3gradientStroke2.addColorStop(1, "rgba(27, 208, 132, 1)");

            Chart.controllers.line = Chart.controllers.line.extend({
                draw: function () {
                    draw.apply(this, arguments);
                    let nk = this.chart.chart.ctx;
                    let _stroke = nk.stroke;
                    nk.stroke = function () {
                        nk.save();
                        nk.shadowColor = 'rgba(78, 54, 226, .5)';
                        nk.shadowBlur = 10;
                        nk.shadowOffsetX = 0;
                        nk.shadowOffsetY = 0;
                        _stroke.apply(this, arguments)
                        nk.restore();
                    }
                }
            });

            lineChart.height = 20;

            new Chart(lineChart, {
                type: 'line',
                data: {
                    defaultFontFamily: 'Poppins',
                    labels: ["Week 01", "Week 02", "Week 03", "Week 04", "Week 05", "Week 06", "Week 07", "Week 08", "Week 09", "Week 10"],
                    datasets: [
                        {
                            label: "My First dataset",
                            data: [78, 80, 20, 40, 75, 75, 25, 40, 10, 30],
                            borderColor: 'rgba(78, 54, 226, 1)',
                            borderWidth: "5",
                            pointHoverRadius: 10,
                            backgroundColor: 'transparent',
                            pointBackgroundColor: 'rgba(78, 54, 226, 1)',
                        }, {
                            label: "My First dataset",
                            data: [30, 50, 30, 40, 30, 40, 20, 10, 10, 10],
                            borderColor: lineChart_3gradientStroke2,
                            borderWidth: "5",
                            backgroundColor: 'transparent',
                            pointHoverRadius: 10,
                            pointBorderWidth: 5,
                            pointBorderColor: 'rgba(255, 255, 255, 1)',
                            pointBackgroundColor: 'rgba(27, 208, 132, 1)'
                        }
                    ]
                },
                options: {
                    legend: false,
                    tooltips: {
                        mode: 'index',
                        intersect: false,
                    },
                    hover: {
                        mode: 'nearest',
                        intersect: true
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true,
                                max: 100,
                                min: 0,
                                stepSize: 20,
                                padding: 10
                            }
                        }],
                        xAxes: [{
                            ticks: {
                                padding: 5
                            }
                        }]
                    },
                    elements: {
                        point: {
                            radius: 0
                        }
                    }
                }
            });
        }
    }
    /* Function ============ */
    return {
        init: function () {
        },


        load: function () {
            donutChart();
            lineChart();
        },

        resize: function () {

        }
    }

}();


window.all = function () {
    alert('asd');
}

window.Base1 = function () {
    (function ($) {
        "use strict"

        // Daterange picker
        $('.input-daterange-datepicker').daterangepicker({
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-danger',
            cancelClass: 'btn-inverse'
        });
        $('.input-daterange-timepicker').daterangepicker({
            timePicker: true,
            format: 'MM/DD/YYYY h:mm A',
            timePickerIncrement: 30,
            timePicker12Hour: true,
            timePickerSeconds: false,
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-danger',
            cancelClass: 'btn-inverse'
        });
        $('.input-limit-datepicker').daterangepicker({
            format: 'MM/DD/YYYY',
            minDate: '06/01/2015',
            maxDate: '06/30/2015',
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-danger',
            cancelClass: 'btn-inverse',
            dateLimit: {
                days: 6
            }
        });
    })(jQuery);
}

window.Base2 = function () {
    (function ($) {
        "use strict"

        //date picker classic default
        $('.datepicker-default').pickadate();

    })(jQuery);
}

window.Base3 = function () {
    (function ($) {
        "use strict"

        // MAterial Date picker
        $('#mdate').bootstrapMaterialDatePicker({
            weekStart: 0,
            time: false
        });
        $('#timepicker').bootstrapMaterialDatePicker({
            format: 'HH:mm',
            time: true,
            date: false
        });
        $('#date-format').bootstrapMaterialDatePicker({
            format: 'dddd DD MMMM YYYY - HH:mm'
        });

        $('#min-date').bootstrapMaterialDatePicker({
            format: 'DD/MM/YYYY HH:mm',
            minDate: new Date()
        });

    })(jQuery);
}

function SkillsCheckbox(ids) {
    alert(ids);
    if (ids != null) {
        for (var i = 0; i < ids.length; i++) {
            //document.getElementById(ids[i]).checked = true;
            var element = document.getElementById(ids[i]);
            alert(element);
            //alert(ids[i]);
            //document.
        }
    }
}

function CertificationsCheckbox(ids) {
    alert(ids);
    if (ids != null) {
        for (var i = 0; i < ids.length; i++) {
            //document.getElementById(ids[i]).checked = true;
            var element = document.getElementById(ids[i]);
            alert(element);
            //alert(ids[i]);
            //document.
        }
    }
}

//window.LoadForm = function () {
//    (function ($) {
//        "use strict"

//        var form = $("#step-form-horizontal");
//        form.children('div').steps({
//            headerTag: "h4",
//            bodyTag: "section",
//            transitionEffect: "slideLeft",
//            autoFocus: true,
//            transitionEffect: "slideLeft",
//            onStepChanging: function (event, currentIndex, newIndex) {
//                form.validate().settings.ignore = ":disabled,:hidden";
//                return form.valid();
//            }
//        });

//    })(jQuery);
//}

//window.Validate = function () {
//    jQuery(".form-valide").validate({
//        rules: {
//            "val-username": {
//                required: !0,
//                minlength: 3
//            },
//            "val-email": {
//                required: !0,
//                email: !0
//            },
//            "val-password": {
//                required: !0,
//                minlength: 5
//            },
//            "val-confirm-password": {
//                required: !0,
//                equalTo: "#val-password"
//            },
//            "val-select2": {
//                required: !0
//            },
//            "val-select2-multiple": {
//                required: !0,
//                minlength: 2
//            },
//            "val-suggestions": {
//                required: !0,
//                minlength: 5
//            },
//            "val-skill": {
//                required: !0
//            },
//            "val-currency": {
//                required: !0,
//                currency: ["$", !0]
//            },
//            "val-website": {
//                required: !0,
//                url: !0
//            },
//            "val-phoneus": {
//                required: !0,
//                phoneUS: !0
//            },
//            "val-digits": {
//                required: !0,
//                digits: !0
//            },
//            "val-number": {
//                required: !0,
//                number: !0
//            },
//            "val-range": {
//                required: !0,
//                range: [1, 5]
//            },
//            "val-terms": {
//                required: !0
//            }
//        },
//        messages: {
//            "val-username": {
//                required: "Please enter a username",
//                minlength: "Your username must consist of at least 3 characters"
//            },
//            "val-email": "Please enter a valid email address",
//            "val-password": {
//                required: "Please provide a password",
//                minlength: "Your password must be at least 5 characters long"
//            },
//            "val-confirm-password": {
//                required: "Please provide a password",
//                minlength: "Your password must be at least 5 characters long",
//                equalTo: "Please enter the same password as above"
//            },
//            "val-select2": "Please select a value!",
//            "val-select2-multiple": "Please select at least 2 values!",
//            "val-suggestions": "What can we do to become better?",
//            "val-skill": "Please select a skill!",
//            "val-currency": "Please enter a price!",
//            "val-website": "Please enter your website!",
//            "val-phoneus": "Please enter a US phone!",
//            "val-digits": "Please enter only digits!",
//            "val-number": "Please enter a number!",
//            "val-range": "Please enter a number between 1 and 5!",
//            "val-terms": "You must agree to the service terms!"
//        },

//        ignore: [],
//        errorClass: "invalid-feedback animated fadeInUp",
//        errorElement: "div",
//        errorPlacement: function (e, a) {
//            jQuery(a).parents(".form-group > div").append(e)
//        },
//        highlight: function (e) {
//            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
//        },
//        success: function (e) {
//            jQuery(e).closest(".form-group").removeClass("is-invalid"), jQuery(e).remove()
//        },
//    });
//}

//window.ValidateIcon = function () {
//    jQuery(".form-valide-with-icon").validate({
//        rules: {
//            "val-username": {
//                required: !0,
//                minlength: 3
//            },
//            "val-password": {
//                required: !0,
//                minlength: 5
//            }
//        },
//        messages: {
//            "val-username": {
//                required: "Please enter a username",
//                minlength: "Your username must consist of at least 3 characters"
//            },
//            "val-password": {
//                required: "Please provide a password",
//                minlength: "Your password must be at least 5 characters long"
//            }
//        },

//        ignore: [],
//        errorClass: "invalid-feedback animated fadeInUp",
//        errorElement: "div",
//        errorPlacement: function (e, a) {
//            jQuery(a).parents(".form-group > div").append(e)
//        },
//        highlight: function (e) {
//            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
//        },
//        success: function (e) {
//            jQuery(e).closest(".form-group").removeClass("is-invalid").addClass("is-valid")
//        }
//    });
//}