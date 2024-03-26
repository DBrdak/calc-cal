using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Food.EatFood
{
    public sealed record EatFoodCommand(string FoodId, decimal FoodQuantity) : ICommand<FoodModel>
    {
    }
}
