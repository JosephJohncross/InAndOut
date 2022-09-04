using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.TypeDropDown = TypeDropDown;
            ExpenseVM expenseVM = new ExpenseVM
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(expenseVM);  
        }

        //Create-POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            var expenseObj = new Expense
            {
                ExpenseTypeId = (int)obj.Expense.ExpenseTypeId,
                ExpenseName = obj.Expense.ExpenseName,
                Amount = obj.Expense.Amount
            };

            _db.Expenses.Add(expenseObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            //return View(obj);
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
