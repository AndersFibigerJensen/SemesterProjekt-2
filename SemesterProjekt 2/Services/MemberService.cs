using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using Microsoft.Data.SqlClient;

namespace SemesterProjekt_2.Services
{
    // Anders
    public class MemberService : Connection, IMemberService
    {
        //sql code
        private string Memberstrings = " select * from Member";
        private string MemberfromID = " select * from Member where memberid=@ID";
        private string Membersearch = " select * from Members where Name Like '%'+@Name+'%'";
        private string MemberUpdate = "update Pro.Member " + " set MemberID=@ID, Name=@Name, Password=@Password , Email=@Email, Address=@Address, IsFamily=@Family, HasDoneHygieneCourse=@Hygiene, isAdmin=@Admin" + "where MemberID=@ID";
        private string MemberDelete = "delete from hotel where MemberID=@ID";
        private string MemberAdd = "insert into Members values(@ID,@Name,@Password,@Email,@Address,@isFamily,@Course,@isAdmin)";


        public MemberService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task AddMemberAsync(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", member.MemberID);
                        command.Parameters.AddWithValue("@Name", member.Name);
                        command.Parameters.AddWithValue("@Password", member.Password);
                        command.Parameters.AddWithValue("@Address", member.Address);
                        command.Parameters.AddWithValue("@isFamily", member.IsFamily);
                        command.Parameters.AddWithValue("@Course", member.HasDoneHygieneCourse);
                        command.Parameters.AddWithValue("@isAdmin", member.IsAdmin);
                        command.Connection.OpenAsync();



                    }
                    catch (SqlException sql)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public async Task<Member> DeleteMemberAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(MemberDelete, connection);
                    Member member = await GetMemberByIdAsync(id);
                    command.Parameters.AddWithValue("@ID", member.MemberID);
                    command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return member;
                    }
                }
                catch (SqlException sql)
                {

                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }

        public async Task<List<Member>> FilterMembersAsync(string filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(MemberAdd, connection))
                {
                    try
                    {
                        List<Member> members= new List<Member>();
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = command.ExecuteReader();
                        while (await reader.ReadAsync())
                        {
                            int MemberID = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string Address = reader.GetString(4);
                            bool isFamily = reader.GetBoolean(5);
                            bool HasDoneHygieneCourse = reader.GetBoolean(6);
                            bool isAdmin = reader.GetBoolean(7);
                            Member member = new Member(MemberID, name, password, email, Address, isFamily, HasDoneHygieneCourse, isAdmin);
                            members.Add(member);
                        }

                    }
                    catch (SqlException sql)
                    {

                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            return null;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(Memberstrings, connection))
                {
                    List<Member> members = new List<Member>();
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader =  await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int MemberID = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string password = reader.GetString(2);
                            string email = reader.GetString(3);
                            string Address = reader.GetString(4);
                            bool isFamily = reader.GetBoolean(5);
                            bool HasDoneHygieneCourse = reader.GetBoolean(6);
                            bool isAdmin = reader.GetBoolean(7);
                            Member member = new Member(MemberID, name, password, email, Address, isFamily, HasDoneHygieneCourse, isAdmin);
                            members.Add(member);
                        }
                        return members;

                    }
                    catch (SqlException sql)
                    {

                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            return null;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(MemberfromID, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = command.ExecuteReader();
                    if (await reader.ReadAsync())
                    {
                        int MemberID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string password = reader.GetString(2);
                        string email = reader.GetString(3);
                        string Address = reader.GetString(4);
                        bool isFamily = reader.GetBoolean(5);
                        bool HasDoneHygieneCourse = reader.GetBoolean(6);
                        bool isAdmin = reader.GetBoolean(7);
                        Member member = new Member(MemberID, name, password, email, Address, isFamily, HasDoneHygieneCourse, isAdmin);
                        return member;
                    }

                }
                catch (SqlException sql)
                {

                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }

        public async Task<bool> UpdateMemberAsync(int id,Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(MemberUpdate, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", member.Name);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@Address", member.Address);
                    command.Parameters.AddWithValue("@Family", member.IsFamily);
                    command.Parameters.AddWithValue("@Hygiene", member.HasDoneHygieneCourse);
                    command.Parameters.AddWithValue("@Admin", member.IsAdmin);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if(noOfRows==1)
                    {
                        return true;
                    }


                }
                catch (SqlException sql)
                {

                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }
    }
}
