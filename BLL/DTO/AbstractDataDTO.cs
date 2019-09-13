using BLL.Interfaces;

namespace BLL.DTO
{
    public abstract class AbstractDataDTO<TDTO>
        where TDTO : class
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
