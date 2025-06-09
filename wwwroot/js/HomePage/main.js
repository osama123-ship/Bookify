
let copywrite = document.querySelector(".copywrite");
let icon = document.querySelector(".navbar-toggler-icon");
let flag = 0;

    icon.addEventListener("click", function() {
        if (flag === 0) {
            copywrite.style.display = "none";
            flag = 1; 
        } else {
            copywrite.style.display = "block";
            flag = 0; 
        }
    });

/*    // document.getElementsByClassName("log_in").addEventListener("click",()=>{
    //     window.location.href='file:///C:/Users/WIN%2010/Desktop/web%20project/Log%20In/index.html';
// })
const loginUrl = '@Url.Action("Login", "Account")';
const registerUrl = '@Url.Action("Register", "Account")';
    document.getElementById("Login").addEventListener("click", function() {
        window.location.href = loginUrl; // يتم التوجيه إلى الصفحة
    });
document.getElementById("Register").addEventListener("click", function () {
    window.location.href = registerUrl; // يتم التوجيه إلى الصفحة
});*/