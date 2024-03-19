using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.GetAllFood
{
    public sealed record GetAllFoodQuery : IQuery<List<Domain.Foods.Food>>
    {
    }
}
