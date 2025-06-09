let btn1 = document.querySelector('.eye .btn1'); 
let btn2 = document.querySelector('.eye .btn2'); 
let pass = document.querySelector("[name='Password']");

btn2.addEventListener('click', function() {
    if (pass.type === 'password') {
        pass.type = 'text';
        btn2.classList.remove('active');
        btn1.classList.add('active');
    } else {
        pass.type = 'password';
    }
});

btn1.addEventListener('click', function() {
    if (pass.type === 'text') {
        pass.type = 'password';
        btn1.classList.remove('active');
        btn2.classList.add('active');
    } else {
        pass.type = 'text';
    }
});



let btn3 = document.querySelector('.eye .btn3'); 
let btn4 = document.querySelector('.eye .btn4'); 
let pass1 = document.querySelector("[name='ConfirmedPassword']")
btn4.addEventListener('click', function() {
    if (pass1.type === 'password') {
        pass1.type = 'text';
        btn4.classList.remove('active');
        btn3.classList.add('active');
    } else {
        pass1.type = 'password';
    }
});

btn3.addEventListener('click', function() {
    if (pass1.type === 'text') {
        pass1.type = 'password';
        btn3.classList.remove('active');
        btn4.classList.add('active');
    } else {
        pass1.type = 'text';
    }
});

let password = document.querySelector("[name='Password']")
let one = document.querySelector('small ul .one');
let two = document.querySelector('small ul .two');
let three = document.querySelector('small ul .three');
let four = document.querySelector('small ul .four');
let form = document.forms[0];
form.addEventListener('submit', (e) => {
  let passvalid = true;
  if (password.value.length <= 6) {
    passvalid = false;
    one.style.color = 'red'
  }
  else {
    one.style.color = 'green'
  }
  if (!/[a-z]/.test(password.value)) {
    passvalid = false;
    two.style.color = 'red'
  }
  else {
    two.style.color = 'green'
  }
  if (!/[A-Z]/.test(password.value)) {
    passvalid = false;
    two.style.color = 'red'
  }
  else {
    two.style.color = 'green'
  }
  if (!/[0-9]/.test(password.value)) {
    passvalid = false;
    three.style.color = 'red'
  }
  else {
    three.style.color = 'green'
  }
  if (!/[!@#$%^&*]/.test(password.value)) {
    passvalid = false;
    four.style.color = 'red';
} else {
    four.style.color = 'green';
}
  if (passvalid === false) {
    e.preventDefault()
  }
})



let update_btn = document.querySelector(".update_btn");
let input1 = document.querySelector("#Password");
let input2 = document.querySelector("#ConfirmedPassword");
let n_match = document.querySelector(".n_match");

update_btn.addEventListener("click", (e) => {
  if (input1.value !== input2.value) {
    e.preventDefault(); 
    n_match.classList.remove("d-none")
    n_match.style.color = 'red';
  } else {
    n_match.classList.add("d-none");
  }
});

input1.addEventListener("input", () => {
  n_match.classList.add("d-none");
});
input2.addEventListener("input", () => {
  n_match.classList.add("d-none");
});
