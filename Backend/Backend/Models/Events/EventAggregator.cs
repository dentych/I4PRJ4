using Prism.Events;

namespace Backend.Models
{
    /// <summary>
    /// Event aggregator.
    /// </summary>
    public class SingleEventAggregator
    {
        /// <summary>
        /// The event aggreator instance.
        /// </summary>
        private static IEventAggregator _agg;

        /// <summary>
        /// Event aggreator property. Makes sure it's a singleton instance.
        /// </summary>
        public static IEventAggregator Aggregator
        {
            get { return _agg ?? (_agg = new EventAggregator()); }
        }
        //public static IEventAggregator Aggregator => _agg ?? (_agg = new EventAggregator());


    }

}
