
$('#logout_click').click(function () {
    var data = { 'op': 'log_out' };
    var s = function (msg) {
        document.cookie = "currentpage=;expires=Wed; 01 Jan 1970";
        window.location = 'login.html';
        return;
    };
    var e = function (x, h, e) {
        document.cookie = "currentpage=;expires=Wed; 01 Jan 1970";
        window.location = 'login.html';
        return;
    };
    callHandler(data, s, e);
});
var groupsdata;
$(function () {
    var data = { 'op': 'InitilizeGroups' };
    var s = function (msg) {
        if (msg) {
            Groupsfilling(msg);
        }
        else {
        }
    };
    var e = function (x, h, e) {
    };
    callHandler(data, s, e);

    function Groupsfilling(data) {
        groupsdata = data;
        var grpstr = "<ul><li><input type='checkbox' value='All Groups' /><span style='vertical-align: top;padding:2px;'>All Groups</span></li>";
        for (var grparray in groupsdata) {
            var grp1 = groupsdata[grparray].groupname.trim();
            grpstr += "<li><input type='checkbox' value=" + grp1 + " /><span style='vertical-align: top;padding:2px;'>" + grp1 + "</span></li>";
        }
        grpstr += "</ul>";
        $('#div_vehmutliSelect').append(grpstr);

        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });
        $('#ckballveh').on('ifChanged', function (event) {
            onallcheck(this);
        });
        $('.ckblvvehscls').on('ifChanged', function (event) {
            var ckdval = $(this);
            oncheck(this, ckdval[0].id);
        });

        $(".dropdownckecklist dt a").on('click', function () {
            $(".dropdownckecklist dd ul").slideToggle('fast');
        });

        $(".dropdownckecklist dd ul li a").on('click', function () {
            $(".dropdownckecklist dd ul").hide();
        });

        function getSelectedValue(id) {
            return $("#" + id).find("dt a span.value").html();
        }

        $(document).bind('click', function (e) {
            var $clicked = $(e.target);
            if (!$clicked.parents().hasClass("dropdownckecklist")) $(".dropdownckecklist dd ul").hide();
        });

        $('.mutliSelect input[type="checkbox"]').on('click', function () {
            //var title = $(this).closest('.mutliSelect').find('input[type="checkbox"]').val();
            var title = $(this).val();
            if (title == "All Groups") {
                var spanval = "";
                var div_vehmutliSelect = $('#div_vehmutliSelect').find('span');
                $(div_vehmutliSelect).each(function () {
                    $('span[title="' + $(this).html() + ',' + '"]').remove();
                });
                if ($(this).is(':checked')) {
                    var html = "";
                    $(div_vehmutliSelect).each(function () {
                        spanval += $(this).html() + ',';
                        var grpcbx = $(this).closest('li').find('input');
                        grpcbx[0].checked = true;
                        if ($(this).html() != "All Groups")
                            html += '<span title="' + $(this).html() + ',' + '">' + $(this).html() + ',' + '</span>';
                    });
                    if (html == "") {
                        $(".hida").show();
                    }
                    else {
                        $('.multiSel').append(html);
                        $(".hida").hide();
                    }
                } else {
                    $(div_vehmutliSelect).each(function () {
                        var grpcbx = $(this).closest('li').find('input');
                        grpcbx[0].checked = false;
                        $('span[title="' + $(this).html() + ',' + '"]').remove();
                    });
                    $('.hida').css('display', 'block');
                }
                filvehdiv(spanval);
            }
            else {
                title = $(this).val() + ",";
                if ($(this).is(':checked')) {
                    var html = '<span title="' + title + '">' + title + '</span>';
                    $('.multiSel').append(html);
                    $(".hida").hide();
                } else {
                    $('span[title="' + title + '"]').remove();
                }

                var div_vehmutliSelect = $('#div_vehmutliSel').find('span');
                var spanval = "";
                $(div_vehmutliSelect).each(function () {
                    spanval += $(this).html() + ',';
                });
                if (spanval.indexOf(',') > -1) {
                    spanval = spanval.substring(0, spanval.length - 1);
                    if (spanval == "All Vehicles")
                        $('.hida').css('display', 'block');
                }
                else {
                    $('.multiSel').append('<span class="hida">All Vehicles</span>');
                }
                filvehdiv(spanval);
            }
        });
    }
});
function filvehdiv(selectedgrp) {
    for (var i = checkedvehicles.length - 1; i >= 0; i--) {
        checkedvehicles.splice(i, 1);
    }
    if (selectedgrp != "All Vehicles" && selectedgrp != "") {
        var grioupsarray = new Array();
        grioupsarray = selectedgrp.split(',');
        var ddlgroup = document.getElementById('divassainedvehs');
        if (typeof groupsdata === "undefined") {
            var data = { 'op': 'InitilizeGroups' };
            var s = function (msg) {
                if (msg) {
                    Groupsfilling(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);

            function Groupsfilling(data) {
                groupsdata = data;
                var vehiclenos = new Array();
                for (var grpdata in grioupsarray) {
                    for (var grparray in groupsdata) {
                        var grp1 = groupsdata[grparray].groupname.trim();
                        var grp2 = grioupsarray[grpdata].trim();
                        if (grp1 == grp2) {
                            var vehicleids = groupsdata[grparray].vehicleno;
                            var vehkeys = Object.keys(vehicleids);
                            vehkeys.forEach(function (veh) {
                                vehiclenos.push({ vehicleno: vehicleids[veh] });
                            });
                        }
                    }
                }
                $('#divassainedvehs').css('display', 'block');
                $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                    $('#divassainedvehs').setTemplateURL('Report.htm');
                    $('#divassainedvehs').processTemplate(vehiclenos);

                });
                $('#divAllvehicles').css('display', 'block');
                $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                    $('#divAllvehicles').setTemplateURL('liveview2.htm');
                    $('#divAllvehicles').processTemplate(vehiclenos);
                    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                        checkboxClass: 'icheckbox_minimal-blue',
                        radioClass: 'iradio_minimal-blue'
                    });
                    $('#ckballveh').on('ifChanged', function (event) {
                        onallcheck(this);
                    });
                    $('.ckblvvehscls').on('ifChanged', function (event) {
                        var ckdval = $(this);
                        oncheck(this, ckdval[0].id);
                    });
                    update();
                    deleteOverlays();
                    checkedvehicles = [];
                });

            }
        }
        else
            if (groupsdata == 0) {
                var data = { 'op': 'InitilizeGroups' };
                var s = function (msg) {
                    if (msg) {
                        Groupsfilling(msg);
                    }
                    else {
                    }
                };
                var e = function (x, h, e) {
                };
                callHandler(data, s, e);

                function Groupsfilling(data) {
                    groupsdata = data;
                    var vehiclenos = new Array();
                    for (var grpdata in grioupsarray) {
                        for (var grparray in groupsdata) {
                            var grp1 = groupsdata[grparray].groupname.trim();
                            var grp2 = grioupsarray[grpdata].trim();
                            if (grp1 == grp2) {
                                var vehicleids = groupsdata[grparray].vehicleno;
                                var vehkeys = Object.keys(vehicleids);
                                vehkeys.forEach(function (veh) {
                                    vehiclenos.push({ vehicleno: vehicleids[veh] });
                                });
                            }
                        }
                    }
                    $('#divassainedvehs').css('display', 'block');
                    $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                        $('#divassainedvehs').setTemplateURL('Report.htm');
                        $('#divassainedvehs').processTemplate(vehiclenos);
                    });
                    $('#divAllvehicles').css('display', 'block');
                    $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                        $('#divAllvehicles').setTemplateURL('liveview2.htm');
                        $('#divAllvehicles').processTemplate(vehiclenos);
                        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                            checkboxClass: 'icheckbox_minimal-blue',
                            radioClass: 'iradio_minimal-blue'
                        });
                        $('#ckballveh').on('ifChanged', function (event) {
                            onallcheck(this);
                        });
                        $('.ckblvvehscls').on('ifChanged', function (event) {
                            var ckdval = $(this);
                            oncheck(this, ckdval[0].id);
                        });
                        liveupdate();
                        deleteOverlays();
                        checkedvehicles = [];
                    });

                }
            }
            else {
                var vehiclenos = new Array();
                for (var grpdata in grioupsarray) {
                    for (var grparray in groupsdata) {
                        var grp1 = groupsdata[grparray].groupname.trim();
                        var grp2 = grioupsarray[grpdata].trim();
                        if (grp1 == grp2) {
                            var vehicleids = groupsdata[grparray].vehicleno;
                            var vehkeys = Object.keys(vehicleids);
                            vehkeys.forEach(function (veh) {
                                vehiclenos.push({ vehicleno: vehicleids[veh] });
                            });
                        }
                    }
                }
                $('#divassainedvehs').css('display', 'block');
                $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                    $('#divassainedvehs').setTemplateURL('Report.htm');
                    $('#divassainedvehs').processTemplate(vehiclenos);
                });
                $('#divAllvehicles').css('display', 'block');
                $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                    $('#divAllvehicles').setTemplateURL('liveview2.htm');
                    $('#divAllvehicles').processTemplate(vehiclenos);
                    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                        checkboxClass: 'icheckbox_minimal-blue',
                        radioClass: 'iradio_minimal-blue'
                    });
                    $('#ckballveh').on('ifChanged', function (event) {
                        onallcheck(this);
                    });
                    $('.ckblvvehscls').on('ifChanged', function (event) {
                        var ckdval = $(this);
                        oncheck(this, ckdval[0].id);
                    });
                    liveupdate();
                    deleteOverlays();
                    checkedvehicles = [];
                });

            }
    }
    else {
        if (typeof vehiclesdata === "undefined") {
            intializeallvehicles();
        }
        else {
            if (vehiclesdata == 0) {
                intializeallvehicles();
            }
            var vehiclenos = new Array();
            var vehkeys = Object.keys(vehiclesdata);
            vehkeys.forEach(function (veh) {
                vehiclenos.push({ vehicleno: vehiclesdata[veh].vehicleno });
            });
            //            }
            $('#divassainedvehs').css('display', 'block');
            $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                $('#divassainedvehs').setTemplateURL('Report.htm');
                $('#divassainedvehs').processTemplate(vehiclenos);
            });
            $('#divAllvehicles').css('display', 'block');
            $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
                $('#divAllvehicles').setTemplateURL('liveview2.htm');
                $('#divAllvehicles').processTemplate(vehiclenos);
                $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                    checkboxClass: 'icheckbox_minimal-blue',
                    radioClass: 'iradio_minimal-blue'
                });
                $('#ckballveh').on('ifChanged', function (event) {
                    onallcheck(this);
                });
                $('.ckblvvehscls').on('ifChanged', function (event) {
                    var ckdval = $(this);
                    oncheck(this, ckdval[0].id);
                });
                liveupdate();
                deleteOverlays();
                checkedvehicles = [];
            });

        }
    }
}

