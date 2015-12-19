namespace PizzaOnline.Model
{
    public abstract class ModelBase : IEntity
    {
        public int? Id { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
