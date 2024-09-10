var BLUE_CANVAS_SETUP={connectorUrl:"https://utexas.bluera.com/utexasBlueConnector/",canvasAPI:"https://utexas.instructure.com",domainName:"com.explorance",consumerID:"4ka4tx4nLdejNUrQDGL9zA==",defaultLanguage:"en-us"},BlueCanvasJS=document.createElement("script");BlueCanvasJS.setAttribute("type","text/javascript");BlueCanvasJS.setAttribute("src","https://utexas.bluera.com/utexasBlueConnector//Scripts/Canvas/BlueCanvas.min.js");document.getElementsByTagName("head")[0].appendChild(BlueCanvasJS);

window.ALLY_CFG = {
    'baseUrl': 'https://prod.ally.ac',
    'clientId': 14392,
    'lti13Id': '10170000000000610'
};
$.getScript(ALLY_CFG.baseUrl + '/integration/canvas/ally.js');

function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
        // call on next available tick
        setTimeout(fn, 0);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}

// Wait for an element specified by a selector to exist, then fire a callback function
function utexas_onElementRendered(selector, cb, _attempts) {
    var el = document.querySelector(selector);
    _attempts = ++_attempts || 1;
    if (el) { cb(el);return; }
    if (_attempts == 60) return;
    setTimeout(function() {
        utexas_onElementRendered(selector, cb, _attempts);
    }, 250);
}

function utexas_mod_warn_course_date_click() {
    // Canvas course settings, in the 'Participation' section, allow choice of 'term' or 'course'.
    // Start and End date choosers are displayed, but disabled if choice is 'term', with no
    // explanation of how to activate them.  Add an explanation.
    utexas_onElementRendered('.CourseAvailabilityOptions > fieldset > span', function(el) {
        el.insertAdjacentHTML("afterbegin",
          "<div style='max-width:400px;font-size:12px;font-weight:300;font-family:LatoWeb,Lato,sans-serif'>" +
          "Student participation dates default to 'Term.' To edit these dates, select 'Course' from the drop-down menu below, enter the desired dates, and click the 'Update Course Details' button below." +
          "</div>"
        );
    });
}

function utexas_mod_hide_course_delete_conclude_buttons() {
    if (window.ENV.current_user_roles && (window.ENV.current_user_roles.indexOf('teacher') > -1)) {
        utexas_onElementRendered('.Button--course-settings[href*=\'confirm_action?event=conclude\']', function (el) {
            el.style.display = 'none';
        });
        utexas_onElementRendered('.Button--course-settings[href*=\'confirm_action?event=delete\']', function (el) {
            el.style.display = 'none';
        });
    }
}

function utexas_mod_alter_add_people_options() {
    utexas_onElementRendered('a#addUsers', function(el) {
        el.addEventListener("click", function () {
            utexas_onElementRendered('label[for=peoplesearch_radio_unique_id]', function (el) {
                el.querySelectorAll('span')[1].innerHTML = "UT EID (preferred)";
            });
            utexas_onElementRendered('label[for=peoplesearch_radio_sis_user_id]', function (el) {
                el.style.display = 'none';
            });
        });
    });
}

function utexas_mod_alter_gdocs_dialog_text() {
    var gd_dialog = document.getElementById('unregistered_service_google_drive_dialog');
    var gd_text_div = gd_dialog.children[0];
    var gd_btn_div = gd_dialog.children[1];

    var replacement_msg = "<h3 style='font-size: 1.2em; font-weight: bold;'>UT Austin Google Docs Access</h3>"
      + '<p>Please ensure you are logged in with your <a href="https://utmail.utexas.edu/">UTmail</a> Google Account (name@utexas.edu). Using a standard Gmail account (name@gmail.com) is not permitted. '
      + " Click <a href='https://gmail.com' target='new'>here</a> to see which account you are logged in as. If you do not have a UTmail account you can sign up for one <a href=\"http://utmail.utexas.edu/\">here.</a> "
      + "</p><p><input type='checkbox' id='utexas_gdrive_agreement'>"
      + "&nbsp;Check this box only when you are certain you are logged in with your UTmail account.</p>"
      + "<p style='font-size:.8em'>Information on accessibility accommodations per the Services for Students with Disabilities (SSD) office are available <a href=\"https://utexas.instructure.com/courses/633028/wiki/accessibility-workarounds\">here</a>.</p>";

    gd_text_div.innerHTML = replacement_msg;

    var orig_display_style = gd_btn_div.style.display;
    gd_btn_div.style.display = 'none';

    document.getElementById('utexas_gdrive_agreement').addEventListener('click', function(evt) {
        var checked = evt.target.checked;
        gd_btn_div.style.display = checked ? orig_display_style : 'none';
    }, false);
}

function utexas_mod_add_footer_links() {
    utexas_onElementRendered('#footer-links', function(el) {
        el.insertAdjacentHTML("beforeend",
          "<a href='https://it.utexas.edu/policies/web-privacy'>UT-Austin Web Privacy Policy</a>" +
          "<a href='https://it.utexas.edu/policies/web-accessibility'>UT-Austin Web Accessibility Policy</a>" +
          "<a href='https://www.facebook.com/UTAustinTX'>UT-Austin Facebook</a>" +
          "<a href='https://twitter.com/UTAustin'>UT-Austin Twitter</a>"
        );
    });
}

/*
utexas_mod_* functions are changes to the behavior of Canvas.
Call these functions on page load for specific pages.
 */
function utexas_mod_setup() {
    if (window.location.pathname.search('/courses/\\d+/settings') >= 0) {
        utexas_mod_hide_course_delete_conclude_buttons();
        utexas_mod_warn_course_date_click();
    }

    if (window.location.pathname.search('/courses/\\d+/users') >= 0) {
        utexas_mod_alter_add_people_options();
    }

    if (window.location.pathname.search('/profile/settings') >= 0) {
        utexas_mod_alter_gdocs_dialog_text();
    }

    if (window.location.pathname === '' || window.location.pathname === '/') {
        utexas_mod_add_footer_links();
    }
}


docReady(utexas_mod_setup);