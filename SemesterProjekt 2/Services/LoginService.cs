using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Services
{
    public class LoginService
    {
        private Member _user;

        public async Task userlogin(Member user)
        {
            _user= user;
        }

        public async Task UserLoginAsync(Member user) 
        {
            _user = null;
        }

        public async Task<Member> GetLogged()
        {
            return _user;
        }




    }
}
