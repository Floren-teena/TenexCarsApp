using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
	public interface ISubscriptionRepository
	{
		Task<Subscription> AddSubscriptionAsync(Subscription subscription);
        Task<IEnumerable<Subscription>> GetAllSubscription();
        Task <Subscription>GetSubscriptionBySubcriber(string Id);
        Task<Subscription> GetSubscriptionForVehicle(string vehicleId);
        Task <Subscription>UpdateSubscription(Subscription getExistingSubscription);
        Task<List<Subscription>> GetAllSubscriptionsForOperator(string operatorId);
        Task<Subscription> GetSubscriptionForOperator(string operatorId);
        Task<IEnumerable<Subscription>> GetSubscriptionsByOperatorAsync(string operatorId);
        bool CancelSubscription(string subscriptionId);
        Task<Subscription> GetActiveSubscriptionBySubscriberId(string subscriberId);
        //Task<string> GetSubscriptionStatusForOperator(string operatorId);


    }
}
