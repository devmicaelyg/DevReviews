using System;

namespace DevReviews.API.Entities
{
    public class ProductReview
    {
        public ProductReview(int id, string author, int rating, string comments, int productId)
        {
            this.Id = id;
            this.Author = author;
            this.Rating = rating;
            this.Comments = comments;
            this.ProductId = productId;
            this.RegisteredAt = DateTime.Now;
        }
        
        public int Id { get; private set; }

        public string Author { get; private set; }

        public int Rating { get; private set; }

        public string Comments { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        public int ProductId { get; private set; }

    }
}