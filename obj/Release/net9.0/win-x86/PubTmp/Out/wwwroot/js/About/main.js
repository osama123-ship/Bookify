let menu = document.querySelector('.nav-item3');
let sidebar = document.querySelector('.sidebar');
let content = document.querySelector('.content');
let disable = document.querySelector('.close');


menu.addEventListener('click', function() {
    sidebar.classList.add('active');
});


disable.addEventListener('click', function() {
    sidebar.classList.remove('active');
});



let btn = document.querySelector('.submit');

btn.addEventListener('click', () => {

    let fname = document.querySelector('#f_name').value;
    let sname = document.querySelector('#l_name').value;
    let email = document.querySelector('#email').value;
    let message = document.querySelector('#message').value;

    if (fname === "" || sname === "" || email === "" || message === "") {
        alert("Please fill all the fields");
    } else {
        window.location.href = "";
    }
});
