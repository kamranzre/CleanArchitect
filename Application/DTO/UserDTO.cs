namespace Application.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string NationalCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditationDate { get; set; }
        public long UserCreate { get; set; }
        public long? UserEditation { get; set; }
    }

}