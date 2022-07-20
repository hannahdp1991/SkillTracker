using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public interface IServiceBusConsumer
    {
        public Task ReceiveMessages();

        public Task CloseQueueAsync();

        public ValueTask DisposeQueuAsync();
    }
}
