using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    [Serializable]
    public class Product
    {
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("Price")]
        public double Price;
        [JsonProperty("Quantity")]
        public int Qty;


        public double getTotalCost(int quantityRequested)
        {
            return Price * quantityRequested;
        }

        public static bool isProductOkToPurchase(User currentUser, Product currentProduct, int quantityToPurchase)
        {
            bool okToPurchase = true;

            if (!currentUser.IsUserBalanceSufficient(currentProduct, quantityToPurchase))
            {
                UI.printInsufficientBalance();
                okToPurchase = false;
            }
            else if (currentProduct.Qty <= quantityToPurchase)
            {
                UI.printProductOutOfStock(currentProduct);
                okToPurchase = false;
            }
            else if (quantityToPurchase < 0)
            {
                UI.printQuantityLessThanZero();
                okToPurchase = false;
            }

            return okToPurchase;
        }
    }
}
