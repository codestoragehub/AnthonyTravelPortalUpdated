let userSection = (function () {
    let component = $('.jsuser');
    let constants = {
        jsuserItems: '.jsuserItems',
        jsAddUser: '.jsAddUser',
        jsEditUser: '.jsEditUser',
        jsRemoveuser: '.jsRemoveuser',
        jsReloadusers: '.jsReloadusers',
        jsuserForm: '.jsuserForm',
        datauserId: 'userId',
        jsUserName: '.jsUserName',

    };

    let viewModel = {
        usersItems: null,
        adduserButton: null,
        reloadusersButton: null,
        userForm: null,
        institutiontype: null
    };

    let variables = {
        userId: null,
        userName: null

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
        viewModel.userForm = htmlResult;
        variables.userId = viewModel.userForm.data(constants.datauserId);
        variables.userName = viewModel.userForm.find(constants.jsUserName);
        viewModel.institutiontype = viewModel.userForm.find(constants.jsInstitutiontype);
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
        reloadusers();
    };

    let submituserForm = function (event) {
        event.preventDefault();

        let options = {
            type: 'post',
            url: $('.jsUserForm').attr('action'),
            success: submitclientFormSucceed,
            error: showException
        };

        viewModel.userForm.ajaxSubmit(options);
        return false;
    };

    let showCreateOrUpdateuserView = function (event) {
        debugger;
        let addAction = $(event.currentTarget).data('addAction');
        $.get(addAction, function (result) {
            initView(result, 'Create user');
        });
    };

    let showEdituserView = function (event) {
        debugger;
        let editAction = $(event.currentTarget).data('editAction');
        $.get(editAction, function (result) {
            initView(result, 'Edit user');
        });
    };

    let removeuser = function (event) {
        if (!confirm('Are you sure to delete this user?'))
            return;

        $.post($(event.currentTarget).data('removeAction'), function () {
            $.toast({
                heading: 'Removed',
                text: 'User removed successfully',
                hideAfter: '5000',
                icon: 'success',
                position: { bottom: 60, right: 40 }
            });
            reloadusers();
        });
    };

    let reloadusers = function () {
        $('.jsUserItems').html('');
        $('.jsUserItems').load($('.jsUserItems').data('refreshAction'));
    };


    let userModalClosing = function (event) {
        if (!$(event.target).find('.modal-title').text().includes('user'))
            return;
        reloadusers();
    };

    let initEventHandlers = function () {
        $(document).on('click', '.jsAddUser', showCreateOrUpdateuserView);
        $(document).on('click', '.jsEditUser', showEdituserView);
        $(document).on('click', '.jsRemoveUser', removeuser);
        $(document).on('click', '.jsReloadUsers', reloadusers);
        $(document).on('submit', '.jsUserForm', submituserForm);
        $(document).on('hide.bs.modal', userModalClosing);
    };

    let init = function () {
        initEventHandlers();
        $('.jsUserItems').load($('.jsUserItems').data('refreshAction'));
    };

    return {
        init: init
    };
})();