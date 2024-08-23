using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class SubscriberProfileViewModel
    {
        public Subscriber Subscriber { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public Operator Operator { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
