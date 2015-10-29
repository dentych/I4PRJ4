using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Backend.Models
{
    public class SingleEventAggregator
    {
        private static IEventAggregator _agg;
        public static IEventAggregator Aggregator => _agg ?? (_agg = new Prism.Events.EventAggregator());
    }
}
