using System;

namespace DevReviews.API.Models.ViewModels
{
    public class ProductReviewViewModel
    {
        public ProductReviewViewModel(int id, string author, int rating, DateTime registeredAt)
        {
            Id = id;
            Author = author;
            Rating = rating;
            RegisteredAt = registeredAt;
        }

        public int Id { get; private set; }
        public string Author { get; private set; }
        public int Rating { get; private set; }
        public DateTime RegisteredAt { get; private set; }
    }
}