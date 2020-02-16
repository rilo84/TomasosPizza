var form = document.querySelectorAll('.needs-validation');

form.forEach(item => {
    item.addEventListener('submit', (e) => {

        if (item.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        item.classList.add('was-validated')
    })
})