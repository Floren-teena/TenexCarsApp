using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface ICoSubscriberRepository
    {
        Task<Co_SubscriberInvitee> AddInvitee(Co_SubscriberInvitee invitee);
        Task<Co_SubscriberInvitee> GetCoSubscriberByUserId(string userId);
        Task<Co_SubscriberInvitee> UpdateInvitee(Co_SubscriberInvitee invitee);
    }
}
