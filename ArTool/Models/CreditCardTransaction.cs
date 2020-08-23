using Newtonsoft.Json;

namespace ArTool.Models
{
    public class CreditCardTransaction
    {
        public string Fid { get; set; }

        [JsonProperty("result_reference_id")]
        public string ResultReferenceId { get; set; }
        
        [JsonProperty("result_reference_expiration_date")]
        public string ResultReferenceExpirationDate { get; set; }
        
        [JsonProperty("account_did")]
        public string AccountDid { get; set; }
        
        [JsonProperty("cc_type")]
        public string CardType { get; set; }
        
        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }

        [JsonProperty("cc_number")]
        public string Last4Digits { get; set; }

        [JsonProperty("cc_name")]
        public string Name { get; set; }

        [JsonProperty("cc_address")]
        public string Address1 { get; set; }

        [JsonProperty("cc_address2")]
        public string Address2 { get; set; }

        [JsonProperty("cc_city")]
        public string City { get; set; }

        [JsonProperty("cc_state")]
        public string State { get; set; }

        [JsonProperty("cc_zip")]
        public string Zip { get; set; }

        [JsonProperty("cc_country_code")]
        public string Country { get; set; }
        
        [JsonProperty("cc_expiration_date")]
        public string ExpirationDate { get; set; }

        [JsonProperty("cc_num_first6")]
        public string CCNumFirst6 { get; set; }
    }

    //    "result_reference_expiration_date": "08/21/2023",
    //    "is_surcharge_applicable": true,
    //    "fid": "TH003DL67G1ZDC0GZ8CZ",
    //    "account_did": "                    ",
    //    "user_id": "unittest",
    //    "batch_id": "unitTest",
    //    "trans_type": "A",
    //    "trans_amount": 0,
    //    "cc_type": "Visa",
    //    "cc_number": "1111",
    //    "cc_expiration_date": "05/2025",
    //    "cc_name": "Abe Lincoln",
    //    "cc_address": "123 Main st",
    //    "cc_address2": null,
    //    "cc_city": "Norcross",
    //    "cc_state": "GA",
    //    "cc_zip": "30092",
    //    "cc_country_code": "US",
    //    "notes": "Declined",
    //    "original_fid": "",
    //    "void_flag": "N",
    //    "batch_number": "",
    //    "transaction_date": "2020-05-21T13:31:34.64",
    //    "modified_date": "2020-05-21T13:31:34.643",
    //    "av_address_token": "",
    //    "av_zip_token": null,
    //    "processor": "BluePay",
    //    "processor_code": "0",
    //    "processor_text": "APPROVED",
    //    "currency_type": "USD",
    //    "company_code": 550,
    //    "transaction_status": "Success",
    //    "transaction_end_date": "1970-01-01T00:00:00",
    //    "cc_reference_id": null,
    //    "cc_num_first6": null,
    //    "avs_raw_response": null,
    //    "cvv_raw_response": null,
    //    "full_raw_response": "UNIT_TEST=THISISFAKE&PAYMENT_ACCOUNT=xxxxxxxxxxxx1111&AUTH_CODE=&CARD_TYPE=VISA&STATUS=1&CC_EXPIRES_MONTH=01&MERCHDATA=locale%3dEnglish%26currency%3dUSD%26companycode%3d550%26transactionid%3dTD96E4B5470AB946B081%26shpf-form-id%3decom%26host_site%3d&MERCHDATA_locale=English&TRANS_ID=100311678339&AVS_RESULT=_&MERCHDATA_currency=USD&MERCHDATA_currency=USD&ADDR1=1096+Sesame+Street&CARD_EXPIRE=0119&NAME2=Duck&BIN=411111&ZIPCODE=30092&COUNTRY=US&CUSTOMER_CODE=100311678339&CUSTOM_ID2=&CUSTOM_ID=&MERCHDATA_COMPANYCODE=550&FLAGS=T&CC_EXPIRES_YEAR=19&AVS=_&MERCHDATA_shpf-form-id=ecom&BINDATA=6~M~C~07E25P30P03E00&Result=APPROVED&NAME=Donald+Duck&MERCHDATA_host_site=&CVV2_RESULT=M&ZIP=30092&TRANS_TYPE=AUTH&LOGIN_ACCOUNT_ID=100078303309&RRNO=100311678339&100849291417&FANCY_STATUS=Approved&PROCESSOR_ID=100078303311&MESSAGE=INFORMATION+STORED&MERCHDATA_transactionid=TD96E4B5470AB946B081&CUSTOM_ID=&TPS_DEF=MERCHANT+TRANSACTION_TYPE+MODE&ORDER_ID=100311678339&PAYMENT_TYPE=CREDIT&ID=100311678339&CUSTOM_ID2=&MERCHANT=100078303309&&CITY=Norcross&CVV2_STATUS=1&CC_EXPIRES=0119&BANK_NAME=&TRANSACTION_TYPE=AUTH&AMOUNT=0.00&ADDR2=Suite+200&CVV2=_M&NAME1=Donald&STATE=GA",
    //    "phone_number": null,
    //    "payment_type": "CRC",
    //    "order_did": "",
    //    "result_reference_id": "100849291417",
    //    "result_text": "Please contact Customer Service at 800-891-8880",
    //    "result_code": "6",
    //    "result_auth_code": ""
    //},
    //}
}
