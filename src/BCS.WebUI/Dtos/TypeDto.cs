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

    public class StreetCreateDto
    {
        public string Title { get; set; }
    }

    public class StreetUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    public class ComplaintCreateDto
    {
        public Guid UserId { get; set; }
        public Guid TypeId { get; set; }
        public string Text { get; set; }
        public DateTime Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public Guid CityId { get; set; }
        public Guid StreetId { get; set; }
        public string? Number { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class ComplaintUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TypeId { get; set; }
        public string Text { get; set; }
        public string Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public Guid CityId { get; set; }
        public Guid StreetId { get; set; }
        public string? Number { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
