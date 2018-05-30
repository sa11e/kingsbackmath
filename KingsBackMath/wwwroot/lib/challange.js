(function () {

    // When user clicks on Next, show an alert with the date of birth in format YYYY-MM-DD.
    $("#next_button").click(function () {
        var selectedDate = $('#date_input').val()
        if (!selectedDate)
            return;

        var birthDate = moment(selectedDate);
        alert('Your date of birth: ' + birthDate.format('YYYY-MM-DD'));
    });
    
    // Hook event when birth date changes.
    // If > 18 years, then enable the next button. Otherwise disable it.
    $("#date_input").on('input', function () {
        // Get the selected birth of date
        var dateInput = $('#date_input').val()
        if (!dateInput)
            return;

        // Use moment.js to parse the date
        var now = moment();
        var birthDate = moment(dateInput);
        if (birthDate.add(18, 'years') > now) {
            // Less than 18 years. Disable button and show validation error
            $("#next_button").addClass('disabledButton');
            $("#next_button").prop('disabled', true);
            $("#validationError").css('display', 'inline');;
            return;
        }
        // Over 18 years - enable button
        $("#next_button").removeClass('disabledButton');
        $("#next_button").prop('disabled', false);
        $("#validationError").css('display', 'none');;
    }); 
})();