function intializeallvehicles() {
    var data = { 'op': 'InitilizeVehicles' };
    var s = function (msg) {
        if (msg) {
            vehiclessfilling(msg);
        }
        else {
        }
    };
    var e = function (x, h, e) {
        // $('#BookingDetails').html(x);
    };
    callHandler(data, s, e);
}

var vehiclesdata;
function vehiclessfilling(data) {
    var vehiclenos = new Array();
    vehiclesdata = data;
    var vehkeys = Object.keys(vehiclesdata);
    vehkeys.forEach(function (veh) {
        vehiclenos.push({ vehicleno: vehiclesdata[veh].vehicleno });
    });
    $('#divassainedvehs').css('display', 'block');
    $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
        $('#divassainedvehs').setTemplateURL('Report.htm');
        $('#divassainedvehs').processTemplate(vehiclenos);
    });
    $('#divAllvehicles').css('display', 'block');
    $.getScript("js/JTemplate.js", function (data, textStatus, jqxhr) {
        $('#divAllvehicles').setTemplateURL('liveview2.htm');
        $('#divAllvehicles').processTemplate(vehiclenos);
        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });
        $('#ckballveh').on('ifChanged', function (event) {
            onallcheck(this);
        });
        $('.ckblvvehscls').on('ifChanged', function (event) {
            var ckdval = $(this);
            oncheck(this, ckdval[0].id);
        });
        liveupdate();
        deleteOverlays();
        checkedvehicles = [];
    });
}
function callHandler(d, s, e) {
    $.ajax({
        url: 'Bus.axd',
        data: d,
        type: 'GET',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: true,
        success: s,
        error: e
    });
}

