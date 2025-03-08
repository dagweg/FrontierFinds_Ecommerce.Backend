// using Ecommerce.Domain.UserAggregate.ValueObjects;

// namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

// public class PaymentInformation
// {
//   public Name CardHolderName { get; set; }
//   public CardNumber CardNumber { get; set; }
//   public Month ExpiryMonth { get; set; }
//   public Year ExpiryYear { get; set; }
//   public CVV CVV { get; set; }

// #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
//   private PaymentInformation() { }
// #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

//   private PaymentInformation(
//     Name cardHolderName,
//     CardNumber cardNumber,
//     Month expiryMonth,
//     Year expiryYear,
//     CVV cvv
//   )
//   {
//     CardHolderName = cardHolderName;
//     CardNumber = cardNumber;
//     ExpiryMonth = expiryMonth;
//     ExpiryYear = expiryYear;
//     CVV = cvv;
//   }

//   public static PaymentInformation Create(
//     Name cardHolderName,
//     CardNumber cardNumber,
//     Month expiryMonth,
//     Year expiryYear,
//     CVV cvv
//   )
//   {
//     return new PaymentInformation(cardHolderName, cardNumber, expiryMonth, expiryYear, cvv);
//   }
// }
