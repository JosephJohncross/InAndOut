using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> expenseList = _db.Expenses;
            return View(expenseList);
        }

        //Create-GET
        public IActionResult Create()
        {
            return View();  
        }

        //Create-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense expense)
        {
            _db.Expenses.Add(expense);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update-Get
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var retrievedExpense = _db.Expenses.SingleOrDefault(x => x.Id == id);
            if (retrievedExpense == null)
            {
                return NotFound();
            }
            return View(retrievedExpense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int? id, Expense expense)
        {
            var retrievedExpense = _db.Expenses.SingleOrDefault(x => x.Id == id);
            if (retrievedExpense == null)
            {
                return NotFound();
            }
            retrievedExpense.ExpenseName = expense.ExpenseName;
            retrievedExpense.Amount = expense.Amount;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (Id != null || Id != 0)
            {
                var expense = _db.Expenses.SingleOrDefault(x => x.Id == Id);
                _db.Expenses.Remove(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
