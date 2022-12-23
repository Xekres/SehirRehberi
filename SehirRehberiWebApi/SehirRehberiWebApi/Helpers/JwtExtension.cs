namespace SehirRehberiWebApi.Helpers
{
    public static class JwtExtension
    {
        //Extension methodlar STATIC classların içerisine yazıldıgı için class ımı static yapıyorum !
        public static void AddApplicationError(HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error", message);
            //Cors sıkıntısı olmaması için;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
        }

    }
}
