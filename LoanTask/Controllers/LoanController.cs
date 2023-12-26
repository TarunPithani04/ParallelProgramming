using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace LoanTask.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _service;

        public LoanController(LoanService loanService)
        {
            _service = loanService;
        }

        #region '   Get   '

        public async Task<IActionResult> Index()
        {
            var loans = await _service.GetLoansAsync();
            return View(loans);
        }

        #endregion

        #region '    Post    '

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LoanMVC loan)
        {
            try
            {
                if (loan != null)
                {
                    await _service.AddLoan(loan);
                }
                else
                {
                    return View(loan);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return View(loan);
            }
        }

        #endregion

        #region '    Put    '

        [HttpPost]
        public async Task<IActionResult> Approve(int Id)
        {
            await _service.ApproveLoan(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int Id)
        {
            await _service.DeclineLoan(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _service.GetLoanByIdAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Loan updatedLoan)
        {
            if (id != updatedLoan.Id)
            {
                return BadRequest();
            }

            await _service.UpdateLoan(updatedLoan);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region '    Delete    '

        [HttpPost]
        public async Task<IActionResult> Delete(List<int> selectedIds)
        {
            foreach (var id in selectedIds)
            {
                await _service.DeleteLoan(id);
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
