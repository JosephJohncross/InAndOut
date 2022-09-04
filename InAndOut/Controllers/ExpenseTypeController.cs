using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ExpenseType> expenseList = _db.ExpenseTypes;
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
        public IActionResult Create(ExpenseType expense)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        //Update-Get
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var retrievedExpenseType = _db.ExpenseTypes.SingleOrDefault(x => x.Id == id);
            if (retrievedExpenseType == null)
            {
                return NotFound();
            }
            return View(retrievedExpenseType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int? id, ExpenseType expense)
        {
            var retrievedExpenseType = _db.ExpenseTypes.SingleOrDefault(x => x.Id == id);
            if (retrievedExpenseType == null)
            {
                return NotFound();
            }
            retrievedExpenseType.Name = expense.Name;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (Id != null || Id != 0)
            {
                var expense = _db.ExpenseTypes.SingleOrDefault(x => x.Id == Id);
                _db.ExpenseTypes.Remove(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
