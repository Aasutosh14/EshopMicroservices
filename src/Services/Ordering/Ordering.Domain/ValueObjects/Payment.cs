using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardName { get; } = default!;
        public string CardNumber { get;  } = default!;
        public string CardHolderName { get;  } = default!;
        public DateTime Expiration { get;   }
        public string CVV { get;  } = default!;
        public int PaymentMethod { get; } = default!;
        protected Payment() { }
        private Payment(string cardName, string cardNumber, string cardHolderName, DateTime expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }
        public static Payment Of(string cardName, string cardNumber, string cardHolderName, DateTime expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            Payment payment = new Payment(cardName, cardNumber, cardHolderName, expiration, cvv, paymentMethod);
            return payment;
        }
    }
}
