using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsMessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string queue_topic_name);
    }
}
