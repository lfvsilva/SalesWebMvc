// Alternância de tema claro/escuro (Bootstrap 5.3 data-bs-theme).
(function () {
    const button = document.getElementById('themeToggle');
    if (!button) return;

    const icon = button.querySelector('i');
    const root = document.documentElement;

    const syncIcon = () => {
        const dark = root.getAttribute('data-bs-theme') === 'dark';
        icon.className = (dark ? 'bi bi-sun' : 'bi bi-moon-stars') + ' fs-5';
    };

    button.addEventListener('click', () => {
        const next = root.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
        root.setAttribute('data-bs-theme', next);
        try { localStorage.setItem('theme', next); } catch { }
        syncIcon();
    });

    syncIcon();
})();
