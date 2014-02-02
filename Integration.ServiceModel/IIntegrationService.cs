using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Integration.ServiceModel
{    
    [ServiceContract(Namespace=Namespaces.ServiceContract)]
    public interface IIntegrationService
    {
        [OperationContract(Action="*", ReplyAction="*")]
        Message ProcessMessage(Message requestMessage);
    }
}
