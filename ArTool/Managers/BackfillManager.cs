using System.Globalization;
using System.Linq;
using ArTool.ApiClients;
using ArTool.Models;
using ArTool.Models.Dtos;
using NLog;

namespace ArTool.Managers
{
    public class BackfillManager : IBackfillManager
    {
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IChargingApiClient _chargingApiClient;
        private readonly ITellerApiClient _tellerApiClient;


        public BackfillManager(IChargingApiClient chargingClient, ITellerApiClient tellerClient)
        {
            _chargingApiClient = chargingClient;
            _tellerApiClient = tellerClient;
        }

        public PaymentMethod AddNewCreditCard(BackFillRequest request, RequestorInformation requestor)
        {

            var epdid = CreateElectronicPaymentInfo(request, requestor);
            return CreateContractPaymentMethod(epdid, request.ContractDid, requestor);
        }

        private string CreateElectronicPaymentInfo(BackFillRequest request, RequestorInformation requestor)
        {
            var records = _chargingApiClient.GetTransactionLogByToken(request.Token, requestor);
            var creditCardRecord = records.Result.FirstOrDefault();

            if (creditCardRecord == null)
                return null;

            if (creditCardRecord.ResultReferenceId != request.Token)
                return null;

            var creditCard = new CreditCard()
            {
                Name = creditCardRecord.Name,
                AddressLine1 = creditCardRecord.Address1,
                AddressLine2 = creditCardRecord.Address2,
                City = creditCardRecord.City,
                Country = creditCardRecord.Country,
                State = creditCardRecord.State,
                PostalCode = creditCardRecord.Zip,
                CardType = creditCardRecord.CardType,
                First6Digits = creditCardRecord.CCNumFirst6,
                Last4Digits = creditCardRecord.Last4Digits,
            };

            var expiresMonthYear = creditCardRecord.ExpirationDate.Split('/');
            if (expiresMonthYear.Length > 1)
            {
                creditCard.ExpirationMonth = expiresMonthYear[0];
                creditCard.ExpirationYear = expiresMonthYear[1];
            }

            var paymentInfo = new ElectronicPaymentInfo()
            {
                AccountDid = creditCardRecord.AccountDid,
                CreatedById = "ArBackfill",
                PaymentType = creditCardRecord.PaymentType,
                TokenId = creditCardRecord.ResultReferenceId,
                TokenExpireDt = creditCardRecord.ResultReferenceExpirationDate,
                CreditCard = creditCard
            };

            var detailResponse = _tellerApiClient.PostPaymentInfo(paymentInfo, requestor);
            return detailResponse.Result.ElectronicPaymentDid;
        }

        private PaymentMethod CreateContractPaymentMethod(string epdid, string contractDid, RequestorInformation requestor)
        {
            var paymentMethod = new PaymentMethod()
            {
                ElectronicPaymentDid = epdid,
                CreatedBy = "ArBackFill"
            };

            var relatesTo = new RelatesTo()
            {
                Did = contractDid,
                Type = "contract"
            };

            var methodResponse = _tellerApiClient.PostPaymentMethod(paymentMethod, relatesTo, requestor);
            var responseEpDid = methodResponse.Result.ElectronicPaymentDid;
            if (responseEpDid != epdid)
                return null;

            return paymentMethod;

        }


    }
}
