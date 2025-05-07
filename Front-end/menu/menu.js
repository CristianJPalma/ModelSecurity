const token = localStorage.getItem('token');

if (!token) {
    alert('No has iniciado sesión');
    window.location.href = '../index.html';
}
const userId = Number(localStorage.getItem("userId")); // 👈 convertir desde string
console.log(userId);

function logout() {
    localStorage.removeItem('token');
    window.location.href = '../index.html';
}
