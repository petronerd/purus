using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Unity.Mvc5;

namespace Services
{
    
    [ServiceContract]
    public interface ICustomer: ICRUD<Customer>
    {
        [OperationContract]
        Feedback Authenticate(Customer Customer);

        [OperationContract]
        Customer GetByEmail(string Email);

        [OperationContract]
        List<Customer> GetAll(SearchModel SearchModel);
    }

    [ServiceContract]
    public interface IQuote : ICRUD<Quote>
    {
        Quote GetByUserID(int ID);
    }

    [ServiceContract]
    public interface ICRUD<T>
    {
        [OperationContract]
        Feedback Add(T Item);

        Feedback Update(T Item);

        T GetByID(int ID);
    }

}
