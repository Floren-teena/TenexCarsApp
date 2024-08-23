using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
	public interface ISubscriberRepository
	{
		Task<Subscriber> AddSubscriberAsync(Subscriber subscriber);
		Task<Subscriber> GetSubscriberByIdAsync(string Id);
        Task<Co_SubscriberInvitee> AddNewDriver(Co_SubscriberInvitee driver);
        Task <Subscriber> GetSubscriberByUserId(string id);
        Task <Subscriber>UpdateSubscriberAsync(Subscriber subscriber);
        List<Subscription> GetSubscriptionsBySubscriberId(string subscriberId);
        Subscriber GetSubscriberById(string subscriberId);
        int CalculateAge(DateTime birthDate);

    }
}
