function guessNumber() {
    // Lấy giá trị người dùng nhập
    var userNumber = document.getElementById("user-input").value;

    // Tạo số ngẫu nhiên từ 1 đến 10
    var randomNumber = Math.floor(Math.random() * 10) + 1;

    // Kiểm tra xem người dùng đoán đúng hay không
    if (userNumber == randomNumber) {
        // Người dùng đoán đúng, hiển thị thông báo
        document.getElementById("result-message").innerHTML = "Chúc mừng! Bạn đã đoán đúng.";

        // Tạo mã giảm giá ngẫu nhiên
        var discountCode = generateDiscountCode();

        // Hiển thị mã giảm giá
        document.getElementById("result-message").innerHTML += "<br>Mã giảm giá của bạn: " + discountCode;
    } else {
        // Người dùng đoán sai, hiển thị thông báo
        document.getElementById("result-message").innerHTML = "Số ngẫu nhiên là " + randomNumber + ". Bạn đoán sai rồi, hãy thử lại!";
    }
}

// Hàm tạo mã giảm giá ngẫu nhiên
function generateDiscountCode() {
    var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var code = "";

    for (var i = 0; i < 8; i++) {
        var randomIndex = Math.floor(Math.random() * characters.length);
        code += characters.charAt(randomIndex);
    }

    return code;
}
