using Prism.Events;

namespace Backend.Models.Events
{
    public class SingleEventAggregator
    {
        private static IEventAggregator _agg;

        public static IEventAggregator Aggregator
        {
            get { return _agg ?? (_agg = new Prism.Events.EventAggregator()); }
        }
     //   public static IEventAggregator Aggregator => _agg ?? (_agg = new Prism.Events.EventAggregator());
    }
}
