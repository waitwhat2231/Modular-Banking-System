using AutoMapper;
using Modules.Transactions.Application.Commands;
using Modules.Transactions.Domain.Entities;

namespace Modules.Transactions.Application.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, DepositCommand>().ReverseMap();
        }
    }
}
