using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.AddFood
{
    public sealed record AddFoodCommand(string FoodName) : ICommand<IEnumerable<FoodModel>>
    {
    }
}
