﻿using Aurum.Models.IncomeDTOs;

namespace Aurum.Services.IncomeServices
{
    public interface IIncomeService
    {
        (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate);
        Task<decimal> GetTotalIncome(int accountId);
        Task<decimal> GetTotalIncome(int accountId, DateTime endDate);
        Task<List<IncomeDto>> GetAll(int accountId);
        Task<List<IncomeDto>> GetAll(int accountId, DateTime endDate);
        Task<List<IncomeDto>> GetAll(int accountId, DateTime startDate, DateTime endDate);
        Task<List<IncomeWithCurrency>> GetAllWithCurrency(int accountId);
        Task<int> Create(ModifyIncomeDto income);
        Task<bool> Delete(int incomeId);
    }
}
