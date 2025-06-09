let lis = document.querySelectorAll(".navbar-expand-lg .navbar-nav .nav-link");

lis.forEach((ele)=>{

    ele.addEventListener("click",function(){
    
        lis.forEach((el)=>{
    
            el.classList.remove("active");
    
        })
    
        ele.classList.add("active");
    
    })

})
