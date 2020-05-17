using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Events
    {
        public static void Event(string eventName, Dictionary<string, object> eventData)
        {
#if UNITY_EDITOR

            Debug.Log($"Event={eventName}, Data={string.Join(", ", eventData.Select(i => i.Key + "=" + i.Value))}");

#endif

            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);

#if !UNITY_STANDALONE

            AppMetrica.Instance.ReportEvent(eventName, eventData);

#endif
        }

        public static void Error(string message, string stackTrace)
        {
            UnityEngine.Analytics.Analytics.CustomEvent("Error", new Dictionary<string, object> { { "Message", message }, { "StackTrace", stackTrace } });

#if !UNITY_STANDALONE

            AppMetrica.Instance.ReportError(message, stackTrace);

#endif
        }

        public static void Event(string eventName)
        {
            Event(eventName, new Dictionary<string, object>());
        }

        public static void Event(string eventName, string paramName, object paramValue)
        {
            Event(eventName, new Dictionary<string, object> { { paramName, paramValue } });
        }

        public static void Event(string eventName, string paramName1, object paramValue1, string paramName2, object paramValue2)
        {
            Event(eventName, new Dictionary<string, object> { { paramName1, paramValue1 }, { paramName2, paramValue2 } });
        }

        public static void UnexpectedBehaviour(string description, string stackTrace)
        {
            Event("UnexpectedBehaviour", "Description", description, "StackTrace", stackTrace);
        }

        public static void OpenURL(string url)
        {
            Event("OpenURL", "URL", url);
        }

        public static void OpenWindow(string name)
        {
            Event("OpenWindow", "Name", name);
        }

#if UNITY_IAP

        public static void BuyPro(UnityEngine.Purchasing.Product product)
        {
            PurchaseComplete("BuyPro", product);
        }

        public static void BuyProSale(UnityEngine.Purchasing.Product product)
        {
            PurchaseComplete("BuyProSale", product);
        }

        public static void BuyProPromo(UnityEngine.Purchasing.Product product, string promoCode)
        {
            PurchaseComplete("BuyProPromo", product, promoCode);
        }

        public static void PurchaseComplete(string eventName, UnityEngine.Purchasing.Product product, string promoCode = null)
        {
#if !UNITY_STANDALONE_OSX

            if (!product.hasReceipt) return;

            var eventData = new Dictionary<string, object>
            {
                { "Product", product.definition.id },
                { "DeviceId", SystemInfo.deviceUniqueIdentifier },
                { "Group", Profile.Instance.Group },
                { "TransactionId", product.transactionID }
            };

            if (!promoCode.IsEmpty()) eventData.Add("PromoCode", promoCode);

            Event(eventName, eventData);

            var revenue = new YandexAppMetricaRevenue((double) product.metadata.localizedPrice, product.metadata.isoCurrencyCode);

            if (product.hasReceipt)
            {
                try
                {
                    var receipt = JsonUtility.FromJson<PurchaseReceipt>(product.receipt);

                    if (receipt.Store == "GooglePlay")
                    {
                        var payload = receipt.GooglePlayPayload;

                        revenue.Receipt = new YandexAppMetricaReceipt
                        {
                            Data = payload.json,
                            Signature = payload.signature,
                            TransactionID = receipt.TransactionID
                        };

                        Debug.Log("YandexAppMetricaReceipt added!");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            AppMetrica.Instance.ReportRevenue(revenue);
            
#endif
	    }

        public static void PurchaseRestore(string productId, decimal price, string currency)
	    {
			Event("PurchaseRestore", "Product", productId);
	    }

	    public static void PurchaseFailed(string productId, string reason)
	    {
		    Event("PurchaseFailed", new Dictionary<string, object> { { "Product", productId }, { "Reason", reason } });
	    }

#endif
    }
}