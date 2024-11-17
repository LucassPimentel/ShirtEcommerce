using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using sh_rt.Models;

namespace sh_rt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagesController : Controller
    {
        private readonly ImagesConfiguration _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagesController(IOptions<ImagesConfiguration> myConfig, IWebHostEnvironment hostingEnvironment)
        {
            _myConfig = myConfig.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Error"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);
            var filePathsName = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                   _myConfig.FolderNameProductImages);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") ||
                         formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Result"] = $"{files.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {size} bytes";

            ViewBag.Archives = filePathsName;

            //retorna a viewdata
            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
                 _myConfig.FolderNameProductImages);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);
            FileInfo[] files = dir.GetFiles();
            model.ProductImagesPath = _myConfig.FolderNameProductImages;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string imageToDelete = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.FolderNameProductImages + "\\", fname);

            if ((System.IO.File.Exists(imageToDelete)))
            {
                System.IO.File.Delete(imageToDelete);
                ViewData["Deletado"] = $"Arquivo(s) {imageToDelete} deletado com sucesso";
            }
            return View("index");
        }

    }
}
