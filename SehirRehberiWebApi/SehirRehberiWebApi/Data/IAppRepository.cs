using SehirRehberiWebApi.Models;

namespace SehirRehberiWebApi.Data
{
    public interface IAppRepository
    {
        //Gerekli operasyonlar VeriTabanına ekleme vs vs
        //CRUD () Create,Read,Update,Delete Operasyonları için
        void Add<T>(T entity) where T : class;
        
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        List<City> GetCities();
        //Şehrin fotolarını çekelim
        List<Photo> GetPhotosByCityId(int cityId);
        //Sadece belirli bir şehrin datası
        City GetCityById(int cityId);
        Photo GetPhoto(int id);
    }
}
