using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class AccountMember
    {
        [Key]
        [StringLength(20)]
        public string MemberID { get; set; }

        public string MemberPassword { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int MemberRole { get; set; }
    }
}
