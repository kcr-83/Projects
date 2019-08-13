namespace Actio.Common.Events {
    public class UserAuthenticated : IEvent {
        public UserAuthenticated (string email) {
            this.Email = email;

        }
        public string Email { get; }
        protected UserAuthenticated () {

        }
    }
}