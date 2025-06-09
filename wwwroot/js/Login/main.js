const name = document.getElementById('EmailOrUserName');
const password = document.getElementById('Password');
const form = document.querySelector('form');
const errorElement = document.getElementById('error');

form.addEventListener('submit', (e) => {
    let messages = [];
   
    if (messages.length > 0) {
        e.preventDefault();
        console.log(messages);
        errorElement.innerText = messages.join(', ');
    }
});

let reg = document.querySelector('.register');
if (reg) {
    reg.addEventListener('click', function () {
        window.location.href = 'http://localhost:49190/';
    });
}
