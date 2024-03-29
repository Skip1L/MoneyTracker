using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samostijna.Models;

namespace Samostijna.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoryController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Category
		public async Task<IActionResult> Index()
		{
			return _context.Categories != null
				? View(await _context.Categories.ToListAsync())
				: Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
		}

		// GET: Category/CreateOrEdit
		public IActionResult CreateOrEdit(int id = 0)
		{
			return View(id == 0 ? new Category() : _context.Categories.Find(id));
		}

		// POST: Category/CreateOrEdit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
		{
			if (ModelState.IsValid)
			{
				if (category.CategoryId == 0)
					_context.Add(category);
				else
					_context.Update(category);

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(category);
		}


		// POST: Category/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Categories == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
			}

			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryExists(int id)
		{
			return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
		}
	}
}