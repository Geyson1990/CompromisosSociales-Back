window.onload = function () {

    document.getElementById('login-form').onsubmit = function (event) {
        let key = CryptoJS.enc.Utf8.parse(document.getElementById('LoginKey')?.value);
        let iv = CryptoJS.enc.Utf8.parse(document.getElementById('LoginToken')?.value);
        let password = document.getElementById('Password')?.value;
        let user = document.getElementById('UsernameOrEmailAddress')?.value;

        let encryptedUser = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(user), key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });

        let encryptedPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });


        document.getElementById('UsernameOrEmailAddress').value = '';
        document.getElementById('Password').value = '';

        document.getElementById('RequestUser').value = encryptedUser.toString();
        document.getElementById('RequestPassword').value = encryptedPassword.toString();
    };
};