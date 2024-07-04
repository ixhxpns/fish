using Microsoft.AspNetCore.Mvc;
using FISH.Model;
using FISH.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FISH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

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


        // 可能還需要其他的 API 方法，例如 GetOrder 來檢索訂單資訊
        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // 其他 CRUD 操作...
    }
}
