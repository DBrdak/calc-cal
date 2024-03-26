using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace CalcCal.Application.Food.EatFood
{
    internal sealed class EatFoodCommandHandler : ICommandHandler<EatFoodCommand, FoodModel>
    {
        public async Task<Result<FoodModel>> Handle(EatFoodCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
