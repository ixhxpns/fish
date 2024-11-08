namespace FISH.Controllers;

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class CaptchaController : Controller
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILocalStorageService localStorageService;

    public CaptchaController(IHttpContextAccessor _httpContextAccessor, ILocalStorageService localStorageService)
    {
        httpContextAccessor = _httpContextAccessor;
        this.localStorageService = localStorageService;
    }

    public async Task<IActionResult> GenerateCaptcha()
    {
        var captchaCode = await GenerateRandomCode();
        // 將驗證碼存儲在 LocalStorage 中
        await localStorageService.SetItemAsync("CaptchaCode", captchaCode);

        var captchaImage = await GenerateCaptchaImage(captchaCode);
        // 可以選擇通過響應頭或另一個方式將驗證碼發送給客戶端
        //Response.Headers.Append("CaptchaCode", captchaCode);
        return File(captchaImage, "image/jpeg");
    }

    private async Task<string> GenerateRandomCode()
    {
        var random = new Random();
        var captchaCode = random.Next(1000, 9999).ToString();
        return captchaCode;
    }

    private async Task<byte[]> GenerateCaptchaImage(string captchaCode)
    {
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