using System.Threading.Tasks;
using ArTool.Models;
using ArTool.Models.Dtos;

namespace ArTool.ApiClients
{
    public interface ITellerApiClient
    {
        Task<PostPaymentInfoResponse> PostPaymentInfo(ElectronicPaymentInfo paymentDetails, RequestorInformation requestorInfo);

        Task<PaymentMethod> PostPaymentMethod(PaymentMethod paymentMethod, RelatesTo relatesTo,
            RequestorInformation requestorInfo);

    }
}
