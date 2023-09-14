using BlueQR.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QRCoder.Extensions;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using static QRCoder.PayloadGenerator;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlueQR.Controllers
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
            QRModel model = new QRModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(QRModel model)
        {
            if (!string.IsNullOrEmpty(model.WebsiteURL))
            {
                Url generator = new Url(model.WebsiteURL);
                string payload = generator.ToString();
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                var qrCodeAsBitmap = qrCode.GetGraphic(20);
                string base64String = Convert.ToBase64String(BitmapToByteArray(qrCodeAsBitmap));
                model.QRImageURL = "data:image/png;base64," + base64String;

            }
            return View("Index", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }


    }


}