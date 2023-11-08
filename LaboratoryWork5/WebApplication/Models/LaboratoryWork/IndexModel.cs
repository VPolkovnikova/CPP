namespace WebApplication.Models.LaboratoryWork;


public static class IndexModel
{
    public class GetInput
    {
        public required int LaboratoryWorkNumber { get; init; }
    }


    public class PostInput
    {
        public required int LaboratoryWorkNumber { get; init; }

        public string? InputText { get; init; }

        public string? OutputText { get; init; }
    }


    public class Output
    {
        public required int LaboratoryWorkNumber { get; set; }

        public string? InputText { get; set; }

        public string? OutputText { get; set; }
    }
}