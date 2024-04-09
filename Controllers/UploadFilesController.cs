using Microsoft.AspNetCore.Mvc;
using CrudUsers0.Models;


namespace CrudUsers0.Controllers
{
  public class UploadFilesController : Controller
  {
    private readonly IWebHostEnvironment _environment;// el IwebHostEnvironment es donde alamacenamos el acceso las rutas de archivos y directorios específicos del entorno, o a configuraciones específicas del entorno.

    public UploadFilesController(IWebHostEnvironment env)
    {
        _environment = env;//  aqui es donde inicilizamos lo explicado anteriomente
    }

    /* Subir imagenes a una carpeta */
        public IActionResult Index()
        {
            string webRootPath = _environment.WebRootPath;// Esta línea de código obtiene la ruta raíz del directorio web (webRootPath) osea lo que hay en la carpeta wwwroot
            string imagenesFolderPath = Path.Combine(webRootPath, "uploads");
            /*
             La función Path.Combine() se utiliza para combinar varias cadenas de ruta en una sola ruta completa. En este caso, 
             se combina la ruta raíz del directorio web (webRootPath) con el nombre del directorio donde se almacenarán las imágenes 
             cargadas por los usuarios, que se llama "uploads".
            */
            string[] imagenesPaths = Directory.GetFiles(imagenesFolderPath);// esto contendrá una lista de rutas de acceso de todos los archivos de imágenes dentro del directorio de imágenes 

            var imagenesNames = new List<string>(); // Crear una lista para almacenar solo los nombres de archivo, excluyendo las rutas completas
            foreach (var path in imagenesPaths)
            {
                var fileName = Path.GetFileName(path);
                imagenesNames.Add(fileName);
            }
            ViewBag.message = TempData["message"];
            return View(imagenesNames); // Pasar solo los nombres de archivo a la vista
        }

        public async Task<IActionResult> Upload2(UploadFile upload){//Metodo para cargar la imagen en la carpeta
            // era aki _environment.WebRootPath tiene que ir esto  _environment.WebRootPath y ud tenia esto _environment.ContentRootPath sopenco lea mejor el codigo
        var fileName = System.IO.Path.Combine(_environment.WebRootPath,"uploads", upload.MyFile.FileName);

        await upload.MyFile.CopyToAsync(new System.IO.FileStream(fileName, System.IO.FileMode.Create));

        TempData["message"] = "Archivo arriba";
        return RedirectToAction("Index");
        }     

        
        public IActionResult DeleteFile(string fileName)// Método para eliminar un archivo
        {
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            return RedirectToAction("Index");
        }
  }
}