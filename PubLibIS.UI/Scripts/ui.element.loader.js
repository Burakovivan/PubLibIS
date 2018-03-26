function setDatePickerTo(elementId) {
   $("#" + elementId).datepicker({
        format: "dd.mm.yyyy", startDate: new Date(0), endDate: new Date(253402207000000)
    });
}