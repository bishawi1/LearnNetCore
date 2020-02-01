using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public enum Dept
    {
        None,
        HR,
        IT,
        Payroll
    }
    public enum TaskOperation
    {
        Approved,
        Rejected,
        MoveBack_Rejected
    }
    public enum PurchaseOrderDeleveryOptions
    {
        Delivered,
        DeliveredPartially,
        CancelDelivery
    }
    public enum VerifyPurchaseOrderOptions
    {
        Approved,
        Rejected
    }
    public enum UserCategoryOptions
    {
        Employee,
        Guest
    }
}
