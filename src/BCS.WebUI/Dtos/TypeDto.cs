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

    public class StructureCreateDto
    {
        public string Title { get; set; }
    }

    public class StructureUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public class ComplaintCreateDto
    {
        public Guid UserId { get; set; }
        public Guid TypeId { get; set; }
        public Guid StructureId { get; set; }
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
        public Guid StructureId { get; set; }
        public string Text { get; set; }
        public string Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public Guid CityId { get; set; }
        public Guid StreetId { get; set; }
        public string? Number { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoIMG { get; set; }
    }

    public class SuggestionCreateDto
    {
        public Guid UserId { get; set; }
        public Guid TypeId { get; set; }
        public Guid StructureId { get; set; }
        public string Text { get; set; }
        public DateTime Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public Guid CityId { get; set; }
        public Guid StreetId { get; set; }
        public string? Number { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class SuggestionUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TypeId { get; set; }
        public Guid StructureId { get; set; }
        public string Text { get; set; }
        public string Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public Guid CityId { get; set; }
        public Guid StreetId { get; set; }
        public string? Number { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoIMG { get; set; }
    }

    public class ComplaintCommentsCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ComplaintId { get; set; }
        public DateTime ComCommentData { get; set; }
        public string Text { get; set; }
        public IFormFile? ComCommentPhoto { get; set; }
    }

    public class SuggestionCommentsCreateDto
    {
        public Guid UserId { get; set; }
        public Guid SuggestionId { get; set; }
        public DateTime SugCommentData { get; set; }
        public string Text { get; set; }
        public IFormFile? SugCommentPhoto { get; set; }
    }
}
