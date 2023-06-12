namespace PAWebApp.Application.Models.Articles
{
    public class AddArticleRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