//--------------------reports--------------------//
function btn_generate_Click() {
    var table = document.getElementById("tbl_report");
    for (var i = table.rows.length - 1; i > 0; i--) {
        table.deleteRow(i);
    }
    table.deleteTHead();
    if (checkedvehicles.length == 0) {
        alert("Please select vehicles to get report!");
        return;
    }
    var ckdvhvls = "";
    for (var i = 0; i < checkedvehicles.length; i++) {
        ckdvhvls += checkedvehicles[i] + ",";
    }
    ckdvhvls = ckdvhvls.slice(0, -1);
    var txtvalue = document.getElementById('txt_Reports_TimeGap').value;
    var reporttype = currentpage;
    var startdate = document.getElementById('txt_startdate').value;
    var enddate = document.getElementById('txt_enddate').value;
    $('#loader_report').show();
    var data = { 'op': 'getvehiclesdatareport', 'startdate': startdate, 'enddate': enddate, 'checkedvehicles': ckdvhvls, 'reporttype': reporttype, 'txtvalue': txtvalue, 'requestfrom': 'web' };
    var s = function (msg) {
        if (msg) {
            $('#loader_report').hide();
            vehicledatafilling(msg);
        }
        else {
            $('#loader_report').hide();
        }
    };
    var e = function (x, h, e) {
        $('#loader_report').hide();
    };
    callHandler(data, s, e);
}

