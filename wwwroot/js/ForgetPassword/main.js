 function validateEmail() {
         const emailInput = document.getElementById('Email');
         const errorDiv = document.getElementById('error');
         const emailValue = emailInput.value.trim();

         const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue);

         if (!isValid) {
             errorDiv.style.display = 'block';
             return false;
         }

         errorDiv.style.display = 'none';
         return true;
     }
}
