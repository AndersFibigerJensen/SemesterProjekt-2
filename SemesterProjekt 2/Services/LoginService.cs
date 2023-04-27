﻿using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Services
{
    public class LoginService
    {
        //Anders

        private Member _user;


        /// <summary>
        /// Logger brugeren ind på Jordnærs system
        /// </summary>
        /// <param name="user"> Tager et medlem som parameter</param>
        /// <returns></returns>
        public async Task userlogin(Member user)
        {
            _user= user;
        }

        /// <summary>
        /// Logger brugeren ud af Jordnærs system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UserLoginAsync(Member user) 
        {
            _user = null;
        }

        /// <summary>
        /// returnere information om brugeren til en bagliggende side
        /// </summary>
        /// <returns>returnere brugeren som er logget ind på Jordnær</returns>
        public async Task<Member> GetLogged()
        {
            return _user;
        }




    }
}
