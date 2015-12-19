using System;

namespace PizzaOnline.Model
{
    public interface IEntity : ICloneable
    {
        int? Id { get; set; }
    }
}
