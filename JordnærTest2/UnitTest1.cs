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
        public async Task AddMemberTestAsync()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members=MemService.GetAllMembersAsync().Result;

            //act
            int Membersbefore = Members.Count();
            Member member= new Member(-2,"a","a","a","a",false,false,false);
            MemService.AddMemberAsync(member);
            int MemberAfter= Members.Count();
            MemService.DeleteMemberAsync(-2);

            //assert
            Assert.AreEqual(Membersbefore+1, MemberAfter);

        }

        [TestMethod]
        public async Task DeleteMemberTestAsync()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members = MemService.GetAllMembersAsync().Result;
            Member member = new Member(-2, "a", "a", "a", "a", false, false, false);

            //act
            MemService.AddMemberAsync(member);
            int MemberBefore= Members.Count();
            MemService.DeleteMemberAsync(-2);
            int MemberAfter= Members.Count();


            //assert
            Assert.AreEqual(MemberBefore,MemberAfter);

        }

        [TestMethod]

        public async Task GetMemberAsync()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members = MemService.GetAllMembersAsync().Result;

            //act
            Member member =await MemService.GetMemberByIdAsync(1);

            //assert
            Assert.AreEqual(Members[1],member);

        }
    }
}