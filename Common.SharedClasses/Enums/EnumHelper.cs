namespace Common.SharedClasses.Enums
{
    public class EnumHelper
    {
        public static IEnumerable<EnumDto> ToEnumDtoList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumDto()
                {
                    Id = Convert.ToInt32(e),
                    Name = e.ToString(),
                })
                .ToList();
        }
    }
}
