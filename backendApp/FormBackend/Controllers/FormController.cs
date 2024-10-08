using Microsoft.AspNetCore.Mvc;
using backendApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace backendApp.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context) {
            _context = context; // コンストラクタでContextを初期化
        }

        // POST メソッド（フォームの送信）
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] Infomation user) {
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Message: {user.Message}");

            try {
                await _context.UserInfo.AddAsync(user); // データベースにユーザー情報を追加
                await _context.SaveChangesAsync(); // 変更を保存
                return Ok(new
                {
                    message = "送信しました"
                });
            }
            catch(Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}"); // スタックトレースも出力
                return StatusCode(500, new
                {
                    message = ex.Message,
                    stackTrace = ex.StackTrace // 開発環境向けのスタックトレース送信
                });
            }
        }

        // GET メソッド（データの取得）
        [HttpGet("contacts")]
        public async Task<IActionResult> GetContacts() {
            try {
                var contacts = await _context.UserInfo.ToListAsync(); // データベースから全てのコンタクト情報を取得
                return Ok(contacts);
            }
            catch(Exception ex) {
                Console.WriteLine($"Error fetching contacts: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new
                {
                    message = "データ取得中にエラーが発生しました",
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}
