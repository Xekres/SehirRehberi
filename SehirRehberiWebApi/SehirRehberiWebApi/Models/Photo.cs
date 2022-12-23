namespace SehirRehberiWebApi.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        //Burada da Bir fotonun hangi şehre ait oldugunun bilgisini yapacagım
        public City City { get; set; }
    }
}
