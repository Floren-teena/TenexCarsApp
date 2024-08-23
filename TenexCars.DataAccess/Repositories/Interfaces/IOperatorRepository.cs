using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface IOperatorRepository
    {
        Task<Operator> GetOperatorById(string Id);
        Task<Operator> GetOperatorByIdAsync(string operatorId);
        Task<OperatorMember> GetOperatorMemberByUserId(string Id);
        Task<IEnumerable<Operator>> GetAllOperatorsAsync();
        Task AddOperatorMemberAsync(OperatorMember member);
		Task<IEnumerable<OperatorMember>> GetAllOperatorMembersAsync();
		Task DeleteOperatorMemberAsync(string email);
        Task<IEnumerable<OperatorMember>> GetAllMembersForOperatorAsync(string operatorId);
        Task<Operator> GetOperatorByUserId(string Id);
        

        Task<Operator> AddOperatorAsync(Operator member);

        Task<int> GetTotalNumberOfCars(string operatorId);
        Task<int> GetTotalNumberOfSubscribers(string operatorId);
        Task<int> GetTotalNumberOfReservedCars(string operatorId);
        Task<int> GetTotalNumberOfActiveCars(string operatorId);


    }
}