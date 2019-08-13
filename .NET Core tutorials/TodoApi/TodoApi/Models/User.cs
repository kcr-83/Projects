namespace TodoApi.Models {
    public class User {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public decimal WbpId { get; set; }
        public byte[] Signature { get; set; } 
        public decimal FnkId { get; set; }
        public decimal SkcId { get; set; }
        public string SignatureExtension { get; set; }
        public string Phone { get; set; }       

    }
}