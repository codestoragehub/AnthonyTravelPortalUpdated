let clientSection = (function () {
    let component = $('.jsclient');
    let constants = {
        jsclientItems: '.jsclientItems',
        jsAddClient: '.jsAddClient',
        jsEditClient: '.jsEditClient',
        jsRemoveclient: '.jsRemoveclient',
        jsReloadclients: '.jsReloadclients',
        jsclientForm: '.jsclientForm',
        dataclientId: 'clientId',
        jsClientName: '.jsClientName',
        jsInstitutiontype: '.jsInstitutiontype'
       
    };

    let viewModel = {
        clientsItems: null,
        addclientButton: null,
        reloadclientsButton: null,
        clientForm: null,
        institutiontype: null
    };

    let variables = {
        clientId: null,
        clientName: null
    };

    let showException = function (response) {
        let toastIcon = 'error';
        let toastHeading = '<strong>An error occured!</strong>';
        let hideAfter = 10000;

        $.toast({
            heading: toastHeading,
            text: response.responseText.substr(0, 300),
            hideAfter: hideAfter,
            icon: toastIcon,
            position: { bottom: 60, right: 40 }
        });
    };

    let initView = function (view, modalTitle) {
        debugger;
        let htmlResult = $($.parseHTML(view)).filter('*');
        viewModel.clientForm = htmlResult;
        variables.clientId = viewModel.clientForm.data(constants.dataclientId);
        variables.clientName = viewModel.clientForm.find(constants.jsClientName);
        viewModel.institutiontype = viewModel.clientForm.find(constants.jsInstitutiontype);
        $('#form-modal .modal-header').append('<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="outline:none;padding: 0;">x</button>');
        formModalComponent.show(modalTitle, htmlResult);
    };

    let submitclientFormSucceed = function (response, status, xhr) {
        $.toast({
            heading: 'Saved',
            text: 'client saved successfully',
            hideAfter: '5000',
            icon: 'success',
            position: { bottom: 60, right: 40 }
        });
        formModalComponent.hide();
        reloadclients();
    };

    let submitclientForm = function (event) {
        event.preventDefault();
        
        let options = {
            type: 'post',
            url: $('.jsClientForm').attr('action'),
            success: submitclientFormSucceed,
            error: showException
        };

        viewModel.clientForm.ajaxSubmit(options);
        return false;
    };

    let showCreateOrUpdateclientView = function (event) {
        debugger;
        let addAction = $(event.currentTarget).data('addAction');
        $.get(addAction, function (result) {
            initView(result, 'Create client');
        });
    };

    let showEditclientView = function (event) {
        debugger;
        let editAction = $(event.currentTarget).data('editAction');
        $.get(editAction, function (result) {
            initView(result, 'Edit client');
        });
    };

    let removeclient = function (event) {
        if (!confirm('Are you sure to delete this client?'))
            return;

        $.post($(event.currentTarget).data('removeAction'), function () {
            $.toast({
                heading: 'Removed',
                text: 'client removed successfully',
                hideAfter: '5000',
                icon: 'success',
                position: { bottom: 60, right: 40 }
            });
            reloadclients();
        });
    };

    let reloadclients = function () {
        $('.jsClientItems').html('');
        $('.jsClientItems').load($('.jsClientItems').data('refreshAction'));
    };


    let clientModalClosing = function (event) {
        if (!$(event.target).find('.modal-title').text().includes('client'))
            return;
       reloadclients();
    };

    let initEventHandlers = function () {
        $(document).on('click', '.jsAddClient', showCreateOrUpdateclientView);
        $(document).on('click', '.jsEditClient', showEditclientView);
        $(document).on('click', '.jsRemoveClient', removeclient);
        $(document).on('click', '.jsReloadClients', reloadclients);
        $(document).on('submit','.jsClientForm', submitclientForm);
        $(document).on('hide.bs.modal', clientModalClosing);
    };

    let init = function () {
        initEventHandlers();
        $('.jsClientItems').load($('.jsClientItems').data('refreshAction'));
    };

    return {
        init: init
    };
})();

