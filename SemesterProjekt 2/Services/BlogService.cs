using Microsoft.Data.SqlClient;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Services
{
    public class BlogService : Connection, IBlogService
    {
        private string createPost = "Insert into Blog values(@BlogPost,@Image,@MemberID)";
        private string GetAllPost = "Select * from Blog";
        private string GetPost = "Select * from Blog Where BlogId=@ID";
        private string DeletePost = " Delete from Blog Where BlogId=@ID";
        private string UpdatePost = "update Blog Set BlogPost=@Post , Image=@Image , MemberID=@MemberID where BlogId=@BlogId";

        public BlogService(IConfiguration configuration):base(configuration)
        {

        }


        /// <summary>
        /// Skaber en blogpost
        /// </summary>
        /// <param name="Blog"> tager en tom blogpost</param>
        /// <returns> returnere en blogpost </returns>
        public async Task<Blog> CreateBlogPostAsync(Blog Blog)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            {
                using (SqlCommand command = new SqlCommand(createPost,connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@BlogPost", Blog.BlogPost);
                        command.Parameters.AddWithValue("@Image",string.IsNullOrEmpty(Blog.Image)? (object)DBNull.Value:Blog.Image);
                        command.Parameters.AddWithValue("@MemberID",Blog.memberID);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                    }
                    catch (SqlException sql)
                    {
                        throw sql;
                    }
                    catch (Exception ex) 
                    {
                        throw ex;
                    
                    }

                }
                return null;
            
            
            }
        }

        /// <summary>
        /// Slet en BlogPost
        /// </summary>
        /// <param name="BlogID">bruger et blogid til at finde den individuelle blogpost</param>
        /// <returns> returnere en blogpost</returns>
        public async Task<Blog> DeleteBlogPostAsync(int BlogID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command= new SqlCommand(DeletePost,connection))
                {
                    try
                    {
                        Blog Blog= await GetBlogAsync(BlogID);
                        command.Parameters.AddWithValue("@ID", BlogID);
                        await command.Connection.OpenAsync();
                        int NoofRows=await command.ExecuteNonQueryAsync();
                        if(NoofRows==1)
                        {
                            return Blog;
                        }
                    }
                    catch(SqlException sql)
                    {
                        throw sql;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// henter en blog
        /// </summary>
        /// <param name="id"> bruger id til at finde den individuelle blogpost</param>
        /// <returns>returnere en blogpost med den samme blogpost Blogpostid som i parameteren id</returns>
        public async Task<Blog> GetBlogAsync(int id)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            {
                using(SqlCommand command= new SqlCommand(GetPost,connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if(await reader.ReadAsync())
                        {
                            int BlogID = reader.GetInt32(0);
                            string BlogPost = reader.GetString(1);
                            string Image = null;
                            if(!reader.IsDBNull(2))
                            {
                                Image= reader.GetString(2);
                            }
                            int MemberId = reader.GetInt32(3);
                            Blog Blog = new Blog(BlogID, BlogPost, Image, MemberId);
                            return Blog;

                        }

                    }
                    catch (SqlException sql) 
                    {
                        throw sql;
                    
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    return null;
                }
            
            }
        }

        /// <summary>
        /// henter alle blogpost i en liste
        /// </summary>
        /// <returns>returnere en liste af blogpost</returns>
        public async Task<List<Blog>> GetBlogPostsAsync()
        {
            using (SqlConnection connection= new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(GetAllPost,connection))
                {
                    try
                    {
                        List<Blog> posts = new List<Blog>();
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while(await reader.ReadAsync()) 
                        {
                            int BlogID = reader.GetInt32(0);
                            string BlogPost= reader.GetString(1);
                            string Image = null;
                            if (!reader.IsDBNull(2))
                            {
                                Image = reader.GetString(2);
                            }
                            int MemberId = reader.GetInt32(3);
                            Blog Blog=new Blog(BlogID, BlogPost, Image, MemberId);
                            posts.Add(Blog);
                        
                        }
                        return posts;


                    }
                    catch(SqlException sql)
                    {
                        throw sql;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
                return null;
        }

        /// <summary>
        /// opdatere en blogpost ud fra et
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBlogPostAsync(Blog blog)
        {
            using(SqlConnection connection= new SqlConnection(connectionString)) 
            {
                using(SqlCommand command= new SqlCommand(UpdatePost,connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@BlogId", blog.BlogID);
                        command.Parameters.AddWithValue("@Post",blog.BlogPost);
                        command.Parameters.AddWithValue("@Image", string.IsNullOrEmpty(blog.Image) ? (object)DBNull.Value : blog.Image);
                        command.Parameters.AddWithValue("@MemberID",blog.memberID);
                        await command.Connection.OpenAsync();
                        int NoOfRows= await command.ExecuteNonQueryAsync();
                        if(NoOfRows==1)
                        {
                            return true;
                        }
                    }
                    catch(SqlException sql) 
                    {
                        throw sql;
                    
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    return false;
                }
            
            }
        }
    }
}
