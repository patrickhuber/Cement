using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Glue.ServiceModel
{    
    [ServiceContract(Namespace=Namespaces.ServiceContract)]
    public interface IGlueService
    {
        [OperationContract(Action="*", ReplyAction="*")]
        Message ProcessMessage(Message requestMessage);
    }
}
