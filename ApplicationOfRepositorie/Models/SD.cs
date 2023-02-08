using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace ApplicationOfRepositorie.Models
{
    public static class SD
    {
        // for roles
        public const string Role_User_Indi = "Individual";
        public const string Role_user_Comp = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        // for order status
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        // for Payment status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

	}
}
