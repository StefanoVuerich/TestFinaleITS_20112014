$(function () {

    var myLocation = location.href;
    var username = location.href.substring(location.href.lastIndexOf('=') + 1)
    if (myLocation != username) {
        $('#welcomeMessage').html('Welcome ' + username)
        sessionStorage.setItem('Username',username)
    } else {
        var username=sessionStorage.getItem('Username')
        if (username  != null) 
            $('#welcomeMessage').html('Welcome ' + username)
        else
            $('#welcomeMessage').html('Welcome')
    }
   
    RicercaDati();
    $('#allRecordsTab').click(function (e) {
        e.preventDefault()
        $('#tableBody').empty()
        $(this).tab('show')
        RicercaDati()
    })

    $('#insertRecordTab').click(function (e) {
        e.preventDefault()
        $('insertRecordTab').empty()
        $(this).tab('show')
    })

    $('#insertRecord').on('click', function (e) {
        e.preventDefault()
        var objToSend = new Object()
        objToSend.titolo = $('#titolo').val()
        objToSend.descrizione = $('#descrizione').val()
        objToSend.duration = parseInt($('#durata').val())
        objToSend.categoryID = parseInt($('#categoryID').val())
        objToSend.categoryDescription = $('#categoryDescription').val()
        Insert(objToSend)
    })

    $('#findRecordByIDBtn').on('click', function (e) {
        e.preventDefault();
        $('#findByIDFeedback').html('')
        $('#singleEntityTableBody').empty()
        var id = $('#findActivityID').val();
        if (id === "") {
            $('#findByIDFeedback').html('Inserire un ID')
        } else {
            SearchByID(id)
        }
    })
});

function RicercaDati() {
    $.ajax({
        type: 'GET',
        url: '../api/activities',
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('token')) },
        success: function (data) {
            for (var x in data) {

                var row = '<tr><td>'
                        + data[x].ID + '</td><td>'
                        + data[x].Titolo + '</td><td>'
                        + data[x].Descrizione + '</td><td>'
                        + data[x].CreationDate + '</td><td>'
                        + data[x].Duration + '</td><td>'
                        + data[x].CategoryID + '</td><td>'
                        + data[x].CategoryDescription + '</td></tr>';

                $('#tableBody').append(row);
            }
        },
        error: function (xhr) {

        }
    });
}

function Insert(record) {

    $('#inserFeedback').html('')

    $.ajax({
        type: 'POST',
        url: '../api/activities',
        data: JSON.stringify(record),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('token')) },
        success: function (data) {
            $('#insertForm').children().children(':input').val('')
            $('#inserFeedback').html('Record inserito correttamente')
            setTimeout(function () { $('#inserFeedback').html('') }, 5000);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });

}

function SearchByID(id) {
    $.ajax({
        type: 'GET',
        url: '../api/activities/' + id,
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('token')) },
        success: function (data) {
            if (data != null) {
                var entity = '<tr id="currentRow"><td>'
                        + data.ID + '</td><td id="activityName">'
                        + data.Titolo + '</td ><td>'
                        + data.Descrizione + '</td><td>'
                        + data.CreationDate + '</td><td>'
                        + data.Duration + '</td><td>'
                        + data.CategoryID + '</td><td>'
                        + data.CategoryDescription + '</td><td>'
                        + '<span class="glyphicon glyphicon-trash" id="cancelActivity" aria-hidden="true"></span></td></tr>';

                $('#singleEntityTableBody').append(entity);

                $('#cancelActivity').on('click', function (e) {
                    e.preventDefault()
                    var activityToCancel = $('#activityName').text()
                    CancellaActitity(activityToCancel)
                })
            } else
                $('#findByIDFeedback').html("Nessun record trovato")
        },
        error: function (xhr) {
            $('#findByIDFeedback').html("Errore nella ricerca")
        }
    });
}

function CancellaActitity(name) {
    var retVal = confirm("Vuoi veramente cancellare questa activity ?");
    if (retVal == true) {
        $.ajax({
            type: 'DELETE',
            url: '../api/activities/' + name,
            beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('token')) },
            success: function (data) {
                $('#currentRow').remove()
                $('#hasBeenRemovedFeedback').html("Dato rimosso correttamente")
                setTimeout(function () {
                    $('#hasBeenRemovedFeedback').fadeOut(1000, function () {
                        $('#hasBeenRemovedFeedback').html('');
                    });
                }, 500);
            },
            error: function (xhr) {
                $('#hasBeenRemovedFeedback').html("Errore nella cancellazione")
                setTimeout(function () { $('#hasBeenRemovedFeedback').html('') }, 2500);
            }
        });
    } else {
        //do nothing
    } 
}