namespace BCS.WebUI.Dtos
{
    public class TypeCreateDto
    {
        public string Title { get; set; }
    }

    public class TypeUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }


    public class StatusCreateDto
    {
        public string Title { get; set; }
    }

    public class StatusUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public class CityCreateDto
    {
        public string Title { get; set; }
    }

    public class CityUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
