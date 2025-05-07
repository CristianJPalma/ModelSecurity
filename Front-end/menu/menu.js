//Logica de generacion dinamica del menu segun el rol del usuario

//guarda la url de la api
const apiUrl = 'http://localhost:5081';

//Obtiene el token del localstorage
const token = localStorage.getItem('token');

//Verifica si el token existe, si no existe redirige al login
if (!token) {
    alert('No has iniciado sesión');
    window.location.href = '../index.html';
}
//Obtiene el id del usuario del localstorage convirtiendolo a numero
const userId = Number(localStorage.getItem("userId"));
console.log(userId);

//Cierra sesion removiendo el token del localstorage y redirigiendo al login
function logout() {
    localStorage.removeItem('token');
    window.location.href = '../index.html';
}


function agruparMenu(data) {
    const menuEstructurado = {};
  
    data.forEach(item => {
      const modulo = item.modulo;
      const formulario = item.formulario;
      const permiso = item.permiso;
  
      if (!menuEstructurado[modulo]) {
        menuEstructurado[modulo] = {};
      }
      if (!menuEstructurado[modulo][formulario]) {
        menuEstructurado[modulo][formulario] = [];
      }
      menuEstructurado[modulo][formulario].push(permiso);
    });
  
    return menuEstructurado;
  }
  
  function mostrarMenu(menuEstructurado) {
    const contenedor = document.getElementById('menuContainer');
    contenedor.innerHTML = '';
  
    for (const modulo in menuEstructurado) {
      const moduloElem = document.createElement('div');
      moduloElem.className = 'modulo';
      moduloElem.textContent = modulo;
  
      const formularioLista = document.createElement('div');
      formularioLista.className = 'formulario-lista';
  
      for (const formulario in menuEstructurado[modulo]) {
        const formItem = document.createElement('div');
        formItem.className = 'formulario-item';
        formItem.textContent = formulario;
        formItem.addEventListener('click', () => {
            const permisos = menuEstructurado[modulo][formulario];
            const contenido = document.getElementById('contenido');
            
            // Actualizar el contenido con el nombre del formulario, permisos y el nuevo div
            const divExtra = `<div id="extra-${formulario}"></div>`;
            contenido.innerHTML = `<h3>${formulario}</h3><ul>` +
              permisos.map(p => `<li>${p}</li>`).join('') + '</ul>' + divExtra;
          
            // Invocar una función cualquiera
            ejecutarFuncionCualquiera();
        });
  
        formularioLista.appendChild(formItem);
      }
  
      moduloElem.addEventListener('click', () => {
        const visible = formularioLista.style.display === 'block';
        formularioLista.style.display = visible ? 'none' : 'block';
      });
  
      contenedor.appendChild(moduloElem);
      contenedor.appendChild(formularioLista);
    }
  }
  
  async function cargarMenu() {
    try {
      const response = await fetch(`${apiUrl}/api/menu/user/${userId}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
  
      if (!response.ok) throw new Error('Error al obtener menú');
  
      const data = await response.json();
      const menuEstructurado = agruparMenu(data);
      mostrarMenu(menuEstructurado);
  
    } catch (err) {
      console.error('Error cargando el menú:', err);
    }
  }
  
  document.addEventListener('DOMContentLoaded', cargarMenu);





  async function fetchApi(url, method = 'GET', data = null, headers = {}) {
    const options = {
        method,
        headers: {
            'Content-Type': 'application/json',
            ...headers
        }
    };

    if (data && method !== 'GET') {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(url, options);
        if (!response.ok) throw new Error(`Error: ${response.status}`);
        return await response.json();
    } catch (error) {
        console.error('Fetch error:', error);
        throw error;
    }
}

function ejecutarFuncionCualquiera() {
    console.log('Función cualquiera ejecutada');
  }
