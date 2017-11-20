using MS.BLL.Service;

namespace MS.UI.Services
{
    public sealed class DataService
    {
        private static readonly EntityService _service = new EntityService();

        private DataService() { }

        public static EntityService Service
        {
            get
            {
                return _service;
            }
        }
    }
}