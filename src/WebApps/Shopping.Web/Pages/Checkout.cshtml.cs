using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Web.Pages
{
    public class CheckoutModel
        (IBasketService basketService, ILogger<CheckoutModel> logger)
        : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("Checkout button clicked");

            try
            {
                Cart = await basketService.LoadUserBasket();

                if (Cart == null)
                {
                    logger.LogWarning("User basket is null.");
                    ModelState.AddModelError(string.Empty, "Unable to load user basket.");
                    return Page();
                }

                if (!ModelState.IsValid)
                {
                    logger.LogWarning("Model state is invalid.");
                    return Page();
                }

                Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
                Order.UserName = Cart.UserName;
                Order.TotalPrice = Cart.TotalPrice;

                logger.LogInformation("Order details set. CustomerId: {CustomerId}, UserName: {UserName}, TotalPrice: {TotalPrice}",
                    Order.CustomerId, Order.UserName, Order.TotalPrice);

                await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
                logger.LogInformation("Basket checkout successful.");

                return RedirectToPage("Confirmation", "OrderSubmitted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during checkout.");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return Page();
            }
        }
    }
}