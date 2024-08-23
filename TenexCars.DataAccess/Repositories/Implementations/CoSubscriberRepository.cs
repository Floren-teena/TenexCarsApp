using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
    public class CoSubscriberRepository : ICoSubscriberRepository
    {
        private readonly ApplicationDbContext _context;

        public CoSubscriberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Co_SubscriberInvitee> AddInvitee(Co_SubscriberInvitee invitee)
        {
            var newInvitee = await _context.Co_SubscriberInvitees.AddAsync(invitee);
            await _context.SaveChangesAsync();
            return newInvitee.Entity;
        }

        public async Task<Co_SubscriberInvitee> GetCoSubscriberByUserId(string userId)
        {
            return await _context.Co_SubscriberInvitees.FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public async Task<Co_SubscriberInvitee> UpdateInvitee(Co_SubscriberInvitee invitee)
        {
            var isSubscriber = await _context.Co_SubscriberInvitees.FindAsync(invitee.Id);

            if (isSubscriber != null)
            {
                _context.Co_SubscriberInvitees.Update(isSubscriber);
                await _context.SaveChangesAsync();
                return isSubscriber;
            }
            else
            {
                return null;
            }
        }
    }
}
