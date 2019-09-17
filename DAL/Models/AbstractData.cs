using DAL.Interfaces;

namespace DAL.Models
{
    public abstract class AbstractData<TDTO>: Data, IData
        where TDTO : Data
    {
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TDTO)) return false;
            TDTO tDTO = (TDTO)obj;
            return GetHashCode() == tDTO.GetHashCode();
        }
        public abstract override int GetHashCode();
    }
}
