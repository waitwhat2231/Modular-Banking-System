using AutoMapper;
using Common.SharedClasses.Dtos.Transactions;
using Modules.Transactions.Application.Commands;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, DepositCommand>().ReverseMap();
            CreateMap<Transaction, WithdrawalCommand>().ReverseMap();
            CreateMap<Transaction, TransferCommand>().ReverseMap();
            CreateMap<Transaction, AddTransactionDto>().ReverseMap();
        }
    }
}
