using Repository.Models;
using Repository;

public class LoanService
{
    private readonly DataActions _dataActions;

    public LoanService(DataActions dataActions)
    {
        _dataActions = dataActions;
    }

    #region Get

    public async Task<IEnumerable<Loan>> GetLoansAsync()
    {
        try
        {
            var loanData = await _dataActions.getAllLoans();
            return loanData;
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            Console.WriteLine($"An error occurred in GetLoansAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<Loan> GetLoanByIdAsync(int id)
    {
        try
        {
            var data = await _dataActions.UpdateLoan(id);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in GetLoanByIdAsync: {ex.Message}");
            throw;
        }
    }

    #endregion

    #region Post

    public async Task<bool> AddLoan(LoanMVC loanMvc)
    {
        try
        {
            if (loanMvc != null)
            {
                var loan = new Loan
                {
                    Name = loanMvc.Name,
                    Amount = loanMvc.Amount,
                    Email = loanMvc.Email,
                    LoanRequest = loanMvc.LoanRequest,
                    PhoneNo = loanMvc.PhoneNo,
                    StatusId = StatusProperties.Pending,
                };
                await _dataActions.AddLoan(loan);
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in AddLoan: {ex.Message}");
            throw;
        }
    }

    public async Task AddMessage()
    {
            await _dataActions.AddMessage();
    }

    #endregion

    #region Put

    public async Task UpdateLoan(Loan updatedLoan)
    {
        try
        {
            await _dataActions.UpdateLoan(updatedLoan);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in UpdateLoan: {ex.Message}");
            throw;
        }
    }

    public async Task ApproveLoan(int Id)
        {
            try
            {
                await _dataActions.ApproveLoan(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in ApproveLoan: {ex.Message}");
                throw;
            }
        }

    public async Task DeclineLoan(int Id)
        {
            try
            {
                await _dataActions.DeclineLoan(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in DeclineLoan: {ex.Message}");
                throw;
            }
        }

    #endregion

    #region Delete

    public async Task DeleteLoan(int id)
    {
        try
        {
            await _dataActions.DeleteLoan(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in DeleteLoan: {ex.Message}");
            throw;
        }
    }

    #endregion

}
