const apiModelSecurity = 'http://localhost:5081';

const registerUser = document.getElementById('registerUser');
const firstName = document.getElementById('firstName');
const lastName = document.getElementById('lastName');
const email = document.getElementById('email');
const password = document.getElementById('password');

document.getElementById('registerButton').addEventListener('click', async (e) => {
    e.preventDefault();

    const person = {
        firstName: firstName.value,
        lastName: lastName.value,
        email: email.value
    };

    try {
        const response = await fetch(`${apiModelSecurity}/api/person`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(person)
        });

        if (!response.ok) { // Verifica que la respuesta sea exitosa (status 200-299)
            throw new Error('Error en la creación de persona');
        }

        const data = await response.json(); // Si la respuesta es válida, la parseamos
        console.log('info:', data);

        // Ahora creamos el User con el ID recibido
        const user = {
            userName: firstName.value.toLowerCase() + lastName.value.toLowerCase(),
            password: password.value,
            code: 'Usernormal',
            Active: true,
            personId: data.id // Asegúrate que esto sea el nombre correcto de la propiedad
        };

        const userResponse = await fetch(`${apiModelSecurity}/api/user`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (!userResponse.ok) {
            throw new Error('Error en la creación de usuario');
        }

        const userData = await userResponse.json(); // Parsear respuesta de usuario
        console.log('info:', userData);

        const patch = {
            id: userData.id,
            personId: 0,
            userName: "",
            password: "",
            code: `User${userData.id}`,
            Active: true
        };

        const patchResponse = await fetch(`${apiModelSecurity}/api/user/${userData.id}`, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(patch)
        });

        if (!patchResponse.ok) {
            throw new Error('Error al actualizar usuario');
        }

        const patchData = await patchResponse.json(); // Parsear respuesta de patch
        console.log('info:', patchData);

        const rolUser = {
            userId: userData.id,
            rolId: 1
        };

        const rolUserResponse = await fetch(`${apiModelSecurity}/api/roluser`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(rolUser)
        });

        if (!rolUserResponse.ok) {
            throw new Error('Error al asignar el rol al usuario');
        }

        const rolUserData = await rolUserResponse.json();
        console.log('info:', rolUserData);

    } catch (err) {
        console.error('Error en el registro:', err);
        alert('Ocurrió un error al registrarse.');
    }
});
