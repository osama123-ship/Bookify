document.addEventListener("DOMContentLoaded", function () {
    const counterBox = document.querySelector(".counter-box");
    const durationInSeconds = 120;

    // ⏳ لو مفيش expiryTime في localStorage، حط واحد جديد
    let expiryTime = localStorage.getItem("expiryTime");
    if (!expiryTime) {
        expiryTime = new Date().getTime() + durationInSeconds * 1000;
        localStorage.setItem("expiryTime", expiryTime);
    } else {
        expiryTime = parseInt(expiryTime);
    }

    // 🕒 حدث التايمر كل ثانية
    const timer = setInterval(() => {
        const now = new Date().getTime();
        const timeLeft = Math.floor((expiryTime - now) / 1000);

        if (timeLeft >= 0) {
            const minutes = Math.floor(timeLeft / 60);
            const seconds = timeLeft % 60;
            counterBox.textContent = `0${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;
        } else {
            counterBox.textContent = "00:00";
            clearInterval(timer);
        }
    }, 1000);

    // 🔁 زرار resend
    window.resetTimer = function () {
        const newExpiry = new Date().getTime() + durationInSeconds * 1000;
        localStorage.setItem("expiryTime", newExpiry);
        location.reload();
    };

    // ⌨️ تحكم في التنقل بين الخانات
    const inputs = document.querySelectorAll(".input-field input");

    inputs.forEach((input, index) => {
        input.addEventListener("input", function () {
            if (this.value.length === 1 && index < inputs.length - 1) {
                inputs[index + 1].focus();
            }
        });

        input.addEventListener("keydown", function (e) {
            if (e.key === "Backspace" && this.value === "") {
                if (index > 0) {
                    const prevInput = inputs[index - 1];
                    prevInput.focus();
                    const length = prevInput.value.length;
                    prevInput.setSelectionRange(length, length);
                }
            }
        });
    });
});
