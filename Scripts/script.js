function ChangeImage(UploadImage, previewImage) {
    if (UploadImage.files && UploadImage.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(UploadImage.files[0]);
    }
}