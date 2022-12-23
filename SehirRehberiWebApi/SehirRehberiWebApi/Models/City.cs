namespace SehirRehberiWebApi.Models
{
    public class City
    {
        public City()
        {
            //ŞEHRİN FOTOLARI 
            Photos = new List<Photo>();
            //Photos u kullanabilmem için bu listeyi olusturmalıyım
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Photo> Photos { get; set; }
        //Bu şehir hangi kullanıcı tarafından eklendi ? one to one
        public User User { get; set; }
    }
}
