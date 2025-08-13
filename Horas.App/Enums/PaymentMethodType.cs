namespace Horas.Domain
{
    public enum PaymentMethodType : byte
    {
        Stripe = 1,
        Instapay = 2,
        VisaCard = 3,
        VodafoneCash = 4,
        OrangeCash = 5,
        Fawry = 6,
        Cash = 7
    }
}
