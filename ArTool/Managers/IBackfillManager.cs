using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArTool.Models;

namespace ArTool.Managers
{
    public interface IBackfillManager
    {
        CreditCard AddNewCreditCard(BackFillRequest request);

    }
}
