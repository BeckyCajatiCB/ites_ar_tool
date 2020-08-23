using ArTool.Models;

namespace ArTool.Managers
{
    public interface IBackfillManager
    {
        PaymentMethod AddNewCreditCard(BackFillRequest request, RequestorInformation requestor);

    }
}
