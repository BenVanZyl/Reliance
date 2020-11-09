// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


//SyncFusion Grid:
function onLoadGrid(args) {
    var grid = document.getElementsByClassName('e-grid')[0].ej2_instances[0];
    grid.toolbarModule.getToolbar().querySelector('.e-update').nextSibling.innerHTML = "Save";
}

function dataBoundGrid(args) {
    this.autoFitColumns();
}

function actionFailureGrid(args) {
    console.log(args);                        // here you will get exception message
    //console.log(args[0].error.status);
    //console.log(args[0].error.responseText);
    //var errorText = args[0].error.status + ": " + args[0].error.responseText;
    //console.log(errorText);
    alert("Error proccesing the request.  Please refresh the page and confirm that you have entered all data correctly. Contact us if the problem persists.");
}