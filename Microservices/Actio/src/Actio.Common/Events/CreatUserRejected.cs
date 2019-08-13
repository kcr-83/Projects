namespace Actio.Common.Events {
    public class CreatUserRejected : IRejectedEvent {
        public CreatUserRejected (string reason, string code, string email) {
            this.Reason = reason;
            this.Code = code;
            this.Email = email;

        }
        public string Reason { get; }

        public string Code { get; }
        public string Email { get; set; }
        protected CreatUserRejected () {

        }
    }
}