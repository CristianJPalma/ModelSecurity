const apiModelSecurity = 'http://localhost:5081'

const loginUser = document.getElementById('loginUser')
const email = document.getElementById('email')
const password = document.getElementById('password')

loginUser.addEventListener('submit', async (evento) => {
    evento.preventDefault();

    if (!loginUser.checkValidity()) {
        alert("Llena todos los campos");
        return;
    }
    const login = {
        email: email.value,
        password: password.value
    }

    try{
        const response = await fetch(`${apiModelSecurity}/api/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(login)
        });
        if (!response.ok) {
            alert('Error al logearse');
            return;
        }

        // Opcional: guardarlo

        const resultado = await response.json();
        console.log('Login exitoso:', resultado);
        const userId = Number(resultado.user.id)
        loginUser.reset();
        
        // Guarda el token
        
        localStorage.setItem('token', resultado.token);
        localStorage.setItem("userId", userId);
    
        
        
        // Redirige o muestra contenido protegido
        window.location.href = './menu/menu.html';
        
    }
    catch(error){
        console.error('Error en el registro:', error);
        alert('Ocurrió un error al registrarse.');
    }
})
