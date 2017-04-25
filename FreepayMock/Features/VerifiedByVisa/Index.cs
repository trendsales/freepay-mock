using System;

namespace FreepayMock.Features.VerifiedByVisa
{
    public class Index
    {
        public class Model
        {
            public int Amount { get; set; }
            public int Currency { get; set; }
            public int Code { get; set; }
            public string Timestamp { get; set; }

            public string CurrencyString
            {
                get
                {
                    // see https://en.wikipedia.org/wiki/ISO_4217
                    switch (Currency)
                    {
                        case 208:
                            return "DKK";
                        case 978:
                            return "EUR";
                        case 578:
                            return "NOK";
                        case 752:
                            return "SEK";
                        default:
                            return "UNKNOWN";
                    }
                }
            }


            public string AmountString
            {
                get
                {
                    var amount = Convert.ToDouble(Amount / Convert.ToDouble(100));
                    return $"{amount:0.00}";
                }
            }
        }
    }
}