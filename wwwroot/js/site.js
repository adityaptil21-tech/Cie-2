// Auto-dismiss alerts after 7 seconds
document.querySelectorAll('.alert-dismissible').forEach(function (alertEl) {
    setTimeout(function () {
        if (window.bootstrap) { bootstrap.Alert.getOrCreateInstance(alertEl).close(); }
    }, 7000);
});

// Navbar shadow on scroll
(function () {
    var nav = document.querySelector('.app-navbar');
    if (!nav) { return; }
    window.addEventListener('scroll', function () {
        nav.classList.toggle('scrolled', window.scrollY > 10);
    });
})();
