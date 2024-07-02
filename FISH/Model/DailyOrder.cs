namespace FISH.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DailyOrder
    {
        public int Id { get; set; }

        [Required]
        public string Detail { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PricingMethod { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string DeliveryTime { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        public string OrderItem { get; set; }

        public string Remarks { get; set; }
    }

}
