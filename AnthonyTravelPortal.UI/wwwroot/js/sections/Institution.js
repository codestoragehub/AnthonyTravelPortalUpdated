let institutionSection = (function () {
    let component = $('.jsinstitution');
    let constants = {
        jsinstitutionItems: '.jsinstitutionItems',
        jsAddinstitution: '.jsAddinstitution',
        jsEditInstitution: '.jsEditInstitution',
        jsRemoveinstitution: '.jsRemoveinstitution',
        jsReloadinstitutions: '.jsReloadinstitutions',
        jsinstitutionForm: '.jsinstitutionForm',
        datainstitutionId: 'institutionId',
        jsInstitutionName:'.jsInstitutionName'
       
    };

    let viewModel = {
        institutionsItems: null,
        addinstitutionButton: null,
        reloadinstitutionsButton: null,
        institutionForm: null
      
    };

    let variables = {
        institutionId: null,
        institutionName : null
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
        let htmlResult = $($.parseHTML(view)).filter('*');
        viewModel.institutionForm = htmlResult;
        variables.institutionId = viewModel.institutionForm.data(constants.datainstitutionId);
        variables.institutionName = viewModel.institutionForm.data(constants.jsInstitutionName);
        $('#form-modal .modal-header').append('<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="outline:none;padding: 0;">x</button>');
        formModalComponent.show(modalTitle, htmlResult);
    };

    let submitinstitutionFormSucceed = function (response, status, xhr) {
        $.toast({
            heading: 'Saved',
            text: 'institution saved successfully',
            hideAfter: '5000',
            icon: 'success',
            position: { bottom: 60, right: 40 }
        });
        formModalComponent.hide();
        reloadinstitutions();
    };

    let submitinstitutionForm = function (event) {
        event.preventDefault();
        
        let options = {
            type: 'post',
            url: $('.jsInstitutionForm').attr('action'),
            success: submitinstitutionFormSucceed,
            error: showException
        };

        viewModel.institutionForm.ajaxSubmit(options);
        return false;
    };

    let showCreateOrUpdateinstitutionView = function (event) {
        let addAction = $(event.currentTarget).data('addAction');
        $.get(addAction, function (result) {
            initView(result, 'Create institution');
        });
    };

    let showEditinstitutionView = function (event) {
        let editAction = $(event.currentTarget).data('editAction');
        $.get(editAction, function (result) {
            initView(result, 'Edit institution');
        });
    };

    let removeinstitution = function (event) {
        if (!confirm('Are you sure to delete this institution?'))
            return;

        $.post($(event.currentTarget).data('removeAction'), function () {
            $.toast({
                heading: 'Removed',
                text: 'institution removed successfully',
                hideAfter: '5000',
                icon: 'success',
                position: { bottom: 60, right: 40 }
            });
            reloadinstitutions();
        });
    };

    let reloadinstitutions = function () {
        $('.jsInstitutionItems').html('');
        $('.jsInstitutionItems').load($('.jsInstitutionItems').data('refreshAction'));
    };

    let initVariables = function () {
        viewModel.institutionsItems = component.find(constants.jsinstitutionItems);
        viewModel.addinstitutionButton = component.find(constants.jsAddinstitution);
        viewModel.reloadinstitutionsButton = component.find(constants.jsReloadinstitutions);
    };

    let institutionModalClosing = function (event) {
        if (!$(event.target).find('.modal-title').text().includes('institution'))
            return;
       reloadinstitutions();
    };

    let initEventHandlers = function () {
        $(document).on('click', constants.jsAddinstitution, showCreateOrUpdateinstitutionView);
        $(document).on('click', constants.jsEditInstitution, showEditinstitutionView);
        $(document).on('click', '.jsRemoveInstitution', removeinstitution);
        $(document).on('click', '.jsReloadInstitutions', reloadinstitutions);
        $(document).on('submit','.jsInstitutionForm', submitinstitutionForm);
        $(document).on('hide.bs.modal', institutionModalClosing);
    };

    let init = function () {
        initVariables();
        initEventHandlers();
        $('.jsInstitutionItems').load($('.jsInstitutionItems').data('refreshAction'));
    };

    return {
        init: init
    };
})();

