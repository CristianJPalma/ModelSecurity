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
    const formCode = item.formCode;  // Aseguramos que 'formCode' esté presente
    
    // Si el módulo no existe, lo creamos
    if (!menuEstructurado[modulo]) {
      menuEstructurado[modulo] = {};
    }
    
    // Si el formulario no existe en el módulo, lo creamos
    if (!menuEstructurado[modulo][formulario]) {
      menuEstructurado[modulo][formulario] = {
        permisos: [],
        formCode: formCode  // Guardamos el 'formCode' aquí
      };
    }
    
    // Agregamos el permiso al formulario
    menuEstructurado[modulo][formulario].permisos.push(permiso);
  });

  return menuEstructurado;
}

  
function mostrarMenu(menuEstructurado) {
  const contenedor = document.getElementById('menuContainer');
  contenedor.innerHTML = ''; // Limpiamos el contenedor para el nuevo menú
  
  // Iteramos sobre los módulos y formularios para crear el menú dinámicamente
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
        const permisos = menuEstructurado[modulo][formulario].permisos;
        const code = menuEstructurado[modulo][formulario].formCode;  // Obtener el formCode correctamente
        const contenido = document.getElementById('contenido');
        
        // Creamos un nuevo div para mostrar los datos dinámicamente
        const divExtra = document.createElement('div');
        divExtra.id = `extra-${formulario}`;
        
        // Limpiamos el contenido anterior (si lo hay)
        contenido.innerHTML = `<h3>${formulario}</h3><h2>Code: ${code}</h2><ul>` +
          permisos.map(p => `<li>${p}</li>`).join('') + '</ul>';
        
        // Añadimos el div para los datos adicionales
        contenido.appendChild(divExtra);
        
        // Llamamos a la función fetchApi para cargar los datos desde la API
        fetchApi(`${apiUrl}/api/${code}`, 'GET')
          .then(data => {
            // Aquí procesamos y mostramos los datos adicionales en el div
            // Verificamos qué datos existen en la respuesta para mostrarlos
            divExtra.innerHTML = `<h4>Datos adicionales:</h4><ul>`;
            
            // Si los datos tienen diferentes estructuras, mostramos solo los campos que existan
            if (data && data.length > 0) {
              data.forEach(item => {
                let listItem = '';
                
                // Verificamos si el campo `id` existe
                if (item.id) {
                  listItem += `<li>ID: ${item.id}</li>`;
                }
                if (item.name) {
                  listItem += `<li>Name: ${item.name}</li>`;
                }

                if (item.userName) {
                  listItem += `<li>Name: ${item.userName}</li>`;
                }

                if (item.firstName) {
                  listItem += `<li>Name: ${item.firstName}</li>`;
                }
                if (item.description) {
                  listItem += `<li>Description: ${item.description}</li>`;
                }
                
                // Añadimos el item al div
                if (listItem) {
                  divExtra.innerHTML += listItem;
                }
              });
            } else {
              divExtra.innerHTML += `<li>No hay datos disponibles.</li>`;
            }
            
            divExtra.innerHTML += `</ul>`;
          })
          .catch(err => {
            divExtra.innerHTML = `<p>Error al cargar los datos adicionales.</p>`;
            console.error('Error en la API:', err);
          });
        
        // Invocar una función cualquiera si es necesario
        ejecutarFuncionCualquiera();
      });
  
      formularioLista.appendChild(formItem);
    }
  
    // Evento de click para mostrar/ocultar la lista de formularios
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
