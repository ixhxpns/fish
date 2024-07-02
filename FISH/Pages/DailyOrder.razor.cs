namespace FISH.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using FISH.Model;

    public class DailyOrderPageModel : PageModel
    {
        [BindProperty]
        public DailyOrder DailyOrder { get; set; }

        [BindProperty]
        [Required]
        public string Captcha { get; set; }

        public IActionResult OnPost()
        {
            var captchaCode = HttpContext.Session.GetString("CaptchaCode");
            if (Captcha != captchaCode)
            {
                ModelState.AddModelError("Captcha", "驗證碼錯誤");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 保存資料到資料庫的邏輯
            return RedirectToPage("Success");
        }
    }
}
