using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace JordnærTest2
{
    [TestClass]
    public class UnitTest1
    {
        private string connection ="";
        
        [TestMethod]
        public void AddMember()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members=MemService.GetAllMembersAsync().Result;

            //act
            int Membersbefore = Members.Count();
            Member member= new Member(-2,"a","a","a","a",false,false,false);
            MemService.AddMemberAsync(member);



        }
    }
}