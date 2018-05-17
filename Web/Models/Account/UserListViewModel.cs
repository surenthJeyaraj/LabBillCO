namespace Web.Models.Account
{
    public class UserListViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string EmailId { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool IsLocked { get; set; }
    }
}