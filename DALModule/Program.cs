using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALModule.Model;
using System.Data;
using System.Data.Entity;
using DALModule.Repositories;

namespace DALModule
{
    // При работе я постарался востпроизвести построение аблонов Repository и Unit of Work
    // как пример стандартизированой работы с БД.

    class Program
    {
        const int blogId = 1;
        const int userId = 1;
        private static UoW _uow;
        static Program()
        {
            _uow = new UoW(new BlogEntities());

        }
        static void Main(string[] args)
        {
            Categories cat = AddNewCat();
            try
            {
                foreach (var item in _uow.Categories.GetAll())
                {
                    if (item.CategoryName == cat.CategoryName)
                        throw new Exception("Such category already exists!"); //Система выдает ошыбку!
                }
                _uow.Categories.Create(cat);
                _uow.Save();
                Console.WriteLine("Category succesfully added!"); //Посылка EMAIL модератору!
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            foreach (var item in _uow.Categories.GetAll())
            {
                Console.WriteLine(item.CategoryName);
            }

            Posts post = AddNewPost();
            try
            {
                foreach (var item in _uow.Posts.GetAll())
                {
                    if (item.PostName == post.PostName)
                        throw new Exception("Such post already exists!"); //Система выдает ошыбку!
                }
                _uow.Posts.Create(post);
                _uow.Save();
                Console.WriteLine("Post succesfully added!"); //Посылка EMAIL модератору!
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            foreach (var item in _uow.Posts.GetAll())
            {
                Console.WriteLine(item.PostName);
            }
            Console.ReadLine();
        }

        public static Categories AddNewCat()
        {
            Categories cat = null;
            Console.WriteLine("Adding New Category:");
            string s = Console.ReadLine();
            cat = new Categories()
            {
                CategoryID = -1,
                CategoryName = s,
                BlogID = blogId,
                DateStamp = DateTime.Now
            };
            return cat;
        }

        public static Posts AddNewPost()
        {
            Posts post = null;
            Console.WriteLine("Adding New Post:");
            string s = Console.ReadLine();
            post = new Posts()
            {
                UserId=userId,
                PostName=s
            };
            return post;
        }
    }
}
