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
	public class SubscriberRepository : ISubscriberRepository
	{
		private readonly ApplicationDbContext _context;

		public SubscriberRepository(ApplicationDbContext context)
        {
			_context = context;
		}
        public async Task<Subscriber> AddSubscriberAsync(Subscriber subscriber)
		{
			var newSubscriber = await _context.Subscribers.AddAsync(subscriber);
			await _context.SaveChangesAsync();
			return newSubscriber.Entity;
		}

		public async Task<Subscriber> GetSubscriberByIdAsync(string Id)
		{
			return await _context.Subscribers.FirstOrDefaultAsync(s => s.Id == Id);

        }
	
        public async Task<Co_SubscriberInvitee> AddNewDriver(Co_SubscriberInvitee driver)
        {
            var coSub = await _context.Co_SubscriberInvitees.AddAsync(driver);
            await _context.SaveChangesAsync();
            return coSub.Entity;
        }

        public async Task<Subscriber> GetSubscriberByUserId(string id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(s => s.AppUserId == id);
        }

        public async Task<Subscriber> UpdateSubscriberAsync(Subscriber subscriber)
        {
            var isSubscriber = await _context.Subscribers.FindAsync(subscriber.Id);

            if (isSubscriber != null)
            {
                _context.Subscribers.Update(isSubscriber);
                await _context.SaveChangesAsync();
                return isSubscriber;
            }
            else
            {
                return null;
            }
        }

        public List<Subscription> GetSubscriptionsBySubscriberId(string subscriberId)
        {
            return _context.Subscriptions
                           .Where(sub => sub.SubscriberId == subscriberId)
                           .Include(s => s.Operator)
                           .Include(s => s.Vehicle)
                           .ToList();
        }

        public Subscriber GetSubscriberById(string subscriberId)
        {
            return _context.Subscribers
                           .Include(s => s.Transactions)
                           .Include(s => s.AppUser)
                           .FirstOrDefault(s => s.AppUserId == subscriberId);
        }

        public int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
