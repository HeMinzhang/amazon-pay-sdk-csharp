﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AmazonPay.Responses
{
    public class GetProviderCreditDetailsResponse : AbstractResponse
    {
        
        private string amazonProviderCreditId;
        private string providerId;
        private string sellerId;
        private string creditReferenceId;

        private decimal creditAmount;
        private string creditAmountCurrencyCode;

        private decimal creditReversalAmount;
        private string creditReversalAmountCurrencyCode;

        private string creditStatus;
        private List<string> creditReversalIdList = new List<string>();

        private DateTime lastUpdateTimestamp;
        private DateTime creationTimestamp;

        private string reasonCode;
        private string reasonDescription;
        private string softDescriptor;

        private string parentKey;

        /// <summary>
        /// ProviderCreditDetailsResponse 
        /// </summary>
        public GetProviderCreditDetailsResponse(string xml)
        {
            SetDictionaryAndErrorResponse(xml);
            if (success)
            {
                ParseDictionaryToNewVariables(this.dictionary);
            }
        }

        /// <summary>
        /// Flattening the Dictionary
        /// The input dictionary contains key value pairs in the below format
        /// Type 1. Key (string) , Value (string)
        /// Type 2. Key (string) , Value (Dictionary)
        /// The function will parse the dictionary values into respective class variables by directly jumping to to the switch case for Type 1 
        /// else it will recursively parse the inner dictionary for Type 2
        /// </summary>
        /// <param name="dictionary"></param>
        private void ParseDictionaryToNewVariables(IDictionary dictionary)
        {
            foreach (string strKey in dictionary.Keys)
            {
                // Obj is the value of the dictionary key. this could either be a string or a nested inner dictionary.
                object obj = dictionary[strKey];
                if (obj != null)
                {
                    // If obj is dictionary recursively parse it
                    if (obj is IDictionary)
                    {
                        parentKey = strKey;
                        ParseDictionaryToNewVariables((IDictionary)obj);
                    }
                    else
                    {
                        if (Enum.IsDefined(typeof(Operator), strKey))
                        {
                            switch ((Operator)Enum.Parse(typeof(Operator), strKey))
                            {
                                case Operator.AmazonProviderCreditId:
                                    this.amazonProviderCreditId = obj.ToString();
                                    break;
                                case Operator.RequestId:
                                    requestId = obj.ToString();
                                    break;
                                case Operator.ProviderId:
                                    providerId = obj.ToString();
                                    break;
                                case Operator.ProviderSellerId:
                                    providerId = obj.ToString();
                                    break;
                                case Operator.SellerId:
                                    sellerId = obj.ToString();
                                    break;
                                case Operator.CreditReferenceId:
                                    creditReferenceId = obj.ToString();
                                    break;
                                case Operator.Amount:
                                    if (parentKey.Equals(Operator.CreditAmount.ToString()))
                                    {
                                        creditAmount = decimal.Parse(obj.ToString(), Constants.USNumberFormat);
                                    }
                                    else if (parentKey.Equals(Operator.CreditReversalAmount.ToString()))
                                    {
                                        creditReversalAmount = decimal.Parse(obj.ToString(), Constants.USNumberFormat);
                                    }
                                    break;
                                case Operator.CurrencyCode:
                                    if (parentKey.Equals(Operator.CreditAmount.ToString()))
                                    {
                                        creditAmountCurrencyCode = obj.ToString();
                                    }
                                    else if (parentKey.Equals(Operator.CreditReversalAmount.ToString()))
                                    {
                                        creditReversalAmountCurrencyCode = obj.ToString();
                                    }
                                    break;
                                case Operator.ReasonCode:
                                    reasonCode = obj.ToString();
                                    break;
                                case Operator.ReasonDescription:
                                    reasonDescription = obj.ToString();
                                    break;
                                case Operator.State:
                                    creditStatus = obj.ToString();
                                    break;
                                case Operator.SoftDescriptor:
                                    softDescriptor = obj.ToString();
                                    break;
                                case Operator.LastUpdateTimestamp:
                                    lastUpdateTimestamp = DateTime.Parse(obj.ToString());
                                    break;
                                /* The below case parses two types of Amazon Provider Credit Reversal ID member fields. When the nested Dictionary is flattened
                                 * it contains JArray. JArray contains the member field which contains the Amazon Provider Credit Reversal ID. this is added to the List
                                 */
                                case Operator.member:
                                    if (obj.GetType() == typeof(JArray))
                                    {
                                        JArray array = JArray.Parse(obj.ToString());
                                        foreach (string creditReversalId in array)
                                        {
                                            creditReversalIdList.Add(creditReversalId);
                                        }
                                    }
                                    else
                                    {
                                        creditReversalIdList.Add(obj.ToString());
                                    }
                                    break;
                                /* The reason for below case is due to IPN key value discrepancy. In the API response XML 
                                 * the Amazon Credit Reversal ID'S have the attribute <member>Amazon Credit Reversal ID</member>
                                 * in the IPN it's returned as <Id>Amazon Credit Reversal ID</Id>
                                 */
                                case Operator.Id:
                                    if (obj.GetType() == typeof(JArray))
                                    {
                                        JArray array = JArray.Parse(obj.ToString());
                                        foreach (string creditReversalId in array)
                                        {
                                            creditReversalIdList.Add(creditReversalId);
                                        }
                                    }
                                    else
                                    {
                                        creditReversalIdList.Add(obj.ToString());
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the Amazon Provider Credit ID
        /// </summary>
        /// <returns>string amazonProviderCreditId</returns>
        public string GetAmazonProviderCreditId()
        {
            return this.amazonProviderCreditId;
        }

        /// <summary>
        /// Get the Creation timestamp of the Amazon Provider Credit ID
        /// </summary>
        /// <returns>DateTime creationTimestamp</returns>
        public DateTime GetCreationTimestamp()
        {
            return this.creationTimestamp;
        }

        /// <summary>
        /// Get the Amount Credit
        /// </summary>
        /// <returns>decimal creditAmount</returns>
        public decimal GetCreditAmount()
        {
            return this.creditAmount;
        }

        /// <summary>
        /// Gret the Currency Code of Credit Amount
        /// </summary>
        /// <returns>string creditAmountCurrencyCode</returns>
        public string GetCreditAmountCurrencyCode()
        {
            return this.creditAmountCurrencyCode;
        }

        /// <summary>
        /// Get the Credit Reference ID
        /// </summary>
        /// <returns>string creditReferenceId</returns>
        public string GetCreditReferenceId()
        {
            return this.creditReferenceId;
        }

        /// <summary>
        /// Get the reversed credit amount
        /// </summary>
        /// <returns>decimal creditReversalAmount</returns>
        public decimal GetCreditReversalAmount()
        {
            return this.creditReversalAmount;
        }

        /// <summary>
        /// Get the Currency code of the Credit Amount Reversed
        /// </summary>
        /// <returns>string creditReversalAmountCurrencyCode</returns>
        public string GetCreditReversalAmountCurrencyCode()
        {
            return this.creditReversalAmountCurrencyCode;
        }

        /// <summary>
        /// Get the List of the Credit Reversal ID(s)
        /// </summary>
        /// <returns>IList creditReversalIdList</returns>
        public IList<string> GetCreditReversalIdList()
        {
            return this.creditReversalIdList.AsReadOnly();
        }

        /// <summary>
        /// Get the status of the Amazon Provider Credit ID
        /// </summary>
        /// <returns>string creditStatus</returns>
        public string GetCreditStatus()
        {
            return this.creditStatus;
        }

        /// <summary>
        /// Get the Reason Code for the state of the Amazon Provider Credit ID
        /// </summary>
        /// <returns>string reasonCode</returns>
        public string GetReasonCode()
        {
            return this.reasonCode;
        }

        /// <summary>
        /// Get the Reason Description for the state of the Amazon Provider Credit ID
        /// </summary>
        /// <returns>string reasonDescription</returns>
        public string GetReasonDescription()
        {
            return this.reasonDescription;
        }

        /// <summary>
        /// Get the Soft desciptor
        /// </summary>
        /// <returns>string softDescriptor</returns>
        public string GetSoftDescriptor()
        {
            return this.softDescriptor;
        }

        /// <summary>
        /// Get the Merchant ID of the seller associated with the Provider
        /// </summary>
        /// <returns>string sellerId</returns>
        public string GetSellerId()
        {
            return this.sellerId;
        }
    }
}
