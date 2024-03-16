namespace Shipping_System.DAL.Entites
{
    public class WeightSetting
    {
        public int Id { get; set;}
        public int Default_Weight { get; set;}
        public int Extra_Weight {  get; set;}
        public virtual ICollection<Order> Orders { get; set; }


    }
}
