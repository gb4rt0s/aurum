using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;

namespace Aurum.Repositories.AccountRepo
{
    public class AccountRepo : IAccountRepo
    {
        public async Task<List<AccountDto>> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Create(ModifyAccountDto account)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Update(int accountId, ModifyAccountDto account)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}