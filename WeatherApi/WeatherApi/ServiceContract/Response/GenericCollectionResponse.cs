using System.Collections.Generic;

namespace WeatherApi.ServiceContract.Response
{
    public class GenericCollectionResponse<T> : BaseResponse
    {
        #region Fields

        private List<T> tList;

        #endregion

        #region Constructor

        public GenericCollectionResponse()
        {
        }

        public GenericCollectionResponse(int capacity)
        {
            this.tList = new List<T>(capacity);
        }

        #endregion

        #region Properties

        public ICollection<T> DtoCollection
        {
            get { return this.tList ??= new List<T>(); }
        }

        #endregion
    }
}
