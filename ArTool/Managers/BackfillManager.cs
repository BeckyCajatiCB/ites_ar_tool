using ArTool.Models;

namespace ArTool.Managers
{
    public class BackfillManager : IBackfillManager
    {
        public CreditCard AddNewCreditCard(BackFillRequest request)
        {
            var creditCard = new CreditCard()
            {
                AddressLine1 = "123 Main St",
                AddressLine2 = "suite 100",
                CardType = "Visa",
                City = "Norcross",
                Country = "US",
                ExpirationMonth = "01",
                ExpirationYear = "2025",
                First6Digits = "411111",
                Last4Digits = "1111",
                Name = "Albert Einstein",
                State = "GA"
            };

            return creditCard;
        }
    }
}
