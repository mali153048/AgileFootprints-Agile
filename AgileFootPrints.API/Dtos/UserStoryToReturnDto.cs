namespace AgileFootPrints.API.Dtos
{
    public class UserStoryToReturnDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int StoryId { get; set; }
        public string StoryName { get; set; }
        public string StoryDescription { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int EpicId { get; set; }
        public string Epic { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
    }
}