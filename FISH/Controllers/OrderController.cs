﻿using FISH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FISH.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly AesEncryptionService encryptionService;

    public OrderController(ApplicationDbContext context)
    {
        var encryptionKey = "Jason_76114";
        encryptionService = new AesEncryptionService(encryptionKey);
        _context = context;
    }
    //public async Task<IActionResult<string>> EncryptString(string input)
    //{
    //    var encryptedString = encryptionService.Encrypt(input);
    //    // 做一些事情，比如將加密的字符串存儲或返回給客戶端
    //    return Ok(encryptedString);
    //}

    //public IActionResult DecryptString(string input)
    //{
    //    var decryptedString = encryptionService.Decrypt(input);
    //    // 做一些事情，比如返回解密的字符串給客戶端
    //    return Ok(decryptedString);
    //}
    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult<Orders>> PostOrder(Orders order)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        return BadRequest(ModelState);
    }

    //取得所有訂單資料
    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    //Delete Order
    // DELETE: api/Order
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid order ID.");

        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
        catch (Exception ex)
        {
            // 記錄異常（可以使用日誌框架如 Serilog 或 NLog）
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


    // 可能還需要其他的 API 方法，例如 GetOrder 來檢索訂單資訊
    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Orders>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null) return NotFound();

        return order;
    }
    //字串加密


    // 其他 CRUD 操作...
}