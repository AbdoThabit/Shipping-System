namespace Shipping_System.ViewModels
{
    public class StatusCountVM
    {
        public int All_Status_Count { get; set; }
        public int New_Status_Count { get; set; }
        public int Waiting_Status_Count { get; set; }
        public int deliveredToRepresentive_Status_Count { get; set; }
        public int CantReach_Status_Count { get; set; }
        public int Suspended_Status_Count { get; set; }
        public int partlyDelivered_Status_Count { get; set; }
        public int CanceledByClient_Status_Count { get; set; }
        public int rejectedWithFullPaying_Status_Count { get; set; }
        public int rejectedWithSomePaying_Status_Count { get; set; }
        public int rejectedWithoutPaying_Status_Count { get; set; }
        public int Delivered_Status_Count { get; set; }

    }
}
