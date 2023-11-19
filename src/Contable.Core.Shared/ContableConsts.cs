namespace Contable
{
    public class ContableConsts
    {
        public const string LocalizationSourceName = "Contable";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;

        public const bool AllowTenantsToChangeEmailSettings = false;

        public const string Currency = "USD";

        public const string CurrencySign = "$";

        public const string AbpApiClientUserAgent = "AbpApiClient";

        // Note:
        // Minimum accepted payment amount. If a payment amount is less then that minimum value payment progress will continue without charging payment
        // Even though we can use multiple payment methods, users always can go and use the highest accepted payment amount.
        //For example, you use Stripe and PayPal. Let say that stripe accepts min 5$ and PayPal accepts min 3$. If your payment amount is 4$.
        // User will prefer to use a payment method with the highest accept value which is a Stripe in this case.
        public const decimal MinimumUpgradePaymentAmount = 1M;

        public const string DefaultTitleText = "Aviso";

        public const int DefaultTenantId = 1;

        public const string SubjectAlertConflict = "<p> Estimados,\n Adjunto a este correo, les remitimos el documento que contiene la lista de casos, junto con el número de actas registradas y la correspondiente unidad territorial asignada.\n Agradecemos de antemano su atención.</p>";
    }
}
