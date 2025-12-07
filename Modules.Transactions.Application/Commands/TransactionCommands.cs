using MediatR;

namespace Modules.Transactions.Application.Commands
{
    public class DepositCommand : IRequest
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class WithdrawalCommand : IRequest
    {
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
    public class TransferCommand : IRequest
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public int Amount { get; set; }
    }

}
