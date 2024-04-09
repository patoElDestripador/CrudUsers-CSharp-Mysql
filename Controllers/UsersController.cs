using CrudUsers0.Data;
using CrudUsers0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrudUsers02.Controllers{

    public class UsersController : Controller// se declara el controllador, nah mentira esto fue lo ultimo k hice jsjs
    {   
        public readonly BaseContext _context; //Contexto base de la base de datos.
        public UsersController(BaseContext context)// Constructor de la clase UsersController.
        {
            _context = context; // recibe un parámetro context que representa el contexto de la base de datos. Luego, asigna este contexto a la variable _context.
        }
        
        public async Task<IActionResult> Index()//Metodo para mostrar la vista Index
        {
            return View(await _context.Users.ToListAsync());//retorna una vista que muestra una lista de todos los usuarios obtenidos de la base de datos
        }

        public async Task<IActionResult> Details(int? id)//Metodo para mostrar la vista Details
        {
            return View(await _context.Users.FirstOrDefaultAsync(element => element.Id == id));//Esta línea retorna una vista que muestra el primer usuario que cumple con la condición especificada. La condición establecida en la expresión lambda 
            //lambda : Una expresión lambda en es una forma breve y concisa de definir una función anónima. Es útil para escribir código más compacto, especialmente cuando se trabaja con métodos que aceptan funciones como argumentos.
        } 

        public IActionResult Create() // Método para mostrar la vista de creación de un nuevo usuario.
        {
            // Primero es importnate declarar y llamar la vista de creación antes de Ejecutar El metodo a continuacion public IActionResult create(User u)
            return View();
        }

        [HttpPost] // indica que el método al que se aplica este atributo [HttpPost] responderá a las solicitudes son comúnmente utilizados para recibir datos de formularios o de otras fuentes de entrada en el servidor.
        public IActionResult Create(User u)//Método para crear un nuevo usuario.
        {
            _context.Users.Add(u);//se está agregando un nuevo usuario a la base de datos 
            _context.SaveChanges(); // Esta línea de código guarda los cambios (cualquier modificación, inserción o eliminación que se haya realizado en los objetos del contexto se confirma y se aplica a la base de datos)
            return RedirectToAction("Index");// se utiliza para redirigir al usuario a otra paghina
        }

        public async Task<ActionResult> Delete(int? id){// Método para eliminar un usuario.
            if(id != null)//valida que el id no este vacio 
            {
                var user = await _context.Users.FindAsync(id);//busca un usuario en la base de datos utilizando su ID (solo por id No acepta otro parametro)
                _context.Users.Remove(user);//elimina el usuario encontrado anteriormente de la base de datos 
                await _context.SaveChangesAsync();// Esta línea de código guarda los cambios de manera asincrona (cualquier modificación, inserción o eliminación que se haya realizado en los objetos del contexto se confirma y se aplica a la base de datos)
                return RedirectToAction("Index");// se utiliza para redirigir al usuario a otra paghina
                
                /* Super Dato para la laif: 
                Si Miran bien ciegos >:v en el método Delete, se utilizan "FindAsync" y "SaveChangesAsync", a diferencia del método Create, que utiliza solo "SaveChanges" (sin el Async).
                Esto se debe a que el método Delete necesita acceder a la base de datos, lo que implica un tiempo de espera.
                Cuando realizamos consultas a una base de datos o a un servicio externo (API o Ms), se utilizan métodos asíncronos.
                Esto indica al código que debe esperar una respuesta exitosa o fallida para poder continuar.
                En el caso del método Create, no es necesario esperar, ya que somos nosotros quienes enviamos la información y no necesitamos una respuesta.
                */
            }
            return RedirectToAction("Index");// en caso de que este vacio automaticamente se redirije al index 

        }

        [HttpGet] // indica que el método al que se aplica este atributo [HttpGet] responderá a las solicitudes  generalmente se utilizan para devolver datos o vistas al cliente sin modificar el estado del servidor.
        public async Task<IActionResult> Update(int id)//Método para actualizar un usuario.
        {
            var user = await _context.Users.FindAsync(id);//busca un usuario en la base de datos utilizando su ID (solo por id No acepta otro parametro)
            if (user == null) // Verifica si se encontró un usuario en la base de datos. En caso de que no entrara al condicional.
            {
              return RedirectToAction("Index");//generalmente aqui debe de ir algun mensaje de error o algo relacionado pero en este caso se redirige al index
            }
            return View(user);//devuelve una vista que muestra los detalles del usuario encontrado. La vista recibirá como modelo el objeto user, que contiene la información del usuario
        }

        [HttpPost] // indica que el método al que se aplica este atributo [HttpPost] responderá a las solicitudes son comúnmente utilizados para recibir datos de formularios o de otras fuentes de entrada en el servidor.
        public async Task<IActionResult> Update(int id, [Bind("Id,FirstName,LastName,Email")] User user)// Método para actualizar un usuario.
        {
            /* Super Dato para la laif: 
                Esa baina de le [Bind] se utiliza para obtener solo datos específicos y no modificar todos los campos en la Tabla.
                Esto significa que los datos existentes que no se están modificando permanecerán intactos.
                Se utiliza para evitar sobrescribir toda la información del usuario y solo modificar lo que expecifiquemos dentro.

            */
            if (id != user.Id)//Esto se utiliza para asegurarse de que el ID del usuario en el objeto user sea el mismo que el ID pasado como parámetro para evitar posibles errores o modificaciones indebidas en otros usuarios.
            {
              return RedirectToAction("Index");//generalmente aqui debe de ir algun mensaje de error o algo relacionado pero en este caso se redirige al index
            }
           
            if (ModelState.IsValid) // Verificar si el modelo es válido
            {
                /* Super Dato para la laif: 
                ModelState.IsValid se encarga de validar que los datos del form pasa todas las reglas de validación definidas en el modelo de datos.
                Solo  devuelve true o false
                */
                    _context.Update(user); // Actualizar el usuario en la base de datos
                    await _context.SaveChangesAsync();// Esta línea de código guarda los cambios de manera asincrona (cualquier modificación, inserción o eliminación que se haya realizado en los objetos del contexto se confirma y se aplica a la base de datos)
                    return RedirectToAction("Index");//generalmente aqui debe de ir algun mensaje de error o algo relacionado pero en este caso se redirige al index
            }
            return View(user);// En caso de que no se pueda actualizar debido a que no pasó las validaciones, se vuelve a cargar la información del usuario para poder corregir. 
        }

        public async Task<IActionResult> Search(string? searchString) // Método para Buscar un usuario.
        {
        var Searchusers = _context.Users.AsQueryable();//esto lo que hace es preparar una consulta IQueryable que luego puede ser modificada y ejecutada para recuperar los resultados deseados de la base de datos.
        if (!string.IsNullOrEmpty(searchString))//primero validamos que no este vacio o nulo 
        {
        Searchusers = Searchusers.Where(u => u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower()) || u.Email.ToLower().Contains(searchString.ToLower()));
        /*
        Esta línea de código se utiliza para filtrar los usuarios que cumplan con la condición especificada. el ToLower convierte
        todo en minuscula para asi obtener mejores resultados en la busqueda
        */
        return View("Index", Searchusers.ToList());//retorna el index con los datos encontrados
        }
        return RedirectToAction("Index");//En caso de que no encuntre nada acciona el index normal
        }

    }

}



