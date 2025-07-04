const apiModelSecurity = 'http://localhost:5081';

const registerUser = document.getElementById('registerUser');
const firstName = document.getElementById('firstName');
const lastName = document.getElementById('lastName');
const email = document.getElementById('email');
const password = document.getElementById('password');
const confirmPassword = document.getElementById('confirmPassword');

registerUser.addEventListener('submit', async (e) => {
    e.preventDefault();

    if (!registerUser.checkValidity()) {
        alert("Llena todos los campos");
        return;
    }
    if (password.value !== confirmPassword.value) {
        alert("Las contraseñas no coinciden");
        return;
    }
    const registerData = {
        firstName: firstName.value,
        lastName: lastName.value,
        email: email.value,
        userName: firstName.value.toLowerCase() + lastName.value.toLowerCase(),
        password: password.value
    };
    try {
        const response = await fetch(`${apiModelSecurity}/api/auth/register`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(registerData)
        });
        if (!response.ok) {
            alert('Error al registrar usuario');
            return;
        }
        
        const resultado = await response.json();
        console.log('Registro exitoso:', resultado);
        registerUser.reset(); // Limpiar el formulario después del registro
        alert('Te registraste');

    } catch (err) {
        console.error('Error en el registro:', err);
        alert('Ocurrió un error al registrarse.');
    }
});
