document.getElementById('VisaKlaraCheck').addEventListener('change', function () {
    document.getElementById('VisaKlaraHidden').value = this.checked;
    this.closest('form').submit();
});
document.getElementById('VisaEjKlaraCheck').addEventListener('change', function () {
    document.getElementById('VisaEjKlaraHidden').value = this.checked;
    this.closest('form').submit();
});