using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Tusc
    {
        public static void Start(List<User> users, List<Product> products)
        {
            int exitIndex = products.Count();
            User currentUser = new User();
            UI.printTUSCWelcome();

            // Login - when a user's login is incorrect, the application flow will return here to prompt for a new login
            Login:

            currentUser.getUserName();

            if (!string.IsNullOrEmpty(currentUser.Name))
            {
                if (currentUser.IsValidUserName())
                {
                    currentUser.getUserPassword();

                    if (currentUser.IsValidUserPassword())
                    {
                        UI.printUserWelcome(currentUser.Name);
                        
                        if (currentUser.IsValidUser())
                            UI.printUserBalance(currentUser);


                        // Show product list
                        while (true)
                        {
                            UI.printProductList(products);
                            int productChoice = UI.promptProductChoice();

                            if (productChoice == exitIndex)
                            {
                                Program.SaveAndClose(currentUser, products);
                            }
                            else
                            {
                                Product currentProduct = products[productChoice];

                                UI.printUserProductSelection(currentProduct.Name, currentUser.GetRemainingBalanceFormatted());

                                int prodQuantityToPurchase = UI.promptProductQuantityToPurchase();

                                if (Product.isProductOkToPurchase(currentUser, currentProduct, prodQuantityToPurchase))
                                {
                                    currentUser.RemainingBalance -= currentProduct.Price * prodQuantityToPurchase;
                                    currentProduct.Qty -= prodQuantityToPurchase;

                                    UI.printUserPurchase(prodQuantityToPurchase, currentProduct, currentUser);
                                    products[productChoice] = currentProduct;
                                }
                                else
                                    continue;
                            }
                        }
                    }
                    else
                    {
                        UI.printInvalidPassword();
                        goto Login;
                    }
                }
                else
                {
                    UI.printInvalidUser();
                    goto Login;
                }
            }

            UI.promptForClose();
        }
    }
}
