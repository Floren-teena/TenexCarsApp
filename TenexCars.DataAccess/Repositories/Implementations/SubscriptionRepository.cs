using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
	public class SubscriptionRepository : ISubscriptionRepository
	{
		private readonly ApplicationDbContext _context;

		public SubscriptionRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Subscription> AddSubscriptionAsync(Subscription subscription)
		{
			var newSubscription = await _context.Subscriptions.AddAsync(subscription);
			await _context.SaveChangesAsync();
			return newSubscription.Entity;
		}
        public async Task<IEnumerable<Subscription>> GetAllSubscription()
        {
            return _context.Subscriptions.ToList();
        }

        public async Task<Subscription> GetSubscriptionBySubcriber(string Id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriberId == Id);
        }

        public async Task<Subscription> GetSubscriptionForVehicle(string vehicleId)
        {
			return await _context.Subscriptions.FirstOrDefaultAsync(s => s.VehicleId == vehicleId);
        }

        public async Task<Subscription> GetSubscriptionForOperator(string operatorId)
        {
            var result = await _context.Subscriptions
                .Include(s => s.Vehicle).Include(s => s.Subscriber)
                .ThenInclude(sub => sub.AppUser)
                .FirstOrDefaultAsync(s => s.OperatorId == operatorId);
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<List<Subscription>> GetAllSubscriptionsForOperator(string operatorId)
        {
            return await _context.Subscriptions
                                 .Include(s => s.Subscriber)
                                 .ThenInclude(sub => sub.AppUser)
                                 .Include(s => s.Vehicle)
                                 .Where(s => s.OperatorId == operatorId)
                                 .ToListAsync();
        }


        public async Task<Subscription> UpdateSubscription(Subscription getExistingSubscription)
        {
            var isSubscriber = await _context.Subscriptions.FindAsync(getExistingSubscription.Id);

            if (isSubscriber != null)
            {
                _context.Subscriptions.Update(isSubscriber);
                await _context.SaveChangesAsync();
                return isSubscriber;
            }
            else
            {
                return null;
            }
        }

        public bool CancelSubscription(string subscriptionId)
        {
            var activeSubscription = _context.Subscriptions
                .FirstOrDefault(sub => sub.Id == subscriptionId && sub.SubscriptionStatus == SubscriptionStatus.Active);

            if (activeSubscription != null)
            {
                activeSubscription.SubscriptionStatus = SubscriptionStatus.Cancelled;
                activeSubscription.TermEnd = DateTime.UtcNow;
                _context.Subscriptions.Update(activeSubscription);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByOperatorAsync(string operatorId)
        {
            return await _context.Subscriptions
                .Where(x => x.OperatorId == operatorId)
                .ToListAsync();
        }

        public async Task<Subscription> GetActiveSubscriptionBySubscriberId(string subscriberId)
        {
                 return await _context.Subscriptions
                .FirstOrDefaultAsync(sub => sub.SubscriberId == subscriberId && sub.SubscriptionStatus == SubscriptionStatus.Active);
        }
    }
}
