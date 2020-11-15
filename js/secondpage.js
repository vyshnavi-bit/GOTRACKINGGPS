

var count = 0;
function drawLayout(serv_layout_type, totcapacity, seatamount, berthamount, _Seats, UBerth, LBerth, BkdSeats, pnlseats) {

    var ServiceLayoutType = serv_layout_type;
    var StrSeats = totcapacity;
    var SeatFare = seatamount;
    var BerthFare = berthamount;
    var TotalBerths = totcapacity;
    var Berthcal = 0;
    var Seatcal = 0;
    var Split = "";
    var Splits = "";
    var setheight = 0;
    var SeatAmount = 0;
    var BerthAmount = 0;
    var setwidth = 60;
    var seatsplit = "";
    var seatlength = 0;
    var selectedSeatNo = 0;
    var berthlength = 0;
    var selectedBerthNo = 0;
    var ladiesSeat = "";
    var orgiAmount = 0;
    var ladiesberth = "";
    //alert(pnlseats.find('#ddl').id);
    if (ServiceLayoutType == "1") {

        var settings = { rowCssPrefix: 'row', colCssPrefix: 'col', SeatWi: 35, seatWidth: 30, berthWidth: 60, BerthHeight: 30, seatHeight: 30, Berthcss: 'Berth', selectedBerthCss: 'selectedBerth', selectingBerthCss: 'selectingBerth', seatCss: 'seat', selectedSeatCss: 'selectedSeat', selectingSeatCss: 'selectingSeat', ladiesseat: 'ladiesSeat' };
        Split = _Seats;
        // SeatFare = document.getElementById('<%= HiddenSeatAmount.ClientID %>').value;
        pnlseats.find('#holder1').css('display', 'none');
        pnlseats.find('#holder2').css('display', 'none');
        pnlseats.find('#holder2').css('display', 'none');
        pnlseats.find('#pBerths').css('display', 'none');
        
        
    }
    if (ServiceLayoutType == "2") {
        if (StrSeats <= 24) {
            if (SeatFare == BerthFare) {
                var settings = { rowCssPrefix: 'row', colCssPrefix: 'col', SeatWi: 25, seatWidth: 45, seatHeight: 100, seatCss: 'Berth1', selectedSeatCss: 'selectedBerth1', selectingSeatCss: 'selectingBerth1', LadiesBerth: 'LadiesBerth' };
            }
            else {
                var settings = { rowCssPrefix: 'row', colCssPrefix: 'col', SeatWi: 30, seatWidth: 50, berthWidth: 50, BerthHeight: 100, seatHeight: 100, Berthcss: 'Berth1', selectedBerthCss: 'selectedBerth1', selectingBerthCss: 'selectingBerth1', seatCss: 'seat', selectedSeatCss: 'selectedSeat', selectingSeatCss: 'selectingSeat', LadiesBerth: 'LadiesBerth' };
            }
        }
        if (StrSeats > 24) {
            var settings = { rowCssPrefix: 'row', colCssPrefix: 'col', SeatWi: 50, seatWidth: 50, seatHeight: 30, seatCss: 'Berth', selectedSeatCss: 'selectedBerth', selectingSeatCss: 'selectingBerth', LadiesBerth: 'LadiesBerth', Berthcss: 'seat', selectedBerthCss: 'selectedSeat', selectingBerthCss: 'selectingSeat' };
        }
        Split = UBerth;
        Splits = LBerth;
        //SeatFare = document.getElementById('<%= HiddenBerthAmount.ClientID %>').value;
        var lblLower = "Lower";
        $('.lblLower').html(lblLower);
        pnlseats.find('#holder').css('display', 'none');
        pnlseats.find('#pSeats').css('display', 'none');
        
    }

    var init = function (reservedSeat) {

        var str = [], seatNo, className;
        var str1 = [];
        var str2 = [];
        //""; // [1, 5, 9, 13, 17, 21, 25, 29, 33, 37, '@', 2, 6, 10, 14, 18, 22, 26, 30, 34, 38, '@', '$', '$', '$', '$', '$', '$', '$', '$', '$', '$', '@', 3, 7, 11, 15, 19, 23, 27, 31, 35, 39, '@', 4, 8, 12, 16, 20, 24, 28, 32, 36, 40];
        if (ServiceLayoutType == "1") {
            var shaSeat = Split.split(",");
            var spr = 0; // 
            var spc = 0; // 
            setheight = 0;
            setwidth = 70;
            for (i = 0; i < shaSeat.length; i++) {
                if (shaSeat[i].split("|")[0] == '@') {
                    spr++;
                    spc = 0;
                    setheight += settings.seatHeight + 5;
                }
                else if (shaSeat[i].split("|")[0] == '$') {
                    spc++;
                }
                else {
                    seatNo = shaSeat[i];
                    seatsplit = shaSeat[i].split("|");

                    if (seatsplit[1] == "S") {
                        if (seatsplit[2] == "F") {
                            //  alert(seatsplit.length);

                            //   alert(seatsplit[0] + " " + seatsplit[1] + " " + seatsplit[2]);
                            className = settings.ladiesseat + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                            ladiesSeat += seatsplit[0] + ",";
                        }
                        else {
                            className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                        }
                        if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                        str.push('<li title="SeatNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '"  class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth).toString() + 'px">' + '<a  title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                        if (spr == 1) {
                            setwidth += settings.SeatWi;
                        }
                    }
                    else {

                        className = settings.Berthcss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();

                        if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedBerthCss; }
                        str.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '"  class="' + className + '"' + 'style="top:' + (spr * settings.BerthHeight).toString() + 'px;left:' + (spc * settings.berthWidth / 2).toString() + 'px">' + '<a  title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                        if (spr == 1) {
                            setwidth += settings.berthWidth;
                        }
                    }



                    //str.push('<a  title="' + seatNo + '|' + SeatFare + '">' + '<li class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth).toString() + 'px">' + '</a>');
                    spc++;
                    //                                                if (spr == 0) {
                    //                                                    setwidth += settings.SeatWi;
                    //                                                }
                }
            }


            // document.getElementById('<%= HiddenLadiesSeat.ClientID %>').value=ladiesSeat;
            pnlseats.find('.lblLadiesSeat').html(ladiesSeat);
            // alert(ladiesSeat);
            setheight += settings.seatHeight;

            var pnlse = pnlseats.find('#holder');
            pnlse.css("height", (setheight).toString());
            pnlse.css("width", (setwidth).toString());


            var pnlse = pnlseats.find('#place');
            //('table>tbody>tr>td>div>ul#place');
            pnlse.html(str.join(''));
            //alert(pnlse.id);
            //alert(pnlse.html());

        }
        if (ServiceLayoutType == "2") {
            var shaSeat = Split.split(",");
            var shaSeats = Splits.split(",");

            var spr = 0;
            var spc = 0;
            setheight = 0;
            setwidth = 70;
            for (i = 0; i < shaSeat.length; i++) {
                if (shaSeat[i] == '@') {
                    spr++;
                    spc = 0;
                    setheight += settings.seatHeight + 5;
                }
                else if (shaSeat[i] == '$') {
                    spc++;
                }
                else {

                    if (SeatFare == BerthFare) {
                        seatNo = shaSeat[i];
                        seatsplit = shaSeat[i].split("|");
                        if (TotalBerths == 24) {
                            className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                            if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                            str2.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[1] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');

                        }
                        else {
                            if (seatsplit[0] != "$") {
                                if (seatsplit[2] == "F") {
                                    className = settings.LadiesBerth + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    ladiesSeat += seatsplit[0] + ",";
                                }
                                else {
                                    className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                }
                                if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                                str2.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                            }
                        }
                    }
                    else {
                        seatNo = shaSeat[i];
                        seatsplit = shaSeat[i].split("|");
                        if (seatsplit[0] == "S") {

                            className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();

                            if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                            str2.push('<li title="SeatNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '"  class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a  title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                        }
                        else {
                            if (TotalBerths == 36) {

                                className = settings.Berthcss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();

                                if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedBerthCss; }
                                str2.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '"  class="' + className + '"' + 'style="top:' + (spr * settings.BerthHeight).toString() + 'px;left:' + (spc * settings.berthWidth / 2).toString() + 'px">' + '<a  title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                            }

                            else {
                                if (seatsplit[0] != "$") {
                                    className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();

                                    if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                                    str2.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                                    setwidth += 2;
                                }
                            }
                        }
                    }
                    spc++;
                    if (spr == 0) {

                        setwidth += settings.SeatWi;
                    }
                }
            }
            //   document.getElementById('<%= HiddenLadiesSeat.ClientID %>').value = ladiesberth;

            ////////...............Width & Height .......................///////////////
            setheight += settings.seatHeight;
            pnlseats.find('.lblLadiesSeat').html(ladiesSeat);

            pnlseats.find('#holder1').css("height", (setheight).toString());
            pnlseats.find('#holder2').css("height", (setheight).toString());

            pnlseats.find('#holder1').css("width", (setwidth).toString());
            pnlseats.find('#holder2').css("width", (setwidth).toString());

            pnlseats.find('#place2').html(str2.join(''));
            spc = 0;
            spr = 0;
            setheight = 0;
            for (i = 0; i < shaSeats.length; i++) {
                if (shaSeats[i] == '@') {
                    spr++;
                    spc = 0;
                    setheight += settings.seatHeight + 5;
                }
                else if (shaSeats[i] == '$') {
                    spc++;
                }
                else {
                    seatNo = shaSeats[i];
                    seatsplit = shaSeats[i].split("|");
                    if (SeatFare == BerthFare) {
                        if (TotalBerths == 24) {
                            className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                            if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                            str1.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[1] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');

                        }
                        else {
                            if (seatsplit[0] != "$") {
                                if (seatsplit[2] == "F") {
                                    className = settings.LadiesBerth + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    ladiesSeat += seatsplit[0] + ",";
                                }
                                else {
                                    className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                }
                                if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                                str1.push('<li title="BerthNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                            }
                        }
                    }
                    else {
                        seatNo = shaSeats[i];
                        seatsplit = shaSeats[i].split("|");
                        if (seatsplit[0] != "$") {
                            if (StrSeats =="46") {
                                if (seatsplit[1] == " S ") {
                                    className = settings.Berthcss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedBerthCss; }
                                }
                                else {
                                    className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                                }
                                str1.push('<li title="SeatNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                      
                            }
                            else {
                                if (seatsplit[1] == "S") {
                                    className = settings.Berthcss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedBerthCss; }
                                }
                                else {
                                    className = settings.seatCss + ' ' + settings.rowCssPrefix + spr.toString() + ' ' + settings.colCssPrefix + spc.toString();
                                    if ($.isArray(reservedSeat) && $.inArray(seatsplit[0], reservedSeat) != -1) { className += ' ' + settings.selectedSeatCss; }
                                }
                                str1.push('<li title="SeatNo: ' + seatsplit[0] + '| Rs.' + seatsplit[3] + '" class="' + className + '"' + 'style="top:' + (spr * settings.seatHeight).toString() + 'px;left:' + (spc * settings.seatWidth / 2).toString() + 'px">' + '<a title="' + seatsplit[0] + '">' + '</a>' + '</li>');
                            }
                        }
                    }
                    spc++;

                }
            }
            //  document.getElementById('<%= HiddenLadiesSeat.ClientID %>').value = ladiesberth;

            setheight += settings.seatHeight;
            pnlseats.find('.lblLadiesSeat').html(ladiesSeat);


            pnlseats.find('#holder1').css("height", (setheight).toString());
            pnlseats.find('#holder2').css("height", (setheight).toString());

            var pnlsber = pnlseats.find('#place1');
            pnlsber.html(str1.join(''));
        }
    };
    //Case II: If already booked
    var bookedSeatsString = BkdSeats;
    var bookedSeatsList = [];

   bookedSeatsList = bookedSeatsString.split(",");
    init(bookedSeatsList);

    ///............ Seat Click.............//////////////////////////
    $('.' + settings.seatCss).click(function () {
        pnlseats = $(this).parents('#buslayoutshow');
        // alert(pnlseats.attr('id'));
        if ($(this).hasClass(settings.selectedSeatCss)) { alert('This seat is already reserved'); }
        else {
            var str1 = [], item1;
            var str2 = [], item2;
            var found = false;
            var selectedseat = $(this).text;
            if (ServiceLayoutType == "1") {
                if ($(this).hasClass(settings.seatCss)) {
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                }
            }
            if (ServiceLayoutType == "2") {
                $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str2.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
            }

            if (str1.length > 5) {
                /////.......................Only Seats 6 Then.................///////////////////////
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);
                    if (ServiceLayoutType == "1") {
                        var str = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

//                        SeatAmount = SeatAmount;
//                        SeatAmount += SeatAmount;
                        orgiAmount = "Rs." + SeatAmount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Seatcal = "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100))));
                        pnlseats.find('.SeatsAmount').html(Seatcal);
                        $('#<%=HiddenAmount.ClientID %>').val(Seatcal);
                    }
                    if (ServiceLayoutType == "2") {
                        var str1 = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');

                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        Amount = 0;
                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            ////////////////.................If Lower Berth Greater Than 6.........///////////////
            else if (str2.length > 5) {
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);

                    if (ServiceLayoutType == "1") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str = [], item;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        Amount = 0;
                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                    }
                    if (ServiceLayoutType == "2") {
                        var str1 = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');
                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        if (TotalBerths != 36) {
                            Amount = 0;
                            Amount = SeatAmount;

                            orgiAmount = "Rs." + Amount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        }
                        else {
                            Amount = 0;
                            Amount = SeatAmount;

                            orgiAmount = "Rs." + Amount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        }
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            else {

                $(this).toggleClass(settings.selectingSeatCss);
                // $(this).toggleClass(settings.selectingSeatCss);

                //--- sha ---
                //                        alert("hi");
                if (ServiceLayoutType == "1") {
                    //                            seatlength = [], item,berthlength[];
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
                        item = $(this).attr('title'); seatlength.push(item);
                        var rate = $(this).parent();
                        SeatAmount+=parseFloat(rate.attr('title').split('.')[1]);
                    });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedSeatNo = selectedBerthNo + ',' + selectedSeatNo;
                    if (selectedSeatNo.endsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substring(0, selectedSeatNo.length - 1);
                    }
                    if (selectedSeatNo.startsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substr(1);
                    }
                    pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
//                    SeatAmount = (seatlength.length) * SeatFare;
//                    BerthAmount = (berthlength.length) * BerthFare;
//                    SeatAmount += BerthAmount;
                    orgiAmount = "Rs." + SeatAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Seatcal = "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100))));
                    pnlseats.find('.SeatsAmount').html(Seatcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Seatcal);
                }
                if (ServiceLayoutType == "2") {
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    if (SeatFare == BerthFare) {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    else {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedBerthNo = selectedSeatNo + ',' + selectedBerthNo;
                    if (selectedBerthNo.endsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substring(0, selectedBerthNo.length - 1);
                    }
                    if (selectedBerthNo.startsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substr(1);
                    }
                    pnlseats.find('.SelectedSeatno').html(selectedBerthNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedBerthNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
                    if (TotalBerths != 36) {
//                        BerthAmount = BerthAmount;
//                        SeatAmount = SeatAmount;
                        BerthAmount = SeatAmount;
                    }
                    else {
//                        BerthAmount = BerthAmount;
//                        SeatAmount = SeatAmount;
                        BerthAmount = SeatAmount;
                    }
                    orgiAmount = "Rs." + BerthAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Berthcal = "Rs." + ((BerthAmount) - (Math.ceil(BerthAmount * (5 / 100))));


                    pnlseats.find('.SeatsAmount').html(Berthcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Berthcal);
                    return false;
                }
            }
            //return true;
        }

    });
    /////////////////////...............Ladies Seats Selected click........................////////////////////////
    $('.' + settings.ladiesseat).click(function () {
        if ($(this).hasClass(settings.selectedSeatCss)) { alert('This seat is already reserved'); }
        else {
            var str1 = [], item1;
            var str2 = [], item2;
            var found = false;
            var selectedseat = $(this).text;
            if (ServiceLayoutType == "1") {
                if ($(this).hasClass(settings.ladiesseat)) {

                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                }
            }
            if (ServiceLayoutType == "2") {
                $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
            }

            if (str1.length > 5) {
                /////.......................Only Seats 6 Then.................///////////////////////
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);
                    if (ServiceLayoutType == "1") {
                        SeatAmount = 0;
                        SeatAmount = 0;
                        var str = [], item;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);


                       // SeatAmount += SeatAmount;
                        orgiAmount = "Rs." + SeatAmount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Seatcal = "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100))));

                        pnlseats.find('.SeatsAmount').html(Seatcal);
                        $('#<%=HiddenAmount.ClientID %>').val(Seatcal);
                    }
                    if (ServiceLayoutType == "2") {
                        var str1 = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');

                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        Amount = 0;
                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            ////////////////.................If Lower Berth Greater Than 6.........///////////////
            else if (str2.length > 5) {
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);

                    if (ServiceLayoutType == "1") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str = [], item;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        Amount = 0;
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        Amount = SeatAmount;
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                    }
                    if (ServiceLayoutType == "2") {
                        var str1 = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        if (TotalBerths != 36) {
                            Amount = 0;
                            Amount = SeatAmount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        }
                        else {
                            Amount = 0;
                            Amount = SeatAmount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));
                        }
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            else {
                $(this).toggleClass(settings.selectingSeatCss);
                //--- sha ---
                //                        alert("hi");
                if (ServiceLayoutType == "1") {
                    //                                   seatlength = [], item,berthlength[];
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedSeatNo = selectedBerthNo + ',' + selectedSeatNo;
                    if (selectedSeatNo.endsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substring(0, selectedSeatNo.length - 1);
                    }
                    if (selectedSeatNo.startsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substr(1);
                    }
                    $('.SelectedSeatno').html(selectedSeatNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);


                    //                    SeatAmount = (seatlength.length) * SeatFare;
                    //                    BerthAmount = (berthlength.length) * BerthFare;
                    //SeatAmount += SeatAmount;
                    orgiAmount = "Rs." + SeatAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Seatcal = "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100))));

                    pnlseats.find('.SeatsAmount').html(Seatcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Seatcal);
                }
                if (ServiceLayoutType == "2") {
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    if (SeatFare == BerthFare) {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    else {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedBerthNo = selectedSeatNo + ',' + selectedBerthNo;
                    if (selectedBerthNo.endsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substring(0, selectedBerthNo.length - 1);
                    }
                    if (selectedBerthNo.startsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substr(1);
                    }
                    $('.SelectedSeatno').html(selectedBerthNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedBerthNo);

                    //                    SeatAmount = 0;
                    //                    BerthAmount = 0;
                    if (TotalBerths != 36) {
                        //                        BerthAmount = (berthlength.length) * BerthFare;
                        //                        SeatAmount = (seatlength.length) * SeatFare;
                        BerthAmount = SeatAmount;
                    }
                    else {
                        //                        BerthAmount = (berthlength.length) * SeatFare;
                        //                        SeatAmount = (seatlength.length) * SeatFare;
                        BerthAmount = SeatAmount;
                    }
                    orgiAmount = "Rs." + BerthAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Berthcal = "Rs." + ((BerthAmount) - (Math.ceil(BerthAmount * (5 / 100))));
                    pnlseats.find('.SeatsAmount').html(Berthcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Berthcal);
                    return false;
                }
            }
            //return true;
        }

    });
    ///////////////................Ladies Berth Click......./..../////////////////////////
    $('.' + settings.LadiesBerth).click(function () {
        if ($(this).hasClass(settings.selectedSeatCss)) { alert('This seat is already reserved'); }
        else {
            var str1 = [], item1;
            var str2 = [], item2;
            var found = false;
            var selectedseat = $(this).text;
            if (ServiceLayoutType == "1") {
                if ($(this).hasClass(settings.LadiesBerth)) {
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                }
            }
            if (ServiceLayoutType == "2") {
                $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
            }

            if (str1.length > 5) {
                /////.......................Only Seats 6 Then.................///////////////////////
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);
                    if (ServiceLayoutType == "1") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str = [], item;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); BerthAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        //SeatAmount = SeatAmount;
                       // SeatAmount += BerthAmount;
                        orgiAmount = "Rs." + SeatAmount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        SeatAmount =  "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100)))); 

                        pnlseats.find('.SeatsAmount').html(SeatAmount);
                        $('#<%=HiddenAmount.ClientID %>').val(SeatAmount);
                    }
                    if (ServiceLayoutType == "2") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str1 = [], item;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');

                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        Amount = 0;
                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100)))); 
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            ////////////////.................If Lower Berth Greater Than 6.........///////////////
            else if (str2.length > 5) {
                if ($(this).hasClass(settings.selectingSeatCss)) {
                    $(this).toggleClass(settings.selectingSeatCss);

                    if (ServiceLayoutType == "1") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str = [], item;
                        $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        Amount = 0;
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100)))); 

                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                    }
                    if (ServiceLayoutType == "2") {
                        SeatAmount = 0;
                        BerthAmount = 0;
                        var str1 = [], item;
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str1.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str1.join(',');
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);
                        if (TotalBerths != 36) {
                            Amount = 0;
                            Amount = SeatAmount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100)))); 
                        }
                        else {
                            Amount = 0;
                            Amount = SeatAmount;
                            Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100)))); 
                        }
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                        return false;
                    }
                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            else {

                $(this).toggleClass(settings.selectingSeatCss);
                // $(this).toggleClass(settings.selectingSeatCss);

                //--- sha ---
                //                        alert("hi");
                if (ServiceLayoutType == "1") {
                    //                            seatlength = [], item,berthlength[];
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); BerthAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedSeatNo = selectedBerthNo + ',' + selectedSeatNo;
                    if (selectedSeatNo.endsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substring(0, selectedSeatNo.length - 1);
                    }
                    if (selectedSeatNo.startsWith(",")) {
                        selectedSeatNo = selectedSeatNo.substr(1);
                    }
                    $('.SelectedSeatno').html(selectedSeatNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
                   // SeatAmount = SeatAmount;
//                    BerthAmount = BerthAmount;
//                    SeatAmount += BerthAmount;
                    orgiAmount = "Rs." + SeatAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Seatcal = "Rs." + ((SeatAmount) - (Math.ceil(SeatAmount * (5 / 100))));
                    pnlseats.find('.SeatsAmount').html(Seatcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Seatcal);
                }
                if (ServiceLayoutType == "2") {
                    seatlength = [], item;
                    berthlength = [], item;
                    SeatAmount = 0;
                    BerthAmount = 0;
                    if (SeatFare == BerthFare) {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    else {
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    }
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedBerthNo = selectedSeatNo + ',' + selectedBerthNo;
                    if (selectedBerthNo.endsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substring(0, selectedBerthNo.length - 1);
                    }
                    if (selectedBerthNo.startsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substr(1);
                    }
                    $('.SelectedSeatno').html(selectedBerthNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedBerthNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
                    if (TotalBerths != 36) {
//                        BerthAmount = (berthlength.length) * BerthFare;
//                        SeatAmount = (seatlength.length) * SeatFare;
                        BerthAmount = SeatAmount;
                    }
                    else {
//                        BerthAmount = (berthlength.length) * SeatFare;
//                        SeatAmount = (seatlength.length) * SeatFare;
                        BerthAmount = SeatAmount;
                    }
                    orgiAmount = "Rs." + BerthAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Berthcal = "Rs." + ((BerthAmount) - (Math.ceil(BerthAmount * (5 / 100))));
                    pnlseats.find('.SeatsAmount').html(Berthcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Berthcal);
                    return false;
                }
            }
            //return true;
        }

    });

    ////////////////..............Berth Click...................//////////////////////////
    $('.' + settings.Berthcss).click(function () {
        if ($(this).hasClass(settings.selectedBerthCss)) { alert('This seat is already reserved'); }
        else {
            var str1 = [], item1;
            var str2 = [], item2;
            var found = false;
            var selectedseat = $(this).text;
            if (ServiceLayoutType == "1") {


                $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item1 = $(this).attr('title'); str1.push(item1); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });

            }
            if (ServiceLayoutType == "2") {
                if ($(this).hasClass(settings.Berthcss)) {
                    $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item2 = $(this).attr('title'); str2.push(item2); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                }
            }
            /////////////////////...........Seat & Berth  Berth greater then 6.................//////////////////
            if (str1.length > 5) {
                if ($(this).hasClass(settings.selectingBerthCss)) {
                    $(this).toggleClass(settings.selectingBerthCss);
                    if (ServiceLayoutType == "1") {
                        var str = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        Amount = 0;
                        $('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));

                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                    }

                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }
            else if (str2.length > 5) {
                //////////////////////..............Seat Berth Geater Than 6.......................///////////////
                if ($(this).hasClass(settings.selectingBerthCss)) {
                    $(this).toggleClass(settings.selectingBerthCss);

                    if (ServiceLayoutType == "2") {
                        var str = [], item;
                        SeatAmount = 0;
                        BerthAmount = 0;
                        $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); str.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                        var selectedSeatNo = str.join(',');
                        Amount = 0;
                        pnlseats.find('.SelectedSeatno').html(selectedSeatNo);
                        $('#<%=HiddenValue.ClientID %>').val(selectedSeatNo);

                        Amount = SeatAmount;
                        orgiAmount = "Rs." + Amount;
                        pnlseats.find('.OrgAmount').html(orgiAmount);
                        Amount = "Rs." + ((Amount) - (Math.ceil(Amount * (5 / 100))));

                        pnlseats.find('.SeatsAmount').html(Amount);
                        $('#<%=HiddenAmount.ClientID %>').val(Amount);
                    }

                }
                else {
                    $(this).toggleClass(settings.selectingSeatCss);
                    $(this).toggleClass(settings.selectingSeatCss);

                    alert("Maximum Seats selection allowed 6 Only");
                }
            }

            else {
                $(this).toggleClass(settings.selectingBerthCss);

                //--- sha ---
                //                        alert("hi");
                if (ServiceLayoutType == "1") {
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    $.each(pnlseats.find('#place li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });

                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedBerthNo = selectedSeatNo + ',' + selectedBerthNo;
                    if (selectedBerthNo.endsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substring(0, selectedBerthNo.length - 1);
                    }
                    if (selectedBerthNo.startsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substr(1);
                    }
                    pnlseats.find('.SelectedSeatno').html(selectedBerthNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedBerthNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
//                    BerthAmount = (berthlength.length) * BerthFare;
//                    SeatAmount = (seatlength.length) * SeatFare;
                    BerthAmount = SeatAmount;
                    orgiAmount = "Rs." + BerthAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Berthcal = "Rs." + ((BerthAmount) - (Math.ceil(BerthAmount * (5 / 100))));

                    pnlseats.find('.SeatsAmount').html(Berthcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Berthcal);
                }
                if (ServiceLayoutType == "2") {
                    SeatAmount = 0;
                    BerthAmount = 0;
                    seatlength = [], item;
                    berthlength = [], item;
                    $.each(pnlseats.find('#place1 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place2 li.' + settings.selectingSeatCss + ' a'), function (index, value) { item = $(this).attr('title'); seatlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place2 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    $.each(pnlseats.find('#place1 li.' + settings.selectingBerthCss + ' a'), function (index, value) { item = $(this).attr('title'); berthlength.push(item); var rate = $(this).parent(); SeatAmount += parseFloat(rate.attr('title').split('.')[1]); });
                    selectedSeatNo = seatlength.join(',');
                    selectedBerthNo = berthlength.join(',');
                    selectedBerthNo = selectedSeatNo + ',' + selectedBerthNo;
                    if (selectedBerthNo.endsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substring(0, selectedBerthNo.length - 1);
                    }
                    if (selectedBerthNo.startsWith(",")) {
                        selectedBerthNo = selectedBerthNo.substr(1);
                    }
                    pnlseats.find('.SelectedSeatno').html(selectedBerthNo);
                    $('#<%=HiddenValue.ClientID %>').val(selectedBerthNo);

//                    SeatAmount = 0;
//                    BerthAmount = 0;
//                    BerthAmount = (berthlength.length) * BerthFare;
//                    SeatAmount = (seatlength.length) * SeatFare;
                    BerthAmount = SeatAmount;
                    orgiAmount = "Rs." + BerthAmount;
                    pnlseats.find('.OrgAmount').html(orgiAmount);
                    Berthcal = "Rs." + ((BerthAmount) - (Math.ceil(BerthAmount * (5 / 100))));
                    pnlseats.find('.SeatsAmount').html(Berthcal);
                    $('#<%=HiddenAmount.ClientID %>').val(Berthcal);
                    return false;
                }
            }
            //return true;
        }

    });
}

