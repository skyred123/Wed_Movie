window.alert = function (msg, title = 'Thông Báo!', type = 'success', autoClose=true) {
    const alert = document.createElement('div');
    alert.classList.add('alert', `alert-${type}`,"alert-custum", 'alert-dismissible', 'fade', 'show-custum', 'd-flex', 'justify-content-end');
    alert.setAttribute('role', 'alert');
    alert.setAttribute('style', `
        width: 400px;
        position: absolute;
        top: 10px;
        right: 10px;
        z-index: 999999;
    `);


    const closeButton = document.createElement('button');
    closeButton.classList.add('btn-close');
    closeButton.setAttribute('type', 'button');
    closeButton.setAttribute('data-bs-dismiss', 'alert');
    closeButton.setAttribute('aria-label', 'Close');

    const strongElement = document.createElement('strong');
    strongElement.innerText = title;

    const messageElement = document.createTextNode(` ${msg}`);

    const contentElement = document.createElement('div');
    contentElement.classList.add('container');

    const rowElement = document.createElement('div');
    rowElement.classList.add('row');

    const colElement = document.createElement('div');
    colElement.classList.add('col');

    colElement.appendChild(strongElement);
    colElement.appendChild(messageElement);

    rowElement.appendChild(colElement);
    contentElement.appendChild(rowElement);

    alert.appendChild(closeButton);
    alert.appendChild(contentElement);

    const container = document.querySelector('.main-content');
    container.insertBefore(alert, container.firstChild);

    if (autoClose) {
        setTimeout(() => {
            alert.remove();
        }, 5000 );
    }
}
