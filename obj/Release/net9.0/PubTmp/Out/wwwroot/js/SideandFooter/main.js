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

document.addEventListener('click', function (e) {
    if (sidebar.classList.contains('active') && !sidebar.contains(e.target) && !menu.contains(e.target)) {
        sidebar.classList.remove('active');
    }
});

