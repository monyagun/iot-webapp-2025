
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp;
using MyPortfolioWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWebApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BoardsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var boards = await _context.Boards.OrderByDescending(b => b.CreatedAt).ToListAsync();
            return View(boards);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var board = await _context.Boards.FirstOrDefaultAsync(m => m.Id == id);
            if (board == null) return NotFound();

            return View(board);
        }

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Board board)
        {
            if (ModelState.IsValid)
            {
                board.UserId = _userManager.GetUserId(User);
                board.CreatedAt = System.DateTime.Now;
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var board = await _context.Boards.FindAsync(id);
            if (board == null) return NotFound();

            if (board.UserId != _userManager.GetUserId(User))
                return Forbid();

            return View(board);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Board board)
        {
            if (id != board.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var originBoard = await _context.Boards.FindAsync(id);
                    if (originBoard.UserId != _userManager.GetUserId(User))
                        return Forbid();

                    originBoard.Title = board.Title;
                    originBoard.Content = board.Content;

                    _context.Update(originBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Boards.Any(e => e.Id == board.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var board = await _context.Boards.FirstOrDefaultAsync(m => m.Id == id);
            if (board == null) return NotFound();

            if (board.UserId != _userManager.GetUserId(User))
                return Forbid();

            return View(board);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null) return NotFound();

            if (board.UserId != _userManager.GetUserId(User))
                return Forbid();

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
