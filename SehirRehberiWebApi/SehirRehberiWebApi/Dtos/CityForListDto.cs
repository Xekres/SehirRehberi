namespace SehirRehberiWebApi.Dtos
{
    public class CityForListDto
    {
        //kullanıcıya hangi bilgileri göndereceğim?
        //Tablonun ilgili alanlarını yollamak istiyorum yani .
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
    }
}
