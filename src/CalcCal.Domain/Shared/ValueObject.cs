using CalcCal.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Domain.Shared
{
    public abstract record ValueObject<T> : ValueObject
    {
        public T Value { get; init; }

        protected ValueObject(T value)
        {
            Value = value;
        }
    }
    public abstract record ValueObject
    {
    }
}