function vehicledatafilling(results) {
    $(".togglediv").css('margin-left', 0);
    $(".togglediv").css('margin-right', 0);
    $(".togglediv").animate({ left: '-300px' }, 500);
    $("#btnrptClose").attr('title', "Show");
    $("#btnrptClose").attr('src', "Images/bigright.png");
    hidden = true;
    var table = document.getElementById("tbl_report");
    var reporttype = currentpage;
    if (reporttype == "General Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">TotalDistanceTravelled(Kms)</th><th scope="col">WorkingHours</th><th scope="col">MotionHours</th><th scope="col">StationaryHours</th><th scope="col">MaxSpeed</th><th scope="col">AvgSpeed</th><th scope="col">IdleTime</th><th scope="col">No Of Stops</th><th scope="col">AC ONTime</th><th scope="col">MainPower OFF Time</th><th></th></tr></thead>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var TotalDistanceTravelled = results[i].TotalDistanceTravelled;
            var WorkingHours = results[i].WorkingHours;
            var MotionHours = results[i].MotionHours;
            var StationaryHours = results[i].StationaryHours;
            var MaxSpeed = results[i].MaxSpeed;
            var AvgSpeed = results[i].AvgSpeed;
            var IdleTime = results[i].IdleTime;
            var NoOfStops = results[i].NoOfStops;
            var ACONTime = results[i].ACONTime;
            var MainPowerOFFTime = results[i].MainPowerOFFTime;
            var sno = results[i].sno;
            var tablerowcnt = document.getElementById("tbl_report").rows.length;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + VehicleID + '</th><td data-title="TotalDistanceTravelled(Kms)">' + TotalDistanceTravelled + '</td><td data-title="WorkingHours" >' + WorkingHours + '</td><td data-title="MotionHours">' + MotionHours + '</td><td data-title="StationaryHours">' + StationaryHours + '</td><td data-title="MaxSpeed">' + MaxSpeed + '</td><td data-title="AvgSpeed">' + AvgSpeed + '</td><td data-title="IdleTime">' + IdleTime + '</td><td data-title="No Of Stops">' + NoOfStops + '</td><td data-title="AC ONTime">' + ACONTime + '</td><td data-title="MainPower OFF Time">' + MainPowerOFFTime + '</td><td><input type="button" class="btn btn-block btn-danger" name="Review" value ="Review on Map" onclick="ReviewonMap(this);"/></td></tr></tbody>');
        }
    }
    else if (reporttype == "Stopage Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">Location</th><th scope="col">DateTime</th><th scope="col">Stopped Time</th>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var DateTime = results[i].DateTime;
            var StoppedHours = results[i].StoppedHours;
            var Latitude = results[i].Latitude;
            var Longitude = results[i].Longitude;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + VehicleID + '</th><td data-title="Location">' + Latitude + ',' + Longitude + '</td><td data-title="DateTime" >' + DateTime + '</td><td data-title="Stopped Time">' + StoppedHours + '</td></tr></tbody>');
        }
    }
    else if (reporttype == "Ignition Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">Ignition On Time</th>');
        for (var i = 0; i < results.length; i++) {
            var Vehicleno = results[i].Vehicleno;
            var idletime = results[i].idletime;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + Vehicleno + '</th><td data-title="IgnitionOnTime">' + idletime + '</td></tr></tbody>');
        }
    }
    else if (reporttype == "OverSpeed Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">Location</th><th scope="col">DateTime</th><th scope="col">Speed</th>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var DateTime = results[i].DateTime;
            var Speed = results[i].Speed;
            var Latitude = results[i].Latitude;
            var Longitude = results[i].Longitude;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + VehicleID + '</th><td data-title="Location">' + Latitude + ',' + Longitude + '</td><td data-title="DateTime" >' + DateTime + '</td><td data-title="Speed">' + Speed + '</td></tr></tbody>');
        }
    }
    else if (reporttype == "Daily Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">SNo</th><th scope="col">VehicleID</th><th scope="col">StartDate</th><th scope="col">StartTime</th><th scope="col">StopDate</th><th scope="col">StopTime</th><th scope="col">TotalDistanceTravelled</th><th scope="col">MotionHours</th><th scope="col">StationaryHours</th><th scope="col">MaxSpeed</th><th scope="col">AvgSpeed</th><th scope="col">IdleTime</th><th></th></tr></thead>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var StartDate = results[i].StartDate;
            var StartTime = results[i].StartTime;
            var StopDate = results[i].StopDate;
            var StopTime = results[i].StopTime;
            var TotalDistanceTravelled = results[i].TotalDistanceTravelled;
            var MotionHours = results[i].MotionHours;
            var StationaryHours = results[i].StationaryHours;
            var MaxSpeed = results[i].MaxSpeed;
            var AvgSpeed = results[i].AvgSpeed;
            var IdleTime = results[i].IdleTime;
            var sno = results[i].sno;
            var tablerowcnt = document.getElementById("tbl_report").rows.length;
            $('#tbl_report').append('<tbody><tr><th>' + (i + 1) + '</th><th scope="row">' + VehicleID + '</th><td data-title="StartDate">' + StartDate + '</td><td data-title="StartTime" >' + StartTime + '</td><td data-title="StopDate">' + StopDate + '</td><td data-title="StopTime">' + StopTime + '</td><td data-title="TotalDistanceTravelled">' + TotalDistanceTravelled + '</td><td data-title="MotionHours">' + MotionHours + '</td><td data-title="StationaryHours">' + StationaryHours + '</td><td data-title="MaxSpeed">' + MaxSpeed + '</td><td data-title="AvgSpeed">' + AvgSpeed + '</td><td data-title="IdleTime">' + IdleTime + '</td><td><input type="button" class="btn btn-block btn-danger" name="Review" value ="Review on Map" onclick="ReviewonMap(this);"/></td></tr></tbody>');
        }
    }
    else if (reporttype == "Location HaltingHours Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">LocationName</th><th scope="col">VehicleEnteredDate</th><th scope="col">VehicleEnteredTime</th><th scope="col">VehicleLeftDate</th><th scope="col">VehicleLeftTime</th><th scope="col">StoppedHours</th><th></th></tr></thead>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var LocationName = results[i].LocationName;
            var VehicleEnteredDate = results[i].VehicleEnteredDate;
            var VehicleEnteredTime = results[i].VehicleEnteredTime;
            var VehicleLeftDate = results[i].VehicleLeftDate;
            var VehicleLeftTime = results[i].VehicleLeftTime;
            var StoppedHours = results[i].StoppedHours;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + VehicleID + '</th><td data-title="LocationName">' + LocationName + '</td><td data-title="VehicleEnteredDate" >' + VehicleEnteredDate + '</td><td data-title="VehicleEnteredTime">' + VehicleEnteredTime + '</td><td data-title="VehicleLeftDate">' + VehicleLeftDate + '</td><td data-title="VehicleLeftTime">' + VehicleLeftTime + '</td><td data-title="StoppedHours">' + StoppedHours + '</td></tr></tbody>');
        }
    }
    else if (reporttype == "Location to Location Report") {
        $('#tbl_report').append('<thead><tr><th scope="col">VehicleID</th><th scope="col">FromLocation</th><th scope="col">StartingDate</th><th scope="col">StartingTime</th><th scope="col">ToLocation</th><th scope="col">ReachingDate</th><th scope="col">ReachingTime</th><th scope="col">Distance</th><th scope="col">JourneyHours</th><th></th></tr></thead>');
        for (var i = 0; i < results.length; i++) {
            var VehicleID = results[i].VehicleID;
            var FromLocation = results[i].FromLocation;
            var StartingDate = results[i].StartingDate;
            var StartingTime = results[i].StartingTime;
            var ToLocation = results[i].ToLocation;
            var ReachingDate = results[i].ReachingDate;
            var ReachingTime = results[i].ReachingTime;
            var Distance = results[i].Distance;
            var JourneyHours = results[i].JourneyHours;
            $('#tbl_report').append('<tbody><tr><th scope="row">' + VehicleID + '</th><td data-title="FromLocation">' + FromLocation + '</td><td data-title="StartingDate" >' + StartingDate + '</td><td data-title="StartingTime">' + StartingTime + '</td><td data-title="ToLocation">' + ToLocation + '</td><td data-title="ReachingDate">' + ReachingDate + '</td><td data-title="ReachingTime">' + ReachingTime + '</td><td data-title="Distance">' + Distance + '</td><td data-title="JourneyHours">' + JourneyHours + '</td><td><input type="button" class="btn btn-block btn-danger" name="Review" value ="Review on Map" onclick="ReviewonMap(this);"/></td></tr></tbody>');
        }
    }
}

function ReviewonMap(thisid) {
    var clickedrow = $(thisid).parents("tr");
    var vehicleno = "";
    var startdatetime = "";
    var enddatetime = "";
    var reporttype = currentpage;
    vehicleno = clickedrow[0].cells[0].innerHTML;
    if (reporttype == "Location to Location Report") {
        startdatetime = clickedrow[0].cells[2].innerHTML + " " + clickedrow[0].cells[3].innerHTML + ":00";
        enddatetime = clickedrow[0].cells[5].innerHTML + " " + clickedrow[0].cells[6].innerHTML + ":00";
    }
    var data = { 'op': 'OnclickDrawRoute', 'vehicleno': vehicleno, 'reporttype': reporttype, 'startdatetime': startdatetime, 'enddatetime': enddatetime };
    var s = function (msg) {
        if (msg) {
            if (msg == "OK") {
                window.open("RouteHistory.html", "_blank");
            }
            else {
                alert("Error,Please try again later");
                return;
            }
        }
        else {
        }
    };
    var e = function (x, h, e) {
    };
    callHandler(data, s, e);
}
//----------------------------------------------//
 