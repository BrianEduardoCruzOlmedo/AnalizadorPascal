using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace AnalizadorPascal.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        [BindProperty]
        public string PascalCode { get; set; }


        public string StatusMessage { get; set; }
        public List<DatoTabla> DataT { get; set; }
        public string Info {  get; set; }

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;

        }

        public void OnGet()
        {
            
            if (TempData.ContainsKey("CodePascal"))
            {
                PascalCode = (string)TempData["CodePascal"];
            }
            if (TempData.ContainsKey("DataT"))
            {
                DataT = JsonSerializer.Deserialize<List<DatoTabla>>(TempData["DataT"] as string);
            }

            if (TempData.ContainsKey("Info"))
            {
                Info = (string)TempData["Info"];
            }
        
        }
        public async Task<IActionResult> OnPostSubirArchivoAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                StatusMessage = "Por favor, seleccione un archivo para subir.";
                return Page();
            }

            // Validación del tipo de archivo
            var fileExtension = Path.GetExtension(UploadedFile.FileName).ToLowerInvariant();
            if (fileExtension != ".pas")
            {
                StatusMessage = "Error: Solo se permiten archivos con la extensión .pas.";
                return Page();
            }

            try
            {
                string fileContent;

                using (var reader = new StreamReader(UploadedFile.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                TempData["CodePascal"] = fileContent;
                Debug.WriteLine(TempData["CodePascal"]);
                StatusMessage = "Archivo cargado y listo para analizar.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al cargar el archivo: {ex.Message}";
            }

            return RedirectToPage("Index");
        }


        public async Task<IActionResult> OnPostGenerarAsync()
        {

            if (string.IsNullOrEmpty(PascalCode))
            {
                StatusMessage = "Por favor, escriba o suba código Pascal.";
                return Page();

            }

            var ana = new Analizador(PascalCode);

            StatusMessage = "Código listo para ser procesado.";
            //  Debug.WriteLine("- " + JsonSerializer.Serialize(ana.GetGeneratorDataT()) + " -");
            TempData["CodePascal"] = PascalCode;
            TempData["DataT"] = JsonSerializer.Serialize(await ana.GetGeneratorDataT());
            TempData["Info"] = ($@"El programa esta {(ana.isCorrectWrite ? "bien" : "mal") } escrito y 
                                    esta {(ana.isCorrectOrden ? "bien" : "mal")} ordenado");
            //TempData[""] = JsonSerializer.Serialize(DataT).ToString()
            return RedirectToPage("Index");
        }


    }
}
