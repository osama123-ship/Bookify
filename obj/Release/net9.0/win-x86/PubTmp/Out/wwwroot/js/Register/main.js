
let password = document.querySelector("[name='Password']")
let one = document.querySelector('.container ul .one');
let two = document.querySelector('.container ul .two');
let three = document.querySelector('.container ul .three');
let four = document.querySelector('.container ul .four');
let form = document.forms[0];
form.addEventListener('submit', (e) => {
  let passvalid = true;
  if (password.value.length <= 8) {
    passvalid = false;
    one.style.color = 'red'
  }
  else {
    one.style.color = 'green'
  }
    if (!/[a-z]/.test(password.value) || (!/[A-Z]/.test(password.value))) {
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
  if (!/[_!@#$%^&*]/.test(password.value)) {
    passvalid = false;
    four.style.color = 'red';
} else {
    four.style.color = 'green';
}
  if (passvalid === false) {
    e.preventDefault()
  }
})


  let btn1 = document.querySelector('.btn1'); 
  let btn2 = document.querySelector('.btn2'); 
  let pass = document.querySelector("[name='Password']");

  btn1.addEventListener('click', function() {
      if (pass.type === 'text') {
          pass.type = 'password';
          btn1.classList.remove('active');
          btn2.classList.add('active');
      } else {
          pass.type = 'password';
      }
  });

  btn2.addEventListener('click', function() {
      if (pass.type === 'password') {
          pass.type = 'text';
          btn2.classList.remove('active');
          btn1.classList.add('active');
      } else {
          pass.type = 'text';
      }
  });

let confirm_pass = document.querySelector("[name='ConfirmedPassword']");
  let btn3 = document.querySelector('.btn3'); 
  let btn4 = document.querySelector('.btn4'); 

  btn3.addEventListener('click', function() {
      if (confirm_pass.type === 'text') {
          confirm_pass.type = 'password';
          btn3.classList.remove('active');
          btn4.classList.add('active');
      } else {
          confirm_pass.type = 'password';
      }
  });

  btn4.addEventListener('click', function() {
      if (confirm_pass.type === 'password') {
          confirm_pass.type = 'text';
          btn4.classList.remove('active');
          btn3.classList.add('active');
      } else {
          confirm_pass.type = 'text';
      }
  });




  let reg = document.querySelector('button[type="submit"]');
  reg.addEventListener("click",(e)=>{
    if(pass.value !== confirm_pass.value || pass.value === "" || confirm_pass.value === ""){
      e.preventDefault();
      alert("Passwords do not match");
    }
  });

