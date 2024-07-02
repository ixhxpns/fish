namespace FISH.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class CaptchaController : Controller
    {
        public IActionResult GenerateCaptcha()
        {
            var captchaCode = GenerateRandomCode();
            HttpContext.Session.SetString("CaptchaCode", captchaCode);
            var captchaImage = GenerateCaptchaImage(captchaCode);
            return File(captchaImage, "image/jpeg");
        }

        private string GenerateRandomCode()
        {
            // 生成隨機的驗證碼
            var random = new Random();
            var captchaCode = random.Next(1000, 9999).ToString();
            return captchaCode;
        }

        private byte[] GenerateCaptchaImage(string captchaCode)
        {
            // 創建驗證碼圖片
            using var bitmap = new Bitmap(100, 40);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            using var font = new Font("Arial", 20, FontStyle.Bold);
            graphics.DrawString(captchaCode, font, Brushes.Black, new PointF(10, 5));

            using var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }
    }

}
