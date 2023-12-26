using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;

namespace Repository
{
    public class DataActions
    {
        private readonly LoanContext _context;
        public DataActions(LoanContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<IEnumerable<Loan>> getAllLoans()
        {
            var loanData = await _context.Loans
                                         .Include(status => status.status)
                                         .ToListAsync();
            return loanData;
        }

        #endregion

        #region Post

        public async Task AddLoan(Loan loan)
        {
            try
            {
                var data = _context.Loans.Where(loans => loans.Email == loan.Email || loans.PhoneNo == loan.PhoneNo).ToList();

                if (data.Any(loand => loand.Email == loan.Email || loand.PhoneNo == loan.PhoneNo))
                {
                    // Throw a more specific exception with a meaningful message
                    throw new Exception();
                }

                await _context.AddAsync(loan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in AddLoan method:{ex.Message}");
                throw;
            }
        }

        public async Task AddMessage()
        {
            /*var loanDetails = await _context.Loans.Include(loan => loan.status)
                                      .Where(loan => loan.StatusId != StatusProperties.Pending)
                                      .ToListAsync();
            var loanMessages = await _context.Messages.ToListAsync();
            foreach (var loan in loanDetails)
            {
                var messageData = loanMessages.Where(loanMsg => loanMsg.Email == loan.Email)
                                              .FirstOrDefault();
                if (!loanMessages.Any(msg => msg.Email == loan.Email))
                {
                    var message = new LoanMessages
                    {
                        Name = loan.Name,
                        Email = loan.Email,
                        Message = loan.StatusId == StatusProperties.Approved
                                                        ? $"{loan.Name} your Loan has been Approved. Please consult us for more Details"
                                                        : $"{loan.Name} your Loan has been Declined. Either resubmit it or visit us for more Details"
                    };
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();
                }
                else if (loanMessages.Any(msg => msg.Email == loan.Email && msg.Name != loan.Name))
                {
                    var message = new LoanMessages
                    {
                        Name = loan.Name,
                        Email = loan.Email,
                        Message = loan.StatusId == StatusProperties.Approved
                                                        ? $"{loan.Name} your Loan has been Approved. Please consult us for more Details"
                                                        : $"{loan.Name} your Loan has been Declined. Either resubmit it or visit us for more Details"
                    };
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();
                }
            }*/
            var loanDetails = await _context.Loans
                                            .Include(loan => loan.status)
                                            .Where(loan => loan.StatusId != StatusProperties.Pending)
                                            .ToListAsync();

            var loanMessages = await _context.Messages
                                             .ToListAsync();

            foreach (var loan in loanDetails)
            {
                var existingMessage = loanMessages
                                             .FirstOrDefault(msg => msg.Email == loan.Email);

                var message = new LoanMessages
                {
                    Name = loan.Name,
                    Email = loan.Email,
                    Message = loan.StatusId == StatusProperties.Approved
                        ? $"{loan.Name} your Loan has been Approved. Please consult us for more Details"
                        : $"{loan.Name} your Loan has been Declined. Either resubmit it or visit us for more Details"
                };

                if (existingMessage == null || existingMessage.Name != loan.Name)
                {
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();
                }
            }

        }

        #endregion

        #region Put

        public async Task ApproveLoan(int Id)
        {
            var data = await _context.Loans
                                     .FindAsync(Id);
            if (data != null)
            {
                data.StatusId = StatusProperties.Approved;
                await _context.SaveChangesAsync();
            }
            return;
        }

        public async Task DeclineLoan(int Id)
        {
            var data = await _context.Loans
                                     .FindAsync(Id);
            if (data != null)
            {
                data.StatusId = StatusProperties.Declined;
                await _context.SaveChangesAsync();
            }
            return;
        }

        public async Task<Loan?> UpdateLoan(int id)
        {
            var data = await _context.Loans
                                     .FindAsync(id);

            return data;
        }

        public async Task UpdateLoan(Loan updatedLoan)
        {
            var existingLoan = await _context.Loans.FindAsync(updatedLoan.Id);

            if (existingLoan == null)
            {
                return;
            }
            existingLoan.Name = updatedLoan.Name;
            existingLoan.LoanRequest = updatedLoan.LoanRequest;
            existingLoan.Amount = updatedLoan.Amount;
            existingLoan.StatusId = StatusProperties.Pending;
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public async Task DeleteLoan(int id)
        {
            var data = await _context.Loans.FindAsync(id);

            if (data != null)
            {
                _context.Loans.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

    }
}
