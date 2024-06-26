﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samostijna.Models;

namespace Samostijna.Controllers
{
	public class TransactionController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TransactionController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Transaction
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Transactions.Include(t => t.Category);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Transaction/CreateOrEdit
		public IActionResult CreateOrEdit(int id = 0)
		{
			PopulateCategories();
			return View(id == 0 ? new Transaction() : _context.Transactions.Find(id));
		}

		// POST: Transaction/CreateOrEdit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrEdit(
			[Bind("TransactionId,CategoryId,Amount,Note,Data")] Transaction transaction)
		{
			if (ModelState.IsValid)
			{
				if (transaction.TransactionId == null)
					_context.Add(transaction);
				else
					_context.Update(transaction);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			PopulateCategories();
			return View(transaction);
		}

		// POST: Transaction/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Transactions == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
			}

			var transaction = await _context.Transactions.FindAsync(id);
			if (transaction != null)
			{
				_context.Transactions.Remove(transaction);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[NonAction]
		public void PopulateCategories()
		{
			var CategoryCollection = _context.Categories.ToList();
			Category DefaultCategory = new Category() {CategoryId = 0, Title = "Choose a Category"};
			CategoryCollection.Insert(0, DefaultCategory);
			ViewBag.Categories = CategoryCollection;
		}
	}
}