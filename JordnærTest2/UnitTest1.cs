using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace JordnærTest2
{
    [TestClass]
    public class UnitTest1
    {
        private string connection =Secret.ConnectionString;
        
        [TestMethod]
        public async Task AddMemberTestAsync()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members=MemService.GetAllMembersAsync().Result;

            //act
            int Membersbefore = Members.Count();
            Member member= new Member("a","a","a","a",false,false,false);
            MemService.AddMemberAsync(member);
            Members = MemService.GetAllMembersAsync().Result;

            int MemberAfter= Members.Count();
            MemService.DeleteMemberAsync(Members[Members.Count()-1].MemberID);

            //assert
            Assert.AreEqual(Membersbefore+1, MemberAfter);

        }

        [TestMethod]
        public async Task DeleteMemberTestAsync()
        {
            //arrange
            MemberService MemService = new MemberService(connection);
            List<Member> Members = MemService.GetAllMembersAsync().Result;
            Member member = new Member(-2, "a", "a", "a", "a", false, false, false,null);

            //act
            MemService.AddMemberAsync(member);
            Members= await MemService.GetAllMembersAsync();
            int MemberBefore = Members.Count();
            MemService.DeleteMemberAsync(Members[Members.Count() - 1].MemberID);
            int MemberAfter= Members.Count();


            //assert
            Assert.AreEqual(MemberBefore,MemberAfter);

        }


    }
}