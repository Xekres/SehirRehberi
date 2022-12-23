namespace SehirRehberiWebApi.Models
{
    public class User
    {
        public User()
        {
            Cities = new List<City>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //kullanıcının şehirlerini tutacagım
        public List<City> Cities { get; set; }
    }
}
