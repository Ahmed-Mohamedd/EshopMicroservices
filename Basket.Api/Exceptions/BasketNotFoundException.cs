namespace Basket.Api.Exceptions
{
    public class BasketNotFoundException:Exception
    {
        public BasketNotFoundException():base("Basket Not Found!")
        {
            
        }
        public BasketNotFoundException(string UserName):base($"Basket With {UserName} Not Found")
        {
            
        }
    }
}
