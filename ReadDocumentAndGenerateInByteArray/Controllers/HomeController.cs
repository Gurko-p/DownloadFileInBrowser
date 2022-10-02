using Microsoft.AspNetCore.Mvc;
using ReadDocumentAndGenerateInByteArray.Models;
using ReadDocumentAndGenerateInByteArray.Services;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace ReadDocumentAndGenerateInByteArray.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> GetByteArray()
        {
            var fileName = "Военкоматы.xlsm";
            byte[] byteArrayFromFile = await Helper.GenerateByteArrayFromFileUsingPath(Path.Combine("E:/", fileName));
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string byteToString = StringFromArrayGenerator<byte>.GenerateStringFromArray(byteArrayFromFile);
            response.Content = new ByteArrayContent(byteArrayFromFile);
            response.TrailingHeaders.Add("test", "test");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.Add("document", byteToString);
            return response;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}